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
			this.x = 0;
			this.y = 0;
			this.width = (byte) TilemapEnum.TileWidth + 4;
			this.height = (short) Systems.screen.windowHeight;
		}

		public void RunTick() {
			if(this.IsMouseOver()) { UIComponent.ComponentWithFocus = this; }
		}

		public void Draw() {

			byte tileHeight = (byte)TilemapEnum.TileHeight + 2;

			// Draw Editor Scroller Background
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(0, 0, this.width, this.height), Color.DarkSlateGray);
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(2, 2, (byte) TilemapEnum.TileWidth, this.height - 6), Color.White);

			// Grid Outline
			for(byte i = 1; i < (byte) EScrollerEnum.NumTiles; i++) {
				Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(0, i * tileHeight, this.width, 2), Color.DarkSlateGray);
			}

			// Draw TileTool Subtype Buttons
			if(EditorTools.tileTool is TileTool) {
				List<EditorPlaceholder[]> placeholders = EditorTools.tileTool.placeholders;

				// Placeholder Loop
				byte len = (byte) placeholders.Count;

				for(byte i = 0; i < len; i++) {
					EditorPlaceholder[] pData = placeholders[i];

					byte phSubLen = (byte)pData.Length;
					for(byte s = 0; s < phSubLen; s++) {
						EditorPlaceholder ph = pData[s];

						byte tileId = ph.tileId;
						byte subType = ph.subType;

						if(Systems.mapper.TileDict.ContainsKey(tileId)) {
							TileGameObject tgo = Systems.mapper.TileDict[tileId];
							tgo.Draw(null, subType, 2, 50 * i + 2);
						}
					}
				}

				// TODO: Draw the highlight for the tile tool scroller
				//// Draw Highlight
				//this.hover.position.set(0, EditorCursor.tileTool.index * (byte)TilemapEnum.TileHeight);
				//this.pixi.draw(this.hover);

				//// Hovering Visual
				//if(UIComponent.ComponentWithFocus is UtilityBar) {
				//	short mx = (short)Snap.GridFloor(tileWidth, Cursor.MouseX - this.x);

				//	Systems.spriteBatch.Draw(Systems.tex2dDarkRed, new Rectangle(this.x + mx * tileWidth, this.y, tileWidth, this.height), Color.White * 0.5f);
				//}
			}

			// Hovering Visual
			if(UIComponent.ComponentWithFocus is EditorScroller) {
				short my = (short) Snap.GridFloor(tileHeight, Cursor.MouseY - this.y);

				Systems.spriteBatch.Draw(Systems.tex2dDarkRed, new Rectangle(this.x, this.y + my * tileHeight, this.width, tileHeight), Color.White * 0.5f);
			}
		}
	}
}
