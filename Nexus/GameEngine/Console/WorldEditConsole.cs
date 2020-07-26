﻿using Microsoft.Xna.Framework;
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
				{ "setwarp", ConsoleWorldMap.SetWarp },
				
				// Loading Worlds and Levels
				{ "load-world", WorldEditConsole.LoadWorldEditor },
				{ "load-level", EditorConsole.LoadLevelEditor },

				// Set World Data
				{ "title", ConsoleWorldMap.SetTitle },
				{ "desc", ConsoleWorldMap.SetDescription },
				{ "mode", ConsoleWorldMap.SetMode },
				{ "lives", ConsoleWorldMap.SetLives },
				{ "music", ConsoleWorldMap.SetMusicTrack },

				// Publishing
				{ "publish", WorldEditConsole.PublishWorld },
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

		public static void LoadWorldEditor() {
			string currentIns = ConsoleTrack.GetArgAsString();

			ConsoleTrack.possibleTabs = "Example: `load-world worldIdHere`";
			ConsoleTrack.helpText = "The world ID of the world to load. Will load a world (if it exists).";

			if(ConsoleTrack.activate) {
				SceneTransition.ToWorldEditor(currentIns);
			}
		}

		public static void PublishWorld() {
			ConsoleTrack.possibleTabs = "";
			ConsoleTrack.helpText = "This will publish your world, providing you with a world ID to share with players.";

			// Attempt to Publish the Level
			if(ConsoleTrack.activate) {
				_ = WebHandler.WorldPublishRequest(Systems.handler.worldContent.worldId);
			}
		}
	}
}
