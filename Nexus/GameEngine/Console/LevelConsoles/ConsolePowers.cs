using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public static class ConsolePowers {

		public static void CheatCodePowers() {
			string statIns = ConsoleTrack.NextArg();

			ConsoleTrack.PrepareTabLookup(basePowerCodes, statIns, "Assign a power to the character.");

			if(basePowerCodes.ContainsKey(statIns)) {
				basePowerCodes[statIns].Invoke();
				return;
			}
		}

		public static readonly Dictionary<string, System.Action> basePowerCodes = new Dictionary<string, System.Action>() {
			{ "mobility", ConsolePowers.CheatCodeMobility },
			{ "weapon", ConsolePowers.CheatCodeWeapon },
			{ "ranged", ConsolePowers.CheatCodeRanged },
			{ "wand", ConsolePowers.CheatCodeWand },
			{ "magic", ConsolePowers.CheatCodeMagic },
		};

		public static void CheatCodeWeapon() {
			string currentIns = ConsoleTrack.NextArg();

			ConsoleTrack.PrepareTabLookup(weaponCodes, currentIns, "Assign a weapon to the character; e.g. `power weapon spear`");

			if(ConsoleTrack.activate) {

				if(weaponCodes.ContainsKey(currentIns)) {
					byte subType = byte.Parse(weaponCodes[currentIns].ToString());
					Power.AssignToCharacter(ConsoleTrack.character, (byte) subType);
					return;
				}

				// "weapon" was the final valid instruction. Give a random weapon to the character.
				Power.AssignToCharacter(ConsoleTrack.character, (byte) PowerSubType.RandomWeapon);
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

			ConsoleTrack.PrepareTabLookup(rangedWeaponCodes, currentIns, "Assign a ranged weapon to the character; e.g. `power ranged axe`");

			if(ConsoleTrack.activate) {

				if(rangedWeaponCodes.ContainsKey(currentIns)) {
					byte subType = byte.Parse(rangedWeaponCodes[currentIns].ToString());
					Power.AssignToCharacter(ConsoleTrack.character, (byte) subType);
					return;
				}

				// "ranged" was the final valid instruction. Give a random ranged weapon to the character.
				Power.AssignToCharacter(ConsoleTrack.character, (byte) PowerSubType.RandomThrown);
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

			ConsoleTrack.PrepareTabLookup(wandWeaponCodes, currentIns, "Assign a wand to the character; e.g. `power wand blue`");

			if(ConsoleTrack.activate) {

				if(wandWeaponCodes.ContainsKey(currentIns)) {
					byte subType = byte.Parse(wandWeaponCodes[currentIns].ToString());
					Power.AssignToCharacter(ConsoleTrack.character, (byte) subType);
					return;
				}

				// "wand" was the final valid instruction. Give a random wand to the character.
				Power.AssignToCharacter(ConsoleTrack.character, (byte) PowerSubType.RandomBolt);
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

			ConsoleTrack.PrepareTabLookup(magicCodes, currentIns, "Assign magic power to the character; e.g. `power magic fire`");

			if(ConsoleTrack.activate) {

				if(magicCodes.ContainsKey(currentIns)) {
					byte subType = byte.Parse(magicCodes[currentIns].ToString());
					Power.AssignToCharacter(ConsoleTrack.character, (byte) subType);
					return;
				}

				// "wand" was the final valid instruction. Give a random wand to the character.
				Power.AssignToCharacter(ConsoleTrack.character, (byte) PowerSubType.RandomPotion);
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

		public static void CheatCodeMobility() {
			string currentIns = ConsoleTrack.NextArg();

			ConsoleTrack.PrepareTabLookup(mobilityCodes, currentIns, "Assign mobility power to the character; e.g. `power mobility levitate`");

			if(ConsoleTrack.activate) {

				if(mobilityCodes.ContainsKey(currentIns)) {
					byte subType = byte.Parse(mobilityCodes[currentIns].ToString());
					Power.AssignToCharacter(ConsoleTrack.character, (byte) subType);
					return;
				}

				// "wand" was the final valid instruction. Give a random wand to the character.
				Power.AssignToCharacter(ConsoleTrack.character, (byte) PowerSubType.RandomBook);
			}
		}

		public static readonly Dictionary<string, object> mobilityCodes = new Dictionary<string, object>() {
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
