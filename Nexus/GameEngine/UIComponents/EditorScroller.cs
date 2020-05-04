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

			// Mouse Scroll (if TileTool is selected as active tool)
			if(EditorTools.tileTool is TileTool == true) {
				sbyte scrollVal = Cursor.GetMouseScrollDelta();
				if(scrollVal == 0) { return; }
				EditorTools.tileTool.CycleSubIndex(scrollVal); // Cycles the SubIndex by -1 or +1
			}
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

				EditorPlaceholder[] pData = placeholders[EditorTools.tileTool.index];

				byte phSubLen = (byte)pData.Length;
				for(byte s = 0; s < phSubLen; s++) {
					EditorPlaceholder ph = pData[s];

					byte tileId = ph.tileId;
					byte subType = ph.subType;

					if(Systems.mapper.TileDict.ContainsKey(tileId)) {
						TileGameObject tgo = Systems.mapper.TileDict[tileId];
						tgo.Draw(null, subType, 2, 50 * s + 2);
					}
				}

				// Highlight the active color
				short my = (short) Snap.GridFloor(tileHeight, EditorTools.tileTool.subIndex * tileHeight - this.y);
				Systems.spriteBatch.Draw(Systems.tex2dDarkRed, new Rectangle(this.x, this.y + my * tileHeight, this.width, tileHeight), Color.White * 0.5f);
			}

			// Hovering Visual
			if(UIComponent.ComponentWithFocus is EditorScroller) {
				short my = (short) Snap.GridFloor(tileHeight, Cursor.MouseY - this.y);

				Systems.spriteBatch.Draw(Systems.tex2dDarkRed, new Rectangle(this.x, this.y + my * tileHeight, this.width, tileHeight), Color.White * 0.5f);
			}
		}
	}
}
