using Microsoft.Xna.Framework.Input;
using Nexus.Gameplay;

namespace Nexus.Engine {

	public static class Cursor {

		public static MouseState mouseState;

		public static int MouseX { get { return Cursor.mouseState.X; } }
		public static int MouseY { get { return Cursor.mouseState.Y; } }
		public static ushort MouseGridX { get { return (ushort) Snap.GridFloor((ushort)TilemapEnum.TileWidth, Systems.camera.posX + Cursor.mouseState.X); } }
		public static ushort MouseGridY { get { return (ushort) Snap.GridFloor((ushort)TilemapEnum.TileHeight, Systems.camera.posY + Cursor.mouseState.Y); } }

		public static void UpdateMouseState() {
			Cursor.mouseState = Mouse.GetState();
		}
	}
}
