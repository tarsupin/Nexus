using System;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class WorldConsole : Console {

		public WorldConsole() : base() {
			this.baseHelperText = "The world console can provide special assistance within a world.";

			this.consoleDict = new Dictionary<string, Action>() {
				//{ "macro", ConsoleMacro.DebugMacro },	// Convert this to EditorMacros. They should be separate from Macros (which are for levels).
			};
		}
	}
}
