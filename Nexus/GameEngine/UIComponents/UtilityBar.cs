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

			// Tile Outlines
			for(byte i = 0; i <= (byte) UtilityBarEnum.BarTiles; i++) {
				Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(posX + i * tileWidth, posY, 2, (byte)TilemapEnum.TileHeight), Color.DarkSlateGray);
			}

			// Tile Icons
			Atlas atlas = Systems.mapper.atlas[(byte)AtlasGroup.Tiles];

			for(byte i = 0; i < 10; i++) {

				// Draw Tile
				atlas.Draw(TileTool.tileToolMap[(byte)(i + 1)].DefaultIcon, posX + i * tileWidth + 2, posY);

				// Draw Keybind Text
				Systems.fonts.baseText.Draw((i+1).ToString(), posX + i * tileWidth + 4, posY + (byte) TilemapEnum.TileHeight - 18, Color.DarkOrange);
			}
		}
	}
}
