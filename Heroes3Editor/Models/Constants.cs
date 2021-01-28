﻿using System.Collections.Generic;
using System.Linq;

namespace Heroes3Editor.Models
{
    public class Constants
    {
        public static Skills Skills { get; } = new Skills();
        public static Spells Spells { get; } = new Spells();
        public static Creatures Creatures { get; } = new Creatures();
        public static Weapons Weapons { get; } = new Weapons();
        public static Shields Shields { get; } = new Shields();
        public static Armor Armor { get; } = new Armor();
        public static Cloak Cloak { get; } = new Cloak();
        public static Helms Helms { get; } = new Helms();
        public static Rings Rings { get; } = new Rings();
        public static Boots Boots { get; } = new Boots();
        public static Neck Neck { get; } = new Neck();
        public static Items Items { get; } = new Items();
        public static WarMachines WarMachines { get; } = new WarMachines();
        public static Artifacts Artifacts { get; } = new Artifacts();

        public static ArtifactInfo ArtifactInfo { get; } = new ArtifactInfo();

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
            "Gelare", "Grindan", "Inteus", "Labetha", "Luna", "Gen. Kendal"
        };

        public static Dictionary<string, int> HeroOffsets = new Dictionary<string, int>()
        {
            {"Attributes", 69}, // Primary Skills
            {"Weapon", 237},
            {"Shield", 245},
            {"Armor", 253},
            {"Helm", 213},
            {"Neck", 229},
            {"Cloak", 221},
            {"Boots", 277},
            {"LeftRing", 261},
            {"RightRing", 269},
            {"Item1", 285},
            {"Item2", 293},
            {"Item3", 301},
            {"Item4", 309},
            {"Item5", 357},
            {"Ballista", 317},
            {"Ammo_Cart", 325},
            {"First_Aid_Tent", 333},
            {"NumOfSkills", -126},
            {"Skills", 13}, // Secondary Skills
            {"SkillSlots", 41},
            {"Spells", 73},
            {"SpellBook", 143},
            {"Creatures", -56},
            {"CreatureAmounts", -28},
            {"Inventory", 365 }
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

    public class Weapons
    {
        private static readonly Dictionary<byte, string> _namesByCode = new Dictionary<byte, string>()
        {
            {0xFF, "None" },
            {0x07, "Centaur's Axe" },
            {0x08, "Blackshard of the Dead Knight" },
            {0x09, "Greater Knoll's Flail" },
            {0x0A, "Ogre's Club of Havoc" },
            {0x0B, "Sword of Hellfire" },
            {0X0C, "Titan's Gladius" },
            {0x11, "Sword of Judgement" },
            {0x26, "Red Dragon Flame Tongue" },
            {0x80, "Armageddon's Blade" },
            {0x81, "Angelic Alliance" },
            {0x87, "Titans Thunder" }
        };

        private static readonly Dictionary<string, byte> _codesByName = _namesByCode.ToDictionary(i => i.Value, i => i.Key);

        public string[] Names { get; } = _namesByCode.Values.ToArray();

        public string this[byte key] => _namesByCode[key];

        public byte this[string key] => _codesByName[key];
    }

    public class Shields
    {
        private static readonly Dictionary<byte, string> _namesByCode = new Dictionary<byte, string>()
        {
            {0xFF, "None" },
            {0x0D, "Shield of the Dwarven Lords" },
            {0x0E, "Shield of the Yawning Dead" },
            {0x0F, "Buckler of the Gnoll King" },
            {0x10, "Targ of the Rampaging Ogre" },
            {0x11, "Shield of the Damned" },
            {0x12, "Sentinel's Shield" },
            {0x22, "Lion's Shield of Courage" },
            {0x27, "Dragon Scale Shield" }
        };

        private static readonly Dictionary<string, byte> _codesByName = _namesByCode.ToDictionary(i => i.Value, i => i.Key);

        public string[] Names { get; } = _namesByCode.Values.ToArray();

        public string this[byte key] => _namesByCode[key];

        public byte this[string key] => _codesByName[key];
    }

    public class Helms
    {
        private static readonly Dictionary<byte, string> _namesByCode = new Dictionary<byte, string>()
        {
            {0xFF, "None" },
            {0x13, "Helm of the Alabaster Unicorn" },
            {0x14, "Skull Helmet" },
            {0x15, "Helm of Chaos" },
            {0x16, "Crown of the Supreme Magi" },
            {0x17, "Hellstorm Helmet" },
            {0x18, "Thunder Helmet" },
            {0x24, "Helm of Heavenly Enlightenment" },
            {0x2C, "Crown of Dragontooth" },
            {0x7B, "Sea Captain's Hat" },
            {0x7C, "Spellbinder's Hat" },
            {0x88, "Admiral's Hat" }
        };

        private static readonly Dictionary<string, byte> _codesByName = _namesByCode.ToDictionary(i => i.Value, i => i.Key);

        public string[] Names { get; } = _namesByCode.Values.ToArray();

        public string this[byte key] => _namesByCode[key];

        public byte this[string key] => _codesByName[key];
    }

    public class Armor
    {
        private static readonly Dictionary<byte, string> _namesByCode = new Dictionary<byte, string>()
        {
            {0xFF, "None" },
            {0x19, "Breastplate of Petrified Wood" },
            {0x1A, "Rib Cage" },
            {0x1B, "Scales of the Greater Basilisk" },
            {0x1C, "Tunic of the Cyclops King" },
            {0x1D, "Breastplate of Brimstone" },
            {0x1E, "Titan's Cuirass" },
            {0x1F, "Armor of Wonder" },
            {0x28, "Dragon Scale Armor" },
            {0x3A, "Surcoat of Counterpoise" },
            {0x84, "Armor of the Dammed" },
            {0x86, "Power of the Dragon Father" }
        };

        private static readonly Dictionary<string, byte> _codesByName = _namesByCode.ToDictionary(i => i.Value, i => i.Key);

        public string[] Names { get; } = _namesByCode.Values.ToArray();

        public string this[byte key] => _namesByCode[key];

        public byte this[string key] => _codesByName[key];
    }

    public class Cloak
    {
        private static readonly Dictionary<byte, string> _namesByCode = new Dictionary<byte, string>()
        {
            {0xFF, "None" },
            {0x2A, "Dragon Wing Tabard" },
            {0x37, "Vampire's Cowl" },
            {0x44, "Ambassador's Sash" },
            {0x48, "Angel Wings" },
            {0x4E, "Cape of Conjuring" },
            {0x53, "Recanter's Cloak" },
            {0x63, "Cape of Velocity" },
            {0x6D, "Everflowing Crystal Cloak" },
            {0x82, "Cloak of Undead King" }
        };

        private static readonly Dictionary<string, byte> _codesByName = _namesByCode.ToDictionary(i => i.Value, i => i.Key);

        public string[] Names { get; } = _namesByCode.Values.ToArray();

        public string this[byte key] => _namesByCode[key];

        public byte this[string key] => _codesByName[key];
    }

    public class Boots
    {
        private static readonly Dictionary<byte, string> _namesByCode = new Dictionary<byte, string>()
        {
            {0xFF, "None" },
            {0x20, "Sandal's of the Saint" },
            {0x29, "Dragonbone Greaves" },
            {0x38, "Dead Men's Boots" },
            {0x3B, "Boots of Polarity" },
            {0x5A, "Boots of Levitation" },
            {0x62, "Boots of Speed" }
        };

        private static readonly Dictionary<string, byte> _codesByName = _namesByCode.ToDictionary(i => i.Value, i => i.Key);

        public string[] Names { get; } = _namesByCode.Values.ToArray();

        public string this[byte key] => _namesByCode[key];

        public byte this[string key] => _codesByName[key];
    }

    public class Neck
    {
        private static readonly Dictionary<byte, string> _namesByCode = new Dictionary<byte, string>()
        {
            {0xFF, "None" },
            {0x21, "Celestial Necklace of Bliss" },
            {0x2B, "Necklace of Dragonteeth" },
            {0x36, "Amulet of the Undertaker" },
            {0x39, "Garniture of Interference" },
            {0x47, "Necklace of Ocean Guidance" },
            {0x4C, "Collar of Conjuring" },
            {0x61, "Necklace of Swiftness" },
            {0x64, "Pendant of Dispassion" },
            {0x65, "Pendant of Second Sight" },
            {0x66, "Pendant of Holiness" },
            {0x67, "Pendant of Life" },
            {0x68, "Pendant of Death" },
            {0x69, "Pendant of Free Will" },
            {0x6A, "Pendant of Negativity" },
            {0x6B, "Pendant of Total Recall" },
            {0x6C, "Pendant of Courage" }
        };

        private static readonly Dictionary<string, byte> _codesByName = _namesByCode.ToDictionary(i => i.Value, i => i.Key);

        public string[] Names { get; } = _namesByCode.Values.ToArray();

        public string this[byte key] => _namesByCode[key];

        public byte this[string key] => _codesByName[key];
    }

    public class Rings
    {
        private static readonly Dictionary<byte, string> _namesByCode = new Dictionary<byte, string>()
        {
            {0xFF, "None" },
            {0x25, "Quiet Eye of the Dragon" },
            {0x2D, "Still Eye of the Dragon" },
            {0x43, "Diplomat Ring" },
            {0x45, "Ring of the Wayfarer" },
            {0x46, "Equestrian's Gloves" },
            {0x4D, "Ring of Conjuring" },
            {0x5E, "Ring of Vitality" },
            {0x5F, "Ring of Life" },
            {0x6E, "Ring of Infinite Gems" },
            {0x71, "Eversmoking Ring of Sulfur" }
        };

        private static readonly Dictionary<string, byte> _codesByName = _namesByCode.ToDictionary(i => i.Value, i => i.Key);

        public string[] Names { get; } = _namesByCode.Values.ToArray();

        public string this[byte key] => _namesByCode[key];

        public byte this[string key] => _codesByName[key];
    }

    public class Items
    {
        private static readonly Dictionary<byte, string> _namesByCode = new Dictionary<byte, string>()
        {
            {0xFF, "None" },
            {0x00, "Spell Book" },
            {0x01, "Spell Scroll" },
            {0x02, "The Grail" },
            {0x03, "Catapult" },
            {0x04, "Ballista" },
            {0x05, "Ammo Cart" },
            {0x06, "First Aid Tent" },
            {0x07, "Centaur's Axe" },
            {0x08, "Blackshard of the Dead Knight" },
            {0x09, "Greater Knoll's Flail" },
            {0x0A, "Ogre's Club of Havoc" },
            {0x0B, "Sword of Hellfire" },
            {0X0C, "Titan's Gladius" },
            {0x0D, "Shield of the Dwarven Lords" },
            {0x0E, "Shield of the Yawning Dead" },
            {0x0F, "Buckler of the Gnoll King" },
            {0x10, "Targ of the Rampaging Ogre" },
            {0x11, "Shield of the Damned" },
            {0x12, "Sentinel's Shield" },
            {0x13, "Helm of the Alabaster Unicorn" },
            {0x14, "Skull Helmet" },
            {0x15, "Helm of Chaos" },
            {0x16, "Crown of the Supreme Magi" },
            {0x17, "Hellstorm Helmet" },
            {0x18, "Thunder Helmet" },
            {0x19, "Breastplate of Petrified Wood" },
            {0x1A, "Rib Cage" },
            {0x1B, "Scales of the Greater Basilisk" },
            {0x1C, "Tunic of the Cyclops King" },
            {0x1D, "Breastplate of Brimstone" },
            {0x1E, "Titan's Cuirass" },
            {0x1F, "Armor of Wonder" },
            {0x20, "Sandal's of the Saint" },
            {0x21, "Celestial Necklace of Bliss" },
            {0x22, "Lion's Shield of Courage" },
            {0x23, "Sword of Judgement" },
            {0x24, "Helm of Heavenly Enlightenment" },
            {0x25, "Quiet Eye of the Dragon" },
            {0x26, "Red Dragon Flame Tongue" },
            {0x27, "Dragon Scale Shield" },
            {0x28, "Dragon Scale Armor" },
            {0x29, "Dragonbone Greaves" },
            {0x2A, "Dragon Wing Tabard" },
            {0x2B, "Necklace of Dragonteeth" },
            {0x2C, "Crown of Dragontooth" },
            {0x2D, "Still Eye of the Dragon" },
            {0x2E, "Clover of Fortune" },
            {0x2F, "Cards of Prophecy" },
            {0x30, "Ladybird of Luck" },
            {0x31, "Badge of Courage" },
            {0x32, "Crest of Valor" },
            {0x33, "Glyph of Gallantry" },
            {0x34, "Speculum" },
            {0x35, "Spyglass" },
            {0x36, "Amulet of the Undertaker" },
            {0x37, "Vampire's Cowl" },
            {0x38, "Dead Men's Boots" },
            {0x39, "Garniture of Interference" },
            {0x3A, "Surcoat of Counterpoise" },
            {0x3B, "Boots of Polarity" },
            {0x3C, "Bow of Elven Cherrywood" },
            {0x3D, "Bowstring of the Unicorns's Mane" },
            {0x3E, "Angel Feather Arrows" },
            {0x3F, "Bird of Perception" },
            {0x40, "Stoic Watchman" },
            {0x41, "Emblem of Cognizance" },
            {0x42, "Statesmen's Medal" },
            {0x43, "Diplomat Ring" },
            {0x44, "Ambassador's Sash" },
            {0x45, "Ring of the Wayfarer" },
            {0x46, "Equestrian's Gloves" },
            {0x47, "Necklace of Ocean Guidance" },
            {0x48, "Angel Wings" },
            {0x49, "Charm of Mana" },
            {0x4A, "Talisman of Mana" },
            {0x4B, "Mystic Orb of Mana" },
            {0x4C, "Collar of Conjuring" },
            {0x4D, "Ring of Conjuring" },
            {0x4E, "Cape of Conjuring" },
            {0x4F, "Orb of Firmament" },
            {0x50, "Orb of Silt" },
            {0x51, "Orb of Tempestous Fire" },
            {0x52, "Orb of Driving Rain" },
            {0x53, "Recanter's Cloak" },
            {0x54, "Spirit of Opression" },
            {0x55, "Hourglass of the Evil Hour" },
            {0x56, "Tome of Fire Magic" },
            {0x57, "Tome of Wind Magic" },
            {0x58, "Tome of Water Magic" },
            {0x59, "Tome of Earth Magic" },
            {0x5A, "Boots of Levitation" },
            {0x5B, "Golden Bow" },
            {0x5C, "Sphere of Permanence" },
            {0x5D, "Orb of Vulnerability" },
            {0x5E, "Ring of Vitality" },
            {0x5F, "Ring of Life" },
            {0x60, "Vial of Lifeblood" },
            {0x61, "Necklace of Swiftness" },
            {0x62, "Boots of Speed" },
            {0x63, "Cape of Velocity" },
            {0x64, "Pendant of Dispassion" },
            {0x65, "Pendant of Second Sight" },
            {0x66, "Pendant of Holiness" },
            {0x67, "Pendant of Life" },
            {0x68, "Pendant of Death" },
            {0x69, "Pendant of Free Will" },
            {0x6A, "Pendant of Negativity" },
            {0x6B, "Pendant of Total Recall" },
            {0x6C, "Pendant of Courage" },
            {0x6D, "Everflowing Crystal Cloak" },
            {0x6E, "Ring of Infinite Gems" },
            {0x6F, "Everpouring Vial of Mercury" },
            {0x70, "Inexhaustable Cart of Ore" },
            {0x71, "Eversmoking Ring of Sulfur" },
            {0x72, "Inexhaustable Cart of Lumber" },
            {0x73, "Endless Sack of Gold" },
            {0x74, "Endless Bag of Gold" },
            {0x75, "Endless Purse of Gold" },
            {0x76, "Legs of Legion" },
            {0x77, "Loins of Legion" },
            {0x78, "Torso of Legion" },
            {0x79, "Arms of Legion" },
            {0x7A, "Head of Legion" },
            {0x7B, "Sea Captain's Hat" },
            {0x7C, "Spellbinder's Hat" },
            {0x7D, "Shackles of War" },
            {0x7E, "Orb of Inhibition" },
            {0x7F, "Vial of Dragonblood" },
            {0x80, "Armageddon's Blade" },
            {0x81, "Angelic Alliance" },
            {0x82, "Cloak of Undead King" },
            {0x83, "Elixir of Life" },
            {0x84, "Armor of the Dammed" },
            {0x85, "Statue of Legion" },
            {0x86, "Power of the Dragon Father" },
            {0x87, "Titans Thunder" },
            {0x88, "Admiral's Hat" },
            {0x89, "Bow of the Sharpshooter" },
            {0x8A, "Wizard's Well" },
            {0x8B, "Ring of the Magi" },
            {0x8C, "Cornucopia" },
            {0x8D, "Diplomat's Cloak" },
            {0x8E, "Pendant of Reflection" },
            {0x8F, "Ironfist of the Ogre" },
            {147, "Trident of Domination"},
            {148, "Shield of Naval Glory"},
            {149, "Royal Armor of Nix"},
            {150, "Crown of the Five Seas"},
            {151, "Wayfarer's Boots"},
            {153, "Demon's Horseshoe"},
            {154, "Shaman's Puppet"},
            {155, "Hideous Mask"},
            {156, "Ring of Suppression"},
            {157, "Pendant of Downfall"},
            {158, "Ring of Oblivion"},
            {159, "Cape of Silence"},
            {160, "Golden Goose"},
            {161, "Horn of the Abyss"},
            {162, "Charm of Eclipse"},
            {163, "Seal of Sunset"},
            {164, "Plate of Dying Light"}
        };

        private static readonly Dictionary<string, byte> _codesByName = _namesByCode.ToDictionary(i => i.Value, i => i.Key);

        public string[] Names { get; } = _namesByCode.Values.ToArray();

        public string this[byte key] => _namesByCode[key];

        public byte this[string key] => _codesByName[key];
    }

    public class WarMachines
    {
        private static readonly Dictionary<byte, string> _namesByCode = new Dictionary<byte, string>()
        {
            {0x04, "Ballista" },
            {0x05, "Ammo_Cart" },
            {0x06, "First_Aid_Tent" }
        };

        private static readonly Dictionary<string, byte> _codesByName = _namesByCode.ToDictionary(i => i.Value, i => i.Key);

        public string[] Names { get; } = _namesByCode.Values.ToArray();

        public string this[byte key] => _namesByCode[key];

        public byte this[string key] => _codesByName[key];
    }

    public class Artifacts
    {
        private static readonly Dictionary<byte, string> _namesByCode = new Dictionary<byte, string>()
        {
            {0x00, "Spell Book" },
            {0x01, "Spell Scroll" },
            {0x02, "The Grail" },
            {0x03, "Catapult" },
            {0x04, "Ballista" },
            {0x05, "Ammo Cart" },
            {0x06, "First Aid Tent" },
            {0x07, "Centaur's Axe" },
            {0x08, "Blackshard of the Dead Knight" },
            {0x09, "Greater Knoll's Flail" },
            {0x0A, "Ogre's Club of Havoc" },
            {0x0B, "Sword of Hellfire" },
            {0X0C, "Titan's Gladius" },
            {0x0D, "Shield of the Dwarven Lords" },
            {0x0E, "Shield of the Yawning Dead" },
            {0x0F, "Buckler of the Gnoll King" },
            {0x10, "Targ of the Rampaging Ogre" },
            {0x11, "Shield of the Damned" },
            {0x12, "Sentinel's Shield" },
            {0x13, "Helm of the Alabaster Unicorn" },
            {0x14, "Skull Helmet" },
            {0x15, "Helm of Chaos" },
            {0x16, "Crown of the Supreme Magi" },
            {0x17, "Hellstorm Helmet" },
            {0x18, "Thunder Helmet" },
            {0x19, "Breastplate of Petrified Wood" },
            {0x1A, "Rib Cage" },
            {0x1B, "Scales of the Greater Basilisk" },
            {0x1C, "Tunic of the Cyclops King" },
            {0x1D, "Breastplate of Brimstone" },
            {0x1E, "Titan's Cuirass" },
            {0x1F, "Armor of Wonder" },
            {0x20, "Sandal's of the Saint" },
            {0x21, "Celestial Necklace of Bliss" },
            {0x22, "Lion's Shield of Courage" },
            {0x23, "Sword of Judgement" },
            {0x24, "Helm of Heavenly Enlightenment" },
            {0x25, "Quiet Eye of the Dragon" },
            {0x26, "Red Dragon Flame Tongue" },
            {0x27, "Dragon Scale Shield" },
            {0x28, "Dragon Scale Armor" },
            {0x29, "Dragonbone Greaves" },
            {0x2A, "Dragon Wing Tabard" },
            {0x2B, "Necklace of Dragonteeth" },
            {0x2C, "Crown of Dragontooth" },
            {0x2D, "Still Eye of the Dragon" },
            {0x2E, "Clover of Fortune" },
            {0x2F, "Cards of Prophecy" },
            {0x30, "Ladybird of Luck" },
            {0x31, "Badge of Courage" },
            {0x32, "Crest of Valor" },
            {0x33, "Glyph of Gallantry" },
            {0x34, "Speculum" },
            {0x35, "Spyglass" },
            {0x36, "Amulet of the Undertaker" },
            {0x37, "Vampire's Cowl" },
            {0x38, "Dead Men's Boots" },
            {0x39, "Garniture of Interference" },
            {0x3A, "Surcoat of Counterpoise" },
            {0x3B, "Boots of Polarity" },
            {0x3C, "Bow of Elven Cherrywood" },
            {0x3D, "Bowstring of the Unicorns's Mane" },
            {0x3E, "Angel Feather Arrows" },
            {0x3F, "Bird of Perception" },
            {0x40, "Stoic Watchman" },
            {0x41, "Emblem of Cognizance" },
            {0x42, "Statesmen's Medal" },
            {0x43, "Diplomat Ring" },
            {0x44, "Ambassador's Sash" },
            {0x45, "Ring of the Wayfarer" },
            {0x46, "Equestrian's Gloves" },
            {0x47, "Necklace of Ocean Guidance" },
            {0x48, "Angel Wings" },
            {0x49, "Charm of Mana" },
            {0x4A, "Talisman of Mana" },
            {0x4B, "Mystic Orb of Mana" },
            {0x4C, "Collar of Conjuring" },
            {0x4D, "Ring of Conjuring" },
            {0x4E, "Cape of Conjuring" },
            {0x4F, "Orb of Firmament" },
            {0x50, "Orb of Silt" },
            {0x51, "Orb of Tempestous Fire" },
            {0x52, "Orb of Driving Rain" },
            {0x53, "Recanter's Cloak" },
            {0x54, "Spirit of Opression" },
            {0x55, "Hourglass of the Evil Hour" },
            {0x56, "Tome of Fire Magic" },
            {0x57, "Tome of Wind Magic" },
            {0x58, "Tome of Water Magic" },
            {0x59, "Tome of Earth Magic" },
            {0x5A, "Boots of Levitation" },
            {0x5B, "Golden Bow" },
            {0x5C, "Sphere of Permanence" },
            {0x5D, "Orb of Vulnerability" },
            {0x5E, "Ring of Vitality" },
            {0x5F, "Ring of Life" },
            {0x60, "Vial of Lifeblood" },
            {0x61, "Necklace of Swiftness" },
            {0x62, "Boots of Speed" },
            {0x63, "Cape of Velocity" },
            {0x64, "Pendant of Dispassion" },
            {0x65, "Pendant of Second Sight" },
            {0x66, "Pendant of Holiness" },
            {0x67, "Pendant of Life" },
            {0x68, "Pendant of Death" },
            {0x69, "Pendant of Free Will" },
            {0x6A, "Pendant of Negativity" },
            {0x6B, "Pendant of Total Recall" },
            {0x6C, "Pendant of Courage" },
            {0x6D, "Everflowing Crystal Cloak" },
            {0x6E, "Ring of Infinite Gems" },
            {0x6F, "Everpouring Vial of Mercury" },
            {0x70, "Inexhaustable Cart of Ore" },
            {0x71, "Eversmoking Ring of Sulfur" },
            {0x72, "Inexhaustable Cart of Lumber" },
            {0x73, "Endless Sack of Gold" },
            {0x74, "Endless Bag of Gold" },
            {0x75, "Endless Purse of Gold" },
            {0x76, "Legs of Legion" },
            {0x77, "Loins of Legion" },
            {0x78, "Torso of Legion" },
            {0x79, "Arms of Legion" },
            {0x7A, "Head of Legion" },
            {0x7B, "Sea Captain's Hat" },
            {0x7C, "Spellbinder's Hat" },
            {0x7D, "Shackles of War" },
            {0x7E, "Orb of Inhibition" },
            {0x7F, "Vial of Dragonblood" },
            {0x80, "Armageddon's Blade" },
            {0x81, "Angelic Alliance" },
            {0x82, "Cloak of Undead King" },
            {0x83, "Elixir of Life" },
            {0x84, "Armor of the Dammed" },
            {0x85, "Statue of Legion" },
            {0x86, "Power of the Dragon Father" },
            {0x87, "Titans Thunder" },
            {0x88, "Admiral's Hat" },
            {0x89, "Bow of the Sharpshooter" },
            {0x8A, "Wizard's Well" },
            {0x8B, "Ring of the Magi" },
            {0x8C, "Cornucopia" },
            {0x8D, "Diplomat's Cloak" },
            {0x8E, "Pendant of Reflection" },
            {0x8F, "Ironfist of the Ogre" },
            {148, "Shield of Naval Glory"},
            {149, "Royal Armor of Nix"},
            {150, "Crown of the Five Seas"},
            {151, "Wayfarer's Boots"},
            {153, "Demon's Horseshoe"},
            {154, "Shaman's Puppet"},
            {155, "Hideous Mask"},
            {156, "Ring of Suppression"},
            {157, "Pendant of Downfall"},
            {158, "Ring of Oblivion"},
            {159, "Cape of Silence"},
            {160, "Golden Goose"},
            {161, "Horn of the Abyss"},
            {162, "Charm of Eclipse"},
            {163, "Seal of Sunset"},
            {164, "Plate of Dying Light"}
        };

        private static readonly Dictionary<string, byte> _codesByName = _namesByCode.ToDictionary(i => i.Value, i => i.Key);

        public string[] Names { get; } = _namesByCode.Values.ToArray();

        public string this[byte key] => _namesByCode[key];

        public byte this[string key] => _codesByName[key];
    }

    public class ArtifactInfo
    {
        //  NAME|ATTACK|DEFENSE|SPELL POWER|KNOWLEDGE|MORALE|LUCK|OTHER
        //   0  |   1  |   2   |     3     |    4    |   5  |  6 |  7
        private static readonly Dictionary<byte, string> _namesByCode = new Dictionary<byte, string>()
        {
            {0x01, "Spell Scroll|||||||" },
            {0x07, "Centaur's Axe|+2||||||" },
            {0x08, "Blackshard of the Dead Knight|+3||||||" },
            {0x09, "Greater Knoll's Flail|+4||||||" },
            {0x0A, "Ogre's Club of Havoc|+5||||||" },
            {0x0B, "Sword of Hellfire|+6||||||" },
            {0X0C, "Titan's Gladius|+12|-3|||||" },
            {0x0D, "Shield of the Dwarven Lords||+2|||||" },
            {0x0E, "Shield of the Yawning Dead||+3|||||" },
            {0x0F, "Buckler of the Gnoll King||+4|||||" },
            {0x10, "Targ of the Rampaging Ogre||+5|||||" },
            {0x11, "Shield of the Damned||+6|||||" },
            {0x12, "Sentinel's Shield|-3|+12|||||" },
            {0x13, "Helm of the Alabaster Unicorn||||+1|||" },
            {0x14, "Skull Helmet||||+2|||" },
            {0x15, "Helm of Chaos||||+3|||" },
            {0x16, "Crown of the Supreme Magi||||+4|||" },
            {0x17, "Hellstorm Helmet||||+5|||" },
            {0x18, "Thunder Helmet|||-2|+10|||" },
            {0x19, "Breastplate of Petrified Wood|||+1||||" },
            {0x1A, "Rib Cage|||+2||||" },
            {0x1B, "Scales of the Greater Basilisk|||+3||||" },
            {0x1C, "Tunic of the Cyclops King|||+4||||" },
            {0x1D, "Breastplate of Brimstone|||+5||||" },
            {0x1E, "Titan's Cuirass|||+10|-2|||" },
            {0x1F, "Armor of Wonder|+1|+1|+1|+1|||" },
            {0x20, "Sandal's of the Saint|+2|+2|+2|+2|||" },
            {0x21, "Celestial Necklace of Bliss|+3|+3|+3|+3|||" },
            {0x22, "Lion's Shield of Courage|+4|+4|+4|+4|||" },
            {0x23, "Sword of Judgement|+5|+5|+5|+5|||" },
            {0x24, "Helm of Heavenly Enlightenment|+6|+6|+6|+6|||" },
            {0x25, "Quiet Eye of the Dragon|+1|+1|||||" },
            {0x26, "Red Dragon Flame Tongue|+2|+2|||||" },
            {0x27, "Dragon Scale Shield|+3|+3|||||" },
            {0x28, "Dragon Scale Armor|+4|+4|||||" },
            {0x29, "Dragonbone Greaves|||+1|+1|||" },
            {0x2A, "Dragon Wing Tabard|||+2|+2|||" },
            {0x2B, "Necklace of Dragonteeth|||+3|+3|||" },
            {0x2C, "Crown of Dragontooth|||+4|+4|||" },
            {0x2D, "Still Eye of the Dragon|||||+1|+1|" },
            {0x2E, "Clover of Fortune||||||+1|" },
            {0x2F, "Cards of Prophecy||||||+1|" },
            {0x30, "Ladybird of Luck||||||+1|" },
            {0x31, "Badge of Courage|||||+1||" },
            {0x32, "Crest of Valor|||||+1||" },
            {0x33, "Glyph of Gallantry|||||+1||" },
            {0x34, "Speculum|||||||Scouting Radius +1" },
            {0x35, "Spyglass|||||||Scouting Radius +1" },
            {0x36, "Amulet of the Undertaker|||||||Necromancy +5%" },
            {0x37, "Vampire's Cowl|||||||Necromancy 10%" },
            {0x38, "Dead Men's Boots|||||||Necromancy 15%" },
            {0x39, "Garniture of Interference|||||||Magic Resistance 5%" },
            {0x3A, "Surcoat of Counterpoise|||||||Magic Resistance 10%" },
            {0x3B, "Boots of Polarity|||||||Magic Resistance 15%" },
            {0x3C, "Bow of Elven Cherrywood|||||||Archery Skill 5%" },
            {0x3D, "Bowstring of the Unicorns's Mane|||||||Archery Skill 10%" },
            {0x3E, "Angel Feather Arrows|||||||Archery Skill 15%" },
            {0x3F, "Bird of Perception|||||||Eagle Eye Skill 5%" },
            {0x40, "Stoic Watchman|||||||Eagle Eye Skill 10%" },
            {0x41, "Emblem of Cognizance|||||||Eagle Eye Skill 15%" },
            {0x42, "Statesmen's Medal|||||||Surrendering Cost -10%" },
            {0x43, "Diplomat Ring|||||||Surrendering Cost -10%" },
            {0x44, "Ambassador's Sash|||||||Surrendering Cost -10%" },
            {0x45, "Ring of the Wayfarer|||||||Unit Speed +1" },
            {0x46, "Equestrian's Gloves|||||||Hero Movement Points +300" },
            {0x47, "Necklace of Ocean Guidance|||||||Hero Sea Movement +1000" },
            {0x48, "Angel Wings|||||||Hero will fly" },
            {0x49, "Charm of Mana|||||||Regenerate +1 Mana per day" },
            {0x4A, "Talisman of Mana|||||||Regenerate +2 Mana per day" },
            {0x4B, "Mystic Orb of Mana|||||||Regenerate +3 Mana per day" },
            {0x4C, "Collar of Conjuring|||||||Spell Duration +1" },
            {0x4D, "Ring of Conjuring|||||||Spell Duration +2" },
            {0x4E, "Cape of Conjuring|||||||Spell Duration +3" },
            {0x4F, "Orb of Firmament|||||||All Air Spell Damage +50%" },
            {0x50, "Orb of Silt|||||||All Earth Spell Damage +50%" },
            {0x51, "Orb of Tempestous Fire|||||||All Fire Spell Damage +50%" },
            {0x52, "Orb of Driving Rain|||||||All Water Spell Damage +50%" },
            {0x53, "Recanter's Cloak|||||||Prevents Casting lvl 3+ Spells" },
            {0x54, "Spirit of Opression|||||||Positive Morale Disabled" },
            {0x55, "Hourglass of the Evil Hour|||||||Luck Disabled" },
            {0x56, "Tome of Fire Magic|||||||All Fire Spells Unlocked" },
            {0x57, "Tome of Wind Magic|||||||All Air Spells Unlocked" },
            {0x58, "Tome of Water Magic|||||||All Water Spells Unlocked" },
            {0x59, "Tome of Earth Magic|||||||All Earth Spells Unlocked" },
            {0x5A, "Boots of Levitation|||||||Hero Will Walk on Water" },
            {0x5B, "Golden Bow|||||||No Range and Obstacle Penalty" },
            {0x5C, "Sphere of Permanence|||||||Immune to Dispel" },
            {0x5D, "Orb of Vulnerability|||||||All Spells Unlocked, Negates Immunities" },
            {0x5E, "Ring of Vitality|||||||+1 Unit Health" },
            {0x5F, "Ring of Life|||||||+1 Unit Health" },
            {0x60, "Vial of Lifeblood|||||||+2 Unit Health" },
            {0x61, "Necklace of Swiftness|||||||+1 Unit Speed" },
            {0x62, "Boots of Speed|||||||Hero Movement Points +600" },
            {0x63, "Cape of Velocity|||||||+2 Unit Speed" },
            {0x64, "Pendant of Dispassion|||||||Immunity to Berserk" },
            {0x65, "Pendant of Second Sight|||||||Immunity to Blind" },
            {0x66, "Pendant of Holiness|||||||Immunity to Curse" },
            {0x67, "Pendant of Life|||||||Immunity to Death Ripple" },
            {0x68, "Pendant of Death|||||||Immunity to Destroy Undead" },
            {0x69, "Pendant of Free Will|||||||Immunity to Hypnotize" },
            {0x6A, "Pendant of Negativity|||||||Immunity to Lightning Bolt and Chain-Lightning" },
            {0x6B, "Pendant of Total Recall|||||||Immunity to Forgetfulness" },
            {0x6C, "Pendant of Courage|||||+3|+3|" },
            {0x6D, "Everflowing Crystal Cloak|||||||+1 Crystal per day" },
            {0x6E, "Ring of Infinite Gems|||||||+1 Gems per day" },
            {0x6F, "Everpouring Vial of Mercury|||||||+1 Mercury per day" },
            {0x70, "Inexhaustable Cart of Ore|||||||+1 Ore per day" },
            {0x71, "Eversmoking Ring of Sulfur|||||||+1 Sulfur per day" },
            {0x72, "Inexhaustable Cart of Lumber|||||||+1 Lumber per day" },
            {0x73, "Endless Sack of Gold|||||||+1000 Gold per day" },
            {0x74, "Endless Bag of Gold|||||||+750 Gold per day" },
            {0x75, "Endless Purse of Gold|||||||+500 Gold per day" },
            {0x76, "Legs of Legion|||||||Lvl 2 Creature Growth +5" },
            {0x77, "Loins of Legion|||||||Lvl 3 Creature Growth +4" },
            {0x78, "Torso of Legion|||||||Lvl 4 Creature Growth +3" },
            {0x79, "Arms of Legion|||||||Lvl 5 Creature Growth +2" },
            {0x7A, "Head of Legion|||||||Lvl 6 Creature Growth +1" },
            {0x7B, "Sea Captain's Hat|||||||Hero Sea Movement +500, Can Cast Summon Boat, Scuttle Boat, Protection from Whirlpools" },
            {0x7C, "Spellbinder's Hat|||||||When equipped, the hat enables hero to cast all 5th level spells." },
            {0x7D, "Shackles of War|||||||Neither you nor your opponent may flee or surrender" },
            {0x7E, "Orb of Inhibition|||||||Prevents Casting All Spells" },
            {0x7F, "Vial of Dragonblood|||||||Dragons receive +5 Attack and Defense" },
            {0x80, "Armageddon's Blade|+3|+3|+3|+6|||Combination Artifact: Can Cast Armageddon, Immune to Armageddon"},
            {0x81, "Angelic Alliance|+21|+21|+21|+21|||Combination Artifact: No Army Penalty for Good and Neutral troops, Can Cast Prayer" },
            {0x82, "Cloak of Undead King|||||||Combination Artifact: Necromancy +60%, Raise more Creature Types" },
            {0x83, "Elixir of Life|||||||Combination Artifact: +25% Unit Health, +4 Health Point Regeneration" },
            {0x84, "Armor of the Dammed|+3|+3|+2|+2|||Combination Artifact: Cast Slow,Curse,Weakness and Misfortune for 50 rounds in combat." },
            {0x85, "Statue of Legion|||||||Combination Artifact: Creature Growth +50% + Artifact Effects" },
            {0x86, "Power of the Dragon Father|+16|+16|+16|+16|+1|+1|Combination Artifact: Immune to Lvl 1-4 Spells" },
            {0x87, "Titans Thunder|+9|+9|+8|+8|||Combination Artifact: Can Cast Titan's Lightning Bolt" },
            {0x88, "Admiral's Hat|||||||Combination Artifact: Hero Sea Movement +1500, No Penalty to Board/Leave Boat, Can Cast Summon Boat and Scuttle Boat, Protection from Whirlpools." },
            {0x89, "Bow of the Sharpshooter|||||||Combination Artifact: No Range and Obstacle Penalty, No Melee Penalty, Archery Skill +30%" },
            {0x8A, "Wizard's Well|||||||Combination Artifact: Regenerates all spell points each day" },
            {0x8B, "Ring of the Magi|||||||Combination Artifact: Add 50 rounds to spell duration (56 rounds together with components effect)" },
            {0x8C, "Cornucopia|||||||Combination Artifact: Generates 5 of each precious resource, each day." },
            {0x8D, "Diplomat's Cloak|||||||Combination Artifact: Allows your hero to retreat or surrender when battling neutral monsters or defending a town. Multiplies your hero army strength by 3" },
            {0x8E, "Pendant of Reflection|||||||Combination Artifact: Increases hero's magic resistance by 20%,increases hero's magic resistance by 30%" },
            {0x8F, "Ironfist of the Ogre|+5|+4|+4|+4|||Combination Artifact: At the beginning of a combat casts Haste, Bloodlust, Fire Shield and Counterstrike." },
            {148, "Shield of Naval Glory|||||||Increases Defense skill by 7" },
            {149, "Royal Armor of Nix|||||||Increases Spell Power skill by 6" },
            {150, "Crown of the Five Seas|||||||Increases Knowledge skill by 6" },
            {151, "Wayfarer's Boots|||||||Allows your hero to move over rough terrain without penalty" },
            {153, "Demon's Horseshoe|||||||Decreases enemy's Luck by 1" },
            {154, "Shaman's Puppet|||||||Decreases enemy's Luck by 2" },
            {155, "Hideous Mask|||||||Decreases enemy's Morale by 1" },
            {156, "Ring of Suppression|||||||Decreases enemy's Morale by 1" },
            {157, "Pendant of Downfall|||||||Decreases enemy's Morale by 2" },
            {158, "Ring of Oblivion|||||||Makes all losses in the battle irrevocable" },
            {159, "Cape of Silence|||||||Bans all level 1 and 2 spells in battle" },
            {160, "Golden Goose|||||||Combination Artifact: brings 7000 gold per day" },
            {161, "Horn of the Abyss|||||||After a stack of living creatures is slain, a stack of Fangarms will rise in their stead and will stay loyal to the hero after the battle concludes" },
            {162, "Charm of Eclipse|||||||Reduces the Power skill of enemy hero by 10% during combat" },
            {163, "Seal of Sunset|||||||Reduces the Power skill of enemy hero by 10% during combat" },
            {164, "Plate of Dying Light|||||||Reduces the Power skill of enemy hero by 25% during combat" }
        };

        private static readonly Dictionary<string, byte> _codesByName = _namesByCode.ToDictionary(i => i.Value, i => i.Key);

        public string[] Names { get; } = _namesByCode.Values.ToArray();

        public string this[byte key] => _namesByCode[key];

        public byte this[string key] => _codesByName[key];
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

    public class Creatures
    {
        private static readonly Dictionary<byte, string> _namesByCode = new Dictionary<byte, string>()
        {
            {0x00, "Pikeman"},
            {0x01, "Halberdier"},
            {0x02, "Archer"},
            {0x03, "Marksman"},
            {0x04, "Griffin"},
            {0x05, "Royal Griffin"},
            {0x06, "Swordsman"},
            {0x07, "Crusader"},
            {0x08, "Monk"},
            {0x09, "Zealot"},
            {0x0A, "Cavalier"},
            {0x0B, "Champion"},
            {0x0C, "Angel"},
            {0x0D, "Archangel"},
            {0x0E, "Centaur"},
            {0x0F, "Centaur Captain"},
            {0x10, "Dwarf"},
            {0x11, "Battle Dwarf"},
            {0x12, "Wood Elf"},
            {0x13, "Grand Elf"},
            {0x14, "Pegasus"},
            {0x15, "Silver Pegasus"},
            {0x16, "Dendroid Guard"},
            {0x17, "Dendroid Soldier"},
            {0x18, "Unicorn"},
            {0x19, "War Unicorn"},
            {0x1A, "Green Dragon"},
            {0x1B, "Gold Dragon"},
            {0x1C, "Gremlin"},
            {0x1D, "Master Gremlin"},
            {0x1E, "Stone Gargoyle"},
            {0x1F, "Obsidian Gargoyle"},
            {0x20, "Stone Golem"},
            {0x21, "Iron Golem"},
            {0x22, "Mage"},
            {0x23, "Arch Mage"},
            {0x24, "Genie"},
            {0x25, "Master Genie"},
            {0x26, "Naga"},
            {0x27, "Naga Queen"},
            {0x28, "Giant"},
            {0x29, "Titan"},
            {0x2A, "Imp"},
            {0x2B, "Familiar"},
            {0x2C, "Gog"},
            {0x2D, "Magog"},
            {0x2E, "Hell Hound"},
            {0x2F, "Cerberus"},
            {0x30, "Demon"},
            {0x31, "Horned Demon"},
            {0x32, "Pit Fiend"},
            {0x33, "Pit Lord"},
            {0x34, "Efreet"},
            {0x35, "Efreet Sultan"},
            {0x36, "Devil"},
            {0x37, "Arch Devil"},
            {0x38, "Skeleton"},
            {0x39, "Skeleton Warrior"},
            {0x3A, "Walking Dead"},
            {0x3B, "Zombie"},
            {0x3C, "Wight"},
            {0x3D, "Wraith"},
            {0x3E, "Vampire"},
            {0x3F, "Vampire Lord"},
            {0x40, "Lich"},
            {0x41, "Power Lich"},
            {0x42, "Black Knight"},
            {0x43, "Dread Knight"},
            {0x44, "Bone Dragon"},
            {0x45, "Ghost Dragon"},
            {0x46, "Troglodyte"},
            {0x47, "Infernal Troglodyte"},
            {0x48, "Harpy"},
            {0x49, "Harpy Hag"},
            {0x4A, "Beholder"},
            {0x4B, "Evil Eye"},
            {0x4C, "Medusa"},
            {0x4D, "Medusa Queen"},
            {0x4E, "Minotaur"},
            {0x4F, "Minotaur King"},
            {0x50, "Manticore"},
            {0x51, "Scorpicore"},
            {0x52, "Red Dragon"},
            {0x53, "Black Dragon"},
            {0x54, "Goblin"},
            {0x55, "Hobgoblin"},
            {0x56, "Wolf Rider"},
            {0x57, "Wolf Raider"},
            {0x58, "Orc"},
            {0x59, "Orc Chieftain"},
            {0x5A, "Ogre"},
            {0x5B, "Ogre Mage"},
            {0x5C, "Roc"},
            {0x5D, "Thunderbird"},
            {0x5E, "Cyclops"},
            {0x5F, "Cyclops King"},
            {0x60, "Behemoth"},
            {0x61, "Ancient Behemoth"},
            {0x62, "Gnoll"},
            {0x63, "Gnoll Marauder"},
            {0x64, "Lizardman"},
            {0x65, "Lizard Warrior"},
            {0x66, "Serpent Fly"},
            {0x67, "Dragon Fly"},
            {0x68, "Basilisk"},
            {0x69, "Greater Basilisk"},
            {0x6A, "Gorgon"},
            {0x6B, "Mighty Gorgon"},
            {0x6C, "Wyvern"},
            {0x6D, "Wyvern Monarch"},
            {0x6E, "Hydra"},
            {0x6F, "Chaos Hydra"},
            {0x70, "Air Elemental"},
            {0x71, "Earth Elemental"},
            {0x72, "Fire Elemental"},
            {0x73, "Water Elemental"},
            {0x74, "Gold Golem"},
            {0x75, "Diamond Golem"},
            {0x76, "Pixies"},
            {0x77, "Sprites"},
            {0x78, "Psychic Elemental"},
            {0x79, "Magic Elemental"},
            {0x7B, "Ice Elemental"},
            {0x7D, "Magma Elemental"},
            {0x7F, "Storm Elemental"},
            {0x81, "Energy Elemental"},
            {0x82, "Firebird"},
            {0x83, "Phoenix"},
            {0x84, "Azure Dragon"},
            {0x85, "Crystal Dragon"},
            {0x86, "Faeri Dragon"},
            {0x87, "Rust Dragon"},
            {0x88, "Enchanter"},
            {0x89, "Sharpshooter"},
            {0x8A, "Halfling"},
            {0x8B, "Peasant"},
            {0x8C, "Boar"},
            {0x8D, "Mummy"},
            {0x8E, "Nomad"},
            {0x8F, "Rogue"},
            {0x90, "Troll"},
            {153, "Nymph"},
            {154, "Oceanids"},
            {155, "Crew Mates"},
            {156, "Seamen"},
            {157, "Pirates"},
            {158, "Corsairs"},
            {151, "Sea Dogs"},
            {159, "Stormbirds"},
            {160, "Ayssids"},
            {161, "Sea Witches"},
            {162, "Sorceresses"},
            {163, "Nix"},
            {164, "Nix Warriors"},
            {165, "Sea Serpents"},
            {166, "Haspids"},
            {167, "Satyrs"},
            {168, "Fangarms"},
            {169, "Leprechauns"},
            {170, "Steel golems"}
        };

        private static readonly Dictionary<string, byte> _codesByName = _namesByCode.ToDictionary(i => i.Value, i => i.Key);

        public string[] Names { get; } = _namesByCode.Values.ToArray();

        public string this[byte key] => _namesByCode[key];

        public byte this[string key] => _codesByName[key];
    }
}
