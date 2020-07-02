using Microsoft.Xna.Framework.Input;
using Nexus.Engine;

namespace Nexus.GameEngine {

	public class LoginMenu : IMenu {

		// Login Components
		private readonly TextBox textBox;
		private readonly UIInput loginInput;
		private readonly UIInput passInput;
		private readonly UIButton loginButton;
		private readonly UIButton registerButton;

		public LoginMenu( short posX, short posY, short width, short height ) : base() {
			this.textBox = new TextBox(null, posX, posY, width, height);
			this.loginInput = new UIInput(this.textBox, 20, 20);
			this.passInput = new UIInput(this.textBox, 20, 120);
			this.loginButton = new UIButton(this.textBox, "Login", 20, 220, null);
			this.registerButton = new UIButton(this.textBox, "Register", 152, 220, null);
		}

		public void RunTick() {

			// Key Handling
			if(UIComponent.ComponentSelected == this.loginInput || UIComponent.ComponentSelected == this.passInput) {

				// Handle Key Presses
				InputClient input = Systems.input;

				// Get Characters Pressed (doesn't assist with order)
				string charsPressed = input.GetCharactersPressed();

				if(charsPressed.Length > 0) {
					((UIInput)UIComponent.ComponentSelected).SetText(((UIInput)UIComponent.ComponentSelected).text + charsPressed);
				}

				// Determine if the console needs to be closed (escape or tilde):
				if(input.LocalKeyPressed(Keys.Tab)) {
					if(UIComponent.ComponentSelected == this.loginInput) {
						UIComponent.ComponentSelected = this.passInput;
					} else {
						UIComponent.ComponentSelected = this.loginInput;
					}
				}
			}

			// Mouse Handling
			if(this.textBox.IsMouseOver()) {
				UIComponent.ComponentWithFocus = this.textBox;

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
