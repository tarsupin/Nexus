using Nexus.Config;
using Nexus.Engine;
using Nexus.ObjectComponents;
using Nexus.Objects;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public static class ConsoleSave {

		public static void SaveState() {
			string currentIns = ConsoleTrack.GetArgAsString();
			ConsoleTrack.PrepareTabLookup(autoSave, currentIns, "Save the level state to a macro key.");

			if(ConsoleTrack.activate) {

				// Get My Character
				Character character = (Character)Systems.localServer.MyCharacter;
				if(character == null) { return; }

				// Get Macro Strings
				string headStr = character.head is Head ? "head " + character.head.subStr + " " + character.head.subStr + " | " : "";
				string suitStr = character.suit is Suit ? "suit " + character.suit.baseStr + " " + character.suit.subStr + " | " : "";
				string hatStr = "hat " + (character.hat is Hat ? character.hat.subStr : "none") + " | ";
				string mobStr = "power mobility " + (character.mobilityPower is PowerMobility ? character.mobilityPower.subStr : "none") + " | ";
				string attStr = "power " + (character.attackPower is PowerAttack ? character.attackPower.baseStr + " " + character.attackPower.subStr : "none") + " | ";

				string healthStr = "health " + character.wounds.Health + " | ";
				string armorStr = "armor " + character.wounds.Armor + " | ";

				// Get the macro substring:
				string macroStr = headStr + suitStr + hatStr + mobStr + attStr + healthStr + armorStr + " move coords " + character.posX + " " + character.posY;
				DebugConfig.AddDebugNote(macroStr);
				// Apply the macro to the appropriate function:
				if(currentIns == "f1") { Systems.settings.input.macroF1 = macroStr; }
				else if(currentIns == "f2") { Systems.settings.input.macroF2 = macroStr; }
				else if(currentIns == "f3") { Systems.settings.input.macroF3 = macroStr; }
				else if(currentIns == "f4") { Systems.settings.input.macroF4 = macroStr; }
				else if(currentIns == "f5") { Systems.settings.input.macroF5 = macroStr; }
				else if(currentIns == "f6") { Systems.settings.input.macroF6 = macroStr; }
				else if(currentIns == "f7") { Systems.settings.input.macroF7 = macroStr; }
				else if(currentIns == "f8") { Systems.settings.input.macroF8 = macroStr; }
				else { return; }

				// Save the macro into the new settings.
				Systems.settings.input.SaveSettings();
			}
		}

		public static readonly Dictionary<string, object> autoSave = new Dictionary<string, object>() {
			{ "f1", "Save to F1 key macro." },
			{ "f2", "Save to F2 key macro." },
			{ "f3", "Save to F3 key macro." },
			{ "f4", "Save to F4 key macro." },
			{ "f5", "Save to F5 key macro." },
			{ "f6", "Save to F6 key macro." },
			{ "f7", "Save to F7 key macro." },
			{ "f8", "Save to F8 key macro." },
		};
	}
}
