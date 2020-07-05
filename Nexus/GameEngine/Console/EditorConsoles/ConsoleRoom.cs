
using Nexus.Engine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public static class ConsoleRoom {

		public static void Resize() {
			string currentIns = ConsoleTrack.GetArgAsString();
			ConsoleTrack.PrepareTabLookup(resizeCodes, currentIns, "Resize a room. WARNING: BE CAREFUL! Reducing a room size can remove tiles you've placed.");

			if(resizeCodes.ContainsKey(currentIns)) {
				resizeCodes[currentIns].Invoke();
				return;
			}
		}

		public static readonly Dictionary<string, System.Action> resizeCodes = new Dictionary<string, System.Action>() {
			{ "width", ConsoleRoom.ResizeWidth },
			{ "height", ConsoleRoom.ResizeHeight },
			{ "one-screen", ConsoleRoom.ResizeOneScreen },
			{ "custom", ConsoleRoom.ResizeCustom },
		};

		public static void ResizeWidth() {
			int currentWidth = ((EditorRoomScene)Systems.scene).xCount;
			ConsoleTrack.possibleTabs = "Example: `resize hor 250`";
			ConsoleTrack.helpText = "Resize the level's width between " + (byte)TilemapEnum.MinWidth + " and " + (short)TilemapEnum.MaxTilesWide + ". Currently at " + currentWidth + ".";

			// Prepare Width
			int getWidth = ConsoleTrack.GetArgAsInt();

			// Activate the Instruction
			if(ConsoleTrack.activate) {
				if(getWidth < (byte)TilemapEnum.MinWidth) { getWidth = (byte)TilemapEnum.MinWidth; }
				if(getWidth > (short)TilemapEnum.MaxTilesWide) { getWidth = (short)TilemapEnum.MaxTilesWide; }
				EditorRoomScene scene = ((EditorScene)Systems.scene).CurrentRoom;
				scene.ResizeWidth((short) getWidth);
			}
		}
		
		public static void ResizeHeight() {
			int currentHeight = ((EditorRoomScene)Systems.scene).yCount;
			ConsoleTrack.possibleTabs = "Example: `resize vert 180`";
			ConsoleTrack.helpText = "Resize the level's height between " + (byte)TilemapEnum.MinHeight + " and " + (short)TilemapEnum.MaxTilesHigh + ". Currently at " + currentHeight + ".";

			// Prepare Height
			int getHeight = ConsoleTrack.GetArgAsInt();

			// Activate the Instruction
			if(ConsoleTrack.activate) {
				if(getHeight < (byte)TilemapEnum.MinHeight) { getHeight = (byte)TilemapEnum.MinHeight; }
				if(getHeight > (short)TilemapEnum.MaxTilesHigh) { getHeight = (short)TilemapEnum.MaxTilesHigh; }
				EditorRoomScene scene = ((EditorScene)Systems.scene).CurrentRoom;
				scene.ResizeHeight((short)getHeight);
			}
		}
		
		public static void ResizeOneScreen() {
			ConsoleTrack.possibleTabs = "Example: `resize one-screen`";
			ConsoleTrack.helpText = "Resize a room to be one screen. WARNING: This will delete all tiles down to one room size.";

			// Activate the Instruction
			if(ConsoleTrack.activate) {
				EditorRoomScene scene = ((EditorScene)Systems.scene).CurrentRoom;
				scene.ResizeHeight((byte)TilemapEnum.MinHeight);
				scene.ResizeWidth((byte)TilemapEnum.MinWidth);
			}
		}
		
		public static void ResizeCustom() {
			int currentWidth = ((EditorRoomScene)Systems.scene).xCount;
			int currentHeight = ((EditorRoomScene)Systems.scene).yCount;
			ConsoleTrack.possibleTabs = "Example: `resize custom 100 35`";
			ConsoleTrack.helpText = "Resize a room to be a custom width and height (in that order). Currently at " + currentWidth + ", " + currentHeight + ".";

			// Prepare Height
			int getWidth = ConsoleTrack.GetArgAsInt();
			int getHeight = ConsoleTrack.GetArgAsInt();

			// Activate the Instruction
			if(ConsoleTrack.activate) {
				if(getWidth < (byte)TilemapEnum.MinWidth) { getWidth = (byte)TilemapEnum.MinWidth; }
				if(getWidth > (short)TilemapEnum.MaxTilesWide) { getWidth = (short)TilemapEnum.MaxTilesWide; }
				if(getHeight < (byte)TilemapEnum.MinHeight) { getHeight = (byte)TilemapEnum.MinHeight; }
				if(getHeight > (short)TilemapEnum.MaxTilesHigh) { getHeight = (short)TilemapEnum.MaxTilesHigh; }
				EditorRoomScene scene = ((EditorScene)Systems.scene).CurrentRoom;
				scene.ResizeHeight((short)getWidth);
				scene.ResizeWidth((short)getHeight);
			}
		}
	}
}
