using Nexus.Engine;

namespace Nexus.GameEngine {

	public static class EditorTools {

		public static TileTool tileTool;    // The active Tile Tool being used.
		public static FuncTool funcTool;    // The active Function Tool being used. High priority than TileTool, so it is set to null often.
		public static FuncTool tempTool;    // The highest priority tool; runs because the user is forcing a temporary tool to activate.

		public static void SetTileTool( TileTool tool, byte index = 0 ) {
			EditorTools.tileTool = tool;
			EditorTools.funcTool = null;
			EditorTools.tempTool = null;

			EditorUI.currentSlotGroup = EditorTools.tileTool.slotGroup;

			// Assign Index and SubIndex to TileTool (if applicable)
			EditorTools.tileTool.SetIndex(index);

			// Update Helper Text (if applicable)
			EditorTools.UpdateHelperText();
		}

		public static void SetTileToolBySlotGroup(byte slotGroup, byte index = 0) {

			// If the current slot group is being changed:
			if(EditorTools.tileTool == null || EditorTools.tileTool.slotGroup != slotGroup) {
				if(TileTool.tileToolMap.ContainsKey(slotGroup)) {
					TileTool tool = TileTool.tileToolMap[slotGroup];
					if(tool == null) { return; }
					EditorTools.SetTileTool(tool, tool.index);
				}
			}

			// If the current slot group is the same, need to change the index only.
			else {
				TileTool tool = EditorTools.tileTool;
				EditorTools.SetTileTool(tool, index);
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

					if(tile.titles != null) {
						editorScene.editorUI.SetHelperText(tile.titles[ph.subType], tile.descriptions[ph.subType]);
						return;
					}

					else if(tile.title.Length > 0) {
						editorScene.editorUI.SetHelperText(tile.title, tile.description);
						return;
					}
				}
			}

			editorScene.editorUI.SetHelperText("", "");
		}
	}
}
