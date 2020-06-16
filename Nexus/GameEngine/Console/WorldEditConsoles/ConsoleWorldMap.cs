using Nexus.Engine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public static class ConsoleWorldMap {

		public static void ResizeMap() {
			string currentIns = ConsoleTrack.GetArgAsString();
			int curVal = ConsoleTrack.GetArgAsInt();

			ConsoleTrack.PrepareTabLookup(resizeOpts, currentIns, "Resize World Map");

			// Width Option
			if(currentIns == "width") {
				ConsoleTrack.possibleTabs = "Example: resize width 60";
				ConsoleTrack.helpText = "Choose a width between " + (byte)WorldmapEnum.MinWidth + " and " + (byte)WorldmapEnum.MaxWidth;
			}

			// Height Option
			else if(currentIns == "height") {
				ConsoleTrack.possibleTabs = "Example: resize height 60";
				ConsoleTrack.helpText = "Choose a height between " + (byte)WorldmapEnum.MinHeight + " and " + (byte)WorldmapEnum.MaxHeight;
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

		public static void SetValue() {
			string currentIns = ConsoleTrack.GetArgAsString();
			int curVal = ConsoleTrack.GetArgAsInt();

			ConsoleTrack.PrepareTabLookup(valueOpts, currentIns, "Set World Campaign Values");

			// Name Option
			if(currentIns == "name") {
				ConsoleTrack.possibleTabs = "Example: setValue name \"My World Name\"";
				ConsoleTrack.helpText = "Choose a name for your world.";
			}

			// Lives Option
			else if(currentIns == "lives") {
				ConsoleTrack.possibleTabs = "Example: setValue lives 30";
				ConsoleTrack.helpText = "Choose the number of lives to start with. Between 1 and 99.";
			}

			// Character Option
			else if(currentIns == "character") {
				ConsoleTrack.possibleTabs = "Example: setValue character Ryu";
				ConsoleTrack.helpText = ".";
			}

			if(valueOpts.ContainsKey(currentIns)) {
				valueOpts[currentIns].Invoke();
				return;
			}
		}

		public static readonly Dictionary<string, System.Action> valueOpts = new Dictionary<string, System.Action>() {
			{ "name", ConsoleWorldMap.SetName },
			{ "lives", ConsoleWorldMap.SetLives },
			{ "character", ConsoleWorldMap.SetCharacter },
		};

		private static void SetName() {
			string insString = ConsoleTrack.instructionText;

			if(ConsoleTrack.activate) {
				System.Console.WriteLine(insString);
			}
		}

		private static void SetLives() {
			byte lives = (byte) ConsoleTrack.GetArgAsInt();

			if(ConsoleTrack.activate) {
				if(lives > 0 && lives < 100) {
					System.Console.WriteLine("Assign Lives: " + lives);
					return;
				}
			}
		}

		private static void SetCharacter() {
			string charName = ConsoleTrack.GetArgAsString();

			ConsoleTrack.PrepareTabLookup(characterOpts, charName, "Assign a character archetype for this world campaign.");

			if(ConsoleTrack.activate) {
				if(characterOpts.ContainsKey(charName)) {
					System.Console.WriteLine("Assign Character " + charName);
					return;
				}
			}
		}

		public static readonly Dictionary<string, object> characterOpts = new Dictionary<string, object>() {
			{ "carl", "carl" },
			{ "poo", "poo" },
			{ "ryu", "ryu" },
		};

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
					if(NodeData.IsObjectANode(wtData[5])) {
						string levelId = ConsoleTrack.GetArg();
						ConsoleTrack.helpText = "Assign a level ID to the specified node. Enter the Level ID.";

						// If the console was activated:
						if(ConsoleTrack.activate) {
							int coordInt = Coords.MapToInt(gridX, gridY);
							zone.nodes[coordInt.ToString()] = levelId;
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
