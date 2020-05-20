using System.Collections.Generic;
using System.Linq;

namespace Heroes3Editor.Models
{
    public class Constants
    {
        public static Skills Skills { get; } = new Skills();
        public static Spells Spells { get; } = new Spells();

        public static string[] Heroes { get; } =
        {
            "Christian", "Edric", "Orrin", "Sylvia", "Valeska", "Sorsha", "Tyris", "Lord Haart", "Catherine",
            "Roland", "Sir Mullich", "Adela", "Adelaide", "Caitlin", "Cuthbert", "Ingham", "Loynis", "Rion",
            "Sanya", "Jenova", "Kyrre", "Ivor", "Ufretin", "Clancy", "Thorgrim", "Ryland", "Mephala", "Gelu",
            "Aeris", "Alagar", "Coronius", "Elleshar", "Malcom", "Melodia", "Gem", "Uland", "Fafner", "Iona",
            "Josephine", "Neela", "Piquedram", "Rissa", "Thane", "Torosar", "Aine", "Astral", "Cyra", "Daremyth",
            "Halon", "Serena", "Solmyr", "Theodorus", "Dracon", "Calh", "Fiona", "Ignatius", "Marius", "Nymus",
            "Octavia", "Pyre", "Rashka", "Xeron", "Ash", "Axsis", "Ayden", "Calid", "Olema", "Xyron", "Xarfax",
            "Zydar", "Charna", "Clavius", "Galthran", "Isra", "Moandor", "Straker", "Tamika", "Vokial", "Aislinn",
            "Nagash", "Nimbus", "Sandro", "Septienna", "Thant", "Vidomina", "Xsi", "Ajit", "Arlach", "Dace",
            "Damacon", "Gunnar", "Lorelei", "Shakti", "Synca", "Mutare", "Mutare Drake", "Alamar", "Darkstorn",
            "Deemer", "Geon", "Jaegar", "Jeddite", "Malekith", "Sephinroth", "Crag Hack", "Gretchin", "Gurnisson",
            "Jabarkas", "Krellion", "Shiva", "Tyraxor", "Yog", "Boragus", "Kilgor", "Dessa", "Gird", "Gundula",
            "Oris", "Saurug", "Terek", "Vey", "Zubin", "Alkin", "Broghild", "Bron", "Drakon", "Gerwulf", "Korbac",
            "Tazar", "Wystan", "Andra", "Merist", "Mirlanda", "Rosic", "Styg", "Tiva", "Verdish", "Voy", "Adrienne",
            "Erdamon", "Fiur", "Ignissa", "Kalt", "Lacus", "Monere", "Pasis", "Thunar", "Aenain", "Brissa", "Ciele",
            "Gelare", "Grindan", "Inteus", "Labetha", "Luna"
        };

        public static Dictionary<string, int> Offsets = new Dictionary<string, int>()
        {
            {"NumOfSkills", -126},
            {"Skills", 13},
            {"SkillSlots", 41},
            {"Spells", 73},
            {"SpellBook", 143}
        };
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

        public string[] Names { get; } = _namesByCode.Values.ToArray();

        public string this[int key] => _namesByCode[key];

        public int this[string key] => _codesByName[key];
    }

    public class Spells
    {
        private static readonly Dictionary<int, string> _namesByCode = new Dictionary<int, string>()
        {
            {0, "Summon_Boat"},
            {1, "Scuttle_Boat"},
            {2, "Visions"},
            {3, "View_Earth"},
            {4, "Disguise"},
            {5, "View_Air"},
            {6, "Fly"},
            {7, "Water_Walk"},
            {8, "Dimension_Door"},
            {9, "Town_Portal"},
            {10, "Quick_Sand"},
            {11, "Land_Mine"},
            {12, "Force_Field"},
            {13, "Fire_Wall"},
            {14, "Earthquake"},
            {15, "Magic_Arrow"},
            {16, "Ice_Bolt"},
            {17, "Lightning_Bolt"},
            {18, "Implosion"},
            {19, "Chain_Lightning"},
            {20, "Frost_Ring"},
            {21, "Fireball"},
            {22, "Inferno"},
            {23, "Meteor_Shower"},
            {24, "Death_Ripple"},
            {25, "Destroy_Undead"},
            {26, "Armageddon"},
            {27, "Shield"},
            {28, "Air_Shield"},
            {29, "Fire_Shield"},
            {30, "Protection_from_Air"},
            {31, "Protection_from_Fire"},
            {32, "Protection_from_Water"},
            {33, "Protection_from_Earth"},
            {34, "Anti_Magic"},
            {35, "Dispel"},
            {36, "Magic_Mirror"},
            {37, "Cure"},
            {38, "Resurrection"},
            {39, "Animate_Dead"},
            {40, "Sacrifice"},
            {41, "Bless"},
            {42, "Curse"},
            {43, "Bloodlust"},
            {44, "Precision"},
            {45, "Weakness"},
            {46, "Stone_Skin"},
            {47, "Disrupting_Ray"},
            {48, "Prayer"},
            {49, "Mirth"},
            {50, "Sorrow"},
            {51, "Fortune"},
            {52, "Misfortune"},
            {53, "Haste"},
            {54, "Slow"},
            {55, "Slayer"},
            {56, "Frenzy"},
            {57, "Titans_Lightning_Bolt"},
            {58, "Counterstrike"},
            {59, "Berserk"},
            {60, "Hypnotize"},
            {61, "Forgetfulness"},
            {62, "Blind"},
            {63, "Teleport"},
            {64, "Remove_Obstacle"},
            {65, "Clone"},
            {66, "Summon_Fire_Elemental"},
            {67, "Summon_Earth_Elemental"},
            {68, "Summon_Water_Elemental"},
            {69, "Summon_Air_Elemental"}
        };

        private static readonly Dictionary<string, int> _codesByName = _namesByCode.ToDictionary(i => i.Value, i => i.Key);

        public string this[int key] => _namesByCode[key];

        public int this[string key] => _codesByName[key];
    }
}
