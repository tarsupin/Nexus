using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nexus.Engine;
using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class GridOverlay : UIComponent {

		private readonly byte gridXCount;
		private readonly byte gridYCount;

		public GridOverlay( UIComponent parent ) : base(parent) {
			this.gridXCount = 30;
			this.gridYCount = 19;
		}

		public void Draw( int posX, int posY ) {

			Color fadedWhite = Color.White * 0.45f;

			// Draw Vertical Lines
			for(int gridX = 0; gridX <= this.gridXCount; gridX++) {
				Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(posX - 1 + gridX * (byte)TilemapEnum.TileWidth, 0, 3, Systems.screen.windowHeight), fadedWhite);
			}

			// Draw Horizontal Lines
			for(int gridY = 0; gridY <= this.gridYCount; gridY++) {
				Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(0, posY - 1 + gridY * (byte)TilemapEnum.TileHeight, Systems.screen.windowWidth, 3), fadedWhite);
			}
		}
	}
}
