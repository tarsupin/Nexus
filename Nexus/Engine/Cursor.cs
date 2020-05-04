using Microsoft.Xna.Framework.Input;
using Nexus.Gameplay;

namespace Nexus.Engine {

	public static class Cursor {

		public static MouseState mouseState;
		public static MouseState mouseStatePrev;

		public static int MouseX { get { return Cursor.mouseState.X; } }
		public static int MouseY { get { return Cursor.mouseState.Y; } }
		public static ushort MouseGridX { get { return (ushort) Snap.GridFloor((ushort)TilemapEnum.TileWidth, Systems.camera.posX + Cursor.mouseState.X); } }
		public static ushort MouseGridY { get { return (ushort) Snap.GridFloor((ushort)TilemapEnum.TileHeight, Systems.camera.posY + Cursor.mouseState.Y); } }

		public static sbyte GetMouseScrollDelta() {
			int val = Cursor.mouseStatePrev.ScrollWheelValue - Cursor.mouseState.ScrollWheelValue;
			if(val == 0) { return 0; }
			return val > 0 ? (sbyte) 1 : (sbyte) -1;
		}

		public static void SetPos( int posX, int posY ) {
			Mouse.SetPosition(posX, posY);
			Cursor.UpdateMouseState();
		}

		public static void UpdateMouseState() {
			Cursor.mouseStatePrev = Cursor.mouseState;
			Cursor.mouseState = Mouse.GetState();
		}
	}
}
