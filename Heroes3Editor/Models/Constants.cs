using System.Collections.Generic;
using System.Linq;

namespace Heroes3Editor.Models
{
    public class Constants
    {
        public static Skills Skills { get; } = new Skills();
        public static Spells Spells { get; } = new Spells();
    }

    public class Skills
    {
        private static readonly Dictionary<int, string> _namesByCode = new Dictionary<int, string>()
        {
            {0, "Pathfinding"},
            {1, "Archery"},
            {2, "Logistics"},
            {3, "Scouting"},
            {4, "Diplomacy"},
            {5, "Navigation"},
            {6, "Leadership"},
            {7, "Wisdom"},
            {8, "Mysticism"},
            {9, "Luck"},
            {10, "Ballistics"},
            {11, "Eagle Eye"},
            {12, "Necromancy"},
            {13, "Estates"},
            {14, "Fire Magic"},
            {15, "Air Magic"},
            {16, "Water Magic"},
            {17, "Earth Magic"},
            {18, "Scholar"},
            {19, "Tactics"},
            {20, "Artillery"},
            {21, "Learning"},
            {22, "Offense"},
            {23, "Armorer"},
            {24, "Intelligence"},
            {25, "Sorcery"},
            {26, "Resistance"},
            {27, "First Aid"}
        };

        private static readonly Dictionary<string, int> _codesByName = _namesByCode.ToDictionary(i => i.Value, i => i.Key);

        public string this[int key] => _namesByCode[key];

        public int this[string key] => _codesByName[key];
    }

    public class Spells
    {
        private static readonly Dictionary<int, string> _namesByCode = new Dictionary<int, string>()
        {
            {0, "Summon Boat"},
            {1, "Scuttle Boat"},
            {2, "Visions"},
            {3, "View Earth"},
            {4, "Disguise"},
            {5, "View Air"},
            {6, "Fly"},
            {7, "Water Walk"},
            {8, "Dimension Door"},
            {9, "Town Portal"},
            {10, "Quick Sand"},
            {11, "Land Mine"},
            {12, "Force Field"},
            {13, "Fire Wall"},
            {14, "Earthquake"},
            {15, "Magic Arrow"},
            {16, "Ice Bolt"},
            {17, "Lightning Bolt"},
            {18, "Implosion"},
            {19, "Chain Lightning"},
            {20, "Frost Ring"},
            {21, "Fireball"},
            {22, "Inferno"},
            {23, "Meteor Shower"},
            {24, "Death Ripple"},
            {25, "Destroy Undead"},
            {26, "Armageddon"},
            {27, "Shield"},
            {28, "Air Shield"},
            {29, "Fire Shield"},
            {30, "Protection from Air"},
            {31, "Protection from Fire"},
            {32, "Protection from Water"},
            {33, "Protection from Earth"},
            {34, "Anti-Magic"},
            {35, "Dispel"},
            {36, "Magic Mirror"},
            {37, "Cure"},
            {38, "Resurrection"},
            {39, "Animate Dead"},
            {40, "Sacrifice"},
            {41, "Bless"},
            {42, "Curse"},
            {43, "Bloodlust"},
            {44, "Precision"},
            {45, "Weakness"},
            {46, "Stone Skin"},
            {47, "Disrupting Ray"},
            {48, "Prayer"},
            {49, "Mirth"},
            {50, "Sorrow"},
            {51, "Fortune"},
            {52, "Misfortune"},
            {53, "Haste"},
            {54, "Slow"},
            {55, "Slayer"},
            {56, "Frenzy"},
            {57, "Titan's Lightning Bolt"},
            {58, "Counterstrike"},
            {59, "Berserk"},
            {60, "Hypnotize"},
            {61, "Forgetfulness"},
            {62, "Blind"},
            {63, "Teleport"},
            {64, "Remove Obstacle"},
            {65, "Clone"},
            {66, "Fire Elemental"},
            {67, "Earth Elemental"},
            {68, "Water Elemental"},
            {69, "Air Elemental"}
        };

        private static readonly Dictionary<string, int> _codesByName = _namesByCode.ToDictionary(i => i.Value, i => i.Key);

        public string this[int key] => _namesByCode[key];

        public int this[string key] => _codesByName[key];
    }
}
