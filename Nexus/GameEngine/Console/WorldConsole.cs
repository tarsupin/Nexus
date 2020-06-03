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
			};
		}
	}
}
