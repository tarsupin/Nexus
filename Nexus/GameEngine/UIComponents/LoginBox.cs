using Nexus.Engine;

namespace Nexus.GameEngine {

	public class LoginBox : UIComponent {

		// Login Components
		private readonly TextBox textBox;
		private readonly UIInput loginInput;
		private readonly UIInput passInput;
		private readonly UIButton loginButton;
		private readonly UIButton registerButton;

		public LoginBox( UIComponent parent, short posX, short posY, short width, short height ) : base(parent) {

			this.SetRelativePosition(posX, posY);
			this.SetWidth(width);
			this.SetHeight(height);

			this.textBox = new TextBox(this, 0, 0, width, height);
			this.loginInput = new UIInput(this, 20, 20, null);
			this.passInput = new UIInput(this, 20, 120, null);
			this.loginButton = new UIButton(this, "Login", 20, 220, null);
			this.registerButton = new UIButton(this, "Register", 152, 220, null);
		}

		public void RunTick() {
			if(this.IsMouseOver()) {
				UIComponent.ComponentWithFocus = this;

				// Mouse Clicked
				if(Cursor.LeftMouseState == Cursor.MouseDownState.Clicked) {

				}
			}

			// If the Mouse just exited this component:
			//else if(this.MouseOver == UIMouseOverState.Exited) {}

			//this.textBox.RunTick();
			this.loginInput.RunTick();
			this.passInput.RunTick();
			this.loginButton.RunTick();
			this.registerButton.RunTick();
		}

		public void Draw() {
			this.textBox.Draw();
			this.loginInput.Draw();
			this.passInput.Draw();
			this.loginButton.Draw();
			this.registerButton.Draw();
		}
	}
}
