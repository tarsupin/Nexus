using Nexus.Engine;
using System;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class EditorConsole : Console {

		public EditorConsole() : base() {
			this.baseHelperText = "The editor console can help you with level editing, including setting special values.";

			this.consoleDict = new Dictionary<string, Action>() {

				//{ "macro", ConsoleMacro.DebugMacro },	// Convert this to EditorMacros. They should be separate from Macros (which are for levels).
				
				{ "resize", ConsoleRoom.Resize },
				
				// Loading Worlds and Levels
				{ "load-world", WorldEditConsole.LoadWorldEditor },
				{ "load-level", EditorConsole.LoadLevelEditor },

				// Set Level Data
				{ "title", ConsoleEditData.SetTitle },
				{ "desc", ConsoleEditData.SetDescription },
				{ "time", ConsoleEditData.SetTimeLimit },
				{ "music", ConsoleEditData.SetMusicTrack },
				//{ "game-class", ConsoleEditData.SetGameClass },
				
				// Publish Level
				{ "publish", EditorConsole.PublishLevel },
			};
		}

		public static void LoadLevelEditor() {
			string currentIns = ConsoleTrack.GetArgAsString();

			ConsoleTrack.possibleTabs = "Example: `load-level QCALQOD16`";
			ConsoleTrack.helpText = "The level ID of the level to load.";

			if(ConsoleTrack.activate) {
				SceneTransition.ToLevelEditor("", currentIns);
			}
		}

		public static void PublishLevel() {
			ConsoleTrack.possibleTabs = "";
			ConsoleTrack.helpText = "This will publish your level, providing you with a level ID to add to world maps.";

			// Attempt to Publish the Level
			if(ConsoleTrack.activate) {
				_ = WebHandler.LevelPublishRequestAsync(Systems.handler.levelContent.levelId);
			}
		}
	}
}
