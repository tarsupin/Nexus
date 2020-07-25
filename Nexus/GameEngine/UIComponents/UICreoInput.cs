using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nexus.Engine;
using Nexus.Gameplay;
using System;

namespace Nexus.GameEngine {

	public class UICreoInput : UIComponent {

		private const string passBlock = "********************************"; // 32 long (should only need UIInput.charsVisible)

		public const byte charsVisible = 26;
		protected const string box = "Input/Box";
		protected const string over = "Input/Over";
		protected const string outline = "Input/Outline";

		private readonly short maxLen;
		private readonly bool isPassword = false;
		public string text { get; protected set; }

		private int lastBack = 0;

		public UICreoInput( UIComponent parent, short posX, short posY, short maxLen = 45, bool isPassword = false ) : base(parent) {
			this.maxLen = maxLen;
			this.text = "";
			this.isPassword = isPassword;

			this.SetWidth(260);
			this.SetHeight(40);
			this.SetRelativePosition(posX, posY);
		}

		public void SetInputText(string newText) {
			if(newText.Length > this.maxLen) { return; }
			this.text = newText;
		}

		public void RunTick() {

			// Mouse Handling
			this.MouseOver = this.GetMouseOverState();
			if(this.MouseOver == UIMouseOverState.On) {
				UIComponent.ComponentWithFocus = this;

				// Mouse Clicked
				if(Cursor.LeftMouseState == Cursor.MouseDownState.Clicked) {
					UIComponent.ComponentSelected = this;
				}
			}

			// If the Mouse just exited this component:
			//else if(this.MouseOver == UIMouseOverState.Exited) {}

			// Key Handling
			if(UIComponent.ComponentSelected == this) {
				InputClient input = Systems.input;

				// Get Characters Pressed (doesn't assist with order)
				string charsPressed = input.GetCharactersPressed();

				UICreoInput comp = (UICreoInput) UIComponent.ComponentSelected;

				if(charsPressed.Length > 0) {
					comp.SetInputText(comp.text + charsPressed);
				}

				// Backspace (+Shift, +Control)
				if(input.LocalKeyDown(Keys.Back) && this.lastBack < Systems.timer.UniFrame) {

					if(input.LocalKeyPressed(Keys.Back)) {
						this.lastBack = Systems.timer.UniFrame + 10;
					} else {
						this.lastBack = Systems.timer.UniFrame + 4;
					}

					if(input.LocalKeyDown(Keys.LeftShift) || input.LocalKeyDown(Keys.RightShift) || input.LocalKeyDown(Keys.LeftControl) || input.LocalKeyDown(Keys.RightControl)) {
						comp.SetInputText("");
					} else if(comp.text.Length > 0) {
						comp.SetInputText(comp.text.Substring(0, comp.text.Length - 1));
					}
				}
			}
		}

		public void Draw() {

			string cursor = "";

			if(UIComponent.ComponentWithFocus == this || UIComponent.ComponentSelected == this) {
				UIHandler.atlas.Draw(UICreoInput.over, this.trueX, this.trueY);

				if(UIComponent.ComponentSelected == this) {
					UIHandler.atlas.Draw(UICreoInput.outline, this.trueX, this.trueY);

					if(Systems.timer.beat4Modulus % 2 == 0) { cursor = "|"; }
				}

			} else {
				UIHandler.atlas.Draw(UICreoInput.box, this.trueX, this.trueY);
			}

			string text = this.isPassword ? UICreoInput.passBlock.Substring(0, Math.Min(30, this.text.Length)) : this.text;

			if(text.Length > UICreoInput.charsVisible) {
				Systems.fonts.console.Draw(text.Substring(text.Length - UICreoInput.charsVisible) + cursor, this.trueX + 17, this.trueY + 13, Color.DarkSlateGray);
			} else {
				Systems.fonts.console.Draw(text + cursor, this.trueX + 17, this.trueY + 13, Color.DarkSlateGray);
			}
		}
	}
}
