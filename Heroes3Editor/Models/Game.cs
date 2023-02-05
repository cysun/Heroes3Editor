using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using ICSharpCode.SharpZipLib.GZip;

namespace Heroes3Editor.Models
{
    public class Game
    {
        public bool IsHOTA { get; set; }
        public byte[] Bytes { get; }

        public IList<Hero> Heroes { get; } = new List<Hero>();

        // CGM is supposed to be a GZip file, but GZipStream from .NET library throws a
        // "unsupported compression method" exception, which is why we use SharpZipLib.
        // Also CGM has incorrect CRC which every tool/library complains about.
        public Game(string file)
        {
            using var fileStream = (new FileInfo(file)).OpenRead();
            using var gzipStream = new GZipInputStream(fileStream);
            using var memoryStream = new MemoryStream();
            gzipStream.CopyTo(memoryStream);
            Bytes = memoryStream.ToArray();
            var gameVersionMajor = Bytes[8];
            var gameVersionMinor = Bytes[12];

            if (gameVersionMajor >= 44 && gameVersionMinor >= 5)
            {
                SetHOTA();
            }
            else
            {
                SetClassic();
            }
            Constants.LoadAllArtifacts();
        }

        public void SetHOTA()
        {
            IsHOTA = true;
            Constants.LoadHOTAItems();
            Constants.HeroOffsets["SkillSlots"] = 923;
        }
        public void SetClassic()
        {
            IsHOTA = false;
            Constants.HeroOffsets["SkillSlots"] = 41;
            Constants.RemoveHOTAReferenceCodes();
        }
        public void Save(string file)
        {
            using var fileStream = (new FileInfo(file)).OpenWrite();
            using var gzipStream = new GZipOutputStream(fileStream);
            using var memoryStream = new MemoryStream(Bytes);
            memoryStream.CopyTo(gzipStream);
        }

        public bool SearchHero(string name)
        {
            int startPosition = Bytes.Length;
            foreach (var hero in Heroes)
            {
                if (hero.Name == name && startPosition > hero.BytePosition)
                    startPosition = hero.BytePosition - 1;
            }

            var bytePosition = SearchHero(name, startPosition);
            if (bytePosition > 0)
            {
                Heroes.Add(new Hero(name, this, bytePosition));
                return true;
            }
            else
                return false;
        }

        private int SearchHero(string name, int startPosition)
        {
            byte[] pattern = new byte[13];
            Encoding.ASCII.GetBytes(name).CopyTo(pattern, 0);
            if (Regex.IsMatch(name, @"\p{IsCyrillic}"))
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Encoding win1251 = Encoding.GetEncoding("windows-1251");
                pattern = win1251.GetBytes(name);
            }

