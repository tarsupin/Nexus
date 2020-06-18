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

			int offsetX = -camX % this.tileWidth;
			int offsetY = -camY % this.tileHeight;

			int right = Math.Min(Systems.screen.windowWidth, xCount * this.tileWidth - camX);
			int bottom = Math.Min(Systems.screen.windowHeight, yCount * this.tileHeight - camY);

			// Draw Vertical Lines
			for(int drawX = offsetX - 1; drawX <= right; drawX += this.tileWidth) {
				Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(drawX, 0, 3, bottom), GridOverlay.fadedWhite);
			}

			// Draw Horizontal Lines
			for(int drawY = offsetY - 1; drawY <= bottom; drawY += this.tileHeight) {
				Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(0, drawY, right, 3), GridOverlay.fadedWhite);
			}

			// Draw Limits
			//Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(0, 0, 3, bottom), GridOverlay.fadedRed);
			//Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(0, 0, right, 3), GridOverlay.fadedRed);
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(right - 1, 0, 3, bottom), GridOverlay.fadedRed);
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(0, bottom - 1, right, 3), GridOverlay.fadedRed);
		}
	}
}
