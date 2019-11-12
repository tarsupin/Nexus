using Nexus.Engine;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public static class ConsoleMacro {

		public static void DebugMacro() {
			string currentIns = ConsoleTrack.NextArg();

			ConsoleTrack.PrepareTabLookup(macroCodes, currentIns, "Save a macro to one of the function keys: F1 - F4.");

			if(macroCodes.ContainsKey(currentIns)) {
				ConsoleTrack.possibleTabs = "Example: macro f1 \"suit ninja red | hat cowboy\"";
				ConsoleTrack.helpText = macroCodes[currentIns].ToString();
			}
			
			// If an invalid macro code was applied, end here. Don't allow any activations to process.
			else { return; }

			if(ConsoleTrack.activate) {
				string insText = ConsoleTrack.instructionText.ToLower().Trim();

				int index1 = insText.IndexOf('"') + 1;
				int index2 = insText.IndexOf('"', index1);

				// Only continue if there are quotes present (must encapsulate the macro in quotes)
				if(index1 < 0 || index2 <= index1) { return; }

				// Get the macro substring:
				string macroStr = insText.Substring(index1, index2 - index1);
				
				// Apply the macro to the appropriate function:
				if(currentIns == "f1") { Systems.settings.input.macroF1 = macroStr; }
				else if(currentIns == "f2") { Systems.settings.input.macroF2 = macroStr; }
				else if(currentIns == "f3") { Systems.settings.input.macroF3 = macroStr; }
				else if(currentIns == "f4") { Systems.settings.input.macroF4 = macroStr; }
				else { return; }

				// Save the macro into the new settings.
				Systems.settings.input.SaveSettings();
			}
		}

		public static readonly Dictionary<string, object> macroCodes = new Dictionary<string, object>() {
			{ "f1", "Assign a macro to the F1 key." },
			{ "f2", "Assign a macro to the F2 key." },
			{ "f3", "Assign a macro to the F3 key." },
			{ "f4", "Assign a macro to the F4 key." },
		};
	}
}
