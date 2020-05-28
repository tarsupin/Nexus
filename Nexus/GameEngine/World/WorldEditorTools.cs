using Nexus.Engine;
using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public static class WorldEditorTools {

		public static WorldTileTool WorldTileTool;	// The active Tile Tool being used.
		public static WorldFuncTool WorldFuncTool;			// The active Function Tool being used. Higher priority than WorldTileTool, so it is set to null often.
		public static WorldFuncTool WorldTempTool;			// The highest priority tool; runs because the user is forcing a temporary tool to activate.

		// The AutoTile tool. Always loaded, but can be enabled or disabled. Higher priority than WorldTileTool, but used in sync with it.
		public static AutoWorldTool autoTool = new AutoWorldTool();

		public static void SetWorldTileTool( WorldTileTool tool, byte index = 0 ) {
			WorldEditorTools.WorldTileTool = tool;
			WorldEditorTools.WorldFuncTool = null;
			WorldEditorTools.WorldTempTool = null;
			WorldEditorTools.autoTool.ClearAutoTiles();

			EditorUI.currentSlotGroup = WorldEditorTools.WorldTileTool.slotGroup;

			// Assign Index and SubIndex to WorldTileTool (if applicable)
			WorldEditorTools.WorldTileTool.SetIndex(index);

			// Update Helper Text (if applicable)
			WorldEditorTools.UpdateHelperText();
		}

		public static void SetWorldTileToolBySlotGroup(byte slotGroup, byte index = 0) {

			// If the current slot group is being changed:
			if(WorldEditorTools.WorldTileTool == null || WorldEditorTools.WorldTileTool.slotGroup != slotGroup) {
				if(WorldTileTool.WorldTileToolMap.ContainsKey(slotGroup)) {
					WorldTileTool tool = WorldTileTool.WorldTileToolMap[slotGroup];
					if(tool == null) { return; }
					WorldEditorTools.SetWorldTileTool(tool, tool.index);
				}
			}

			// If the current slot group is the same, need to change the index only.
			else {
				WorldTileTool tool = WorldEditorTools.WorldTileTool;
				WorldEditorTools.SetWorldTileTool(tool, index);
			}
		}

		public static void StartAutoTool(ushort gridX, ushort gridY) {

			// Can only set an AutoTile tool if a WorldTileTool is also active.
			if(WorldEditorTools.WorldTileTool == null) { return; }

			WEPlaceholder ph = WorldEditorTools.WorldTileTool.CurrentPlaceholder;

			WorldEditorTools.autoTool.StartAutoTile(ph.tBase, ph.tCat, gridX, gridY);

			// Only disable other tools if the AutoTile tool started.
			if(WorldEditorTools.autoTool.IsActive) {
				WorldEditorTools.WorldFuncTool = null;
				WorldEditorTools.WorldTempTool = null;
			}
		}

		public static void SetWorldFuncTool( WorldFuncTool tool ) {
			WorldEditorTools.WorldFuncTool = tool;
			WorldEditorTools.WorldTileTool = null;
			WorldEditorTools.WorldTempTool = null;
			WorldEditorTools.autoTool.ClearAutoTiles();

			// Update Helper Text (if applicable)
			WorldEditorTools.UpdateHelperText();
		}

		public static void SetWorldTempTool( WorldFuncTool tool ) {
			WorldEditorTools.WorldTempTool = tool;
			WorldEditorTools.autoTool.ClearAutoTiles();

			// Update Helper Text (if applicable)
			WorldEditorTools.UpdateHelperText();
		}

		public static void ClearWorldTempTool() {
			if(WorldEditorTools.WorldTempTool != null) {
				WorldEditorTools.WorldTempTool = null;

				// Update Helper Text (if applicable)
				WorldEditorTools.UpdateHelperText();
			}
		}

		public static void UpdateHelperText() {
			if(Systems.scene is EditorScene == false) { return; }
			EditorScene editorScene = (EditorScene) Systems.scene;

			// Function Tool Helper Text
			WorldFuncTool tool = WorldEditorTools.WorldTempTool != null ? WorldEditorTools.WorldTempTool : WorldEditorTools.WorldFuncTool;

			if(tool != null) {
				editorScene.editorUI.SetHelperText(tool.title, tool.description);
				return;
			}

			// Tile Tool Helper Text
			if(WorldEditorTools.WorldTileTool != null) {
				WEPlaceholder ph = WorldEditorTools.WorldTileTool.CurrentPlaceholder;

				if(ph.tBase > 0) {
					TileObject tile = Systems.mapper.TileDict[ph.tBase];

					if(tile.titles != null) {
						editorScene.editorUI.SetHelperText(tile.titles[ph.tCat], tile.descriptions[ph.tCat]);
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
