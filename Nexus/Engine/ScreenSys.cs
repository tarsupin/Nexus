using Microsoft.Xna.Framework;
using System;

namespace Nexus.Engine {

	public class ScreenSys {

		public GraphicsDeviceManager graphics;
		public short screenWidth;
		public short screenHeight;
		public short windowWidth;
		public short windowHeight;
		public short windowHalfWidth;
		public short windowHalfHeight;

		public ScreenSys(GameClient game) {
			this.graphics = game.graphics;
			this.UpdateSizes();
		}

		public void UpdateSizes() {

			// Screen Size
			this.screenWidth = (short) this.graphics.GraphicsDevice.DisplayMode.Width;
			this.screenHeight = (short) this.graphics.GraphicsDevice.DisplayMode.Height;

			// Window Size
			this.windowWidth = (short) this.graphics.GraphicsDevice.Viewport.Width;
			this.windowHeight = (short) this.graphics.GraphicsDevice.Viewport.Height;

			this.windowHalfWidth = (short) Math.Floor((decimal) (this.windowWidth / 2));
			this.windowHalfHeight = (short) Math.Floor((decimal) (this.windowHeight / 2));
		}

		public void ResizeWindowTo( short width, short height ) {
			graphics.PreferredBackBufferWidth = width;
			graphics.PreferredBackBufferHeight = height;
			//graphics.IsFullScreen = true;
			graphics.ApplyChanges();
			this.UpdateSizes();
		}

		public void ResizeWindowToBestFit() {

			// Common Resolutions
			// 1440x900
			// 1280x720, 1280x800, 1280x1240
			// 1920x1080, 1600x900, 1536x864, 1366x768, 1024x768

			// Max Window Size should be 1440x900. After that, just fill the space as best as possible.
			this.ResizeWindowTo((short) (this.screenWidth >= 1440 ? 1440 : this.screenWidth), (short) (this.screenHeight >= 900 ? 900 : this.screenHeight));
		}
		
		public void ResizeWindowToLargestFit() {
			this.ResizeWindowTo(this.screenWidth, this.screenHeight);
		}

		public void ToggleFullScreen() {
			this.graphics.ToggleFullScreen();
		}

		public void OnResizeWindow(Object sender, EventArgs e) {
			this.UpdateSizes();
		}
	}
}
