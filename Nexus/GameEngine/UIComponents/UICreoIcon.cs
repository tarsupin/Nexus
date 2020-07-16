using Nexus.Engine;
using System;

namespace Nexus.GameEngine {

	public class UICreoIcon : UIComponent {

		public const string Up = "Button/Up";
		public const string Down = "Button/Down";

		protected string SpriteName;
		public Action onClick { get; protected set; }

		// Used for Tool Tips (Optional)
		protected string title;
		protected string desc;

		// onClick = delegate() { doSomething(); };
		public UICreoIcon( UIComponent parent, string spriteName, short posX, short posY, Action onClick, string title, string desc ) : base(parent) {
			this.SpriteName = spriteName;
			this.onClick = onClick;
			this.SetWidth(56);
			this.SetHeight(56);
			this.SetRelativePosition(posX, posY);

			// Apply ToolTip Text
			this.title = title;
			this.desc = desc;
		}

		public void UpdateSprite(string spriteName) {
			this.SpriteName = spriteName;
		}

		public void RunTick() {
			this.MouseOver = this.GetMouseOverState();
			if(this.MouseOver == UIMouseOverState.On) {
				UIComponent.ComponentWithFocus = this;

				// Run Tool TIp
				if(this.title.Length > 0) {
					UIHandler.RunToolTip(this.title, this.title, this.desc, UIPrimaryDirection.None);
				}

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
				UIHandler.atlas.Draw(UICreoIcon.Down, this.trueX, this.trueY);
				UIHandler.atlas.Draw(this.SpriteName, this.trueX + 5, this.trueY + 5);
			}

			else {
				UIHandler.atlas.Draw(UICreoIcon.Up, this.trueX, this.trueY);
				UIHandler.atlas.Draw(this.SpriteName, this.trueX + 4, this.trueY + 4);
			}
		}
	}
}
