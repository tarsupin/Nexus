﻿using Microsoft.Xna.Framework.Input;
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
		public static byte MouseGridX { get { return (byte) Snap.GridFloor((byte)TilemapEnum.TileWidth, Systems.camera.posX + Cursor.mouseState.X); } }
		public static byte MouseGridY { get { return (byte) Snap.GridFloor((byte)TilemapEnum.TileHeight, Systems.camera.posY + Cursor.mouseState.Y); } }

		public static MouseDownState LeftMouseState;		// NOTE: Can use mouseState.LeftButton if you only need ON/OFF.
		public static MouseDownState RightMouseState;		// NOTE: Can use mouseState.RightButton if you only need ON/OFF.

		public static sbyte GetMouseScrollDelta() {
			int val = Cursor.mouseStatePrev.ScrollWheelValue - Cursor.mouseState.ScrollWheelValue;
			if(val == 0) { return 0; }
			return val > 0 ? (sbyte) 1 : (sbyte) -1;
		}

		public static void SetPos( int posX, int posY ) {
			Mouse.SetPosition(posX, posY);
			Cursor.UpdateMouseState(); // Update mouse state, even if it happens on same frame.
		}

		public static void UpdateMouseState() {
			Cursor.mouseStatePrev = Cursor.mouseState;
			Cursor.mouseState = Mouse.GetState();

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
