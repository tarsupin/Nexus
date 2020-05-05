using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nexus.Engine;
using Nexus.Gameplay;
using System.Collections.Generic;

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
				UIComponent.ComponentWithFocus = this;

				if(Cursor.mouseState.LeftButton == ButtonState.Pressed) {
					byte barTileNum = this.GetBarIndex(Cursor.MouseX);
					EditorTools.SetTileToolBySlotGroup(EditorUI.currentSlotGroup, barTileNum);
				}
			}
		}

		public byte GetBarIndex(int posX) {
			byte tileWidth = ((byte) TilemapEnum.TileWidth + 2);
			short offsetX = (short) (posX - this.x);
			byte index = (byte) System.Math.Floor((decimal) (offsetX / tileWidth));
			return index;
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
			if(TileTool.tileToolMap.ContainsKey(EditorUI.currentSlotGroup)) {
				List<EditorPlaceholder[]> placeholders = TileTool.tileToolMap[EditorUI.currentSlotGroup].placeholders;
				Dictionary<byte, TileGameObject> tileDict = Systems.mapper.TileDict;

				for(byte i = 0; i < 10; i++) {
					if(placeholders.Count <= i) { continue; }
					EditorPlaceholder ph = placeholders[i][0];
					byte tileId = ph.tileId;
					if(!tileDict.ContainsKey(tileId)) { continue; }

					// Draw Tile (with correct subtype)
					tileDict[tileId].Draw(null, ph.subType, this.x + i * tileWidth + 2, this.y);

					// Draw Keybind Text
					Systems.fonts.baseText.Draw((i + 1).ToString(), this.x + i * tileWidth + 4, this.y + this.height - 18, Color.DarkOrange);
				}
			}

			// Hovering Visual
			if(UIComponent.ComponentWithFocus is UtilityBar) {
				short mx = (short) Snap.GridFloor(tileWidth, Cursor.MouseX - this.x);

				Systems.spriteBatch.Draw(Systems.tex2dDarkRed, new Rectangle(this.x + mx * tileWidth, this.y, tileWidth, this.height), Color.White * 0.5f);
			}
		}
	}
}
