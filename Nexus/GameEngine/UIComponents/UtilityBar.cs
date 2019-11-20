using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class UtilityBar : UIComponent {

		private enum UtilityBarEnum : byte {
			BarTiles = 26,
		}

		public UtilityBar( UIComponent parent ) : base(parent) {

		}

		public void Draw(int posX, int posY) {

			byte tileWidth = (byte) TilemapEnum.TileWidth + 2;

			// Draw Utility Bar Background
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(posX, posY - 2, tileWidth * (byte)UtilityBarEnum.BarTiles, (byte)TilemapEnum.TileHeight + 2), Color.DarkSlateGray);
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(posX + 2, posY, tileWidth * (byte) UtilityBarEnum.BarTiles - 2, (byte) TilemapEnum.TileHeight), Color.White);

			// Grid Outline
			for(byte i = 0; i <= (byte) UtilityBarEnum.BarTiles; i++) {
				Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(posX + i * tileWidth, posY, 2, (byte)TilemapEnum.TileHeight), Color.DarkSlateGray);
			}
		}
	}
}