            for (int i = startPosition - pattern.Length; i > 0; --i)
            {
                bool found = true;
                for (int j = 0; j < pattern.Length; ++j)
                {
                    if (Bytes[i + j] != pattern[j])
                    {
                        found = false;
                        break;
                    }
                }
                if (found)
                {
                    return i;
                }
            }
            return -1;
        }
    }

    public class Hero
    {
        public string Name { get; }

        private Game _game;

        public bool IsHOTAGame => _game.IsHOTA;
        public int BytePosition { get; }

        public byte[] Attributes { get; } = new byte[4];
        public int NumOfSkills { get; private set; }
        public string[] Skills { get; } = new string[8];
        public byte[] SkillLevels { get; } = new byte[8];

        public ISet<string> Spells { get; } = new HashSet<string>();

        public string[] Creatures { get; } = new string[7];
        public int[] CreatureAmounts { get; } = new int[7];

        public ISet<string> WarMachines { get; } = new HashSet<string>();
        public string[] ArtifactInfo { get; } = new string[1];

        public IDictionary<string, string> EquippedArtifacts = new Dictionary<string, string>()
        {
            {"Helm", ""},
            {"Neck", ""},
            {"Armor", ""},
            {"Cloak", ""},
            {"Boots", ""},
            {"Weapon", ""},
            {"Shield", ""},
            {"LeftRing", ""},
            {"RightRing", ""},
            {"Item1", ""},
            {"Item2", ""},
            {"Item3", ""},
            {"Item4", ""},
            {"Item5", ""}
        };

        private const int ON = 0;
        private const int OFF = 255;

        public Hero(string name, Game game, int bytePosition)
        {
            Name = name;
            _game = game;
            BytePosition = bytePosition;

            for (int i = 0; i < 4; ++i)
                Attributes[i] = _game.Bytes[BytePosition + Constants.HeroOffsets["Attributes"] + i];

            NumOfSkills = _game.Bytes[BytePosition + Constants.HeroOffsets["NumOfSkills"]];
            for (int i = 0; i < 28; ++i)
            {
                var skillSlotIndex = _game.Bytes[BytePosition + Constants.HeroOffsets["SkillSlots"] + i];
                if (skillSlotIndex != 0)
                {
                    Skills[skillSlotIndex - 1] = Constants.Skills[i];
                    SkillLevels[skillSlotIndex - 1] = _game.Bytes[BytePosition + Constants.HeroOffsets["Skills"] + i];
                }
            }

            for (int i = 0; i < 70; ++i)
            {
                if (_game.Bytes[BytePosition + Constants.HeroOffsets["Spells"] + i] == 1)
                    Spells.Add(Constants.Spells[i]);
            }

            for (int i = 0; i < 7; ++i)
            {
                var code = _game.Bytes[BytePosition + Constants.HeroOffsets["Creatures"] + i * 4];
                if (code != OFF)
                {
                    Creatures[i] = Constants.Creatures[code];
                    var amountBytes = _game.Bytes.AsSpan().Slice(BytePosition + Constants.HeroOffsets["CreatureAmounts"] + i * 4, 4);
                    CreatureAmounts[i] = BinaryPrimitives.ReadInt16LittleEndian(amountBytes);
                }
                else
                {
                    CreatureAmounts[i] = 0;
                }
            }

            foreach (var warMachine in Constants.WarMachines.Names)
            {
                if (_game.Bytes[BytePosition + Constants.HeroOffsets[warMachine]] == Constants.WarMachines[warMachine])
                    WarMachines.Add(warMachine);
            }

            var gears = new List<string>(EquippedArtifacts.Keys);
            foreach (var gear in gears)
            {
                var code = _game.Bytes[BytePosition + Constants.HeroOffsets[gear]];
                if (code != OFF)
                    EquippedArtifacts[gear] = Constants.Artifacts[code];
            }
        }

        public void UpdateAttribute(int i, byte value)
        {
            Attributes[i] = value;
            _game.Bytes[BytePosition + Constants.HeroOffsets["Attributes"] + i] = value;
        }

        public void UpdateSkill(int slot, string skill)
        {
            if (slot < 0 || slot > NumOfSkills) return;
            for (int i = 0; i < NumOfSkills; ++i)
                if (Skills[i] == skill) return;

            byte skillLevel = 1;

            if (slot < NumOfSkills)
            {
                var oldSkill = Skills[slot];
                var oldSkillLevelPosition = BytePosition + Constants.HeroOffsets["Skills"] + Constants.Skills[oldSkill];
                skillLevel = _game.Bytes[oldSkillLevelPosition];
                _game.Bytes[oldSkillLevelPosition] = 0;
                _game.Bytes[BytePosition + Constants.HeroOffsets["SkillSlots"] + Constants.Skills[oldSkill]] = 0;
            }

            Skills[slot] = skill;
            SkillLevels[slot] = skillLevel;
            _game.Bytes[BytePosition + Constants.HeroOffsets["Skills"] + Constants.Skills[skill]] = skillLevel;
            _game.Bytes[BytePosition + Constants.HeroOffsets["SkillSlots"] + Constants.Skills[skill]] = (byte)(slot + 1);

            if (slot == NumOfSkills)
            {
                ++NumOfSkills;
                _game.Bytes[BytePosition + Constants.HeroOffsets["NumOfSkills"]] = (byte)NumOfSkills;
            }
        }

        public void UpdateSkillLevel(int slot, byte level)
        {
            if (slot < 0 || slot > NumOfSkills || level < 1 || level > 3) return;

            SkillLevels[slot] = level;
            _game.Bytes[BytePosition + Constants.HeroOffsets["Skills"] + Constants.Skills[Skills[slot]]] = level;
        }

        public void AddSpell(string spell)
        {
            if (!Spells.Add(spell)) return;

            int spellPosition = BytePosition + Constants.HeroOffsets["Spells"] + Constants.Spells[spell];
            _game.Bytes[spellPosition] = 1;

            int spellBookPosition = BytePosition + Constants.HeroOffsets["SpellBook"] + Constants.Spells[spell];
            _game.Bytes[spellBookPosition] = 1;
        }

        public void RemoveSpell(string spell)
        {
            if (!Spells.Remove(spell)) return;

            int spellPosition = BytePosition + Constants.HeroOffsets["Spells"] + Constants.Spells[spell];
            _game.Bytes[spellPosition] = 0;

            int spellBookPosition = BytePosition + Constants.HeroOffsets["SpellBook"] + Constants.Spells[spell];
            _game.Bytes[spellBookPosition] = 0;
        }

        public void UpdateCreature(int i, string creature)
        {
            if (Creatures[i] == null)
            {
                CreatureAmounts[i] = 1;
                UpdateCreatureAmount(i, 1);
            }

            Creatures[i] = creature;
            _game.Bytes[BytePosition + Constants.HeroOffsets["Creatures"] + i * 4] = Constants.Creatures[creature];
            _game.Bytes[BytePosition + Constants.HeroOffsets["Creatures"] + (i * 4) + 1] = ON;
            _game.Bytes[BytePosition + Constants.HeroOffsets["Creatures"] + (i * 4) + 2] = ON;
            _game.Bytes[BytePosition + Constants.HeroOffsets["Creatures"] + (i * 4) + 3] = ON;
        }

        public void UpdateCreatureAmount(int i, int amount)
        {
            var amountBytes = _game.Bytes.AsSpan().Slice(BytePosition + Constants.HeroOffsets["CreatureAmounts"] + i * 4, 4);
            BinaryPrimitives.WriteInt32LittleEndian(amountBytes, amount);
        }

        public void AddWarMachine(string warMachine)
        {
            if (!WarMachines.Add(warMachine)) return;

            int position = BytePosition + Constants.HeroOffsets[warMachine];
            _game.Bytes[position] = Constants.WarMachines[warMachine];
            _game.Bytes[position + 1] = ON;
            _game.Bytes[position + 2] = ON;
            _game.Bytes[position + 3] = ON;
        }

        public void RemoveWarMachine(string warMachine)
        {
            if (!WarMachines.Remove(warMachine)) return;

            int currentBytePos = BytePosition + Constants.HeroOffsets[warMachine];
            _game.Bytes[currentBytePos] = OFF;
            _game.Bytes[currentBytePos + 1] = OFF;
            _game.Bytes[currentBytePos + 2] = OFF;
            _game.Bytes[currentBytePos + 3] = OFF;
        }

        public void UpdateEquippedArtifact(string gear, string artifact)
        {
            int currentBytePos = BytePosition + Constants.HeroOffsets[gear];
            if (!artifact.Contains("None"))
            {
                EquippedArtifacts[gear] = artifact;
                _game.Bytes[currentBytePos] = Constants.Artifacts[artifact];
                _game.Bytes[currentBytePos + 1] = ON;
                _game.Bytes[currentBytePos + 2] = ON;
                _game.Bytes[currentBytePos + 3] = ON;
            }
            else
            {
                EquippedArtifacts[gear] = "";
                _game.Bytes[currentBytePos] = OFF;
                _game.Bytes[currentBytePos + 1] = OFF;
                _game.Bytes[currentBytePos + 2] = OFF;
                _game.Bytes[currentBytePos + 3] = OFF;
            }
        }

        //  NAME|ATTACK|DEFENSE|POWER|KNOWLEDGE|MORALE|LUCK|OTHER
        //   0  |   1  |   2   |  3  |    4    |   5  |  6 |  7
        public string[] UpdateArtifactInfo(string artifact)
        {
            if (null != artifact && !"None".Equals(artifact))
            {
                return Constants.ArtifactInfo[Constants.Artifacts[artifact]].Split("|");
            }
            return null;
        }
    }
}
