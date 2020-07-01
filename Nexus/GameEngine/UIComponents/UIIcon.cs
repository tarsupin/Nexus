using Nexus.Engine;
using Nexus.Gameplay;
using System;

namespace Nexus.GameEngine {

	public class UIIcon : UIComponent {

		protected static string[] ButtonSprite = new string[2] { "UI/Button/Up", "UI/Button/Down" };

		protected Atlas atlas;
		protected string SpriteName;
		public Action onClick { get; protected set; }

		// onClick = delegate() { doSomething(); };
		public UIIcon( UIComponent parent, string spriteName, short posX, short posY, Action onClick ) : base(parent) {
			this.atlas = Systems.mapper.atlas[(byte)AtlasGroup.Tiles];
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
				this.atlas.Draw(UIIcon.ButtonSprite[1], this.trueX, this.trueY);
				this.atlas.Draw(this.SpriteName, this.trueX + 1, this.trueY + 1);
			}

			else {
				this.atlas.Draw(UIIcon.ButtonSprite[0], this.trueX, this.trueY);
				this.atlas.Draw(this.SpriteName, this.trueX, this.trueY);
			}
		}
	}
}
