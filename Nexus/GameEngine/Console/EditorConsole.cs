using System;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class EditorConsole : Console {

		public EditorConsole() : base() {
			this.baseHelperText = "The editor console can help you with level editing, including setting special values.";

			this.consoleDict = new Dictionary<string, Action>() {

				//{ "macro", ConsoleMacro.DebugMacro },	// Convert this to EditorMacros. They should be separate from Macros (which are for levels).
				
				{ "resize", ConsoleRoom.Resize },
				
				// Level
				{ "level", EditorConsole.LevelChange },

				// Set Level Data
				{ "title", ConsoleEditData.SetTitle },
				{ "desc", ConsoleEditData.SetDescription },
				{ "time", ConsoleEditData.SetTimeLimit },
				{ "music", ConsoleEditData.SetMusicTrack },
				//{ "game-class", ConsoleEditData.SetGameClass },
			};
		}

		public static void LevelChange() {
			string currentIns = ConsoleTrack.GetArgAsString();

			ConsoleTrack.possibleTabs = "Example: `level QCALQOD16`";
			ConsoleTrack.helpText = "The level ID of the level to load.";

			if(ConsoleTrack.activate) {
				SceneTransition.ToLevelEditor("", currentIns);
			}
		}
	}
}
