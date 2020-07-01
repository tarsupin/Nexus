using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;
using System;

namespace Nexus.GameEngine {

	public class UIInput : UIComponent {

		protected const string box = "UI/Input/Box";
		protected const string outline = "UI/Input/Outline";

		protected Atlas atlas;
		public Action onSubmit { get; protected set; }

		// onSubmit = delegate() { doSomething(); };
		public UIInput( UIComponent parent, short posX, short posY, Action onClick ) : base(parent) {
			this.atlas = Systems.mapper.atlas[(byte)AtlasGroup.Tiles];
			this.onSubmit = onClick;

			this.SetRelativePosition(posX, posY);
			this.SetWidth(260);
			this.SetHeight(40);
		}

		public void RunTick() {
			if(this.IsMouseOver()) {
				UIComponent.ComponentWithFocus = this;

				// Mouse Clicked
				if(Cursor.LeftMouseState == Cursor.MouseDownState.Clicked) {
					this.onSubmit();
				}
			}
			
			// If the Mouse just exited this component:
			//else if(this.MouseOver == UIMouseOverState.Exited) {}
		}

		public void Draw() {
			this.atlas.Draw(UIInput.box, this.trueX, this.trueY);

			if(UIComponent.ComponentWithFocus == this) {
				this.atlas.Draw(UIInput.outline, this.trueX, this.trueY);
			}

			Systems.fonts.console.Draw("Test This Thing", this.trueX + 17, this.trueY + 13, Color.DarkSlateGray);
		}
	}
}
