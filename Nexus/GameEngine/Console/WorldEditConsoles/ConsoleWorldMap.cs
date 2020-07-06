﻿using Nexus.Engine;
using Nexus.Gameplay;
using System;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public static class ConsoleWorldMap {

		public static void ResizeMap() {
			string currentIns = ConsoleTrack.GetArgAsString();
			int curVal = ConsoleTrack.GetArgAsInt();

			ConsoleTrack.PrepareTabLookup(resizeOpts, currentIns, "Resize World Map");

			// Width Option
			if(currentIns == "width") {
				int currentWidth = ((WEScene)Systems.scene).xCount;
				ConsoleTrack.possibleTabs = "Example: resize width 60";
				ConsoleTrack.helpText = "Choose a width between " + (byte)WorldmapEnum.MinWidth + " and " + (byte)WorldmapEnum.MaxWidth + ". Currently at " + currentWidth + ".";
			}

			// Height Option
			else if(currentIns == "height") {
				int currentHeight = ((WEScene)Systems.scene).yCount;
				ConsoleTrack.possibleTabs = "Example: resize height 60";
				ConsoleTrack.helpText = "Choose a height between " + (byte)WorldmapEnum.MinHeight + " and " + (byte)WorldmapEnum.MaxHeight + ". Currently at " + currentHeight + ".";
			}

			else { return; }

			// Activate Resize
			if(ConsoleTrack.activate && curVal > 0) {
				WEScene scene = (WEScene)Systems.scene;

				if(currentIns == "width" && curVal >= (byte)WorldmapEnum.MinWidth && curVal <= (byte)WorldmapEnum.MaxWidth) {
					scene.ResizeWidth((byte) curVal);
				}

				else if(currentIns == "height" && curVal >= (byte)WorldmapEnum.MinHeight && curVal <= (byte)WorldmapEnum.MaxHeight) {
					scene.ResizeHeight((byte)curVal);
				}
			}
		}

		public static readonly Dictionary<string, object> resizeOpts = new Dictionary<string, object>() {
			{ "width", "Change the world map's width." },
			{ "height", "Change the world map's height." },
		};

		public static void SetMode() {
			string currentIns = ConsoleTrack.GetArgAsString();

			ConsoleTrack.PrepareTabLookup(modeOpts, currentIns, "Set the campaign mode for this world.");

			// If an option is selected:
			if(modeOpts.ContainsKey(currentIns)) {
				ConsoleTrack.helpText = modeOpts[currentIns].ToString();
				ConsoleTrack.possibleTabs = "";
			}

			if(ConsoleTrack.activate) {
				WEScene scene = (WEScene)Systems.scene;
				scene.weUI.alertText.SetNotice("Unavailable During Alpha", "These game modes cannot be set during the alpha release.", 300);
				return;
			}
		}

		public static readonly Dictionary<string, object> modeOpts = new Dictionary<string, object>() {
			{ "softcore", GameplayTypes.HardcoreDesc[0] },
			{ "mediumcore", GameplayTypes.HardcoreDesc[1] },
			{ "hardcore", GameplayTypes.HardcoreDesc[2] },
			{ "punishing", GameplayTypes.HardcoreDesc[3] },
			{ "brutal", GameplayTypes.HardcoreDesc[4] },
			{ "nightmare", GameplayTypes.HardcoreDesc[5] },
			{ "hell", GameplayTypes.HardcoreDesc[6] },
		};

		public static void SetLives() {
			short lives = (short) ConsoleTrack.GetArgAsInt();
			WEScene scene = (WEScene)Systems.scene;

			ConsoleTrack.possibleTabs = "Example: `lives 50`";
			ConsoleTrack.helpText = "Set the number of lives you start with: 1 to 500. Currently: " + scene.worldData.lives;

			if(ConsoleTrack.activate) {
				if(lives < 1 || lives > 500) {
					scene.weUI.alertText.SetNotice("Cannot Set Lives", "Lives must be set between 1 and 500.", 240);
					return;
				}

				scene.worldContent.data.lives = lives;
				scene.weUI.alertText.SetNotice("Set World Lives", "Set to \"" + lives + "\" lives.", 240);
			}
		}

		public static void SetName() {
			string text = Sanitize.Title(ConsoleTrack.instructionText.Substring(4).TrimStart());
			WEScene scene = (WEScene)Systems.scene;

			if(text.Length > 0) {
				ConsoleTrack.instructionText = "name " + text.Substring(0, Math.Min(text.Length, 24));
			}

			short remain = (short)(24 - text.Length);

			ConsoleTrack.possibleTabs = "Example: `name My World`";
			ConsoleTrack.helpText = "Provide a world name. Currently: \"" + scene.worldData.name + "\". " + remain.ToString() + " characters remaining.";

			// Activate the Instruction
			if(ConsoleTrack.activate) {

				// Prevent Rename if it exceeds name length.
				if(text.Length > 24) {
					scene.weUI.alertText.SetNotice("Unable to Rename Level", "Title must be 24 characters or less.", 240);
					return;
				}

				scene.worldContent.data.name = text;
				scene.weUI.alertText.SetNotice("New World Title", "Title Set: \"" + text + "\"", 240);
			}
		}

		public static void SetDescription() {
			string text = Sanitize.Description(ConsoleTrack.instructionText.Substring(4).TrimStart());
			WEScene scene = (WEScene)Systems.scene;

			if(text.Length > 0) {
				ConsoleTrack.instructionText = "desc " + text.Substring(0, Math.Min(text.Length, 72));
			}

			short remain = (short)(72 - text.Length);

			ConsoleTrack.possibleTabs = "Example: `desc This world is awesome. You will love it.`";
			ConsoleTrack.helpText = "Provide a description. Currently: \"" + scene.worldData.description + "\". " + remain.ToString() + " characters remaining.";

			// Activate the Instruction
			if(ConsoleTrack.activate) {

				// Prevent Rename if it exceeds name length.
				if(text.Length > 72) {
					scene.weUI.alertText.SetNotice("Unable to Set Description", "World description must be 72 characters or less.", 240);
					return;
				}

				scene.worldContent.data.description = text;
				scene.weUI.alertText.SetNotice("New World Description", "Description Set: \"" + text + "\"", 240);
			}
		}

		public static void SetMusicTrack() {
			string currentIns = ConsoleTrack.GetArgAsString();
			WEScene scene = (WEScene)Systems.scene;

			// Update the tab lookup.
			ConsoleTrack.PrepareTabLookup(ConsoleEditData.TrackCategory, currentIns, "Choose a music category for the track to play.");

			if(!ConsoleEditData.TrackCategory.ContainsKey(currentIns)) { return; }

			// Remove Music From Level
			if(currentIns == "none") {
				scene.worldContent.data.music = 0;
				scene.weUI.alertText.SetNotice("Removed Music Track", "", 240);
				return;
			}

			Dictionary<string, object> trackCat = ConsoleEditData.TrackLookup[currentIns];

			string trackName = ConsoleTrack.GetArgAsString();
			ConsoleTrack.PrepareTabLookup(trackCat, trackName, "Set a music track for the world.");

			if(MusicAssets.TrackNames.ContainsKey(scene.worldContent.data.music)) {
				ConsoleTrack.helpText += "Currently: \"" + MusicAssets.TrackNames[scene.worldContent.data.music].Replace("Music/", "") + "\".";
			}

			// Activate the Instruction
			if(ConsoleTrack.activate) {

				if(trackCat.ContainsKey(trackName)) {
					byte track = (byte)trackCat[trackName];
					scene.worldContent.data.music = (byte)track;
					scene.weUI.alertText.SetNotice("Set Music Track", "Music Track set to " + MusicAssets.TrackNames[track].Replace("Music/", "") + ".", 240);
					return;
				}

				// Prevent Rename if it exceeds name length.
				scene.weUI.alertText.SetNotice("Unable to Set Music Track", "Designated music track doesn't exist.", 240);
			}
		}
		
		public static void SetLevel() {
			byte gridX = (byte) ConsoleTrack.GetArgAsInt();
			ConsoleTrack.possibleTabs = "Example: setLevel 10 10 MyLevelID";

			// If gridX is assigned:
			if(ConsoleTrack.instructionList.Count >= 2) {
				byte gridY = (byte)ConsoleTrack.GetArgAsInt();

				// If gridY is assigned:
				if(ConsoleTrack.instructionList.Count >= 3) {

					// Check if this X, Y grid is valid (has a node at it).
					WEScene scene = (WEScene) Systems.scene;
					WorldZoneFormat zone = scene.currentZone;
					byte[] wtData = scene.worldContent.GetWorldTileData(zone, gridX, gridY);

					// If the location is a valid node, we can attempt to add a level ID.
					if(NodeData.IsObjectANode(wtData[5], false, false, true)) {
						string coordStr = Coords.MapToInt(gridX, gridY).ToString();
						string levelId = ConsoleTrack.GetArg();
						ConsoleTrack.helpText = "Assign a level ID to the specified node.";

						if(zone.nodes.ContainsKey(coordStr)) {
							ConsoleTrack.helpText += " Current level ID is: " + zone.nodes[coordStr];
						}

						// If the console was activated:
						if(ConsoleTrack.activate) {
							zone.nodes[coordStr] = levelId;
							return;
						}
					}

					// If the location is invalid:
					else {
						ConsoleTrack.helpText = "WARNING! There is not a level node at " + gridX.ToString() + ", " + gridY.ToString();
					}
				}

				// If gridY has not been assigned:
				else {
					ConsoleTrack.helpText = "Assign a level ID to a node at the specified X, Y coordinate. Enter the Y position.";
				}
			}
			
			// If gridX has not been assigned:
			else {
				ConsoleTrack.helpText = "Assign a level ID to a node at the specified X, Y coordinate. Enter the X position.";
			}
		}
	}
}
