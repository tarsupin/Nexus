using System;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class WorldEditConsole : Console {

		public WorldEditConsole() : base() {
			this.baseHelperText = "The world editor console can provide special tools for world editing.";

			this.consoleDict = new Dictionary<string, Action>() {
				//{ "macro", ConsoleMacro.DebugMacro },	// Convert this to EditorMacros. They should be separate from Macros (which are for levels).
			};
		}
	}
}
