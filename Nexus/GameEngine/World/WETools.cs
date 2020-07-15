using Nexus.Engine;

namespace Nexus.GameEngine {

	public static class WETools {

		public static WETileTool WETileTool;		// The active Tile Tool being used.
		public static WEFuncTool WEFuncTool;		// The active Function Tool being used. Higher priority than WorldTileTool, so it is set to null often.
		public static WEFuncTool WETempTool;		// The highest priority tool; runs because the user is forcing a temporary tool to activate.

		public static void SetWorldTileTool( WETileTool tool, byte index = 0 ) {
			WETools.WETileTool = tool;
			WETools.WEFuncTool = null;
			WETools.WETempTool = null;

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

		public static void SetWorldFuncTool( WEFuncTool tool ) {
			WETools.WEFuncTool = tool;
			WETools.WETileTool = null;
			WETools.WETempTool = null;

			// Update Helper Text (if applicable)
			WETools.UpdateHelperText();
		}

		public static void SetWorldTempTool( WEFuncTool tool ) {
			WETools.WETempTool = tool;

			// Update Helper Text (if applicable)
			WETools.UpdateHelperText();
		}

		public static void ClearWorldTempTool() {
			if(WETools.WETempTool != null) {
				WETools.WETempTool = null;
			}
		}

		public static void UpdateHelperText() {
			if(Systems.scene is WEScene == false) { return; }
			WEScene WEScene = (WEScene) Systems.scene;

			// Tile Tool Helper Text
			if(WETools.WETileTool != null) {
				WEPlaceholder ph = WETools.WETileTool.CurrentPlaceholder;

				if(ph.obj > 0) {

					// Object ID
					if(WEShadowTile.HelpText.ContainsKey(ph.obj)) {
						string[] help = WEShadowTile.HelpText[ph.obj];
						WEScene.weUI.statusText.SetText(help[0], help[1]);
						return;
					}
				}
			}

			WEScene.weUI.statusText.ClearStatus();
		}
	}
}
