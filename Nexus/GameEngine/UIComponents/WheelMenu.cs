using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nexus.Engine;
using Nexus.Gameplay;
using System;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class WheelMenu : UIComponent {
		private readonly byte size;
		private static FontClass font;
		private static byte iconOffset = (byte)((byte)WheelMenuEnum.HalfSize - (byte)Math.Floor((byte)TilemapEnum.TileWidth * 0.5f));

		private enum WheelMenuEnum : byte {
			Size = 100,
			HalfSize = 50,
		}

		private Dictionary<byte, ContextMenuOpt> menuOptions;

		public WheelMenu( UIComponent parent, short posX, short posY ) : base(parent) {

			this.size = (byte) WheelMenuEnum.Size;
			this.width = (short) (this.size * 3);
			this.height = (short) (this.size * 3);

			// posX, posY describes the center of the wheel.
			// x, y describes the top-left corner of the wheel.
			this.x = (short) (posX - (byte) WheelMenuEnum.Size - (byte) WheelMenuEnum.HalfSize);
			this.y = (short) (posY - (byte) WheelMenuEnum.Size - (byte) WheelMenuEnum.HalfSize);

			// Prepare Menu Options
			this.menuOptions = new Dictionary<byte, ContextMenuOpt>();

			// Ensure Font has been set.
			if(WheelMenu.font is FontClass == false) {
				WheelMenu.font = Systems.fonts.console;
			}
		}

		public void SetMenuOption(byte position, Atlas atlas, string texture, string text) {
			this.menuOptions[position] = new ContextMenuOpt(atlas, texture, text);
		}

		public void RunTick() {

			// End method if the wheel menu isn't visible, or if the tab key was released.
			if(!this.visible) { return; }
			else if(!Systems.input.LocalKeyDown(Keys.Tab)) { this.CloseMenu(); return; }

			if(this.IsMouseOver()) {
				UIComponent.ComponentWithFocus = this;

				if(Cursor.mouseState.LeftButton == ButtonState.Pressed) {
					//EditorUI.menuOptChosen = this.GetWheelDir(Cursor.MouseX, Cursor.MouseY);
					this.CloseMenu(); return;
				}
			}
		}

		public DirRotate GetWheelDir( int posX, int posY ) {

			// Left Section
			if(posX < this.x + this.size) {
				if(posY < this.y + this.size) { return DirRotate.UpLeft; }
				if(posY > this.y + this.size * 2) { return DirRotate.DownLeft; }
				return DirRotate.Left;
			}

			// Right Section
			else if(posX > this.x + this.size * 2) {
				if(posY < this.y + this.size) { return DirRotate.UpRight; }
				if(posY > this.y + this.size * 2) { return DirRotate.DownRight; }
				return DirRotate.Right;
			}

			// Center Section
			if(posY < this.y + this.size) { return DirRotate.Up; }
			if(posY > this.y + this.size * 2) { return DirRotate.Down; }
			return DirRotate.Center;
		}

		public void OpenMenu() {
			//byte offset = (byte)WheelMenuEnum.Size + (byte)WheelMenuEnum.HalfSize;
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

			// Draw Line Divisions
			Systems.spriteBatch.Draw(Systems.tex2dBlack, new Rectangle(this.x, this.y, 2, this.height), Color.Black);
			Systems.spriteBatch.Draw(Systems.tex2dBlack, new Rectangle(this.x + this.size, this.y, 2, this.height), Color.Black);
			Systems.spriteBatch.Draw(Systems.tex2dBlack, new Rectangle(this.x + this.size * 2, this.y, 2, this.height), Color.Black);
			Systems.spriteBatch.Draw(Systems.tex2dBlack, new Rectangle(this.x + this.size * 3, this.y, 2, this.height), Color.Black);

			Systems.spriteBatch.Draw(Systems.tex2dBlack, new Rectangle(this.x, this.y, this.width, 2), Color.Black);
			Systems.spriteBatch.Draw(Systems.tex2dBlack, new Rectangle(this.x, this.y + this.size, this.width, 2), Color.Black);
			Systems.spriteBatch.Draw(Systems.tex2dBlack, new Rectangle(this.x, this.y + this.size * 2, this.width, 2), Color.Black);
			Systems.spriteBatch.Draw(Systems.tex2dBlack, new Rectangle(this.x, this.y + this.size * 3, this.width, 2), Color.Black);

			// Draw Menu Options
			this.DrawMenuOption((byte) DirRotate.UpLeft, this.x, this.y);
			this.DrawMenuOption((byte) DirRotate.Up, (short) (this.x + this.size), this.y);
			this.DrawMenuOption((byte) DirRotate.UpRight, (short) (this.x + this.size * 2), this.y);

			this.DrawMenuOption((byte) DirRotate.Left, this.x, (short) (this.y + this.size));
			this.DrawMenuOption((byte) DirRotate.Center, (short) (this.x + this.size), (short) (this.y + this.size));
			this.DrawMenuOption((byte) DirRotate.Right, (short) (this.x + this.size * 2), (short) (this.y + this.size));

			this.DrawMenuOption((byte) DirRotate.DownLeft, this.x, (short)(this.y + this.size * 2));
			this.DrawMenuOption((byte) DirRotate.Down, (short)(this.x + this.size), (short)(this.y + this.size * 2));
			this.DrawMenuOption((byte) DirRotate.DownRight, (short)(this.x + this.size * 2), (short)(this.y + this.size * 2));

			// Hovering Visual
			if(UIComponent.ComponentWithFocus is WheelMenu) {
				short mx = (short)Snap.GridFloor(this.size, Cursor.MouseX - this.x);
				short my = (short)Snap.GridFloor(this.size, Cursor.MouseY - this.y);

				Systems.spriteBatch.Draw(Systems.tex2dDarkRed, new Rectangle(this.x + mx * this.size, this.y + my * this.size, this.size, this.size), Color.White * 0.5f);
			}
		}

		public void DrawMenuOption( byte position, short posX, short posY ) {
			if(!this.menuOptions.ContainsKey(position)) { return; }
			ContextMenuOpt option = this.menuOptions[position];
			option.atlas.Draw(option.texture, posX + WheelMenu.iconOffset, posY + 20);
			Vector2 textSize = WheelMenu.font.font.MeasureString(option.text);
			WheelMenu.font.Draw(option.text, posX + (byte) WheelMenuEnum.HalfSize - (byte) Math.Floor(textSize.X * 0.5f), posY + 75, Color.Black);
		}
	}
}
