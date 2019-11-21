using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class UtilityBar : UIComponent {

		private enum UtilityBarEnum : byte {
			BarTiles = 26,
		}

		public UtilityBar( UIComponent parent, short posX, short posY ) : base(parent) {
			this.x = posX;
			this.y = posY;
			this.width = ((byte) TilemapEnum.TileWidth + 2) * (byte) UtilityBarEnum.BarTiles;
			this.height = (byte) TilemapEnum.TileHeight;
		}

		public void RunTick() {
			if(this.IsMouseOver()) {
				UIComponent.hoverComp = this;
			}
		}

		public bool IsMouseOver() {
			int mouseX = Cursor.MouseX;
			int mouseY = Cursor.MouseY;

			if(mouseX < this.x || mouseY > this.x + this.width || mouseY < this.y || mouseY > this.y + this.height) { return false; }
			return true;
		}

		public void Draw() {

			byte tileWidth = (byte) TilemapEnum.TileWidth + 2;

			// Draw Utility Bar Background
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.x, this.y - 2, this.width, this.height + 2), Color.DarkSlateGray);
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.x + 2, this.y, this.width - 2, this.height), Color.White);

			// Tile Outlines
			for(byte i = 0; i <= (byte) UtilityBarEnum.BarTiles; i++) {
				Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.x + i * tileWidth, this.y, 2, this.height), Color.DarkSlateGray);
			}

			// Tile Icons
			Atlas atlas = Systems.mapper.atlas[(byte)AtlasGroup.Tiles];

			for(byte i = 0; i < 10; i++) {

				// Draw Tile
				atlas.Draw(TileTool.tileToolMap[(byte)(i + 1)].DefaultIcon, this.x + i * tileWidth + 2, this.y);

				// Draw Keybind Text
				Systems.fonts.baseText.Draw((i+1).ToString(), this.x + i * tileWidth + 4, this.y + this.height - 18, Color.DarkOrange);
			}

			// Hovering Visual
			if(UIComponent.hoverComp != null) {
				short mx = (short) Snap.GridFloor(tileWidth, Cursor.MouseX - this.x);

				Systems.spriteBatch.Draw(Systems.tex2dDarkRed, new Rectangle(this.x + mx * tileWidth, this.y, tileWidth, this.height), Color.White * 0.5f);
			}
		}
	}
}
