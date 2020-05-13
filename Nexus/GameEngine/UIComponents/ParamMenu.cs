using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nexus.Engine;
using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class ParamMenu : UIComponent {

		private Params paramGroup;		// The parameter group to use.
		private ushort splitPos;		// The "central" section in the middle that splits the left and right sides.
		private ushort leftWidth;		// The amount of width for the left side (titles)
		private ushort rightWidth;		// The amount of width for the right side (integers, percents, labels, etc)
		private static FontClass font;

		public ParamMenu( UIComponent parent, string paramKey, ushort posX, ushort posY ) : base(parent) {

			// Ensure Font has been set.
			if(ParamMenu.font is FontClass == false) {
				ParamMenu.font = Systems.fonts.console;
			}

			this.LoadMenu(paramKey, posX, posY);
		}

		// Rebuild the ParamMenu design:
		public void LoadMenu(string paramKey, ushort posX, ushort posY) {

			// Get Parameter List
			this.paramGroup = Params.paramMap[paramKey];

			this.leftWidth = this.GetLeftWidth();
			this.rightWidth = this.GetRightWidth();

			this.width = (short)(this.leftWidth + 20 + this.rightWidth);
			this.height = (short)(this.paramGroup.rules.Count * 50);

			// posX, posY describes the center of the context menu.
			// x, y describes the top-left corner of the context menu.
			this.x = (short)(posX - this.width);		// Only need to readjust x coord, since we're right-aligning it.
			this.y = (short)(posY);
			this.splitPos = (ushort) (posX + this.leftWidth + 10);
		}

		// Identifies the width that the left side should be by determining the width of the largest string.
		private ushort GetLeftWidth() {
			var rules = this.paramGroup.rules;
			ushort largestWidth = 0;

			foreach(string paramKey in rules.Keys) {
				ushort w = (ushort) ParamMenu.font.font.MeasureString(rules[paramKey].name).X;
				if(w > largestWidth) { largestWidth = w; }
			}

			return (ushort) (largestWidth + 50);
		}

		// Identifies the width that the left side should be by determining the width of the largest string.
		private ushort GetRightWidth() {
			var rules = this.paramGroup.rules;
			ushort rightWidth = 80;

			foreach(ParamGroup group in rules.Values) {
				if(group is LabeledParam || group is DictionaryParam) {
					rightWidth += 80;
					break;
				}
			}

			return rightWidth;
		}

		public void RunTick() {

			// End method if the context menu isn't visible, or if the tab key was released.
			if(!this.visible) { return; }
			else if(!Systems.input.LocalKeyDown(Keys.Tab)) { this.CloseMenu(); return; }

			if(this.IsMouseOver()) {
				UIComponent.ComponentWithFocus = this;

				if(Cursor.LeftMouseState == Cursor.MouseDownState.Clicked) {
					EditorUI.currentSlotGroup = this.GetContextOpt(Cursor.MouseX, Cursor.MouseY);
					EditorTools.SetTileToolBySlotGroup(EditorUI.currentSlotGroup);
					EditorTools.UpdateHelperText();
					this.CloseMenu();
					return;
				}
			}
		}

		// Identify which menu option is at a specific location:
		public byte GetContextOpt( int posX, int posY ) {
			return 0;
		}

		public void OpenMenu() {
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
			byte count = (byte) this.paramGroup.rules.Count;

			// Loop through vertical set:
			for(byte i = 0; i <= count; i++) {
				Systems.spriteBatch.Draw(Systems.tex2dBlack, new Rectangle(this.x, this.y + this.leftWidth * i, this.width, 2), Color.Black);
			}

			// TODO: ADD BUTTONS / WORDS
			//Vector2 textSize = ParamMenu.font.font.MeasureString(option.text);
			//ParamMenu.font.Draw(option.text, posX + (byte)ParamMenuEnum.HalfSize - (byte)Math.Floor(textSize.X * 0.5f), posY + 75, Color.Black);

			// Hovering Visual
			if(UIComponent.ComponentWithFocus is ParamMenu) {
				short mx = (short)Snap.GridFloor(this.leftWidth, Cursor.MouseX - this.x);
				short my = (short)Snap.GridFloor(this.leftWidth, Cursor.MouseY - this.y);

				Systems.spriteBatch.Draw(Systems.tex2dDarkRed, new Rectangle(this.x + mx * this.leftWidth, this.y + my * this.leftWidth, this.leftWidth, this.leftWidth), Color.White * 0.5f);
			}
		}
	}
}
