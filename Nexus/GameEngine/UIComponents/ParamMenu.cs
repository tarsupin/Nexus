using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nexus.GameEngine {

	public class ParamMenu : UIComponent {

		protected static byte SlotHeight = 30;      // The height of each slot.

		public bool actionParams;           // FALSE if this is the "Move Param" menu, TRUE if this is the "Action Param" menu.
		public Params paramSet;				// The param set loaded into this menu.

		protected short splitPos;			// The "central" section in the middle that splits the left and right sides.
		protected short leftWidth;			// The amount of width for the left side (titles)
		protected short rightWidth;		// The amount of width for the right side (integers, percents, labels, etc)
		protected static FontClass font;

		// Basic Menu Information
		public byte numberOptsToShow = 1;        // Number of Menu Options to Show
		public sbyte optionSelected = 0;         // Current Menu Option Highlighted

		// The Menu Options Displayed
		// Stored for speed, as well as because it can change dynamically.
		public string[] menuOptLabels = new string[13] { "", "", "", "", "", "", "", "", "", "", "", "", "" };
		public string[] menuOptText = new string[13] { "", "", "", "", "", "", "", "", "", "", "", "", "" };

		// Rules Currently Loaded
		// This is important because some rules might not be loaded at times, such as with the "Flight" wand menu - rules change based on the Flight type.
		public byte[] menuOptRuleIds = new byte[13] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

		public ParamMenu( UIComponent parent, bool actionParams = false ) : base(parent) {

			this.actionParams = actionParams;

			// Ensure Font has been set.
			if(ParamMenu.font is FontClass == false) {
				ParamMenu.font = Systems.fonts.console;
			}

			this.SetVisible(false);
		}

		// Rebuild the ParamMenu design:
		public void LoadParamMenu() {
			this.SetVisible(false);

			// If the wand returned params, check if this menu corresponds to the params shown:
			if(this.actionParams) {
				if(WandData.actParamSet == null) { return; }
				this.paramSet = WandData.actParamSet;
			} else {
				if(WandData.moveParamSet == null) { return; }
				this.paramSet = WandData.moveParamSet;
			}

			this.ResizeMenu(true);
			this.SetVisible(true);

			// Update the Menu Options (custom, if applicable)
			if(!this.paramSet.RunCustomMenuUpdate()) {
				this.UpdateMenuOptions();
			}
		}

		private void ResizeMenu(bool fullResize) {

			if(fullResize) {
				this.leftWidth = this.GetLeftWidth();
				this.rightWidth = this.GetRightWidth();
				this.width = (short)(this.leftWidth + 20 + this.rightWidth);
			}

			this.height = (short)(this.numberOptsToShow * ParamMenu.SlotHeight - 1);

			// x, y describes the top-left corner of the context menu.
			// Will reposition the menu if it would overlap other content:
			if(fullResize) {

				// Readjust x coord, since we're right-aligning it.
				if(this.actionParams) {
					this.x = (short)((WandData.gridX * (byte)TilemapEnum.TileWidth) - Systems.camera.posX + (byte)TilemapEnum.TileWidth);
				} else {
					this.x = (short)((WandData.gridX * (byte)TilemapEnum.TileWidth) - Systems.camera.posX - this.width);
					if(this.x < 100) { this.x = 100; }
				}
				this.splitPos = (short)(this.x + this.leftWidth + 10);
			}

			this.y = (short)((WandData.gridY * (byte)TilemapEnum.TileHeight) - Systems.camera.posY);
			if(this.y < 50) { this.y = 50; }
			if(this.y + this.height > Systems.screen.windowHeight - 50) { this.y = (short)(Systems.screen.windowHeight - 50 - this.height); }
		}

		// Identifies the width that the left side should be by determining the width of the largest string.
		private short GetLeftWidth() {
			short largestWidth = 0;

			for(byte i = 0; i < this.paramSet.rules.Count; i++) {
				short w = (short) ParamMenu.font.font.MeasureString(this.paramSet.rules[i].name).X;
				if(w > largestWidth) { largestWidth = w; }
			}

			return (short) (largestWidth + 50);
		}

		// Identifies the width that the left side should be by determining the width of the largest string.
		private short GetRightWidth() {
			short rightWidth = 140;

			foreach(ParamGroup group in this.paramSet.rules) {
				if(group is LabeledParam) {
					rightWidth += 80;
					break;
				}
			}

			return rightWidth;
		}

		// Update Menu Options (run when menu changes)
		public void UpdateMenuOptions(byte numOptsToShow = 0, byte[] optRuleIds = null) {
			if(!this.visible) { return; }

			Dictionary<string, short> paramList = WandData.GetAllParamsOnTile();

			// Get Rules
			List<ParamGroup> rules = this.paramSet.rules;

			// Prepare Menu Options
			this.numberOptsToShow = (numOptsToShow == 0 ? (byte)rules.Count : numOptsToShow);

			// Determine the Rule IDs that appear in the Option List. The default is just to list each rule in order.
			// Some wand menus (such as Flight and Chest) will have different sequences that must be sent to this method.
			// The reason for this is because there are certain rules that will affect others. Such as Flight Type of Rotation making the rotating diameter value visible.
			for(byte i = 0; i < this.numberOptsToShow; i++) {
				this.menuOptRuleIds[i] = optRuleIds == null ? i : optRuleIds[i];
			}

			// Loop through each rule:
			for(byte i = 0; i < this.numberOptsToShow; i++) {
				byte ruleId = this.menuOptRuleIds[i];
				ParamGroup rule = rules[ruleId];

				this.menuOptLabels[i] = rule.name;

				// Determine the Text
				if(paramList != null && paramList.ContainsKey(rule.key)) {

					// Labeled Params
					if(rule is LabeledParam) {
						this.menuOptText[i] = ((LabeledParam)(rule)).labels[short.Parse(paramList[rule.key].ToString())];
					}

					// Dictionary Params
					else if(rule is DictParam) {
						DictParam dictRule = (DictParam)(rule);
						byte[] contentKeys = dictRule.dict.Keys.ToArray<byte>();
						byte paramVal = byte.Parse(paramList[rule.key].ToString());
						this.menuOptText[i] = dictRule.dict[contentKeys[paramVal]];
					}

					// Frame Params (show them as milliseconds, rather than by frames)
					else if(rule is FrameParam) {
						int newVal = short.Parse(paramList[rule.key].ToString()) * 1000 / 60;
						this.menuOptText[i] = newVal.ToString() + " ms";
					}

					// Default Numeric Params
					else {
						this.menuOptText[i] = paramList[rule.key].ToString() + rule.unitName;
					}
				}
				
				// Display Default String
				else {

					// Default Rule for Frame Params is still altered.
					if(rule is FrameParam) {
						this.menuOptText[i] = (rule.defValue * 1000 / 60).ToString() + " ms";
					} else {
						this.menuOptText[i] = rule.defStr;
					}
				}

				// Resize Menu after any update.
				this.ResizeMenu(false);
			}
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

				this.optionSelected = this.GetMenuSelection(Cursor.MouseY);
				if(this.optionSelected < 0) { return; }

				// Cycle through parameter using mouse scroll:
				if(Cursor.MouseScrollDelta != 0) {

					// Get the parameter key (e.g. "speed", "count", etc)
					string paramKey = this.paramSet.GetParamKey((byte) this.menuOptRuleIds[(byte)this.optionSelected]);

					// Increment the parameter value by 1 step:
					bool ranCustomMenuUpdate = this.paramSet.CycleParam(paramKey, Cursor.MouseScrollDelta < 0);
					if(!ranCustomMenuUpdate) { this.UpdateMenuOptions(); }
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

			Dictionary<string, short> paramList = WandData.GetAllParamsOnTile();

			// Draw Line Divisions & Menu Options
			byte count = (byte) this.numberOptsToShow;

			// Draw White Background
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.x, this.y, this.width, this.height), Color.White * 0.75f);

			// Loop through vertical set:
			for(byte i = 0; i < count; i++) {

				string label = this.menuOptLabels[i];
				string text = this.menuOptText[i];

				// Draw Line
				Systems.spriteBatch.Draw(Systems.tex2dBlack, new Rectangle(this.x, this.y + ParamMenu.SlotHeight * i, this.width, 2), Color.Black);

				// Set this line as green to indicate that it's not a default value (unless currently being highlighted):
				if(this.optionSelected != i && paramList != null && paramList.ContainsKey(this.paramSet.rules[(byte) this.menuOptRuleIds[i]].key)) {
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
			if(this.optionSelected > -1) {
				Systems.spriteBatch.Draw(Systems.tex2dDarkRed, new Rectangle(this.x, this.y + ParamMenu.SlotHeight * this.optionSelected, this.width, ParamMenu.SlotHeight), Color.White * 0.5f);
			}
		}
	}
}
