using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class WorldEditorScroller : UIComponent {

		private enum WEScrollerEnum : byte {
			NumTiles = 24,
		}

		public WorldEditorScroller( UIComponent parent, short posX, short posY) : base(parent) {
			this.x = posX;
			this.y = posY;
			this.width = (byte) WorldmapEnum.TileWidth + 4;
			this.height = (short) Systems.screen.windowHeight;
		}

		public void RunTick() {
			if(this.IsMouseOver()) { UIComponent.ComponentWithFocus = this; }

			// Mouse Scroll (if WorldTileTool is selected as active tool)
			if(WorldEditorTools.WorldTileTool is WorldTileTool == true) {
				sbyte scrollVal = Cursor.MouseScrollDelta;
				if(scrollVal == 0) { return; }
				WorldEditorTools.WorldTileTool.CycleSubIndex(scrollVal); // Cycles the SubIndex by -1 or +1
				WorldEditorTools.UpdateHelperText();
			}
		}

		public void Draw() {

			byte tileHeight = (byte)WorldmapEnum.TileHeight + 2;

			// Draw Editor Scroller Background
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.x, this.y, this.width, this.height), Color.DarkSlateGray);
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.x + 2, this.y + 2, (byte) WorldmapEnum.TileWidth, this.height - 6), Color.White);

			// Grid Outline
			for(byte i = 1; i < (byte) WEScrollerEnum.NumTiles; i++) {
				Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.x, this.y + i * tileHeight, this.width, 2), Color.DarkSlateGray);
			}

			// Draw WorldTileTool Subtype Buttons
			if(WorldEditorTools.WorldTileTool is WorldTileTool) {
				List<WorldEditorPlaceholder[]> placeholders = WorldEditorTools.WorldTileTool.placeholders;

				// Placeholder Loop
				byte len = (byte) placeholders.Count;

				WorldEditorPlaceholder[] pData = placeholders[WorldEditorTools.WorldTileTool.index];

				byte phSubLen = (byte)pData.Length;
				for(byte s = 0; s < phSubLen; s++) {
					WorldEditorPlaceholder ph = pData[s];

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
				short my = (short) Snap.GridFloor(tileHeight, WorldEditorTools.WorldTileTool.subIndex * tileHeight - this.y);
				Systems.spriteBatch.Draw(Systems.tex2dDarkRed, new Rectangle(this.x, this.y + my * tileHeight, this.width, tileHeight), Color.White * 0.5f);
			}

			// Hovering Visual
			if(UIComponent.ComponentWithFocus is WorldEditorScroller) {
				short my = (short) Snap.GridFloor(tileHeight, Cursor.MouseY - this.y);
				Systems.spriteBatch.Draw(Systems.tex2dDarkRed, new Rectangle(this.x, this.y + my * tileHeight, this.width, tileHeight), Color.White * 0.5f);
			}
		}
	}
}
