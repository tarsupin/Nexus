using Nexus.ObjectComponents;
using System.Collections.Generic;
using static Nexus.Objects.Goodie;

namespace Nexus.GameEngine {

	public static class ParamDict {

		// Suits
		public static Dictionary<byte, string> Suits = new Dictionary<byte, string>() {
			{ (byte) SuitSubType.RandomSuit, "Random Suit" },
			{ (byte) SuitSubType.RandomNinja, "Random Ninja Suit" },
			{ (byte) SuitSubType.RandomWizard, "Random Wizard Suit" },
			{ (byte) SuitSubType.BlackNinja, "Black Ninja Suit" },
			{ (byte) SuitSubType.BlueNinja, "Blue Ninja Suit" },
			{ (byte) SuitSubType.GreenNinja, "Green Ninja Suit" },
			{ (byte) SuitSubType.RedNinja, "Red Ninja Suit" },
			{ (byte) SuitSubType.WhiteNinja, "White Ninja Suit" },
			{ (byte) SuitSubType.BlueWizard, "Blue Wizard Suit" },
			{ (byte) SuitSubType.GreenWizard, "Green Wizard Suit" },
			{ (byte) SuitSubType.RedWizard, "Red Wizard Suit" },
			{ (byte) SuitSubType.WhiteWizard, "White Wizard Suit" },
		};

		// Hats
		public static Dictionary<byte, string> Hats = new Dictionary<byte, string>() {
			{ (byte) HatSubType.RandomHat, "Random Hat" },
			{ (byte) HatSubType.AngelHat, "Angel Hat" },
			{ (byte) HatSubType.BambooHat, "Bamboo Hat" },
			{ (byte) HatSubType.CowboyHat, "Cowboy Hat" },
			{ (byte) HatSubType.FeatheredHat, "Feathered Hat" },
			{ (byte) HatSubType.FedoraHat, "Fedora" },
			{ (byte) HatSubType.HardHat, "Hard Hat" },
			{ (byte) HatSubType.RangerHat, "Ranger Hat" },
			{ (byte) HatSubType.SpikeyHat, "Spikey Hat" },
			{ (byte) HatSubType.TopHat, "Top Hat" },
		};

		// Goodies
		public static Dictionary<byte, string> Goodies = new Dictionary<byte, string>() {
			{ (byte) GoodieSubType.Apple, "Apple" },
			{ (byte) GoodieSubType.Pear, "Pear" },
			{ (byte) GoodieSubType.Heart, "Heart" },
			{ (byte) GoodieSubType.Shield, "Shield" },
			{ (byte) GoodieSubType.ShieldPlus, "Power Shield" },

			{ (byte) GoodieSubType.Guard, "Shield Amulet" },
			{ (byte) GoodieSubType.GuardPlus, "Powerful Shield Amulet" },

			{ (byte) GoodieSubType.Shiny, "Shiny" },
			{ (byte) GoodieSubType.Stars, "Stars" },
			{ (byte) GoodieSubType.GodMode, "God Mode" },

			{ (byte) GoodieSubType.Disrupt, "Disrupt" },
			{ (byte) GoodieSubType.Explosive, "Explosive" },
			{ (byte) GoodieSubType.Key, "Key" },
			{ (byte) GoodieSubType.Blood, "Blood" },
		};

		// Timers
		public static Dictionary<byte, string> Timers = new Dictionary<byte, string>() {
			{ (byte) GoodieSubType.Plus5, "+5 Seconds" },
			{ (byte) GoodieSubType.Plus10, "+10 Seconds" },
			{ (byte) GoodieSubType.Plus20, "+20 Seconds" },
			{ (byte) GoodieSubType.Set5, "Set to 5 Seconds" },
			{ (byte) GoodieSubType.Set10, "Set to 10 Seconds" },
			{ (byte) GoodieSubType.Set20, "Set to 20 Seconds" },
		};

		// Collectable Powers - Mobility
		public static Dictionary<byte, string> MobPowers = new Dictionary<byte, string>() {
			{ (byte) PowerSubType.RandomPotion, "Random Mobility Power" },
			{ (byte) PowerSubType.SlowFall, "Slow Fall" },
			{ (byte) PowerSubType.Hover, "Hover" },
			{ (byte) PowerSubType.Levitate, "Levitate" },
			{ (byte) PowerSubType.Flight, "Flight" },
			{ (byte) PowerSubType.Athlete, "Athletic Augment" },
			{ (byte) PowerSubType.Leap, "Leap" },
			{ (byte) PowerSubType.Slam, "Slam" },
			{ (byte) PowerSubType.Burst, "Burst" },
			{ (byte) PowerSubType.Air, "Air" },
			{ (byte) PowerSubType.Phase, "Phase" },
			{ (byte) PowerSubType.Teleport, "Teleport" },
		};

		// Collectable Powers - Weapon
		public static Dictionary<byte, string> Weapons = new Dictionary<byte, string>() {
			{ (byte) PowerSubType.RandomWeapon, "Random Weapon" },
			{ (byte) PowerSubType.BoxingRed, "Red Boxing Glove" },
			{ (byte) PowerSubType.BoxingWhite, "White Boxing Glove" },
			{ (byte) PowerSubType.Dagger, "Dagger" },
			{ (byte) PowerSubType.DaggerGreen, "Green Dagger" },
			{ (byte) PowerSubType.Spear, "Spear" },
			{ (byte) PowerSubType.Sword, "Sword" },
		};

		// Collectable Powers - Spells, Books
		public static Dictionary<byte, string> Spells = new Dictionary<byte, string>() {
			{ (byte) PowerSubType.RandomBook, "Random Spellbook" },
			{ (byte) PowerSubType.Electric, "Electric Spellbook" },
			{ (byte) PowerSubType.Fire, "Fire Spellbook" },
			{ (byte) PowerSubType.Frost, "Frost Spellbook" },
			{ (byte) PowerSubType.Rock, "Rock Spellbook" },
			{ (byte) PowerSubType.Water, "Water Spellbook" },
			{ (byte) PowerSubType.Slime, "Slime Spellbook" },
		};

		// Collectable Powers - Thrown
		public static Dictionary<byte, string> Thrown = new Dictionary<byte, string>() {
			{ (byte) PowerSubType.RandomThrown, "Random Throwing Weapon" },
			{ (byte) PowerSubType.Axe, "Axe" },
			{ (byte) PowerSubType.Hammer, "Hammer" },
			{ (byte) PowerSubType.Shuriken, "Shuriken" },
		};

		// Collectable Powers - Bolts
		public static Dictionary<byte, string> Bolts = new Dictionary<byte, string>() {
			{ (byte) PowerSubType.RandomBolt, "Random Bolt" },
			{ (byte) PowerSubType.BoltBlue, "Blue Bolt Staff" },
			{ (byte) PowerSubType.BoltGold, "Gold Bolt Staff" },
			{ (byte) PowerSubType.BoltGreen, "Green Bolt Staff" },
			//{ (byte) PowerSubType.Necro1, "Necro1" },
			//{ (byte) PowerSubType.Necro2, "Necro2" },
		};

		// Collectable Powers - Stack
		public static Dictionary<byte, string> Stacks = new Dictionary<byte, string>() {
			{ (byte) PowerSubType.Chakram, "Chakram" },
			{ (byte) PowerSubType.ChakramPack, "Chakram Pack" },
			{ (byte) PowerSubType.Grenade, "Grenade" },
			{ (byte) PowerSubType.GrenadePack, "Grenade Pack" },
		};
	}
}
