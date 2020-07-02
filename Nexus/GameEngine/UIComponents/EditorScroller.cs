using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class EditorScroller : UIComponent {

		private enum EScrollerEnum : byte {
			NumTiles = 19,
		}

		public EditorScroller( UIComponent parent, short posX, short posY) : base(parent) {
			this.SetRelativePosition(posX, posY);
			this.SetWidth((byte)TilemapEnum.TileWidth + 4);
			this.SetHeight((short)Systems.screen.windowHeight);
		}

		public void RunTick() {
			if(this.IsMouseOver()) { UIComponent.ComponentWithFocus = this; }

			// Mouse Scroll (if TileTool is selected as active tool)
			if(EditorTools.tileTool is TileTool == true) {
				sbyte scrollVal = Cursor.MouseScrollDelta;
				if(scrollVal == 0) { return; }
				EditorTools.tileTool.CycleSubIndex(scrollVal); // Cycles the SubIndex by -1 or +1
				EditorTools.UpdateHelperText();
			}
		}

		public void Draw() {

			byte tileHeight = (byte)TilemapEnum.TileHeight + 2;

			// Draw Editor Scroller Background
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.x, this.y, this.width, this.height), Color.DarkSlateGray);
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.x + 2, this.y + 2, (byte) TilemapEnum.TileWidth, this.height - 6), Color.White);

			// Grid Outline
			for(byte i = 1; i < (byte) EScrollerEnum.NumTiles; i++) {
				Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.x, this.y + i * tileHeight, this.width, 2), Color.DarkSlateGray);
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

					byte subType = ph.subType;
					byte tileId = ph.tileId;

					// Draw Tiles
					if(tileId > 0) {
						if(Systems.mapper.TileDict.ContainsKey(tileId)) {
							TileObject tgo = Systems.mapper.TileDict[tileId];
							tgo.Draw(null, subType, this.x + 2, this.y + 50 * s + 2);
						}
					}

					// Draw Objects
					else if(ph.objectId > 0) {
						ShadowTile.Draw(ph.objectId, ph.subType, null, this.x + 2, this.y + 50 * s + 2);
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
