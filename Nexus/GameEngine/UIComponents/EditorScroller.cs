using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class EditorScroller : UIComponent {

		private enum EScrollerEnum : byte {
			NumTiles = 19,
		}

		public EditorScroller( UIComponent parent ) : base(parent) {

		}

		public void Draw() {

			byte tileHeight = (byte)TilemapEnum.TileHeight + 2;

			// Draw Editor Scroller Background
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(0, 0, (byte) TilemapEnum.TileWidth + 4, Systems.screen.screenHeight), Color.DarkSlateGray);
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(2, 2, (byte) TilemapEnum.TileWidth, Systems.screen.screenHeight - 6), Color.White);

			// Grid Outline
			for(byte i = 1; i < (byte) EScrollerEnum.NumTiles; i++) {
				Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(0, i * tileHeight, (byte)TilemapEnum.TileWidth + 4, 2), Color.DarkSlateGray);
			}
		}
	}
}
