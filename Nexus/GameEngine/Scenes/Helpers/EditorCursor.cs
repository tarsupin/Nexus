using Microsoft.Xna.Framework.Input;
using Nexus.Engine;
using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public static class EditorCursor {

		public static TileTool tileTool;    // The active Tile Tool being used.
		public static FuncTool funcTool;    // The active Function Tool being used. High priority than TileTool, so it is set to null often.
		public static FuncTool tempTool;    // The highest priority tool; runs because the user is forcing a temporary tool to activate.

		public static MouseState mouseState;

		public static int MouseX { get { return EditorCursor.mouseState.X; } }
		public static int MouseY { get { return EditorCursor.mouseState.Y; } }
		public static ushort MouseGridX { get { return (ushort) Snap.GridFloor((ushort)TilemapEnum.TileWidth, Systems.camera.posX + EditorCursor.mouseState.X); } }
		public static ushort MouseGridY { get { return (ushort) Snap.GridFloor((ushort)TilemapEnum.TileHeight, Systems.camera.posY + EditorCursor.mouseState.Y); } }

		public static void SetTileTool( TileTool tool ) {
			EditorCursor.tileTool = tool;
			EditorCursor.funcTool = null;
			EditorCursor.tempTool = null;
		}

		public static void SetFuncTool( FuncTool tool ) {
			EditorCursor.funcTool = tool;
			EditorCursor.tempTool = null;
		}

		public static void SetTempTool( FuncTool tool ) {
			EditorCursor.tempTool = tool;
		}

		public static void UpdateMouseState() {
			EditorCursor.mouseState = Mouse.GetState();
		}
	}
}
