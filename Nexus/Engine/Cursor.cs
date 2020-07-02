using Microsoft.Xna.Framework.Input;
using Nexus.Gameplay;

namespace Nexus.Engine {

	public static class Cursor {

		public enum MouseDownState {
			None,
			HeldDown,
			Clicked,
			Released,
		}

		public static MouseState mouseState;
		public static MouseState mouseStatePrev;

		public static int MouseX { get { return Cursor.mouseState.X; } }
		public static int MouseY { get { return Cursor.mouseState.Y; } }

		public static short TileGridX { get { return (short) Snap.GridFloor((short)TilemapEnum.TileWidth, Systems.camera.posX + Cursor.mouseState.X); } }
		public static short TileGridY { get { return (short) Snap.GridFloor((short)TilemapEnum.TileHeight, Systems.camera.posY + Cursor.mouseState.Y); } }

		public static short MiniGridX { get { return (short) Snap.GridFloor((short)WorldmapEnum.TileWidth, Systems.camera.posX + Cursor.mouseState.X); } }
		public static short MiniGridY { get { return (short) Snap.GridFloor((short)WorldmapEnum.TileHeight, Systems.camera.posY + Cursor.mouseState.Y); } }

		public static MouseDownState LeftMouseState;		// NOTE: Can use mouseState.LeftButton if you only need ON/OFF.
		public static MouseDownState RightMouseState;       // NOTE: Can use mouseState.RightButton if you only need ON/OFF.

		public static sbyte MouseScrollDelta;

		public static void SetPos( int posX, int posY ) {
			Mouse.SetPosition(posX, posY);
			Cursor.UpdateMouseState(); // Update mouse state, even if it happens on same frame.
		}

		public static void UpdateMouseState() {
			if(!Systems.game.IsMouseVisible) { return; }

			Cursor.mouseStatePrev = Cursor.mouseState;
			Cursor.mouseState = Mouse.GetState();

			// Get Mouse Scroll Delta
			int val = Cursor.mouseStatePrev.ScrollWheelValue - Cursor.mouseState.ScrollWheelValue;
			if(val == 0) { Cursor.MouseScrollDelta = 0; }
			else { Cursor.MouseScrollDelta = val > 0 ? (sbyte)1 : (sbyte)-1; }

			// If the Left Button is held down:
			if(Cursor.mouseState.LeftButton == ButtonState.Released) {
				Cursor.LeftMouseState = (Cursor.mouseStatePrev.LeftButton == ButtonState.Pressed) ? MouseDownState.Released : MouseDownState.None;
			} else {
				Cursor.LeftMouseState = (Cursor.mouseStatePrev.LeftButton == ButtonState.Pressed) ? MouseDownState.HeldDown : MouseDownState.Clicked;
			}

			// If the Right Button is held down:
			if(Cursor.mouseState.RightButton == ButtonState.Released) {
				Cursor.RightMouseState = (Cursor.mouseStatePrev.RightButton == ButtonState.Pressed) ? MouseDownState.Released : MouseDownState.None;
			} else {
				Cursor.RightMouseState = (Cursor.mouseStatePrev.RightButton == ButtonState.Pressed) ? MouseDownState.HeldDown : MouseDownState.Clicked;
			}
		}
	}
}
