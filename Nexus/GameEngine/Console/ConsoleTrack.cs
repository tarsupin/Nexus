using Nexus.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Nexus.GameEngine {

	// Detects and tracks information related to the most recent instruction sent to the console.
	// Used to determine what your interface will tell you, such as helper text and tab information.
	public static class ConsoleTrack {

		public static Player player = null; // Tracks which player to apply the effect to, if applicable.
		public static Character character = null; // Tracks which character to apply the effect to, if applicable.
		public static string instructionText = String.Empty; // The original command, as written into the console.
		public static List<string> instructionList = new List<string>() {}; // Stores the split() instructions.
		public static byte instructionArgIndex = 0; // The index of the instructionList argument that is next in line.
		public static string tabLookup = String.Empty; // The tab to highlight (whichever is most likely).
		public static string helpText = String.Empty; // Helpful text that applies to the current instruction set you're in.
		public static string possibleTabs = String.Empty; // A list of tab options to show.
		public static bool activate = false; // TRUE on the RunTick() that 'enter' gets pressed.

		public static void LoadInstructionText( string insText ) {

			// Get the instruction array.
			string[] insArray = insText.Split(' ');

			ConsoleTrack.instructionList = new List<string>(insArray);

			// Reset Console Track Information
			ConsoleTrack.instructionArgIndex = 0;
			ConsoleTrack.tabLookup = String.Empty;
			ConsoleTrack.helpText = String.Empty;
			ConsoleTrack.possibleTabs = String.Empty;
			ConsoleTrack.player = null;
			ConsoleTrack.character = null;
		}

		public static void PrepareTabLookup( Dictionary<string, Action> dict, string currentIns, string helpText ) {

			// Update help text.
			ConsoleTrack.helpText = helpText;

			// Make sure you're on the last instruction, otherwise no tab lookup applies.
			if(!ConsoleTrack.OnLastInstruction()) { return; }

			if(currentIns.Length > 0) {

				// Scan for instructions that start with the arg provided:
				string tab = dict.Where(pv => pv.Key.StartsWith(currentIns)).FirstOrDefault().Key;

				// Update the tab lookup.
				ConsoleTrack.tabLookup = tab != null ? Regex.Replace(tab, "^" + currentIns, "") : string.Empty;
			}

			else {
				ConsoleTrack.tabLookup = string.Empty;
			}

			// Update possible tabs.
			ConsoleTrack.possibleTabs = "Options: " + String.Join(", ", dict.Keys.ToArray());
		}

		public static void PrepareTabLookup( Dictionary<string, object> dict, string currentIns, string helpText ) {

			// Update help text.
			ConsoleTrack.helpText = helpText;

			// Make sure you're on the last instruction, otherwise no tab lookup applies.
			if(!ConsoleTrack.OnLastInstruction()) { return; }

			if(currentIns.Length > 0) {

				// Scan for instructions that start with the arg provided:
				string tab = dict.Where(pv => pv.Key.StartsWith(currentIns)).FirstOrDefault().Key;

				// Update the tab lookup.
				ConsoleTrack.tabLookup = tab != null ? Regex.Replace(tab, "^" + currentIns, "") : string.Empty;

			} else {
				ConsoleTrack.tabLookup = string.Empty;
			}

			// Update possible tabs.
			ConsoleTrack.possibleTabs = "Options: " + String.Join(", ", dict.Keys.ToArray());
		}
		
		public static void PrepareTabLookup(string helpText) {

			// Update help text.
			ConsoleTrack.helpText = helpText;

			// Clear the Menu's Text
			ConsoleTrack.tabLookup = string.Empty;
			ConsoleTrack.possibleTabs = string.Empty;
		}

		// Returns `true` if we're on the last instruction arg.
		public static bool OnLastInstruction() {
			return ConsoleTrack.instructionArgIndex >= ConsoleTrack.instructionList.Count;
		}

		// Updates the instruction index and returns next instruction instruction string (or "" if none).
		public static string GetArg(bool increment = true) {
			byte num = ConsoleTrack.instructionArgIndex;
			if(increment) { ConsoleTrack.instructionArgIndex++; }

			if(ConsoleTrack.instructionList.Count > num) {
				return ConsoleTrack.instructionList[num];
			}

			return string.Empty;
		}

		// Returns the next instruction arg as a string.
		public static string GetArgAsString(bool increment = true) {
			string arg = ConsoleTrack.GetArg(increment);
			return arg.ToString().ToLower();
		}
		
		// Returns the next instruction arg as a bool.
		public static bool GetArgAsBool(bool increment = true) {
			string arg = ConsoleTrack.GetArg(increment);
			return (arg == "true" || arg == "1" || arg == "on");
		}

		// Returns the next instruction arg as an int.
		public static int GetArgAsInt(bool increment = true) {
			string arg = ConsoleTrack.GetArg(increment);

			if(arg != string.Empty) {
				int intVal;
				if(!Int32.TryParse(arg, out intVal)) { intVal = 0; }
				return intVal;
			}

			return 0;
		}

		// Returns the next instruction arg as a float.
		public static float GetArgAsFloat(bool increment = true) {
			string arg = ConsoleTrack.GetArg(increment);

			if(arg != string.Empty) {
				float floatVal;
				if(!float.TryParse(arg, out floatVal)) { floatVal = 0; }
				return floatVal;
			}

			return 0;
		}

		// Reset the Console Track Activation
		public static void ResetValues() {
			ConsoleTrack.activate = false; // Not resetting this would cause the instructions to activate every cycle.
			ConsoleTrack.instructionText = string.Empty;
			ConsoleTrack.helpText = String.Empty;
			ConsoleTrack.possibleTabs = String.Empty;
		}
	}
}
