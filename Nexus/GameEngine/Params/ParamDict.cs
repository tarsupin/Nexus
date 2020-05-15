using Nexus.ObjectComponents;
using System.Collections.Generic;
using static Nexus.Objects.Goodie;

namespace Nexus.GameEngine {

	public static class ParamDict {

		// Suits
		public static Dictionary<byte, string> Suits = new Dictionary<byte, string>() {
			{ (byte) SuitSubType.RandomSuit, "RandomSuit" },
			{ (byte) SuitSubType.RandomNinja, "RandomNinja" },
			{ (byte) SuitSubType.RandomWizard, "RandomWizard" },
			{ (byte) SuitSubType.BlackNinja, "BlackNinja" },
			{ (byte) SuitSubType.BlueNinja, "BlueNinja" },
			{ (byte) SuitSubType.GreenNinja, "GreenNinja" },
			{ (byte) SuitSubType.RedNinja, "RedNinja" },
			{ (byte) SuitSubType.WhiteNinja, "WhiteNinja" },
			{ (byte) SuitSubType.BlueWizard, "BlueWizard" },
			{ (byte) SuitSubType.GreenWizard, "GreenWizard" },
			{ (byte) SuitSubType.RedWizard, "RedWizard" },
			{ (byte) SuitSubType.WhiteWizard, "WhiteWizard" },
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
			{ (byte) GoodieSubType.ShieldPlus, "ShieldPlus" },

			{ (byte) GoodieSubType.Guard, "Guard" },
			{ (byte) GoodieSubType.GuardPlus, "GuardPlus" },

			{ (byte) GoodieSubType.Shiny, "Shiny" },
			{ (byte) GoodieSubType.Stars, "Stars" },
			{ (byte) GoodieSubType.GodMode, "GodMode" },

			{ (byte) GoodieSubType.Disrupt, "Disrupt" },
			{ (byte) GoodieSubType.Explosive, "Explosive" },
			{ (byte) GoodieSubType.Key, "Key" },
			{ (byte) GoodieSubType.Blood, "Blood" },
		};

		// Timers
		public static Dictionary<byte, string> Timers = new Dictionary<byte, string>() {
			{ (byte) GoodieSubType.Plus5, "Plus5" },
			{ (byte) GoodieSubType.Plus10, "Plus10" },
			{ (byte) GoodieSubType.Plus20, "Plus20" },
			{ (byte) GoodieSubType.Set5, "Set5" },
			{ (byte) GoodieSubType.Set10, "Set10" },
			{ (byte) GoodieSubType.Set20, "Set20" },
		};

		// Collectable Powers - Mobility
		public static Dictionary<byte, string> MobPowers = new Dictionary<byte, string>() {
			{ (byte) PowerSubType.RandBook, "RandBook" },
			{ (byte) PowerSubType.SlowFall, "SlowFall" },
			{ (byte) PowerSubType.Hover, "Hover" },
			{ (byte) PowerSubType.Levitate, "Levitate" },
			{ (byte) PowerSubType.Flight, "Flight" },
			{ (byte) PowerSubType.Athlete, "Athlete" },
			{ (byte) PowerSubType.Leap, "Leap" },
			{ (byte) PowerSubType.Slam, "Slam" },
			{ (byte) PowerSubType.Burst, "Burst" },
			{ (byte) PowerSubType.Air, "Air" },
			{ (byte) PowerSubType.Phase, "Phase" },
			{ (byte) PowerSubType.Teleport, "Teleport" },
		};

		// Collectable Powers - Weapon
		public static Dictionary<byte, string> Weapons = new Dictionary<byte, string>() {
			{ (byte) PowerSubType.RandWeapon, "RandWeapon" },
			{ (byte) PowerSubType.BoxingRed, "BoxingRed" },
			{ (byte) PowerSubType.BoxingWhite, "BoxingWhite" },
			{ (byte) PowerSubType.Dagger, "Dagger" },
			{ (byte) PowerSubType.DaggerGreen, "DaggerGreen" },
			{ (byte) PowerSubType.Spear, "Spear" },
			{ (byte) PowerSubType.Sword, "Sword" },
		};

		// Collectable Powers - Potions
		public static Dictionary<byte, string> Potions = new Dictionary<byte, string>() {
			{ (byte) PowerSubType.RandPot, "RandPot" },
			{ (byte) PowerSubType.Electric, "Electric" },
			{ (byte) PowerSubType.Fire, "Fire" },
			{ (byte) PowerSubType.Frost, "Frost" },
			{ (byte) PowerSubType.Rock, "Rock" },
			{ (byte) PowerSubType.Water, "Water" },
			{ (byte) PowerSubType.Slime, "Slime" },
		};

		// Collectable Powers - Thrown
		public static Dictionary<byte, string> Thrown = new Dictionary<byte, string>() {
			{ (byte) PowerSubType.RandThrown, "RandThrown" },
			{ (byte) PowerSubType.Axe, "Axe" },
			{ (byte) PowerSubType.Hammer, "Hammer" },
			{ (byte) PowerSubType.Shuriken, "Shuriken" },
		};

		// Collectable Powers - Bolts
		public static Dictionary<byte, string> Bolts = new Dictionary<byte, string>() {
			{ (byte) PowerSubType.RandBolt, "RandBolt" },
			{ (byte) PowerSubType.BoltBlue, "BoltBlue" },
			{ (byte) PowerSubType.BoltGold, "BoltGold" },
			{ (byte) PowerSubType.BoltGreen, "BoltGreen" },
			//{ (byte) PowerSubType.Necro1, "Necro1" },
			//{ (byte) PowerSubType.Necro2, "Necro2" },
		};

		// Collectable Powers - Stack
		public static Dictionary<byte, string> Stacks = new Dictionary<byte, string>() {
			{ (byte) PowerSubType.Chakram, "Chakram" },
			{ (byte) PowerSubType.ChakramPack, "ChakramPack" },
			{ (byte) PowerSubType.Grenade, "Grenade" },
			{ (byte) PowerSubType.GrenadePack, "GrenadePack" },
		};
	}
}
