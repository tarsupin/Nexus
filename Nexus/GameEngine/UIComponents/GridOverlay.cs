using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class GridOverlay : UIComponent {

		private readonly byte gridXCount;
		private readonly byte gridYCount;
		private readonly byte tileWidth;
		private readonly byte tileHeight;

		public GridOverlay( UIComponent parent, byte gridXCount = 30, byte gridYCount = 19, byte tileWidth = (byte)TilemapEnum.TileWidth, byte tileHeight = (byte)TilemapEnum.TileHeight ) : base(parent) {
			this.gridXCount = gridXCount;
			this.gridYCount = gridYCount;
			this.tileWidth = tileWidth;
			this.tileHeight = tileHeight;
		}

		public void Draw( int posX, int posY ) {

			Color fadedWhite = Color.White * 0.45f;

			// Draw Vertical Lines
			for(int gridX = 0; gridX <= this.gridXCount; gridX++) {
				Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(posX - 1 + gridX * (byte)this.tileWidth, 0, 3, Systems.screen.windowHeight), fadedWhite);
			}

			// Draw Horizontal Lines
			for(int gridY = 0; gridY <= this.gridYCount; gridY++) {
				Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(0, posY - 1 + gridY * (byte)this.tileHeight, Systems.screen.windowWidth, 3), fadedWhite);
			}
		}
	}
}
