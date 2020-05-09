using Nexus.Engine;

namespace Nexus.GameEngine {

	public static class EditorTools {

		public static TileTool tileTool;    // The active Tile Tool being used.
		public static FuncTool funcTool;    // The active Function Tool being used. High priority than TileTool, so it is set to null often.
		public static FuncTool tempTool;    // The highest priority tool; runs because the user is forcing a temporary tool to activate.

		public static void SetTileTool( TileTool tool, byte index = 0, byte subIndex = 0 ) {
			EditorTools.tileTool = tool;
			EditorTools.funcTool = null;
			EditorTools.tempTool = null;

			EditorUI.currentSlotGroup = EditorTools.tileTool.slotGroup;

			// Assign Index and SubIndex to TileTool (if applicable)
			EditorTools.tileTool.SetIndex(index);
			EditorTools.tileTool.SetSubIndex(subIndex);

			// Update Helper Text (if applicable)
			EditorTools.UpdateHelperText();
		}

		public static void SetTileToolBySlotGroup(byte slotGroup, byte index = 0) {

			// If the current slot group is being changed:
			if(EditorTools.tileTool == null || EditorTools.tileTool.slotGroup != slotGroup) {
				TileTool tool = TileTool.tileToolMap[slotGroup];
				if(tool == null) { return; }
				EditorTools.SetTileTool(tool, tool.index, tool.subIndex);
			}

			// If the current slot group is the same:
			else {
				EditorTools.SetTileTool(EditorTools.tileTool, index);
			}
		}

		public static void SetFuncTool( FuncTool tool ) {
			EditorTools.funcTool = tool;
			EditorTools.tempTool = null;

			// Update Helper Text (if applicable)
			EditorTools.UpdateHelperText();
		}

		public static void SetTempTool( FuncTool tool ) {
			EditorTools.tempTool = tool;

			// Update Helper Text (if applicable)
			EditorTools.UpdateHelperText();
		}

		public static void UpdateHelperText() {
			EditorScene editorScene = (EditorScene) Systems.scene;

			// Tile Tool Helper Text
			if(EditorTools.tileTool != null) {
				EditorPlaceholder ph = EditorTools.tileTool.CurrentPlaceholder;

				if(ph.tileId > 0) {
					TileGameObject tile = Systems.mapper.TileDict[ph.tileId];
					editorScene.editorUI.SetHelperText(tile.tileId.ToString(), ph.subType.ToString());
				} else {
					editorScene.editorUI.SetHelperText("", "");
				}

				return;
			}

			editorScene.editorUI.SetHelperText("", "");
		}
	}
}
