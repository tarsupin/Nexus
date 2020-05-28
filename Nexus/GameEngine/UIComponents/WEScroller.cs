﻿using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class WEScroller : UIComponent {

		public Atlas atlas;

		private enum WEScrollerEnum : byte {
			NumTiles = 24,
		}

		public WEScroller( UIComponent parent, short posX, short posY) : base(parent) {
			this.x = posX;
			this.y = posY;
			this.width = (byte) WorldmapEnum.TileWidth + 4;
			this.height = (short) Systems.screen.windowHeight;
			this.atlas = Systems.mapper.atlas[(byte)AtlasGroup.World];
		}

		public void RunTick() {
			if(this.IsMouseOver()) { UIComponent.ComponentWithFocus = this; }

			// Mouse Scroll (if WorldTileTool is selected as active tool)
			if(WETools.WETileTool is WETileTool == true) {
				sbyte scrollVal = Cursor.MouseScrollDelta;
				if(scrollVal == 0) { return; }
				WETools.WETileTool.CycleSubIndex(scrollVal); // Cycles the SubIndex by -1 or +1
				WETools.UpdateHelperText();
			}
		}

		public void Draw() {

			byte tileHeight = (byte)WorldmapEnum.TileHeight + 2;

			// Prepare Zone Data
			var WorldTerrain = Systems.mapper.WorldTerrain;
			var WorldTerrainCat = Systems.mapper.WorldTerrainCat;
			var WorldLayers = Systems.mapper.WorldLayers;
			var WorldObjects = Systems.mapper.WorldObjects;

			// Draw Editor Scroller Background
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.x, this.y, this.width, this.height), Color.DarkSlateGray);
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.x + 2, this.y + 2, (byte) WorldmapEnum.TileWidth, this.height - 6), Color.White);

			// Grid Outline
			for(byte i = 1; i < (byte) WEScrollerEnum.NumTiles; i++) {
				Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.x, this.y + i * tileHeight, this.width, 2), Color.DarkSlateGray);
			}

			// Draw WorldTileTool Subtype Buttons
			if(WETools.WETileTool is WETileTool) {
				List<WEPlaceholder[]> placeholders = WETools.WETileTool.placeholders;

				// Placeholder Loop
				WEPlaceholder[] pData = placeholders[WETools.WETileTool.index];

				byte phSubLen = (byte)pData.Length;
				for(byte s = 0; s < phSubLen; s++) {
					WEPlaceholder ph = pData[s];

					// Draw Base
					if(ph.tBase != 0) {

						// If there is a top layer:
						if(ph.tTop != 0) {

							// Draw a standard base tile with no varient, so that the top layer will look correct.
							this.atlas.Draw(WorldTerrain[ph.tBase] + "/b1", this.x + 2, this.y + tileHeight * s + 2);

							// Draw the Top Layer
							this.atlas.Draw(WorldTerrain[ph.tTop] + "/" + WorldLayers[ph.tLayer], this.x + 2, this.y + tileHeight * s + 2);
						}

						// If there is not a top layer:
						else {

							// If there is a category:
							if(ph.tCat != 0) {
								this.atlas.Draw(WorldTerrain[ph.tBase] + "/" + WorldTerrainCat[ph.tCat] + "/" + WorldLayers[ph.tLayer], this.x + 2, this.y + tileHeight * s + 2);
							} else {
								this.atlas.Draw(WorldTerrain[ph.tBase] + "/" + WorldLayers[ph.tLayer], this.x + 2, this.y + tileHeight * s + 2);
							}
						}
					}

					// Draw Top, with no base:
					else if(ph.tTop != 0) {
						this.atlas.Draw(WorldTerrain[ph.tTop] + "/" + WorldLayers[ph.tLayer], this.x + 2, this.y + tileHeight * s + 2);
					}

					// Draw Object Layer
					if(ph.tObj != 0) {
						this.atlas.Draw("Objects/" + WorldObjects[ph.tObj], this.x + 2, this.y + tileHeight * s + 2);
					}
				}

				// Highlight the active color
				short my = (short) Snap.GridFloor(tileHeight, WETools.WETileTool.subIndex * tileHeight - this.y);
				//Systems.spriteBatch.Draw(Systems.tex2dDarkRed, new Rectangle(this.x, this.y + my * tileHeight, this.width, tileHeight), Color.White * 0.5f);
				Systems.mapper.atlas[(byte)AtlasGroup.Tiles].DrawAdvanced("Icons/Small/Brush", this.x, this.y + my * tileHeight + 2, Color.White * 0.7f);
			}

			// Hovering Visual
			if(UIComponent.ComponentWithFocus is WEScroller) {
				short my = (short) Snap.GridFloor(tileHeight, Cursor.MouseY - this.y);
				//Systems.spriteBatch.Draw(Systems.tex2dDarkRed, new Rectangle(this.x, this.y + my * tileHeight, this.width, tileHeight), Color.White * 0.5f);
				Systems.mapper.atlas[(byte)AtlasGroup.Tiles].Draw("Icons/Small/Brush", this.x, this.y + my * tileHeight + 2);
			}
		}
	}
}
