using Nexus.Engine;
using Nexus.ObjectComponents;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public static class ConsoleEquipment {

		public static void CheatCodeSuit() {
			string currentIns = ConsoleTrack.GetArgAsString();

			// Update the tab lookup.
			ConsoleTrack.PrepareTabLookup(suitCodes, currentIns, "Add a suit to the character. Can designate which suit, if desired.");

			// Invoke the Relevant Next Function
			if(suitCodes.ContainsKey(currentIns)) {
				suitCodes[currentIns].Invoke();
				return;
			}

			// Activate the Instruction
			if(ConsoleTrack.activate) {

				// "suit" was the final valid instruction. Give a random suit to the character.
				Suit.AssignToCharacter(ConsoleTrack.character, (byte)SuitSubType.RandomSuit, true);
			}
		}

		public static readonly Dictionary<string, System.Action> suitCodes = new Dictionary<string, System.Action>() {
			{ "basic", ConsoleEquipment.CheatCodeSuitBasic },
			{ "ninja", ConsoleEquipment.CheatCodeSuitNinja },
			{ "wizard", ConsoleEquipment.CheatCodeSuitWizard },
		};

		public static void CheatCodeSuitBasic() {
			string currentIns = ConsoleTrack.GetArgAsString();

			// Update the tab lookup.
			ConsoleTrack.PrepareTabLookup(suitBasicCodes, currentIns, "Add a basic, unpowered suit to the character.");

			// Activate the Instruction
			if(ConsoleTrack.activate) {

				// Apply the Suit
				if(suitBasicCodes.ContainsKey(currentIns)) {
					byte subType = byte.Parse(suitBasicCodes[currentIns].ToString());
					Suit.AssignToCharacter(ConsoleTrack.character, (byte)subType, true);
					return;
				}

				// "suit basic" was the final valid instruction. Give a random suit to the character.
				Suit.AssignToCharacter(ConsoleTrack.character, (byte)SuitSubType.RandomBasic, true);
			}
		}

		public static readonly Dictionary<string, object> suitBasicCodes = new Dictionary<string, object>() {
			{ "red", (byte) SuitSubType.RedBasic },
		};
		
		public static void CheatCodeSuitNinja() {
			string currentIns = ConsoleTrack.GetArgAsString();

			// Update the tab lookup.
			ConsoleTrack.PrepareTabLookup(suitNinjaCodes, currentIns, "Add a ninja suit to the character.");

			// Activate the Instruction
			if(ConsoleTrack.activate) {
				UIHandler.AddNotification(UIAlertType.Success, "Ninja Suit", "Added Ninja Suit to Character.", 180);

				// Apply the Suit
				if(suitNinjaCodes.ContainsKey(currentIns)) {
					byte subType = byte.Parse(suitNinjaCodes[currentIns].ToString());
					Suit.AssignToCharacter(ConsoleTrack.character, (byte)subType, true);
					return;
				}

				// "suit ninja" was the final valid instruction. Give a random suit to the character.
				Suit.AssignToCharacter(ConsoleTrack.character, (byte)SuitSubType.RandomNinja, true);
			}
		}

		public static readonly Dictionary<string, object> suitNinjaCodes = new Dictionary<string, object>() {
			{ "black", (byte) SuitSubType.BlackNinja },
			{ "blue", (byte) SuitSubType.BlueNinja },
			{ "green", (byte) SuitSubType.GreenNinja },
			{ "red", (byte) SuitSubType.RedNinja },
			{ "white", (byte) SuitSubType.WhiteNinja },
		};
		
		public static void CheatCodeSuitWizard() {
			string currentIns = ConsoleTrack.GetArgAsString();

			// Update the tab lookup.
			ConsoleTrack.PrepareTabLookup(suitWizardCodes, currentIns, "Add a wizard suit to the character.");

			// Activate the Instruction
			if(ConsoleTrack.activate) {
				UIHandler.AddNotification(UIAlertType.Success, "Wizard Suit", "Added Wizard Suit to Character.", 180);

				// Apply the Suit
				if(suitWizardCodes.ContainsKey(currentIns)) {
					byte subType = byte.Parse(suitWizardCodes[currentIns].ToString());
					Suit.AssignToCharacter(ConsoleTrack.character, (byte)subType, true);
					return;
				}

				// "suit wizard" was the final valid instruction. Give a random suit to the character.
				Suit.AssignToCharacter(ConsoleTrack.character, (byte)SuitSubType.RandomWizard, true);
			}
		}

		public static readonly Dictionary<string, object> suitWizardCodes = new Dictionary<string, object>() {
			{ "blue", (byte) SuitSubType.BlueWizard },
			{ "green", (byte) SuitSubType.GreenWizard },
			{ "red", (byte) SuitSubType.RedWizard },
			{ "white", (byte) SuitSubType.WhiteWizard },
		};

		public static void CheatCodeHat() {
			string currentIns = ConsoleTrack.GetArgAsString();
			
			// Update the tab lookup.
			ConsoleTrack.PrepareTabLookup(hatCodes, currentIns, "Add a hat to the character. Can designate which hat, if desired.");

			// Activate the Instruction
			if(ConsoleTrack.activate) {
				UIHandler.AddNotification(UIAlertType.Success, "Hat", "Added Hat to Character.", 180);

				// Apply the Hat
				if(hatCodes.ContainsKey(currentIns)) {
					byte subType = byte.Parse(hatCodes[currentIns].ToString());
					Hat.AssignToCharacter(ConsoleTrack.character, (byte) subType, true);
					return;
				}

				// "hat" was the final valid instruction. Give a random hat to the character.
				Hat.AssignToCharacter(ConsoleTrack.character, (byte) HatSubType.RandomPowerHat, true);
			}
		}

		public static readonly Dictionary<string, object> hatCodes = new Dictionary<string, object>() {
			{ "none", (byte) HatSubType.None },
			{ "cosmetic", (byte) HatSubType.RandomMagicHat },
			{ "angel", (byte) HatSubType.AngelHat },
			{ "bamboo", (byte) HatSubType.BambooHat },
			{ "cowboy", (byte) HatSubType.CowboyHat },
			{ "feather", (byte) HatSubType.FeatheredHat },
			{ "fedora", (byte) HatSubType.Fedora },
			{ "hard", (byte) HatSubType.HardHat },
			{ "ranger", (byte) HatSubType.RangerHat },
			{ "spikey", (byte) HatSubType.SpikeyHat },
			{ "top", (byte) HatSubType.TopHat },
		};

		public static void ConsoleAssignHead() {
			string currentIns = ConsoleTrack.GetArgAsString();

			// Update the tab lookup.
			ConsoleTrack.PrepareTabLookup(headCodes, currentIns, "Assign the character's head.");

			// Activate the Instruction
			if(ConsoleTrack.activate) {

				// If "head" is the only instruction, give a random head to the character.
				if(currentIns == string.Empty) {
					Head.AssignToCharacter(ConsoleTrack.character, (byte)HeadSubType.RandomStandard, true);
					Systems.settings.login.HeadVal = 0;
					Systems.settings.login.SaveSettings();
					return;
				}

				// Get the Head Type by instruction:
				if(headCodes.ContainsKey(currentIns)) {
					byte subType = byte.Parse(headCodes[currentIns].ToString());
					Head.AssignToCharacter(ConsoleTrack.character, (byte)subType, true);
					Systems.settings.login.HeadVal = subType;
					Systems.settings.login.SaveSettings();
				}
			}
		}

		public static readonly Dictionary<string, object> headCodes = new Dictionary<string, object>() {

			// Random Options
			{ "any", (byte) HeadSubType.RandomStandard },

			// Generic Heads
			{ "lana", (byte) HeadSubType.LanaHead },

			// Assigned Heads
			{ "ryu", (byte) HeadSubType.RyuHead },
			{ "poo", (byte) HeadSubType.PooHead },
			{ "carl", (byte) HeadSubType.CarlHead },
			//{ "kirbs", (byte) HeadSubType.KirbsHead },
			//{ "panda", (byte) HeadSubType.PandaHead },
			//{ "neo", (byte) HeadSubType.NeoHead },
		};

		public static void CheatKey() {

			// Update the tab lookup.
			ConsoleTrack.possibleTabs = "";
			ConsoleTrack.helpText = "Provides a key to the character.";

			// Activate the Instruction
			if(ConsoleTrack.activate) {
				UIHandler.AddNotification(UIAlertType.Success, "Key", "Granted Key to Character.", 180);
				ConsoleTrack.character.trailKeys.AddKey();
			}
		}
	}
}
