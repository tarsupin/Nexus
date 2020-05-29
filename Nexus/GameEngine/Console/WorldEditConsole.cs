using System;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class WorldEditConsole : Console {

		public WorldEditConsole() : base() {
			this.baseHelperText = "This console provides tools for world editing.";

			this.consoleDict = new Dictionary<string, Action>() {
				{ "resize", ConsoleWorldMap.ResizeMap },
			};
		}
	}
}
