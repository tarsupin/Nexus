using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.GameEngine {

	public static class EditorTools {

		public static TileTool tileTool;		// The active Tile Tool being used.
		public static FuncTool funcTool;		// The active Function Tool being used. Higher priority than TileTool, so it is set to null often.
		public static FuncTool tempTool;        // The highest priority tool; runs because the user is forcing a temporary tool to activate.

		// The AutoTile tool. Always loaded, but can be enabled or disabled. Higher priority than TileTool, but used in sync with it.
		public static AutoTileTool autoTool = new AutoTileTool();

		public static void SetTileTool( TileTool tool, byte index = 0 ) {
			EditorTools.tileTool = tool;
			EditorTools.funcTool = null;
			EditorTools.tempTool = null;
			EditorTools.autoTool.ClearAutoTiles();

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

		public static void StartAutoTool(short gridX, short gridY) {

			// Can only set an AutoTile tool if a TileTool is also active.
			if(EditorTools.tileTool == null) { return; }

			EditorPlaceholder ph = EditorTools.tileTool.CurrentPlaceholder;

			EditorTools.autoTool.StartAutoTile(ph.tileId, ph.subType, ph.layerEnum, gridX, gridY);

			// Only disable other tools if the AutoTile tool started.
			if(EditorTools.autoTool.IsActive) {
				EditorTools.funcTool = null;
				EditorTools.tempTool = null;
			}
		}

		public static void SetFuncTool( FuncTool tool ) {
			EditorTools.funcTool = tool;
			EditorTools.tileTool = null;
			EditorTools.tempTool = null;
			EditorTools.autoTool.ClearAutoTiles();

			// Update Helper Text (if applicable)
			EditorTools.UpdateHelperText();
		}

		public static void SetTempTool( FuncTool tool ) {
			EditorTools.tempTool = tool;
			EditorTools.autoTool.ClearAutoTiles();

			// Update Helper Text (if applicable)
			EditorTools.UpdateHelperText();
		}

		public static void ClearTempTool() {
			if(EditorTools.tempTool != null) {
				EditorTools.tempTool = null;
			}
		}

		public static void UpdateHelperText() {
			if(Systems.scene is EditorScene == false) { return; }
			EditorScene editorScene = (EditorScene) Systems.scene;

			// Tile Tool Helper Text
			if(EditorTools.tileTool != null) {
				EditorPlaceholder ph = EditorTools.tileTool.CurrentPlaceholder;

				// Display Tile Helper Text
				if(ph.tileId > 0) {

					// If there's an error here, we need to add the value to TileDict.
					TileObject tile = Systems.mapper.TileDict[ph.tileId];

					if(tile.titles != null) {
						editorScene.editorUI.statusText.SetText(tile.titles[ph.subType], tile.descriptions[ph.subType]);
						return;
					}

					else if(tile.title.Length > 0) {
						editorScene.editorUI.statusText.SetText(tile.title, tile.description);
						return;
					}
				}

				// Display Object Helper Text
				if(ph.objectId > 0) {
					string title = ShadowTile.ObjHelpText[ph.objectId][ph.subType][0];
					string desc = ShadowTile.ObjHelpText[ph.objectId][ph.subType][1];

					editorScene.editorUI.statusText.SetText(title, desc);
					return;
				}
			}

			editorScene.editorUI.statusText.ClearStatus();
		}
	}
}
