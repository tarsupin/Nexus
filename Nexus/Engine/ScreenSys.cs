using Microsoft.Xna.Framework;
using System;

namespace Nexus.Engine {

	public class ScreenSys {

		public GraphicsDeviceManager graphics;
		public readonly short width;
		public readonly short height;
		public short windowWidth;
		public short windowHeight;

		public ScreenSys(GameClient game) {
			this.graphics = game.graphics;

			// Screen Size
			this.width = (short)this.graphics.GraphicsDevice.DisplayMode.Width;
			this.height = (short)this.graphics.GraphicsDevice.DisplayMode.Height;

			// Window Size
			this.windowWidth = (short) this.graphics.GraphicsDevice.Viewport.Width;
			this.windowHeight = (short) this.graphics.GraphicsDevice.Viewport.Height;
		}

		public void ToggleFullScreen() {
			this.graphics.ToggleFullScreen();
		}

		public void OnResizeWindow(Object sender, EventArgs e) {
			this.windowWidth = (short) this.graphics.GraphicsDevice.Viewport.Width;
			this.windowHeight = (short) this.graphics.GraphicsDevice.Viewport.Height;
		}
	}
}
