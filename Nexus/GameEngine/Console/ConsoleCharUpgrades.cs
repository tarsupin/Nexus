using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public static class ConsoleCharUpgrades {

		public static void CheatCodeSuit() {
			string currentIns = ConsoleTrack.NextString();

			// If "suit" is the only instruction, give a random suit to the character.
			if(currentIns == string.Empty) {
				Suit.AssignToCharacter(ConsoleTrack.character, (byte) SuitSubType.RandomSuit, true);
				return;
			}

			// Get the Suit Type by instruction:
			if(currentIns == "ninja") { ConsoleCharUpgrades.CheatCodeSuitNinja(); }
			else if(currentIns == "wizard") { ConsoleCharUpgrades.CheatCodeSuitWizard(); }
			else if(currentIns == "basic") { ConsoleCharUpgrades.CheatCodeSuitNinja(); }

			// Get Random Suit Type
			else { Suit.AssignToCharacter(ConsoleTrack.character, (byte) SuitSubType.RandomSuit, true); }
		}

		public static void CheatCodeSuitBasic() {
			string currentIns = ConsoleTrack.NextString();

			// If "suit basic" is the only instruction, give a random suit to the character.
			if(currentIns == string.Empty) {
				Suit.AssignToCharacter(ConsoleTrack.character, (byte)SuitSubType.RandomBasic, true);
				return;
			}

			// Get the Suit Type by instruction:
			if(suitBasicCodes.ContainsKey(currentIns)) {
				SuitSubType subType = suitBasicCodes[currentIns];
				Suit.AssignToCharacter(ConsoleTrack.character, (byte)subType, true);
			}
		}

		public static readonly Dictionary<string, SuitSubType> suitBasicCodes = new Dictionary<string, SuitSubType>() {

			// Random Options
			{ "any", SuitSubType.RandomBasic },

			// Basic Suits
			{ "red", SuitSubType.RedBasic },
		};

		public static void CheatCodeSuitNinja() {
			string currentIns = ConsoleTrack.NextString();

			// If "suit ninja" is the only instruction, give a random suit to the character.
			if(currentIns == string.Empty) {
				Suit.AssignToCharacter(ConsoleTrack.character, (byte) SuitSubType.RandomNinja, true);
				return;
			}

			// Get the Suit Type by instruction:
			if(suitNinjaCodes.ContainsKey(currentIns)) {
				SuitSubType subType = suitNinjaCodes[currentIns];
				Suit.AssignToCharacter(ConsoleTrack.character, (byte) subType, true);
			}
		}

		public static readonly Dictionary<string, SuitSubType> suitNinjaCodes = new Dictionary<string, SuitSubType>() {

			// Random Options
			{ "any", SuitSubType.RandomNinja },

			// Ninja Suits
			{ "black", SuitSubType.BlackNinja },
			{ "blue", SuitSubType.BlueNinja },
			{ "green", SuitSubType.GreenNinja },
			{ "red", SuitSubType.RedNinja },
			{ "white", SuitSubType.WhiteNinja },
		};
		
		public static void CheatCodeSuitWizard() {
			string currentIns = ConsoleTrack.NextString();

			// If "suit wizard" is the only instruction, give a random suit to the character.
			if(currentIns == string.Empty) {
				Suit.AssignToCharacter(ConsoleTrack.character, (byte) SuitSubType.RandomWizard, true);
				return;
			}

			// Get the Suit Type by instruction:
			if(suitWizardCodes.ContainsKey(currentIns)) {
				SuitSubType subType = suitWizardCodes[currentIns];
				Suit.AssignToCharacter(ConsoleTrack.character, (byte) subType, true);
			}
		}

		public static readonly Dictionary<string, SuitSubType> suitWizardCodes = new Dictionary<string, SuitSubType>() {

			// Random Options
			{ "any", SuitSubType.RandomWizard },

			// Wizard Suits
			{ "blue", SuitSubType.BlueWizard },
			{ "green", SuitSubType.GreenWizard },
			{ "red", SuitSubType.RedWizard },
			{ "white", SuitSubType.WhiteWizard },
		};
		
		public static void CheatCodeHat() {
			string currentIns = ConsoleTrack.NextString();

			// If "hat" is the only instruction, give a random hat to the character.
			if(currentIns == string.Empty) {
				Hat.AssignToCharacter(ConsoleTrack.character, (byte) HatSubType.RandomHat, true);
				return;
			}

			// Get the Hat Type by instruction:
			if(hatCodes.ContainsKey(currentIns)) {
				HatSubType subType = hatCodes[currentIns];
				Hat.AssignToCharacter(ConsoleTrack.character, (byte) subType, true);
			}
		}

		public static readonly Dictionary<string, HatSubType> hatCodes = new Dictionary<string, HatSubType>() {

			// Random Options
			{ "any", HatSubType.RandomHat },

			// Power Hats
			{ "angel", HatSubType.AngelHat },
			{ "bamboo", HatSubType.BambooHat },
			{ "cowboy", HatSubType.CowboyHat },
			{ "feather", HatSubType.FeatheredHat },
			{ "fedora", HatSubType.FedoraHat },
			{ "hard", HatSubType.HardHat },
			{ "ranger", HatSubType.RangerHat },
			{ "spikey", HatSubType.SpikeyHat },
			{ "top", HatSubType.TopHat },
		};
		
		public static void CheatCodePower() {
			string currentIns = ConsoleTrack.NextString();

			// If "power" is the only instruction, give a random power to the character.
			if(currentIns == string.Empty) {
				Power.AssignToCharacter(ConsoleTrack.character, (byte) PowerSubType.RandPot);
				return;
			}

			// Get the Power Type by instruction:
			if(powerCodes.ContainsKey(currentIns)) {
				PowerSubType subType = powerCodes[currentIns];
				Power.AssignToCharacter(ConsoleTrack.character, (byte) subType);
			}
		}

		public static readonly Dictionary<string, PowerSubType> powerCodes = new Dictionary<string, PowerSubType>() {

			// Random Options
			{ "any", PowerSubType.RandPot },

			// Collectable Powers - Mobility
			{ "pot", PowerSubType.RandPot },
			{ "slowfall", PowerSubType.SlowFall },
			{ "hover", PowerSubType.Hover },
			{ "levitate", PowerSubType.Levitate },
			{ "fly", PowerSubType.Flight },
			{ "athlete", PowerSubType.Athlete },
			{ "leap", PowerSubType.Leap },
			{ "slam", PowerSubType.Slam },
			{ "burst", PowerSubType.Burst },
			{ "air", PowerSubType.Air },
			{ "phase", PowerSubType.Phase },
			{ "teleport", PowerSubType.Teleport },

			// Collectable Powers - Weapon
			{ "weapon", PowerSubType.RandWeapon },
			{ "redglove", PowerSubType.BoxingRed },
			{ "whiteglove", PowerSubType.BoxingWhite },
			{ "dagger", PowerSubType.Dagger },
			{ "daggergreen", PowerSubType.DaggerGreen },
			{ "spear", PowerSubType.Spear },
			{ "sword", PowerSubType.Sword },

			// Collectable Powers - Book / Projectiles
			{ "book", PowerSubType.RandBook },
			{ "electric", PowerSubType.Electric },
			{ "fire", PowerSubType.Fire },
			{ "frost", PowerSubType.Frost },
			{ "rock", PowerSubType.Rock },
			{ "water", PowerSubType.Water },
			{ "slime", PowerSubType.Slime },
			//{ "necro1", PowerSubType.Necro1 },
			//{ "necro2", PowerSubType.Necro2 },
			
			// Collectable Powers - Thrown
			{ "thrown", PowerSubType.RandThrown },
			{ "axe", PowerSubType.Axe },
			{ "hammer", PowerSubType.Hammer },
			{ "shuriken", PowerSubType.Shuriken },

			// Power Collectable - Bolts
			{ "bolt", PowerSubType.RandBolt },
			{ "boltblue", PowerSubType.BoltBlue },
			{ "boltgold", PowerSubType.BoltGold },
			{ "boltgreen", PowerSubType.BoltGreen },
			{ "boltnecro", PowerSubType.BoltNecro },

			// Collectable Powers - Stack
			{ "chakram", PowerSubType.Chakram },
			{ "chakrampack", PowerSubType.ChakramPack },
			{ "grenade", PowerSubType.Grenade },
			{ "grenadepack", PowerSubType.GrenadePack },
		};

		public static void CheatCodeHead() {
			string currentIns = ConsoleTrack.NextString();

			// If "head" is the only instruction, give a random head to the character.
			if(currentIns == string.Empty) {
				Head.AssignToCharacter(ConsoleTrack.character, (byte)HeadSubType.RandomStandard, true);
				return;
			}

			// Get the Head Type by instruction:
			if(headCodes.ContainsKey(currentIns)) {
				HeadSubType subType = headCodes[currentIns];
				Head.AssignToCharacter(ConsoleTrack.character, (byte)subType, true);
			}
		}

		public static readonly Dictionary<string, HeadSubType> headCodes = new Dictionary<string, HeadSubType>() {

			// Random Options
			{ "any", HeadSubType.RandomStandard },

			// Standard Heads
			{ "ryu", HeadSubType.RyuHead },
		};
	}
}
