using Microsoft.Xna.Framework;
using System;

namespace Nexus.Engine {

	public class ScreenSys {

		public const short VirtualWidth = 1440;
		public const short VirtualHeight = 900;

		public GraphicsDeviceManager graphics;
		public short screenWidth;
		public short screenHeight;
		public short viewWidth;
		public short viewHeight;
		public short viewHalfWidth;
		public short viewHalfHeight;

		public Rectangle destRender;
		public short offsetX = 0;
		public short offsetY = 0;
		public float aspectX = 1;
		public float aspectY = 1;

		public ScreenSys(GameClient game) {
			this.graphics = game.graphics;

			// Fixed Window Size
			this.viewWidth = (short) ScreenSys.VirtualWidth;
			this.viewHeight = (short) ScreenSys.VirtualHeight;

			this.viewHalfWidth = (short)Math.Floor((double)(ScreenSys.VirtualWidth / 2));
			this.viewHalfHeight = (short)Math.Floor((double)(ScreenSys.VirtualHeight / 2));

			this.UpdateSizes();
		}

		public void UpdateSizes() {

			// Screen Size
			this.screenWidth = (short)this.graphics.GraphicsDevice.DisplayMode.Width;
			this.screenHeight = (short)this.graphics.GraphicsDevice.DisplayMode.Height;

			// Prepare Destination Render Target
			Rectangle windowBounds = Systems.game.Window.ClientBounds;

			float outputAspect = windowBounds.Width / (float)windowBounds.Height;
			float preferredAspect = this.screenWidth / (float)this.screenHeight;
			//float preferredAspect = 1.6f;

			// Output is Tall (Bars on top/bottom)
			if(outputAspect <= preferredAspect) {
				int presentHeight = (int)((windowBounds.Width / preferredAspect) + 0.5f);
				this.offsetY = (short)((windowBounds.Height - presentHeight) / 2);
				//this.aspectX = 1f;
				//this.aspectY = (presentHeight + this.offsetY * 2) / (float)presentHeight;
				this.aspectX = (float) ScreenSys.VirtualWidth / (float) windowBounds.Width;
				this.aspectY = (float )ScreenSys.VirtualHeight / (float) presentHeight;
				this.destRender = new Rectangle(0, this.offsetY, windowBounds.Width, presentHeight);
			}

			// Output is Wide (Bars on left/right)
			else
			{
				int presentWidth = (int)((windowBounds.Height * preferredAspect) + 0.5f);
				this.offsetX = (short)((windowBounds.Width - presentWidth) / 2);
				//this.aspectX = (presentWidth + this.offsetX * 2) / (float) presentWidth;
				//this.aspectY = 1f;
				this.aspectX = (float) ScreenSys.VirtualWidth / (float) presentWidth;
				this.aspectY = (float) ScreenSys.VirtualHeight / (float) windowBounds.Height;
				this.destRender = new Rectangle(this.offsetX, 0, presentWidth, windowBounds.Height);
			}
		}

		public void ResizeWindowTo( short width, short height ) {
			Systems.game.Window.IsBorderless = false;
			Systems.game.Window.AllowAltF4 = true;
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
			this.ResizeWindowTo((short)(this.screenWidth >= ScreenSys.VirtualWidth ? ScreenSys.VirtualWidth : this.screenWidth), (short)(this.screenHeight >= ScreenSys.VirtualHeight ? ScreenSys.VirtualHeight : this.screenHeight));
		}
		
		public void ResizeWindowToLargestFit() {
			Systems.game.Window.Position = new Point(0, 0);
			Systems.game.Window.IsBorderless = true;
			Systems.game.Window.AllowAltF4 = true;
			graphics.PreferredBackBufferWidth = this.screenWidth;
			graphics.PreferredBackBufferHeight = this.screenHeight;
			//graphics.IsFullScreen = true;
			graphics.ApplyChanges();
			this.UpdateSizes();
		}

		public void OnResizeWindow(Object sender, EventArgs e) {
			this.UpdateSizes();
		}
	}
}
