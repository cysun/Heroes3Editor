using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using ICSharpCode.SharpZipLib.GZip;
using System.Diagnostics;

namespace Heroes3Editor.Models
{
    public class Game
    {
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
            for (int i = startPosition - 13; i > 0; --i)
            {
                bool found = true;
                for (int j = 0; j < 13; ++j)
                    if (Bytes[i + j] != pattern[j])
                    {
                        found = false;
                        break;
                    }
                if (found)
                    return i;
            }
            return -1;
        }
    }

    public class Hero
    {
        public string Name { get; }

        private Game _game;

        public int BytePosition { get; }

        public byte[] Attributes { get; } = new byte[4];
        public int NumOfSkills { get; private set; }
        public string[] Skills { get; } = new string[8];
        public byte[] SkillLevels { get; } = new byte[8];

        public ISet<string> Spells { get; } = new HashSet<string>();

        public string[] Artifacts { get; } = new string[659];

        public string Weapon { get; set; } = "";
        public string Shield { get; set; } = "";
        public string Armor { get; set; } = "";
        public string RightRing { get; set; } = "";
        public string LeftRing { get; set; } = "";
        public string Helm { get; set; } = "";
        public string Neck { get; set; } = "";
        public string Boots { get; set; } = "";
        public string Cloak { get; set; } = "";
        public string Slot1 { get; set; } = "";
        public string Slot2 { get; set; } = "";
        public string Slot3 { get; set; } = "";
        public string Slot4 { get; set; } = "";


        public string[] Creatures { get; } = new string[7];
        public int[] CreatureAmounts { get; } = new int[7];

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
                if (code != 0xFF)
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

            var hexCode = _game.Bytes[BytePosition + Constants.HeroOffsets["Weapon"]];
            Weapon = Constants.Weapons[hexCode];

            hexCode = _game.Bytes[BytePosition + Constants.HeroOffsets["Helm"]];
            Helm = Constants.Helms[hexCode];

            hexCode = _game.Bytes[BytePosition + Constants.HeroOffsets["Shield"]];
            Shield = Constants.Shields[hexCode];

            hexCode = _game.Bytes[BytePosition + Constants.HeroOffsets["RightRing"]];
            RightRing = Constants.Rings[hexCode];

            hexCode = _game.Bytes[BytePosition + Constants.HeroOffsets["Neck"]];
            Neck = Constants.Neck[hexCode];

            hexCode = _game.Bytes[BytePosition + Constants.HeroOffsets["LeftRing"]];
            LeftRing = Constants.Rings[hexCode];

            hexCode = _game.Bytes[BytePosition + Constants.HeroOffsets["Boots"]];
            Boots = Constants.Boots[hexCode];

            hexCode = _game.Bytes[BytePosition + Constants.HeroOffsets["Armor"]];
            Armor = Constants.Armor[hexCode];

            hexCode = _game.Bytes[BytePosition + Constants.HeroOffsets["Cloak"]];
            Cloak = Constants.Cloak[hexCode];

            hexCode = _game.Bytes[BytePosition + Constants.HeroOffsets["Slot1"]];
            Slot1 = Constants.Items[hexCode];

            hexCode = _game.Bytes[BytePosition + Constants.HeroOffsets["Slot2"]];
            Slot2 = Constants.Items[hexCode];

            hexCode = _game.Bytes[BytePosition + Constants.HeroOffsets["Slot3"]];
            Slot3 = Constants.Items[hexCode];

            hexCode = _game.Bytes[BytePosition + Constants.HeroOffsets["Slot4"]];
            Slot4 = Constants.Items[hexCode];
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
        }

        public void UpdateCreatureAmount(int i, int amount)
        {
            var amountBytes = _game.Bytes.AsSpan().Slice(BytePosition + Constants.HeroOffsets["CreatureAmounts"] + i * 4, 4);
            BinaryPrimitives.WriteInt32LittleEndian(amountBytes, amount);
        }

        public void UpdateWeapon(string artifact)
        {
            if (!artifact.Contains("None"))
            {
                Weapon = artifact;
                _game.Bytes[BytePosition + Constants.HeroOffsets["Weapon"]] = Constants.Weapons[artifact];
                _game.Bytes[BytePosition + Constants.HeroOffsets["Weapon"] + 1] = ON;
                _game.Bytes[BytePosition + Constants.HeroOffsets["Weapon"] + 2] = ON;
                _game.Bytes[BytePosition + Constants.HeroOffsets["Weapon"] + 3] = ON;
            }
            else
            {
                int currentBytePos = BytePosition + Constants.HeroOffsets["Weapon"];
                _game.Bytes[currentBytePos] = OFF;
                _game.Bytes[currentBytePos + 1] = OFF;
                _game.Bytes[currentBytePos + 2] = OFF;
                _game.Bytes[currentBytePos + 3] = OFF;
            }
        }

        public void UpdateRightRing(string artifact)
        {
            if (!artifact.Contains("None"))
            {
                RightRing = artifact;
                int currentBytePos = BytePosition + Constants.HeroOffsets["RightRing"];
                _game.Bytes[currentBytePos] = Constants.Rings[artifact];
                _game.Bytes[currentBytePos + 1] = ON;
                _game.Bytes[currentBytePos + 2] = ON;
                _game.Bytes[currentBytePos + 3] = ON;
            }
            else
            {
                int currentBytePos = BytePosition + Constants.HeroOffsets["RightRing"];
                _game.Bytes[currentBytePos] = OFF;
                _game.Bytes[currentBytePos + 1] = OFF;
                _game.Bytes[currentBytePos + 2] = OFF;
                _game.Bytes[currentBytePos + 3] = OFF;
            }
        }

        public void UpdateLeftRing(string artifact)
        {
            if (!artifact.Contains("None"))
            {
                LeftRing = artifact;
                int currentBytePos = BytePosition + Constants.HeroOffsets["LeftRing"];
                _game.Bytes[currentBytePos] = Constants.Rings[artifact];
                _game.Bytes[currentBytePos + 1] = ON;
                _game.Bytes[currentBytePos + 2] = ON;
                _game.Bytes[currentBytePos + 3] = ON;
            }
            else
            {
                int currentBytePos = BytePosition + Constants.HeroOffsets["LeftRing"];
                _game.Bytes[currentBytePos] = OFF;
                _game.Bytes[currentBytePos + 1] = OFF;
                _game.Bytes[currentBytePos + 2] = OFF;
                _game.Bytes[currentBytePos + 3] = OFF;
            }
        }

        public void UpdateHelm(string artifact)
        {
            if (!artifact.Contains("None"))
            {
                Helm = artifact;
                int currentBytePos = BytePosition + Constants.HeroOffsets["Helm"];
                _game.Bytes[currentBytePos] = Constants.Helms[artifact];
                _game.Bytes[currentBytePos + 1] = ON;
                _game.Bytes[currentBytePos + 2] = ON;
                _game.Bytes[currentBytePos + 3] = ON;
            }
            else
            {
                int currentBytePos = BytePosition + Constants.HeroOffsets["Helm"];
                _game.Bytes[currentBytePos] = OFF;
                _game.Bytes[currentBytePos + 1] = OFF;
                _game.Bytes[currentBytePos + 2] = OFF;
                _game.Bytes[currentBytePos + 3] = OFF;
            }
        }

        public void UpdateSlot1(string artifact)
        {
            if (!artifact.Contains("None"))
            {
                Slot1 = artifact;
                int currentBytePos = BytePosition + Constants.HeroOffsets["Slot1"];
                _game.Bytes[currentBytePos] = Constants.Items[artifact];
                _game.Bytes[currentBytePos + 1] = ON;
                _game.Bytes[currentBytePos + 2] = ON;
                _game.Bytes[currentBytePos + 3] = ON;
            }
            else
            {
                int currentBytePos = BytePosition + Constants.HeroOffsets["Slot1"];
                _game.Bytes[currentBytePos] = OFF;
                _game.Bytes[currentBytePos + 1] = OFF;
                _game.Bytes[currentBytePos + 2] = OFF;
                _game.Bytes[currentBytePos + 3] = OFF;
            }
        }

        public void UpdateSlot2(string artifact)
        {
            if (!artifact.Contains("None"))
            {
                Slot2 = artifact;
                int currentBytePos = BytePosition + Constants.HeroOffsets["Slot2"];
                _game.Bytes[currentBytePos] = Constants.Items[artifact];
                _game.Bytes[currentBytePos + 1] = ON;
                _game.Bytes[currentBytePos + 2] = ON;
                _game.Bytes[currentBytePos + 3] = ON;
            }
            else
            {
                int currentBytePos = BytePosition + Constants.HeroOffsets["Slot2"];
                _game.Bytes[currentBytePos] = OFF;
                _game.Bytes[currentBytePos + 1] = OFF;
                _game.Bytes[currentBytePos + 2] = OFF;
                _game.Bytes[currentBytePos + 3] = OFF;
            }
        }

        public void UpdateSlot3(string artifact)
        {
            if (!artifact.Contains("None"))
            {
                Slot3 = artifact;
                int currentBytePos = BytePosition + Constants.HeroOffsets["Slot3"];
                _game.Bytes[currentBytePos] = Constants.Items[artifact];
                _game.Bytes[currentBytePos + 1] = ON;
                _game.Bytes[currentBytePos + 2] = ON;
                _game.Bytes[currentBytePos + 3] = ON;
            }
            else
            {
                int currentBytePos = BytePosition + Constants.HeroOffsets["Slot3"];
                _game.Bytes[currentBytePos] = OFF;
                _game.Bytes[currentBytePos + 1] = OFF;
                _game.Bytes[currentBytePos + 2] = OFF;
                _game.Bytes[currentBytePos + 3] = OFF;
            }
        }

        public void UpdateSlot4(string artifact)
        {
            if (!artifact.Contains("None"))
            {
                Slot4 = artifact;
                int currentBytePos = BytePosition + Constants.HeroOffsets["Slot4"];
                _game.Bytes[currentBytePos] = Constants.Items[artifact];
                _game.Bytes[currentBytePos + 1] = ON;
                _game.Bytes[currentBytePos + 2] = ON;
                _game.Bytes[currentBytePos + 3] = ON;
            }
            else
            {
                int currentBytePos = BytePosition + Constants.HeroOffsets["Slot4"];
                _game.Bytes[currentBytePos] = OFF;
                _game.Bytes[currentBytePos + 1] = OFF;
                _game.Bytes[currentBytePos + 2] = OFF;
                _game.Bytes[currentBytePos + 3] = OFF;
            }
        }

        public void UpdateNeck(string artifact)
        {
            if (!artifact.Contains("None"))
            {
                Neck = artifact;
                int currentBytePos = BytePosition + Constants.HeroOffsets["Neck"];
                _game.Bytes[currentBytePos] = Constants.Neck[artifact];
                _game.Bytes[currentBytePos + 1] = ON;
                _game.Bytes[currentBytePos + 2] = ON;
                _game.Bytes[currentBytePos + 3] = ON;
            }
            else
            {
                int currentBytePos = BytePosition + Constants.HeroOffsets["Neck"];
                _game.Bytes[currentBytePos] = OFF;
                _game.Bytes[currentBytePos + 1] = OFF;
                _game.Bytes[currentBytePos + 2] = OFF;
                _game.Bytes[currentBytePos + 3] = OFF;
            }
        }

        public void UpdateCloak(string artifact)
        {
            if (!artifact.Contains("None"))
            {
                Cloak = artifact;
                int currentBytePos = BytePosition + Constants.HeroOffsets["Cloak"];
                _game.Bytes[currentBytePos] = Constants.Cloak[artifact];
                _game.Bytes[currentBytePos + 1] = ON;
                _game.Bytes[currentBytePos + 2] = ON;
                _game.Bytes[currentBytePos + 3] = ON;
            }
            else
            {
                int currentBytePos = BytePosition + Constants.HeroOffsets["Cloak"];
                _game.Bytes[currentBytePos] = OFF;
                _game.Bytes[currentBytePos + 1] = OFF;
                _game.Bytes[currentBytePos + 2] = OFF;
                _game.Bytes[currentBytePos + 3] = OFF;
            }
        }

        public void UpdateArmor(string artifact)
        {
            if (!artifact.Contains("None"))
            {
                Armor = artifact;
                int currentBytePos = BytePosition + Constants.HeroOffsets["Armor"];
                _game.Bytes[currentBytePos] = Constants.Armor[artifact];
                _game.Bytes[currentBytePos + 1] = ON;
                _game.Bytes[currentBytePos + 2] = ON;
                _game.Bytes[currentBytePos + 3] = ON;
            }
            else
            {
                int currentBytePos = BytePosition + Constants.HeroOffsets["Armor"];
                _game.Bytes[currentBytePos] = OFF;
                _game.Bytes[currentBytePos + 1] = OFF;
                _game.Bytes[currentBytePos + 2] = OFF;
                _game.Bytes[currentBytePos + 3] = OFF;
            }
        }

        public void UpdateShield(string artifact)
        {
            if (!artifact.Contains("None"))
            {
                Shield = artifact;
                int currentBytePos = BytePosition + Constants.HeroOffsets["Shield"];
                _game.Bytes[currentBytePos] = Constants.Shields[artifact];
                _game.Bytes[currentBytePos + 1] = ON;
                _game.Bytes[currentBytePos + 2] = ON;
                _game.Bytes[currentBytePos + 3] = ON;
            }
            else
            {
                int currentBytePos = BytePosition + Constants.HeroOffsets["Shield"];
                _game.Bytes[currentBytePos] = OFF;
                _game.Bytes[currentBytePos + 1] = OFF;
                _game.Bytes[currentBytePos + 2] = OFF;
                _game.Bytes[currentBytePos + 3] = OFF;
            }
        }

        public void UpdateBoots(string artifact)
        {
            if (!artifact.Contains("None"))
            {
                Boots = artifact;
                int currentBytePos = BytePosition + Constants.HeroOffsets["Boots"];
                _game.Bytes[currentBytePos] = Constants.Boots[artifact];
                _game.Bytes[currentBytePos + 1] = ON;
                _game.Bytes[currentBytePos + 2] = ON;
                _game.Bytes[currentBytePos + 3] = ON;
            }
            else
            {
                int currentBytePos = BytePosition + Constants.HeroOffsets["Boots"];
                _game.Bytes[currentBytePos] = OFF;
                _game.Bytes[currentBytePos + 1] = OFF;
                _game.Bytes[currentBytePos + 2] = OFF;
                _game.Bytes[currentBytePos + 3] = OFF;
            }
        }
    }
}
