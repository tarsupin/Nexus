using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;
using System.Collections.Generic;

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

			// Draw TileTool Subtype Buttons
			if(EditorTools.tileTool is TileTool) {
				List<EditorPlaceholder[]> placeholders = EditorTools.tileTool.placeholders;

				// Placeholder Loop
				byte len = (byte) placeholders.Count;

				for(byte i = 0; i < len; i++) {
					EditorPlaceholder[] pData = placeholders[i];

					byte tileId = pData[0].tileId;
					byte subType = pData[0].subType;

					if(Systems.mapper.TileMap.ContainsKey(tileId)) {
						TileGameObject tgo = Systems.mapper.TileMap[tileId];
						tgo.Draw(null, subType, 2, 50 * i + 2);
					}
				}

				// TODO: Draw the highlight for the tile tool scroller
				//// Draw Highlight
				//this.hover.position.set(0, EditorCursor.tileTool.index * (byte)TilemapEnum.TileHeight);
				//this.pixi.draw(this.hover);
			}
		}
	}
}
