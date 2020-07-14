using Microsoft.Xna.Framework;
using Nexus.Engine;
using System;

namespace Nexus.GameEngine {

	public class UIButton : UIComponent {

		protected const string active = "Button/TextOn";
		protected const string inactive = "Button/Text";

		protected string text;
		private short xOffset;
		public Action onClick { get; protected set; }

		// onClick = delegate() { doSomething(); };
		public UIButton( UIComponent parent, string text, short posX, short posY, Action onClick ) : base(parent) {
			this.text = text;
			this.onClick = onClick;

			this.SetWidth(124);
			this.SetHeight(48);
			this.SetRelativePosition(posX, posY);

			// Prepare Center Text (X Offset)
			Vector2 textSize = Systems.fonts.baseText.font.MeasureString(this.text);
			this.xOffset = (short) Math.Floor(textSize.X * 0.5f);
		}

		public void RunTick() {
			this.MouseOver = this.GetMouseOverState();
			if(this.MouseOver == UIMouseOverState.On) {
				UIComponent.ComponentWithFocus = this;

				// Mouse Clicked
				if(Cursor.LeftMouseState == Cursor.MouseDownState.Clicked) {
					this.onClick();
				}
			}
			
			// If the Mouse just exited this component:
			//else if(this.MouseOver == UIMouseOverState.Exited) {}
		}

		public void Draw() {
			
			if(UIComponent.ComponentWithFocus == this) {
				UIHandler.atlas.Draw(UIButton.active, this.trueX, this.trueY);
				Systems.fonts.baseText.Draw(this.text, this.trueX + 62 + 1 - this.xOffset, this.trueY + 16 + 1, Color.DarkSlateGray);
			}

			else {
				UIHandler.atlas.Draw(UIButton.inactive, this.trueX, this.trueY);
				Systems.fonts.baseText.Draw(this.text, this.trueX + 62 - this.xOffset, this.trueY + 16, Color.DarkSlateGray);
			}
		}
	}
}
