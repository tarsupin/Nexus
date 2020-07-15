﻿using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class UIInput : UIComponent {

		public const byte charsVisible = 26;
		protected const string box = "Input/Box";
		protected const string over = "Input/Over";
		protected const string outline = "Input/Outline";

		private readonly short maxLen;
		public string text { get; protected set; }

		public UIInput( UIComponent parent, short posX, short posY, short maxLen = 45 ) : base(parent) {
			this.maxLen = maxLen;
			this.text = "";

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
		}

		public void Draw() {

			string cursor = "";

			if(UIComponent.ComponentWithFocus == this || UIComponent.ComponentSelected == this) {
				UIHandler.atlas.Draw(UIInput.over, this.trueX, this.trueY);

				if(UIComponent.ComponentSelected == this) {
					UIHandler.atlas.Draw(UIInput.outline, this.trueX, this.trueY);

					if(Systems.timer.beat4Modulus % 2 == 0) { cursor = "|"; }
				}

			} else {
				UIHandler.atlas.Draw(UIInput.box, this.trueX, this.trueY);
			}

			if(this.text.Length > UIInput.charsVisible) {
				Systems.fonts.console.Draw(this.text.Substring(this.text.Length - UIInput.charsVisible) + cursor, this.trueX + 17, this.trueY + 13, Color.DarkSlateGray);
			} else {
				Systems.fonts.console.Draw(this.text + cursor, this.trueX + 17, this.trueY + 13, Color.DarkSlateGray);
			}
		}
	}
}
