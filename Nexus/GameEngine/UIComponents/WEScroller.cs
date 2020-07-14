using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class WEScroller : UIComponent {

		public WEScene scene;

		private enum WEScrollerEnum : byte {
			NumTiles = 24,
		}

		public WEScroller( UIComponent parent, WEScene scene, short posX, short posY) : base(parent) {
			this.scene = scene;
			this.SetRelativePosition(posX, posY);
			this.SetWidth((byte)WorldmapEnum.TileWidth + 4);
			this.SetHeight((short)Systems.screen.windowHeight);
		}

		public void RunTick() {
			this.MouseOver = this.GetMouseOverState();
			if(this.MouseOver == UIMouseOverState.On) { UIComponent.ComponentWithFocus = this; }

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

					this.scene.DrawWorldTile(new byte[] { ph.tBase, ph.top, ph.topLay, ph.cover, ph.coverLay, ph.obj, ph.tNodeId }, this.x + 2, this.y + tileHeight * s + 2);
				}

				// Highlight the active color
				short my = (short) Snap.GridFloor(tileHeight, WETools.WETileTool.subIndex * tileHeight - this.y);
				//Systems.spriteBatch.Draw(Systems.tex2dDarkRed, new Rectangle(this.x, this.y + my * tileHeight, this.width, tileHeight), Color.White * 0.5f);
				UIHandler.atlas.DrawAdvanced("Small/Brush", this.x, this.y + my * tileHeight + 2, Color.White * 0.7f);
			}

			// Hovering Visual
			if(UIComponent.ComponentWithFocus is WEScroller) {
				short my = (short) Snap.GridFloor(tileHeight, Cursor.MouseY - this.y);
				//Systems.spriteBatch.Draw(Systems.tex2dDarkRed, new Rectangle(this.x, this.y + my * tileHeight, this.width, tileHeight), Color.White * 0.5f);
				UIHandler.atlas.Draw("Small/Brush", this.x, this.y + my * tileHeight + 2);
			}
		}
	}
}
