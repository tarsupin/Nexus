using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nexus.Engine;
using Nexus.Gameplay;
using System.Collections.Generic;
using static Nexus.GameEngine.FuncButton;

namespace Nexus.GameEngine {

	public class UtilityBar : UIComponent {

		private Dictionary<byte, FuncButton> buttonMap = FuncButton.funcButtonMap;

		private enum UtilityBarEnum : byte {
			BarTiles = 26,
		}

		private enum FuncButtonPos : byte {
			Info = 11,
			Eraser = 12,
			Move = 13,
			Eyedrop = 14,

			Wand = 15,
			Settings = 16,

			Undo = 17,
			Redo = 18,

			RoomLeft = 19,
			Home = 20,
			RoomRight = 21,
			SwapRight = 22,

			Save = 24,
			Play = 25,
		}

		public UtilityBar( UIComponent parent, short posX, short posY ) : base(parent) {
			this.x = posX;
			this.y = posY;
			this.width = ((byte) TilemapEnum.TileWidth + 2) * (byte) UtilityBarEnum.BarTiles;
			this.height = (byte) TilemapEnum.TileHeight;
		}

		public void RunTick() {
			if(this.IsMouseOver()) {
				UIComponent.ComponentWithFocus = this;

				// Identify which Bar Number is being highlighted:
				byte barIndex = this.GetBarIndex(Cursor.MouseX);

				// Mouse was pressed
				if(Cursor.mouseState.LeftButton == ButtonState.Pressed) {

					// Selected a Tile Tool
					if(barIndex < 10) {
						EditorTools.SetTileToolBySlotGroup(EditorUI.currentSlotGroup, barIndex);
					}
				}

				// Mouse is only highlighting (not pressed)
				else {
					this.ShowHelperTextForFuncButton(barIndex, (byte) FuncButtonEnum.Info, (byte) FuncButtonPos.Info);
					this.ShowHelperTextForFuncButton(barIndex, (byte) FuncButtonEnum.Eraser, (byte) FuncButtonPos.Eraser);
					this.ShowHelperTextForFuncButton(barIndex, (byte) FuncButtonEnum.Move, (byte) FuncButtonPos.Move);
					this.ShowHelperTextForFuncButton(barIndex, (byte) FuncButtonEnum.Eyedrop, (byte) FuncButtonPos.Eyedrop);
					this.ShowHelperTextForFuncButton(barIndex, (byte) FuncButtonEnum.Wand, (byte) FuncButtonPos.Wand);
					this.ShowHelperTextForFuncButton(barIndex, (byte) FuncButtonEnum.Settings, (byte) FuncButtonPos.Settings);
					this.ShowHelperTextForFuncButton(barIndex, (byte) FuncButtonEnum.Undo, (byte) FuncButtonPos.Undo);
					this.ShowHelperTextForFuncButton(barIndex, (byte) FuncButtonEnum.Redo, (byte) FuncButtonPos.Redo);
					this.ShowHelperTextForFuncButton(barIndex, (byte) FuncButtonEnum.RoomLeft, (byte) FuncButtonPos.RoomLeft);
					this.ShowHelperTextForFuncButton(barIndex, (byte) FuncButtonEnum.Home, (byte) FuncButtonPos.Home);
					this.ShowHelperTextForFuncButton(barIndex, (byte) FuncButtonEnum.RoomRight, (byte) FuncButtonPos.RoomRight);
					this.ShowHelperTextForFuncButton(barIndex, (byte) FuncButtonEnum.SwapRight, (byte) FuncButtonPos.SwapRight);
					this.ShowHelperTextForFuncButton(barIndex, (byte) FuncButtonEnum.Save, (byte) FuncButtonPos.Save);
					this.ShowHelperTextForFuncButton(barIndex, (byte) FuncButtonEnum.Play, (byte) FuncButtonPos.Play);
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

		private void ShowHelperTextForFuncButton(byte barIndex, byte funcNum, byte funcPos) {
			if(barIndex != funcPos) { return; }
			FuncButton funcButton = this.buttonMap[funcNum];
			EditorScene editorScene = (EditorScene)Systems.scene;
			editorScene.editorUI.SetHelperText(funcButton.title, funcButton.description);
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
				Dictionary<byte, TileGameObject> tileDict = Systems.mapper.TileDict;

				for(byte i = 0; i < 10; i++) {
					if(placeholders.Count <= i) { continue; }
					EditorPlaceholder ph = placeholders[i][0];
					byte tileId = ph.tileId;
					if(!tileDict.ContainsKey(tileId)) { continue; }

					// Draw Tile (with correct subtype)
					tileDict[tileId].Draw(null, ph.subType, this.x + i * tileWidth + 2, this.y);
				}
			}

			// Draw Keybind Text
			for(byte i = 0; i < 10; i++) {
				Systems.fonts.baseText.Draw((i + 1).ToString(), this.x + i * tileWidth + 4, this.y + this.height - 18, Color.DarkOrange);
			}

			// Function Icons
			this.buttonMap[(byte)FuncButtonEnum.Info].DrawFunctionTile(this.x + (byte)FuncButtonPos.Info * tileWidth + 2, this.y);
			this.buttonMap[(byte)FuncButtonEnum.Eraser].DrawFunctionTile(this.x + (byte)FuncButtonPos.Eraser * tileWidth + 2, this.y);
			this.buttonMap[(byte)FuncButtonEnum.Move].DrawFunctionTile(this.x + (byte)FuncButtonPos.Move * tileWidth + 2, this.y);
			this.buttonMap[(byte)FuncButtonEnum.Eyedrop].DrawFunctionTile(this.x + (byte)FuncButtonPos.Eyedrop * tileWidth + 2, this.y);
			this.buttonMap[(byte)FuncButtonEnum.Wand].DrawFunctionTile(this.x + (byte)FuncButtonPos.Wand * tileWidth + 2, this.y);
			this.buttonMap[(byte)FuncButtonEnum.Settings].DrawFunctionTile(this.x + (byte)FuncButtonPos.Settings * tileWidth + 2, this.y);

			this.buttonMap[(byte)FuncButtonEnum.Undo].DrawFunctionTile(this.x + (byte)FuncButtonPos.Undo * tileWidth + 2, this.y);
			this.buttonMap[(byte)FuncButtonEnum.Redo].DrawFunctionTile(this.x + (byte)FuncButtonPos.Redo * tileWidth + 2, this.y);

			this.buttonMap[(byte)FuncButtonEnum.RoomLeft].DrawFunctionTile(this.x + (byte)FuncButtonPos.RoomLeft * tileWidth + 2, this.y);
			this.buttonMap[(byte)FuncButtonEnum.Home].DrawFunctionTile(this.x + (byte)FuncButtonPos.Home * tileWidth + 2, this.y);
			this.buttonMap[(byte)FuncButtonEnum.RoomRight].DrawFunctionTile(this.x + (byte)FuncButtonPos.RoomRight * tileWidth + 2, this.y);
			this.buttonMap[(byte)FuncButtonEnum.SwapRight].DrawFunctionTile(this.x + (byte)FuncButtonPos.SwapRight * tileWidth + 2, this.y);

			this.buttonMap[(byte)FuncButtonEnum.Save].DrawFunctionTile(this.x + (byte)FuncButtonPos.Save * tileWidth + 2, this.y);
			this.buttonMap[(byte)FuncButtonEnum.Play].DrawFunctionTile(this.x + (byte)FuncButtonPos.Play * tileWidth + 2, this.y);

			// Hovering Visual
			if(UIComponent.ComponentWithFocus is UtilityBar) {
				short mx = (short) Snap.GridFloor(tileWidth, Cursor.MouseX - this.x);

				Systems.spriteBatch.Draw(Systems.tex2dDarkRed, new Rectangle(this.x + mx * tileWidth, this.y, tileWidth, this.height), Color.White * 0.5f);
			}
		}
	}
}
