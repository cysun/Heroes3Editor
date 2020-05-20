using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using ICSharpCode.SharpZipLib.GZip;

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
            try
            {
                gzipStream.CopyTo(memoryStream);
            }
            catch (GZipException e) { /* Ignore the CRC error */ }

            Bytes = memoryStream.ToArray();

            AddCampaignHeroes();
        }

        public void Save(string file)
        {
            using var fileStream = (new FileInfo(file)).OpenWrite();
            using var gzipStream = new GZipOutputStream(fileStream);
            using var memoryStream = new MemoryStream(Bytes);
            memoryStream.CopyTo(gzipStream);
        }

        public void AddCampaignHeroes()
        {
            foreach (var hero in Constants.CampaignHeroes)
                AddHero(hero);
        }

        public bool AddHero(string name)
        {
            foreach (var hero in Heroes)
                if (hero.Name == name) return false;

            byte[] pattern = new byte[13];
            Encoding.ASCII.GetBytes(name).CopyTo(pattern, 0);
            for (int i = Bytes.Length - 13; i > 0; --i)
            {
                bool found = true;
                for (int j = 0; j < 13; ++j)
                    if (Bytes[i + j] != pattern[j])
                    {
                        found = false;
                        break;
                    }
                if (found)
                {
                    Heroes.Add(new Hero(name, this, i));
                    return true;
                }
            }
            return false;
        }
    }

    public class Hero
    {
        public string Name { get; }

        private Game _game;
        private int _bytePosition;

        private int _numOfSkills;
        public int NumOfSkills { get => _numOfSkills; }
        public string[] Skills = new string[8];

        public ISet<string> Spells { get; } = new HashSet<string>();

        public Hero(string name, Game game, int bytePosition)
        {
            Name = name;
            _game = game;
            _bytePosition = bytePosition;

            _numOfSkills = _game.Bytes[_bytePosition + Constants.Offsets["NumOfSkills"]];
            for (int i = 0; i < 28; ++i)
            {
                var skillSlotIndex = _game.Bytes[_bytePosition + Constants.Offsets["SkillSlots"] + i];
                if (skillSlotIndex != 0)
                    Skills[skillSlotIndex - 1] = Constants.Skills[i];
            }

            for (int i = 0; i < 70; ++i)
            {
                if (_game.Bytes[_bytePosition + Constants.Offsets["Spells"] + i] == 1)
                    Spells.Add(Constants.Spells[i]);
            }
        }

        public void UpdateSkill(int slot, string skill)
        {
            if (slot < 0 || slot > _numOfSkills) return;
            for (int i = 0; i < _numOfSkills; ++i)
                if (Skills[i] == skill) return;

            byte skillLevel = 1;

            if (slot < _numOfSkills)
            {
                var oldSkill = Skills[slot];
                var oldSkillLevelPosition = _bytePosition + Constants.Offsets["Skills"] + Constants.Skills[oldSkill];
                skillLevel = _game.Bytes[oldSkillLevelPosition];
                _game.Bytes[oldSkillLevelPosition] = 0;
                _game.Bytes[_bytePosition + Constants.Offsets["SkillSlots"] + Constants.Skills[oldSkill]] = 0;
            }

            Skills[slot] = skill;
            _game.Bytes[_bytePosition + Constants.Offsets["Skills"] + Constants.Skills[skill]] = skillLevel;
            _game.Bytes[_bytePosition + Constants.Offsets["SkillSlots"] + Constants.Skills[skill]] = (byte)(slot + 1);

            if (slot == _numOfSkills)
            {
                ++_numOfSkills;
                _game.Bytes[_bytePosition + Constants.Offsets["NumOfSkills"]] = (byte)_numOfSkills;
            }
        }

        public void AddSpell(string spell)
        {
            if (!Spells.Add(spell)) return;

            int spellPosition = _bytePosition + Constants.Offsets["Spells"] + Constants.Spells[spell];
            _game.Bytes[spellPosition] = 1;

            int spellBookPosition = _bytePosition + Constants.Offsets["SpellBook"] + Constants.Spells[spell];
            _game.Bytes[spellBookPosition] = 1;
        }

        public void RemoveSpell(string spell)
        {
            if (!Spells.Remove(spell)) return;

            int spellPosition = _bytePosition + Constants.Offsets["Spells"] + Constants.Spells[spell];
            _game.Bytes[spellPosition] = 0;

            int spellBookPosition = _bytePosition + Constants.Offsets["SpellBook"] + Constants.Spells[spell];
            _game.Bytes[spellBookPosition] = 0;
        }
    }
}
