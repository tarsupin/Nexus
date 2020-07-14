using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;
using System.Collections.Generic;
using static Nexus.GameEngine.FuncButton;

namespace Nexus.GameEngine {

	public class UtilityBar : UIComponent {

		private enum UtilityBarEnum : byte {
			BarTiles = 26,
		}

		private Dictionary<byte, FuncButton> buttonMap = new Dictionary<byte, FuncButton>() {
			{ 11, FuncButton.funcButtonMap[(byte) FuncButtonEnum.Info] },
			{ 12, FuncButton.funcButtonMap[(byte) FuncButtonEnum.Select] },
			{ 13, FuncButton.funcButtonMap[(byte) FuncButtonEnum.Eraser] },
			{ 14, FuncButton.funcButtonMap[(byte) FuncButtonEnum.Eyedrop] },
			{ 15, FuncButton.funcButtonMap[(byte) FuncButtonEnum.Wand] },
			{ 16, FuncButton.funcButtonMap[(byte) FuncButtonEnum.Settings] },
			//{ 17, FuncButton.funcButtonMap[(byte) FuncButtonEnum.Undo] },
			//{ 18, FuncButton.funcButtonMap[(byte) FuncButtonEnum.Redo] },
			{ 19, FuncButton.funcButtonMap[(byte) FuncButtonEnum.RoomLeft] },
			{ 20, FuncButton.funcButtonMap[(byte) FuncButtonEnum.Home] },
			{ 21, FuncButton.funcButtonMap[(byte) FuncButtonEnum.RoomRight] },
			{ 22, FuncButton.funcButtonMap[(byte) FuncButtonEnum.SwapRight] },
			{ 24, FuncButton.funcButtonMap[(byte) FuncButtonEnum.Save] },
			{ 25, FuncButton.funcButtonMap[(byte) FuncButtonEnum.Play] },
		};

		public UtilityBar( UIComponent parent, short posX, short posY ) : base(parent) {
			this.SetWidth(((byte)TilemapEnum.TileWidth + 2) * (byte)UtilityBarEnum.BarTiles);
			this.SetHeight((byte)TilemapEnum.TileHeight);
			this.SetRelativePosition(posX, posY);
		}

		public void RunTick() {
			this.MouseOver = this.GetMouseOverState();
			if(this.MouseOver == UIMouseOverState.On) {
				UIComponent.ComponentWithFocus = this;
				FuncButton funcButton = null;

				// Identify which Bar Number is being highlighted:
				byte barIndex = this.GetBarIndex(Cursor.MouseX);

				// Check if a Function Button is highlighted:
				if(buttonMap.ContainsKey(barIndex)) {
					funcButton = buttonMap[barIndex];

					// Draw the Helper Text associated with the Function Button
					EditorScene editorScene = (EditorScene)Systems.scene;
					editorScene.editorUI.noticeText.SetNotice(funcButton.title, funcButton.description);
				}

				// Mouse was pressed
				if(Cursor.LeftMouseState == Cursor.MouseDownState.Clicked) {

					// Clicked a Tile Tool
					if(barIndex < 10) {
						EditorTools.SetTileToolBySlotGroup(EditorUI.currentSlotGroup, barIndex);
					}

					// Clicked a Function Button
					if(funcButton != null) {
						funcButton.ActivateFuncButton();
					}
				}
			}
			
			// If the Mouse just exited this component:
			else if(this.MouseOver == UIMouseOverState.Exited) {
				EditorTools.UpdateHelperText(); // Update the Helper Text (since it may have changed from overlaps)
			}
		}

		private byte GetBarIndex(int posX) {
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

					// Draw Object (if tile is not present)
					else if(ph.objectId > 0) {
						ShadowTile.Draw(ph.objectId, ph.subType, null, this.x + i * tileWidth + 2, this.y);
					}
				}
			}

			// Draw Keybind Text
			for(byte i = 0; i < 10; i++) {
				Systems.fonts.baseText.Draw((i + 1).ToString(), this.x + i * tileWidth + 4, this.y + this.height - 18, Color.DarkOrange);
			}

			// Function Icons
			foreach(KeyValuePair<byte, FuncButton> button in this.buttonMap) {
				byte barIndex = button.Key;
				button.Value.DrawFunctionTile(this.x + barIndex * tileWidth + 2, this.y);
			}

			// Hovering Visual
			if(UIComponent.ComponentWithFocus is UtilityBar) {
				short mx = (short) Snap.GridFloor(tileWidth, Cursor.MouseX - this.x);

				Systems.spriteBatch.Draw(Systems.tex2dDarkRed, new Rectangle(this.x + mx * tileWidth, this.y, tileWidth, this.height), Color.White * 0.5f);
			}
		}
	}
}
