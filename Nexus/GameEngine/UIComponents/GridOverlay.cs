using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;
using System;

namespace Nexus.GameEngine {

	public class GridOverlay : UIComponent {

		private static Color fadedWhite = Color.White * 0.45f;
		private static Color fadedRed = Color.Red * 0.65f;

		private readonly byte tileWidth;
		private readonly byte tileHeight;

		public GridOverlay( UIComponent parent, byte tileWidth = (byte)TilemapEnum.TileWidth, byte tileHeight = (byte)TilemapEnum.TileHeight ) : base(parent) {
			this.tileWidth = tileWidth;
			this.tileHeight = tileHeight;
		}

		public void DrawGridOverlay( int camX, int camY, short xCount, short yCount ) {

			int left = Math.Max(0, -camX);
			int top = Math.Max(0, -camY);
			int right = Math.Min(Systems.screen.windowWidth, xCount * this.tileWidth - camX);
			int bottom = Math.Min(Systems.screen.windowHeight, yCount * this.tileHeight - camY);

			int offsetX = left == 0 ? -camX % this.tileWidth : left;
			int offsetY = top == 0 ? -camY % this.tileHeight : top;

			// Draw Vertical Lines
			for(int drawX = offsetX - 1; drawX <= right; drawX += this.tileWidth) {
				Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(drawX, top, 3, bottom), GridOverlay.fadedWhite);
			}
			
			// Draw Horizontal Lines
			for(int drawY = offsetY - 1; drawY <= bottom; drawY += this.tileHeight) {
				Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(left, drawY, right, 3), GridOverlay.fadedWhite);
			}

			// Draw Limits
			if(camX <= left) { Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(left - 1, top, 3, bottom), GridOverlay.fadedRed); }
			if(camY <= top) { Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(left, top - 1, right, 3), GridOverlay.fadedRed); }
			if(camX + Systems.screen.windowWidth >= right) { Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(right - 1, top, 3, bottom), GridOverlay.fadedRed); }
			if(camY + Systems.screen.windowHeight >= bottom) { Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(left, bottom - 1, right, 3), GridOverlay.fadedRed); }
		}
	}
}
