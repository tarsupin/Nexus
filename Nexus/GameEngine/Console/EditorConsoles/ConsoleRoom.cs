
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
			ConsoleTrack.possibleTabs = "Example: `resize hor 250`";
			ConsoleTrack.helpText = "Resize the level's width, in tiles. Minimum width is 30.";

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
			ConsoleTrack.possibleTabs = "Example: `resize vert 180`";
			ConsoleTrack.helpText = "Resize the level's height, in tiles. Minimum height is 19.";

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
			ConsoleTrack.possibleTabs = "Example: `resize custom 100 35`";
			ConsoleTrack.helpText = "Resize a room to be a custom size. Add width and height (in that order).";

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
