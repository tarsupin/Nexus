using Microsoft.Xna.Framework;
using Nexus.Engine;
using System;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class WorldEditConsole : Console {

		public WorldEditConsole() : base() {
			this.baseHelperText = "This console provides tools for world editing.";

			this.consoleDict = new Dictionary<string, Action>() {
				{ "resize", ConsoleWorldMap.ResizeMap },
				{ "setlevel", ConsoleWorldMap.SetLevel },
				
				// Set World Data
				{ "name", ConsoleWorldMap.SetName },
				{ "desc", ConsoleWorldMap.SetDescription },
				{ "mode", ConsoleWorldMap.SetMode },
				{ "lives", ConsoleWorldMap.SetLives },
				{ "music", ConsoleWorldMap.SetMusicTrack },
			};
		}

		public override void OnFirstOpen() {
			ChatConsole.Clear();
			ChatConsole.SendMessage("Welcome to the World Edit Console.", Color.White);
			ChatConsole.SendMessage("----------------------------------", Color.White);
			ChatConsole.SendMessage("This console allows you to alter the settings for your world campaign.", Color.White);
			ChatConsole.SendMessage("To open or close this console, press the tilde (~) key.", Color.OrangeRed);
			ChatConsole.SendMessage("This console can also be accessed from the tab menu.", Color.OrangeRed);
		}

		public override void OnOpen() { 

		}
	}
}
