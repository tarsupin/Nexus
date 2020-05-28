using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;
using System.Collections.Generic;
using static Nexus.GameEngine.WEFuncBut;

namespace Nexus.GameEngine {

	public class WEUtilityBar : UIComponent {

		private enum WEUtilityBarEnum : byte {
			BarTiles = 30,
		}

		private Dictionary<byte, WEFuncBut> buttonMap = new Dictionary<byte, WEFuncBut>() {
			{ 11, WEFuncBut.WEFuncButMap[(byte) WEFuncButEnum.Info] },
			{ 12, WEFuncBut.WEFuncButMap[(byte) WEFuncButEnum.Move] },
			{ 13, WEFuncBut.WEFuncButMap[(byte) WEFuncButEnum.Eraser] },
			{ 14, WEFuncBut.WEFuncButMap[(byte) WEFuncButEnum.Eyedrop] },
			{ 15, WEFuncBut.WEFuncButMap[(byte) WEFuncButEnum.Wand] },
			{ 16, WEFuncBut.WEFuncButMap[(byte) WEFuncButEnum.Settings] },
			{ 17, WEFuncBut.WEFuncButMap[(byte) WEFuncButEnum.Undo] },
			{ 18, WEFuncBut.WEFuncButMap[(byte) WEFuncButEnum.Redo] },
			{ 19, WEFuncBut.WEFuncButMap[(byte) WEFuncButEnum.RoomLeft] },
			{ 20, WEFuncBut.WEFuncButMap[(byte) WEFuncButEnum.Home] },
			{ 21, WEFuncBut.WEFuncButMap[(byte) WEFuncButEnum.RoomRight] },
			{ 22, WEFuncBut.WEFuncButMap[(byte) WEFuncButEnum.SwapRight] },
			{ 24, WEFuncBut.WEFuncButMap[(byte) WEFuncButEnum.Save] },
			{ 25, WEFuncBut.WEFuncButMap[(byte) WEFuncButEnum.Play] },
		};

		public WEUtilityBar( UIComponent parent, short posX, short posY ) : base(parent) {
			this.x = posX;
			this.y = posY;
			this.width = ((byte) WorldmapEnum.TileWidth + 2) * (byte) WEUtilityBarEnum.BarTiles;
			this.height = (byte) WorldmapEnum.TileHeight;
		}

		public void RunTick() {
			if(this.IsMouseOver()) {
				UIComponent.ComponentWithFocus = this;
				WEFuncBut WorldFuncBut = null;

				// Identify which Bar Number is being highlighted:
				byte barIndex = this.GetBarIndex(Cursor.MouseX);

				// Check if a Function Button is highlighted:
				if(buttonMap.ContainsKey(barIndex)) {
					WorldFuncBut = buttonMap[barIndex];

					// Draw the Helper Text associated with the Function Button
					WEScene WEScene = (WEScene)Systems.scene;
					WEScene.weUI.SetHelperText(WorldFuncBut.title, WorldFuncBut.description);
				}

				// Mouse was pressed
				if(Cursor.LeftMouseState == Cursor.MouseDownState.Clicked) {

					// Clicked a Tile Tool
					if(barIndex < 10) {
						EditorTools.SetTileToolBySlotGroup(EditorUI.currentSlotGroup, barIndex);
					}

					// Clicked a Function Button
					if(WorldFuncBut != null) {
						WorldFuncBut.ActivateWorldFuncButton();
					}
				}
			}
			
			// If the Mouse just exited this component:
			else if(this.MouseOver == UIMouseOverState.Exited) {
				EditorTools.UpdateHelperText(); // Update the Helper Text (since it may have changed from overlaps)
			}
		}

		private byte GetBarIndex(int posX) {
			byte tileWidth = ((byte) WorldmapEnum.TileWidth + 2);
			short offsetX = (short) (posX - this.x);
			byte index = (byte) System.Math.Floor((decimal) (offsetX / tileWidth));
			return index;
		}

		public void Draw() {
			byte tileWidth = (byte) WorldmapEnum.TileWidth + 2;

			// Draw Utility Bar Background
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.x, this.y - 2, this.width, this.height + 2), Color.DarkSlateGray);
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.x + 2, this.y, this.width - 2, this.height), Color.White);

			// Tile Outlines
			for(byte i = 0; i <= (byte) WEUtilityBarEnum.BarTiles; i++) {
				Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.x + i * tileWidth, this.y, 2, this.height), Color.DarkSlateGray);
			}

			// Tile Icons
			if(TileTool.tileToolMap.ContainsKey(EditorUI.currentSlotGroup)) {
				List<EditorPlaceholder[]> placeholders = TileTool.tileToolMap[EditorUI.currentSlotGroup].placeholders;
				Dictionary<byte, TileObject> tileDict = Systems.mapper.TileDict;

				for(byte i = 0; i < 10; i++) {
					if(placeholders.Count <= i) { continue; }
					EditorPlaceholder ph = placeholders[i][0];
					byte tileId = ph.tileId;

					// Draw Tile
					if(tileId > 0) {
						if(!tileDict.ContainsKey(tileId)) { continue; }
						tileDict[tileId].Draw(null, ph.subType, this.x + i * tileWidth + 2, this.y);
					}
				}
			}

			// Draw Keybind Text
			for(byte i = 0; i < 10; i++) {
				Systems.fonts.baseText.Draw((i + 1).ToString(), this.x + i * tileWidth + 4, this.y + this.height - 18, Color.DarkOrange);
			}

			// Function Icons
			foreach(KeyValuePair<byte, WEFuncBut> button in this.buttonMap) {
				byte barIndex = button.Key;
				button.Value.DrawFunctionTile(this.x + barIndex * tileWidth + 2, this.y);
			}

			// Hovering Visual
			if(UIComponent.ComponentWithFocus is WEUtilityBar) {
				short mx = (short) Snap.GridFloor(tileWidth, Cursor.MouseX - this.x);
				Systems.spriteBatch.Draw(Systems.tex2dDarkRed, new Rectangle(this.x + mx * tileWidth, this.y, tileWidth, this.height), Color.White * 0.5f);
			}
		}
	}
}
