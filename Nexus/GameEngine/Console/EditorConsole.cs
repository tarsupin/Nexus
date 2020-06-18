using System;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class EditorConsole : Console {

		public EditorConsole() : base() {
			this.baseHelperText = "The editor console can help you with level editing, including setting special values.";

			this.consoleDict = new Dictionary<string, Action>() {

				//{ "macro", ConsoleMacro.DebugMacro },	// Convert this to EditorMacros. They should be separate from Macros (which are for levels).
				
				{ "tile", ConsoleTileWand.TileWand },
				{ "resize", ConsoleRoom.Resize },
			};
		}
	}
}
