using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;
using System;

namespace Nexus.GameEngine {

	public class UIIconWithText : UIComponent {

		private static string BaseSprite = "UI/Button/Up";
		private static FontClass font = Systems.fonts.console;

		private Atlas atlas;
		private string SpriteName;
		private string text;
		private short xOffset;
		Action onActivate;

		// onActivate = delegate() { doSomething(); };
		public UIIconWithText( UIComponent parent, string spriteName, string text, short posX, short posY, Action onActivate ) : base(parent) {
			this.atlas = Systems.mapper.atlas[(byte)AtlasGroup.Tiles];
			this.SpriteName = spriteName;
			this.text = text;
			this.onActivate = onActivate;
			this.SetRelativePosition(posX, posY);
			this.SetWidth(56);
			this.SetHeight(56);

			Vector2 textSize = UIIconWithText.font.font.MeasureString(this.text);
			this.xOffset = (short)Math.Floor(textSize.X * 0.5f);
		}

		public void ActivateIcon() { this.onActivate(); }

		public void RunTick() {
			if(this.IsMouseOver()) {
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
				this.atlas.Draw(UIIconWithText.BaseSprite, this.trueX, this.trueY);
				this.atlas.Draw(this.SpriteName, this.trueX, this.trueY);
				UIIconWithText.font.Draw(this.text, this.trueX + 28 - this.xOffset, this.trueY + 66, Color.White);
			}

			else {
				this.atlas.DrawWithColor(UIIconWithText.BaseSprite, this.trueX, this.trueY, Color.White * 0.5f);
				this.atlas.DrawWithColor(this.SpriteName, this.trueX, this.trueY, Color.White * 0.5f);
				UIIconWithText.font.Draw(this.text, this.trueX + 28 - this.xOffset, this.trueY + 66, Color.White * 0.65f);
			}
		}
	}
}
