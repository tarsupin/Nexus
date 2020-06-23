using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nexus.Engine;
using Nexus.Gameplay;
using System;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class ContextMenuOpt {
		public Atlas atlas;
		public string texture;
		public string text;

		public ContextMenuOpt( Atlas atlas, string texture, string text ) {
			this.atlas = atlas;
			this.texture = texture;
			this.text = text;
		}
	}

	public class ContextMenu : UIComponent {
		private readonly byte xCount;
		private readonly byte yCount;
		private readonly byte size;
		private readonly int count;
		private static FontClass font;
		private static byte iconOffset = (byte)((byte)ContextMenuEnum.HalfSize - (byte)Math.Floor((byte)TilemapEnum.TileWidth * 0.5f));

		private enum ContextMenuEnum : byte {
			Size = 100,
			HalfSize = 50,
		}

		private Dictionary<byte, ContextMenuOpt> menuOptions;

		public ContextMenu( UIComponent parent, short posX, short posY, byte xCount = 4, byte yCount = 4 ) : base(parent) {

			this.xCount = xCount;
			this.yCount = yCount;

			this.count = xCount * yCount;
			this.size = (byte)ContextMenuEnum.Size;
			this.width = (short)(this.size * this.xCount);
			this.height = (short)(this.size * this.yCount);

			// posX, posY describes the center of the context menu.
			// x, y describes the top-left corner of the context menu.
			this.x = (short)(posX - (byte)ContextMenuEnum.HalfSize * this.xCount);
			this.y = (short)(posY - (byte)ContextMenuEnum.HalfSize * this.yCount);

			// Prepare Menu Options
			this.menuOptions = new Dictionary<byte, ContextMenuOpt>();

			// Ensure Font has been set.
			if(ContextMenu.font is FontClass == false) {
				ContextMenu.font = Systems.fonts.console;
			}
		}

		public void SetMenuOption(byte position, Atlas atlas, string texture, string text) {
			this.menuOptions[position] = new ContextMenuOpt(atlas, texture, text);
		}

		public void RunTick() {

			// End method if the context menu isn't visible, or if the tab key was released.
			if(!this.visible) { return; }
			else if(!Systems.input.LocalKeyDown(Keys.Tab)) { this.CloseMenu(); return; }

			if(this.IsMouseOver()) {
				UIComponent.ComponentWithFocus = this;

				if(Cursor.LeftMouseState == Cursor.MouseDownState.Clicked) {
					this.OnClick();
					return;
				}
			}
		}

		public virtual void OnClick() {
			EditorUI.currentSlotGroup = this.GetContextOpt(Cursor.MouseX, Cursor.MouseY);
			EditorTools.SetTileToolBySlotGroup(EditorUI.currentSlotGroup);
			EditorTools.UpdateHelperText();
			this.CloseMenu();
			DrawTracker.AttemptDraw((short) Cursor.TileGridX, (short) Cursor.TileGridY);
		}

		public byte GetContextOpt( int posX, int posY ) {

			// Loop through each Context Menu Option. Return based on position of mouse.
			for( byte i = 0; i < this.xCount; i++ ) {
				int left = this.x + this.size * i;

				// Make sure you're within the X bounds:
				if(posX >= left && posX <= left + this.size) {

					// Loop through vertical set:
					for(byte j = 0; j < this.yCount; j++) {
						int top = this.y + this.size * j;

						// Make sure you're within the Y bounds:
						if(posY >= top && posY <= top + this.size) {
							return (byte) (j * this.xCount + i + 1); // +1 is because we need a "0" option to define NO-OPTION
						}
					}
				}
			}

			return 0;
		}

		public void OpenMenu() {
			//byte offset = (byte)ContextMenuEnum.Size + (byte)ContextMenuEnum.HalfSize;
			//Cursor.SetPos(this.x + offset, this.y + offset);
			this.SetVisible(true);
		}

		public void CloseMenu() {
			this.SetVisible(false);
		}

		public void Draw() {
			if(!this.visible) { return; }

			// Draw White Background
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.x, this.y, this.width, this.height), Color.White * 0.6f);

			// Draw Line Divisions & Menu Options

			// Loop through horizontal set:
			for(byte i = 0; i < this.xCount; i++) {
				Systems.spriteBatch.Draw(Systems.tex2dBlack, new Rectangle(this.x + this.size * i, this.y, 2, this.height), Color.Black);

				// Draw Each Slot while we're at it:
				for(byte j = 0; j < this.yCount; j++) {
					this.DrawMenuOption((byte)(j * this.xCount + i + 1), (short) (this.x + this.size * i), (short) (this.y + this.size * j));
				}
			}

			// Loop through vertical set:
			for(byte i = 0; i <= this.yCount; i++) {
				Systems.spriteBatch.Draw(Systems.tex2dBlack, new Rectangle(this.x, this.y + this.size * i, this.width, 2), Color.Black);
			}

			// Hovering Visual
			if(UIComponent.ComponentWithFocus is ContextMenu) {
				short mx = (short)Snap.GridFloor(this.size, Cursor.MouseX - this.x);
				short my = (short)Snap.GridFloor(this.size, Cursor.MouseY - this.y);

				Systems.spriteBatch.Draw(Systems.tex2dDarkRed, new Rectangle(this.x + mx * this.size, this.y + my * this.size, this.size, this.size), Color.White * 0.5f);
			}
		}

		public void DrawMenuOption( byte position, short posX, short posY ) {
			if(!this.menuOptions.ContainsKey(position)) { return; }
			ContextMenuOpt option = this.menuOptions[position];
			option.atlas.Draw(option.texture, posX + ContextMenu.iconOffset, posY + 20);
			Vector2 textSize = ContextMenu.font.font.MeasureString(option.text);
			ContextMenu.font.Draw(option.text, posX + (byte) ContextMenuEnum.HalfSize - (byte) Math.Floor(textSize.X * 0.5f), posY + 75, Color.Black);
		}
	}
}
