using Nexus.Engine;
using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public static class ConsoleTileWand {

		public static void TileWand() {

			// The next instruction should be the grid location of the tile, split by a comma. For example: 25,33
			string statIns = ConsoleTrack.NextArg();

			// Make sure the instruction is set up correctly:
			if(statIns.Length == 0) { return; }

			string tmp = statIns;
			statIns = Sanitize.Coordinates(statIns);

			// Sanitizing removed any characters that aren't coordinates. Repair the instruction.
			if(tmp != statIns) {
				ConsoleTrack.instructionText = "tile " + statIns;
			}

			// Must contain a coordinate; e.g. "104,33"
			// Split the instruction between the comma, make sure it has coordinates on both sides:
			if(!statIns.Contains(",")) { return; }
			
			// Split the instruction; needs to be valid coordinates on each side.
			string[] split = statIns.Split(',');

			if(split.Length != 2 || split[0].Length == 0 || split[1].Length == 0) { return; }

			// Identify the Coordinates
			ushort xCoord = ushort.Parse(split[0]);
			ushort yCoord = ushort.Parse(split[1]);

			// Make sure the Coordinates are within the grid.
			EditorRoomScene scene = ((EditorScene)Systems.scene).CurrentRoom;

			if(xCoord < 0 || xCoord > scene.Width || (yCoord < 0 || yCoord > scene.Height)) {
				ConsoleTrack.helpText = "Coordinates must be in bounds.";
				return;
			}

			// Get Tile at location
			byte[] tileData = LevelContent.GetTileData(scene.levelContent.data.rooms[scene.roomID].main, xCoord, yCoord);

			if(tileData == null) {
				ConsoleTrack.helpText = "The coordinates " + statIns + " don't possess a valid tile to modify.";
				return;
			}

			// Get the Tile Object
			TileGameObject tileObj = Systems.mapper.TileDict[tileData[0]];

			// Render the tile with its designated Class Object:
			if(tileObj is TileGameObject) {
				if(tileObj.title != null) {
					ConsoleTrack.helpText = tileObj.title + " at " + statIns + "!";
				} else if(tileObj.titles != null) {
					ConsoleTrack.helpText = tileObj.titles[tileData[1]] + " at " + statIns + "!";
				}
			};

			//ConsoleTrack.PrepareTabLookup(statCodes, statIns, "Assign stats for the character.");

			//// If the stat instruction is a full word, then we can indicate that it's time to provide additional help text:
			//if(statCodes.ContainsKey(statIns)) {

			//	if(statCodes[statIns] is Action[]) {
			//		(statCodes[statIns] as Action[])[0].Invoke();
			//		return;
			//	}

			//	ConsoleTrack.possibleTabs = "";
			//	ConsoleTrack.helpText = statCodes[statIns].ToString();
			//}

			if(ConsoleTrack.activate) {
				//Character character = ConsoleTrack.character;

				//// Reset All Stats
				//if(statIns == "reset-all") { character.stats.ResetCharacterStats(); }

				//// Gravity
				//if(statIns == "gravity") { character.stats.BaseGravity = FInt.Create(ConsoleTrack.NextArgAsFloat()); }
			}
		}

		//public static void CheatStatWall() {
		//	string currentIns = ConsoleTrack.NextArg();

		//	ConsoleTrack.PrepareTabLookup(wallStatCodes, currentIns, "Assign wall-related stats for the character.");

		//	// If the stat instruction is a full word, then we can indicate that it's time to provide additional help text:
		//	if(wallStatCodes.ContainsKey(currentIns)) {
		//		ConsoleTrack.possibleTabs = "";
		//		ConsoleTrack.helpText = wallStatCodes[currentIns].ToString();
		//	}

		//	if(ConsoleTrack.activate) {
		//		Character character = ConsoleTrack.character;

		//		// Abilities
		//		if(currentIns == "can-wall-jump") { bool boolVal = ConsoleTrack.NextArgAsBool(); character.stats.CanWallJump = boolVal; character.stats.CanWallSlide = boolVal; }
		//		else if(currentIns == "can-grab") { character.stats.CanWallGrab = ConsoleTrack.NextArgAsBool(); }
		//		else if(currentIns == "can-slide") { character.stats.CanWallSlide = ConsoleTrack.NextArgAsBool(); }
		//	}
		//}

		//public static readonly Dictionary<string, object> wallStatCodes = new Dictionary<string, object>() {
		//	{ "can-wall-jump", "Character can jump off of walls? Set to TRUE or FALSE." },
		//	{ "can-grab", "Character can cling to walls? Set to TRUE or FALSE." },
		//	{ "can-slide", "Character slides down walls slowly? Set to TRUE or FALSE." },
		//};
	}
}
