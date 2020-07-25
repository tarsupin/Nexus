using System;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class WorldConsole : Console {

		public WorldConsole() : base() {
			this.baseHelperText = "The world console can provide special assistance within a world.";

			this.consoleDict = new Dictionary<string, Action>() {
				
				// Editor
				{ "editor", WCToEditor.ToEditor },

				// Reset Position (in case of getting stuck)
				{ "reset", WCReset.ResetOptions },

				// Loading Worlds and Levels
				{ "load-world", WorldConsole.WorldChange },
				{ "load-level", ConsoleLevel.LoadLevel },
			};
		}

		public static void WorldChange() {
			string currentIns = ConsoleTrack.GetArgAsString();

			ConsoleTrack.possibleTabs = "Example: `load-world worldIdHere`";
			ConsoleTrack.helpText = "This will load a world (if it exists). Enter the world ID of the world to load.";

			if(ConsoleTrack.activate && currentIns.Length > 0) {
				SceneTransition.ToWorld(currentIns);
			}
		}
	}
}
