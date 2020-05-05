
using System.Collections.Generic;

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

			// Index must be <= the number of placeholders available:
			List<EditorPlaceholder[]> placeholders = tool.placeholders;
			if(placeholders.Count < index) { index = 0; }

			EditorTools.tileTool.index = index;

			// SubIndex must be legal option.
			byte phLen = (byte) placeholders[index].Length;
			if(phLen < subIndex) { subIndex = 0; }

			EditorTools.tileTool.subIndex = subIndex;
		}

		public static void SetTileToolBySlotGroup(byte slotGroup, byte index = 0) {
			TileTool tool = TileTool.tileToolMap[slotGroup];
			if(tool == null) { return; }
			EditorTools.SetTileTool(tool, index);
		}

		public static void SetFuncTool( FuncTool tool ) {
			EditorTools.funcTool = tool;
			EditorTools.tempTool = null;
		}

		public static void SetTempTool( FuncTool tool ) {
			EditorTools.tempTool = tool;
		}
	}
}
