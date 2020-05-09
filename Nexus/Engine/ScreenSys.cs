using Microsoft.Xna.Framework;
using System;

namespace Nexus.Engine {

	public class ScreenSys {

		public GraphicsDeviceManager graphics;
		public ushort screenWidth;
		public ushort screenHeight;
		public ushort windowWidth;
		public ushort windowHeight;
		public ushort windowHalfWidth;

		public ScreenSys(GameClient game) {
			this.graphics = game.graphics;
			this.UpdateSizes();
		}

		public void UpdateSizes() {

			// Screen Size
			this.screenWidth = (ushort) this.graphics.GraphicsDevice.DisplayMode.Width;
			this.screenHeight = (ushort) this.graphics.GraphicsDevice.DisplayMode.Height;

			// Window Size
			this.windowWidth = (ushort) this.graphics.GraphicsDevice.Viewport.Width;
			this.windowHeight = (ushort) this.graphics.GraphicsDevice.Viewport.Height;

			this.windowHalfWidth = (ushort) Math.Floor((decimal) (this.windowWidth / 2));
		}

		public void ResizeWindowTo( ushort width, ushort height ) {
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
			this.ResizeWindowTo((ushort) (this.screenWidth >= 1440 ? 1440 : this.screenWidth), (ushort) (this.screenHeight >= 900 ? 900 : this.screenHeight));
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
