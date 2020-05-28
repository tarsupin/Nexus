using Nexus.Engine;
using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public static class WETools {

		public static WETileTool WETileTool;		// The active Tile Tool being used.
		public static WEFuncTool WEFuncTool;		// The active Function Tool being used. Higher priority than WorldTileTool, so it is set to null often.
		public static WEFuncTool WETempTool;		// The highest priority tool; runs because the user is forcing a temporary tool to activate.

		// The AutoTile tool. Always loaded, but can be enabled or disabled. Higher priority than WorldTileTool, but used in sync with it.
		public static AutoWorldTool autoTool = new AutoWorldTool();

		public static void SetWorldTileTool( WETileTool tool, byte index = 0 ) {
			WETools.WETileTool = tool;
			WETools.WEFuncTool = null;
			WETools.WETempTool = null;
			WETools.autoTool.ClearAutoTiles();

			WE_UI.curWESlotGroup = WETools.WETileTool.slotGroup;

			// Assign Index and SubIndex to WorldTileTool (if applicable)
			WETools.WETileTool.SetIndex(index);

			// Update Helper Text (if applicable)
			WETools.UpdateHelperText();
		}

		public static void SetWorldTileToolBySlotGroup(byte slotGroup, byte index = 0) {

			// If the current slot group is being changed:
			if(WETools.WETileTool == null || WETools.WETileTool.slotGroup != slotGroup) {
				if(WETileTool.WorldTileToolMap.ContainsKey(slotGroup)) {
					WETileTool tool = WETileTool.WorldTileToolMap[slotGroup];
					if(tool == null) { return; }
					WETools.SetWorldTileTool(tool, tool.index);
				}
			}

			// If the current slot group is the same, need to change the index only.
			else {
				WETileTool tool = WETools.WETileTool;
				WETools.SetWorldTileTool(tool, index);
			}
		}

		public static void StartAutoTool(ushort gridX, ushort gridY) {

			// Can only set an AutoTile tool if a WorldTileTool is also active.
			if(WETools.WETileTool == null) { return; }

			WEPlaceholder ph = WETools.WETileTool.CurrentPlaceholder;

			WETools.autoTool.StartAutoTile(ph.tBase, ph.tCat, gridX, gridY);

			// Only disable other tools if the AutoTile tool started.
			if(WETools.autoTool.IsActive) {
				WETools.WEFuncTool = null;
				WETools.WETempTool = null;
			}
		}

		public static void SetWorldFuncTool( WEFuncTool tool ) {
			WETools.WEFuncTool = tool;
			WETools.WETileTool = null;
			WETools.WETempTool = null;
			WETools.autoTool.ClearAutoTiles();

			// Update Helper Text (if applicable)
			WETools.UpdateHelperText();
		}

		public static void SetWorldTempTool( WEFuncTool tool ) {
			WETools.WETempTool = tool;
			WETools.autoTool.ClearAutoTiles();

			// Update Helper Text (if applicable)
			WETools.UpdateHelperText();
		}

		public static void ClearWorldTempTool() {
			if(WETools.WETempTool != null) {
				WETools.WETempTool = null;

				// Update Helper Text (if applicable)
				WETools.UpdateHelperText();
			}
		}

		public static void UpdateHelperText() {
			if(Systems.scene is WEScene == false) { return; }
			WEScene WEScene = (WEScene) Systems.scene;

			// Function Tool Helper Text
			WEFuncTool tool = WETools.WETempTool != null ? WETools.WETempTool : WETools.WEFuncTool;

			if(tool != null) {
				WEScene.weUI.SetHelperText(tool.title, tool.description);
				return;
			}

			// Tile Tool Helper Text
			if(WETools.WETileTool != null) {
				WEPlaceholder ph = WETools.WETileTool.CurrentPlaceholder;

				//if(ph.tBase > 0) {
				//	TileObject tile = Systems.mapper.TileDict[ph.tBase];

				//	if(tile.titles != null) {
				//		WorldEditorScene.worldEditorUI.SetHelperText(tile.titles[ph.tCat], tile.descriptions[ph.tCat]);
				//		return;
				//	}

				//	else if(tile.title.Length > 0) {
				//		WorldEditorScene.worldEditorUI.SetHelperText(tile.title, tile.description);
				//		return;
				//	}
				//}
			}

			WEScene.weUI.SetHelperText("", "");
		}
	}
}
