using Nexus.Engine;
using System;

namespace Nexus.GameEngine {

	public class UIIcon : UIComponent {

		public const string Up = "Button/Up";
		public const string Down = "Button/Down";

		protected string SpriteName;
		public Action onClick { get; protected set; }

		// onClick = delegate() { doSomething(); };
		public UIIcon( UIComponent parent, string spriteName, short posX, short posY, Action onClick ) : base(parent) {
			this.SpriteName = spriteName;
			this.onClick = onClick;
			this.SetRelativePosition(posX, posY);
			this.SetWidth(56);
			this.SetHeight(56);
		}

		public void UpdateSprite(string spriteName) {
			this.SpriteName = spriteName;
		}

		public void RunTick() {
			if(this.IsMouseOver()) {
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
				UIHandler.atlas.Draw(UIIcon.Down, this.trueX, this.trueY);
				UIHandler.atlas.Draw(this.SpriteName, this.trueX + 5, this.trueY + 5);
			}

			else {
				UIHandler.atlas.Draw(UIIcon.Up, this.trueX, this.trueY);
				UIHandler.atlas.Draw(this.SpriteName, this.trueX + 4, this.trueY + 4);
			}
		}
	}
}
