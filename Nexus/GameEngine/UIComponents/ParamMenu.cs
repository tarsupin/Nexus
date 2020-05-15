using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json.Linq;
using Nexus.Engine;
using Nexus.Gameplay;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class ParamMenu : UIComponent {

		protected static byte SlotHeight = 30;		// The height of each slot.

		protected ushort splitPos;			// The "central" section in the middle that splits the left and right sides.
		protected ushort leftWidth;			// The amount of width for the left side (titles)
		protected ushort rightWidth;		// The amount of width for the right side (integers, percents, labels, etc)
		protected static FontClass font;

		public ParamMenu( UIComponent parent ) : base(parent) {

			// Ensure Font has been set.
			if(ParamMenu.font is FontClass == false) {
				ParamMenu.font = Systems.fonts.console;
			}

			this.SetVisible(false);
		}

		// Rebuild the ParamMenu design:
		public void LoadParamMenu(EditorRoomScene scene, ushort gridX, ushort gridY) {
			bool validWand = WandData.InitializeWandData(scene, gridX, gridY);
			if(validWand == false) { return; }

			// Get Sizing Details
			this.leftWidth = this.GetLeftWidth();
			this.rightWidth = this.GetRightWidth();

			this.width = (short)(this.leftWidth + 20 + this.rightWidth);
			this.height = (short)(WandData.paramRules.Length * ParamMenu.SlotHeight - 1);

			// posX, posY describes the center of the context menu.
			// x, y describes the top-left corner of the context menu.
			this.x = (short)((gridX * (byte) TilemapEnum.TileWidth) - Systems.camera.posX - this.width);		// Only need to readjust x coord, since we're right-aligning it.
			this.y = (short)((gridY * (byte) TilemapEnum.TileHeight) - Systems.camera.posY);

			// Reposition the menu if it would overlap other content:
			if(this.x < 100) { this.x = 100; }
			if(this.y < 50) { this.y = 50; }
			if(this.y + this.height > Systems.screen.windowHeight - 50) { this.y = (short) (Systems.screen.windowHeight - 50 - this.height); }

			this.splitPos = (ushort) (this.x + this.leftWidth + 10);

			// Update Menu Options
			WandData.UpdateMenuOptions();

			// Update Menu Visibility
			this.SetVisible(true);
		}

		// Identifies the width that the left side should be by determining the width of the largest string.
		private ushort GetLeftWidth() {
			ushort largestWidth = 0;

			for(byte i = 0; i < WandData.paramRules.Length; i++) {
				ushort w = (ushort) ParamMenu.font.font.MeasureString(WandData.paramRules[i].name).X;
				if(w > largestWidth) { largestWidth = w; }
			}

			return (ushort) (largestWidth + 50);
		}

		// Identifies the width that the left side should be by determining the width of the largest string.
		private ushort GetRightWidth() {
			ushort rightWidth = 140;

			foreach(ParamGroup group in WandData.paramRules) {
				if(group is LabeledParam) {
					rightWidth += 80;
					break;
				}
			}

			return rightWidth;
		}

		public void RunTick() {

			// End method if the context menu isn't visible, or if the tab key was released.
			if(!this.visible) { return; }

			// If the current tool switches away from the wand, close the menu:
			if(EditorTools.tempTool is FuncToolWand == false && EditorTools.funcTool is FuncToolWand == false) {
				this.CloseMenu();
				return;
			}

			if(this.IsMouseOver()) {
				UIComponent.ComponentWithFocus = this;

				WandData.optionSelected = this.GetMenuSelection(Cursor.MouseY);
				if(WandData.optionSelected < 0) { return; }

				// Cycle through parameter using mouse scroll:
				if(Cursor.MouseScrollDelta != 0) {

					// Get the parameter key (e.g. "speed", "count", etc)
					string paramKey = WandData.paramSet.GetParamKey((byte) WandData.optionSelected);

					// Increment the parameter value by 1 step:
					WandData.paramSet.CycleParam(paramKey, Cursor.MouseScrollDelta < 0);
				}
			}
		}

		// Identify which menu option is at a specific location:
		public sbyte GetMenuSelection( int posY ) {
			if(UIComponent.ComponentWithFocus is ParamMenu == false) { return -1; }
			short distFromTop = (short) (posY - this.y);
			if(distFromTop <= ParamMenu.SlotHeight) { return 0; }
			return (sbyte) Math.Floor((decimal) (distFromTop / ParamMenu.SlotHeight));
		}

		public void OpenMenu() {
			this.SetVisible(true);
		}

		public void CloseMenu() {
			this.SetVisible(false);
		}

		public virtual void Draw() {
			if(!this.visible) { return; }

			// If the LevelContent Tile exists and has data, draw it:
			ArrayList tileObj = WandData.wandTileData;
			JObject paramList = null;

			if(tileObj.Count > 2 && tileObj[2] is JObject) {
				paramList = (JObject) tileObj[2];
			}

			// Draw Line Divisions & Menu Options
			byte count = (byte) WandData.optionsToShow;

			// Draw White Background
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.x, this.y, this.width, this.height), Color.White * 0.75f);

			// Loop through vertical set:
			for(byte i = 0; i < count; i++) {

				string label = WandData.menuOptionLabels[i];
				string text = WandData.menuOptionText[i];

				// Draw Line
				Systems.spriteBatch.Draw(Systems.tex2dBlack, new Rectangle(this.x, this.y + ParamMenu.SlotHeight * i, this.width, 2), Color.Black);

				// Set this line as green to indicate that it's not a default value (unless currently being highlighted):
				if(WandData.optionSelected != i && paramList != null && paramList.ContainsKey(WandData.paramRules[(byte) WandData.menuOptionRule[i]].key)) {
					Systems.spriteBatch.Draw(Systems.tex2dDarkGreen, new Rectangle(this.x, this.y + ParamMenu.SlotHeight * i, this.width, ParamMenu.SlotHeight), Color.White * 0.2f);
				}

				// Draw Label + Text
				Vector2 textSize = ParamMenu.font.font.MeasureString(label);
				ParamMenu.font.Draw(label, this.splitPos - 10 - (byte) Math.Floor(textSize.X), this.y + ParamMenu.SlotHeight * i + 8, Color.Black);
				ParamMenu.font.Draw(text, this.splitPos + 10, this.y + ParamMenu.SlotHeight * i + 8, Color.Black);
			}

			// Draw Bottom Line
			Systems.spriteBatch.Draw(Systems.tex2dBlack, new Rectangle(this.x, this.y + ParamMenu.SlotHeight * count, this.width, 2), Color.Black);

			// Hovering Visual
			if(WandData.optionSelected > -1) {
				Systems.spriteBatch.Draw(Systems.tex2dDarkRed, new Rectangle(this.x, this.y + ParamMenu.SlotHeight * WandData.optionSelected, this.width, ParamMenu.SlotHeight), Color.White * 0.5f);
			}
		}
	}
}
