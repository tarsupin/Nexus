using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;
using System;

namespace Nexus.GameEngine {

	public class UICreoTextIcon : UIComponent {

		private static string BaseSprite = "Button/Up";
		private static FontClass font = Systems.fonts.console;

		private string SpriteName;
		private string text;
		private short xOffset;
		Action onActivate;

		// onActivate = delegate() { doSomething(); };
		public UICreoTextIcon( UIComponent parent, string spriteName, string text, short posX, short posY, Action onActivate ) : base(parent) {
			this.SpriteName = spriteName;
			this.text = text;
			this.onActivate = onActivate;
			this.SetWidth(56);
			this.SetHeight(56);
			this.SetRelativePosition(posX, posY);

			Vector2 textSize = UICreoTextIcon.font.font.MeasureString(this.text);
			this.xOffset = (short)Math.Floor(textSize.X * 0.5f);
		}

		public void ActivateIcon() { this.onActivate(); }

		public void RunTick() {
			this.MouseOver = this.GetMouseOverState();
			if(this.MouseOver == UIMouseOverState.On) {
				UIComponent.ComponentWithFocus = this;

				// Mouse Clicked
				if(Cursor.LeftMouseState == Cursor.MouseDownState.Clicked) {
					this.onActivate();
				}
			}

			// If the Mouse just exited this component:
			//else if(this.MouseOver == UIMouseOverState.Exited) {}
		}

		public void Draw(bool showFocus = false) {
			
			if(showFocus || UIComponent.ComponentWithFocus == this) {
				UIHandler.atlas.Draw(UICreoTextIcon.BaseSprite, this.trueX, this.trueY);
				UIHandler.atlas.Draw(this.SpriteName, this.trueX + 4, this.trueY + 4);
				UICreoTextIcon.font.Draw(this.text, this.trueX + 28 - this.xOffset, this.trueY + 66, Color.White);
			}

			else {
				UIHandler.atlas.DrawWithColor(UICreoTextIcon.BaseSprite, this.trueX, this.trueY, Color.White * 0.5f);
				UIHandler.atlas.DrawWithColor(this.SpriteName, this.trueX + 4, this.trueY + 4, Color.White * 0.5f);
				UICreoTextIcon.font.Draw(this.text, this.trueX + 28 - this.xOffset, this.trueY + 66, Color.White * 0.65f);
			}
		}
	}
}
