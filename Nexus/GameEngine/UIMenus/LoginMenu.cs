using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nexus.Engine;

namespace Nexus.GameEngine {

	public class LoginMenu : IMenu {

		public const string passBlock = "********************************"; // 32 long (should only need UIInput.charsVisible)

		// Login Components
		private readonly TextBox textBox;
		private readonly UICreoInput loginInput;
		private readonly UICreoInput passInput;
		private readonly UICreoButton loginButton;
		private readonly UICreoButton registerButton;

		public LoginMenu(short width, short height) : base() {

			short centerX = (short)(Systems.screen.windowHalfWidth - (short)(width * 0.5));
			short centerY = (short)(Systems.screen.windowHalfHeight - (short)(height * 0.5));

			this.textBox = new TextBox(null, centerX, centerY, width, height);
			this.loginInput = new UICreoInput(this.textBox, 20, 50);
			this.passInput = new UICreoInput(this.textBox, 20, 135);
			this.loginButton = new UICreoButton(this.textBox, "Login", 20, 220, null);
			this.registerButton = new UICreoButton(this.textBox, "Register", 152, 220, null);
		}

		public void RunTick() {

			// Handle Key Presses
			InputClient input = Systems.input;

			// Check if the menu should be closed:
			if(input.LocalKeyPressed(Keys.Escape)) {
				UIHandler.SetMenu(null, false);
				return;
			}

			// Key Handling
			if(UIComponent.ComponentSelected == this.loginInput || UIComponent.ComponentSelected == this.passInput) {

				// Get Characters Pressed (doesn't assist with order)
				string charsPressed = input.GetCharactersPressed();

				UICreoInput comp = (UICreoInput)UIComponent.ComponentSelected;

				if(charsPressed.Length > 0) {
					comp.SetInputText(comp.text + charsPressed);
				}

				// Backspace (+Shift, +Control)
				if(input.LocalKeyPressed(Keys.Back)) {
					if(input.LocalKeyDown(Keys.LeftShift) || input.LocalKeyDown(Keys.RightShift) || input.LocalKeyDown(Keys.LeftControl) || input.LocalKeyDown(Keys.RightControl)) {
						comp.SetInputText("");
					} else {
						comp.SetInputText(comp.text.Substring(0, comp.text.Length - 1));
					}
				}
			}

			// Tab Between Options
			if(input.LocalKeyPressed(Keys.Tab)) {
				if(UIComponent.ComponentSelected == this.loginInput) {
					UIComponent.ComponentSelected = this.passInput;
				} else {
					UIComponent.ComponentSelected = this.loginInput;
				}
			}

			this.textBox.RunTick();
			this.loginInput.RunTick();
			this.passInput.RunTick();
			this.loginButton.RunTick();
			this.registerButton.RunTick();
		}

		public void Draw() {
			this.textBox.Draw();

			Systems.fonts.baseText.Draw("Username", this.textBox.trueX + 28, this.textBox.trueY + 26, Color.Black);
			this.loginInput.Draw();

			// LoginMenu.passBlock.Substring(0, Math.Min(UIInput.charsVisible, this.passInput.text.Length)
			Systems.fonts.baseText.Draw("Password", this.textBox.trueX + 28, this.textBox.trueY + 110, Color.Black);
			this.passInput.Draw();

			this.loginButton.Draw();
			this.registerButton.Draw();
		}
	}
}
