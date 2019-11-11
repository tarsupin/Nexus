using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public static class ConsolePowers {

		public static void CheatCodeWeapon() {
			string currentIns = ConsoleTrack.NextArg();

			ConsoleTrack.PrepareTabLookup(weaponCodes, currentIns, "Assign a weapon to the character. Can designate which weapon, if desired.");

			if(ConsoleTrack.activate) {

				if(weaponCodes.ContainsKey(currentIns)) {
					byte subType = byte.Parse(weaponCodes[currentIns].ToString());
					Power.AssignToCharacter(ConsoleTrack.character, (byte) subType);
					return;
				}

				// "weapon" was the final valid instruction. Give a random weapon to the character.
				Power.AssignToCharacter(ConsoleTrack.character, (byte) PowerSubType.RandWeapon);
			}
		}

		public static readonly Dictionary<string, object> weaponCodes = new Dictionary<string, object>() {
			{ "glove", (byte) PowerSubType.BoxingRed },
			{ "white-glove", (byte) PowerSubType.BoxingWhite },
			{ "dagger", (byte) PowerSubType.Dagger },
			{ "green-dagger", (byte) PowerSubType.DaggerGreen },
			{ "spear", (byte) PowerSubType.Spear },
			{ "sword", (byte) PowerSubType.Sword },
		};
		
		public static void CheatCodeRanged() {
			string currentIns = ConsoleTrack.NextArg();

			ConsoleTrack.PrepareTabLookup(rangedWeaponCodes, currentIns, "Assign a ranged weapon to the character. Can designate which weapon, if desired.");

			if(ConsoleTrack.activate) {

				if(rangedWeaponCodes.ContainsKey(currentIns)) {
					byte subType = byte.Parse(rangedWeaponCodes[currentIns].ToString());
					Power.AssignToCharacter(ConsoleTrack.character, (byte) subType);
					return;
				}

				// "ranged" was the final valid instruction. Give a random ranged weapon to the character.
				Power.AssignToCharacter(ConsoleTrack.character, (byte) PowerSubType.RandThrown);
			}
		}

		public static readonly Dictionary<string, object> rangedWeaponCodes = new Dictionary<string, object>() {

			// Ranged Weapons
			{ "axe", (byte) PowerSubType.Axe },
			{ "hammer", (byte) PowerSubType.Hammer },
			{ "shuriken", (byte) PowerSubType.Shuriken },
			
			// Stacked Ranged Weapons
			{ "chakram", (byte) PowerSubType.Chakram },
			{ "chakrampack", (byte) PowerSubType.ChakramPack },
			{ "grenade", (byte) PowerSubType.Grenade },
			{ "grenadepack", (byte) PowerSubType.GrenadePack },
		};

		public static void CheatCodeWand() {
			string currentIns = ConsoleTrack.NextArg();

			ConsoleTrack.PrepareTabLookup(wandWeaponCodes, currentIns, "Assign a wand to the character; e.g. `wand blue`");

			if(ConsoleTrack.activate) {

				if(wandWeaponCodes.ContainsKey(currentIns)) {
					byte subType = byte.Parse(wandWeaponCodes[currentIns].ToString());
					Power.AssignToCharacter(ConsoleTrack.character, (byte) subType);
					return;
				}

				// "wand" was the final valid instruction. Give a random wand to the character.
				Power.AssignToCharacter(ConsoleTrack.character, (byte) PowerSubType.RandBolt);
			}
		}

		public static readonly Dictionary<string, object> wandWeaponCodes = new Dictionary<string, object>() {
			{ "blue", (byte) PowerSubType.BoltBlue },
			{ "gold", (byte) PowerSubType.BoltGold },
			{ "green", (byte) PowerSubType.BoltGreen },
			{ "necro", (byte) PowerSubType.BoltNecro },
		};

		public static void CheatCodeMagic() {
			string currentIns = ConsoleTrack.NextArg();

			ConsoleTrack.PrepareTabLookup(magicCodes, currentIns, "Assign magic power to the character; e.g. `magic fire`");

			if(ConsoleTrack.activate) {

				if(magicCodes.ContainsKey(currentIns)) {
					byte subType = byte.Parse(magicCodes[currentIns].ToString());
					Power.AssignToCharacter(ConsoleTrack.character, (byte) subType);
					return;
				}

				// "wand" was the final valid instruction. Give a random wand to the character.
				Power.AssignToCharacter(ConsoleTrack.character, (byte) PowerSubType.RandBook);
			}
		}

		public static readonly Dictionary<string, object> magicCodes = new Dictionary<string, object>() {
			{ "electric", (byte) PowerSubType.Electric },
			{ "fire", (byte) PowerSubType.Fire },
			{ "frost", (byte) PowerSubType.Frost },
			{ "rock", (byte) PowerSubType.Rock },
			{ "water", (byte) PowerSubType.Water },
			{ "slime", (byte) PowerSubType.Slime },
			//{ "necro1", (byte) PowerSubType.Necro1 },
			//{ "necro2", (byte) PowerSubType.Necro2 },
		};

		public static void CheatCodePower() {
			string currentIns = ConsoleTrack.NextArg();

			ConsoleTrack.PrepareTabLookup(powerCodes, currentIns, "Assign magic power to the character; e.g. `magic fire`");

			if(ConsoleTrack.activate) {

				if(powerCodes.ContainsKey(currentIns)) {
					byte subType = byte.Parse(powerCodes[currentIns].ToString());
					Power.AssignToCharacter(ConsoleTrack.character, (byte) subType);
					return;
				}

				// "wand" was the final valid instruction. Give a random wand to the character.
				Power.AssignToCharacter(ConsoleTrack.character, (byte) PowerSubType.RandPot);
			}
		}

		public static readonly Dictionary<string, object> powerCodes = new Dictionary<string, object>() {
			{ "slowfall", (byte) PowerSubType.SlowFall },
			{ "hover", (byte) PowerSubType.Hover },
			{ "levitate", (byte) PowerSubType.Levitate },
			{ "fly", (byte) PowerSubType.Flight },
			{ "athlete", (byte) PowerSubType.Athlete },
			{ "leap", (byte) PowerSubType.Leap },
			{ "slam", (byte) PowerSubType.Slam },
			{ "burst", (byte) PowerSubType.Burst },
			{ "air", (byte) PowerSubType.Air },
			{ "phase", (byte) PowerSubType.Phase },
			{ "teleport", (byte) PowerSubType.Teleport },
		};
	}
}
