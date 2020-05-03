
namespace Nexus.GameEngine {

	public static class EditorTools {

		public static TileTool tileTool;    // The active Tile Tool being used.
		public static FuncTool funcTool;    // The active Function Tool being used. High priority than TileTool, so it is set to null often.
		public static FuncTool tempTool;    // The highest priority tool; runs because the user is forcing a temporary tool to activate.

		public static void SetTileTool( TileTool tool ) {
			EditorTools.tileTool = tool;
			EditorTools.funcTool = null;
			EditorTools.tempTool = null;

			EditorUI.currentSlotGroup = EditorTools.tileTool.slotGroup;
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
