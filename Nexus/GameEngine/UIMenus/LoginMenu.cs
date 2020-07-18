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
					} else if(comp.text.Length > 0) {
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

			// Clicked on Login Button.
			if(UIComponent.ComponentWithFocus == this.loginButton && Cursor.LeftMouseState == Cursor.MouseDownState.Clicked) {
				this.RunLoginAttemmpt();
			}

			this.registerButton.RunTick();

			// Clicked on Login Button.
			if(UIComponent.ComponentWithFocus == this.registerButton && Cursor.LeftMouseState == Cursor.MouseDownState.Clicked) {
				this.RunRegisterAttemmpt();
			}
		}

		// Close the menu, run a login attempt, and notify of results.
		public async void RunLoginAttemmpt() {
			UIHandler.SetMenu(null, false);

			bool loginAttempt = await WebHandler.LoginRequest(this.loginInput.text, this.passInput.text);

			if(loginAttempt) {
				UIHandler.AddNotification(UIAlertType.Success, "Logged In", "You have successfully logged into Creo.", 180);
			}

			else {
				UIHandler.AddNotification(UIAlertType.Error, "Login Failed", WebHandler.ResponseMessage, 300);
			}
		}

		// Close the menu, run a registration attempt, and notify of results.
		public async void RunRegisterAttemmpt() {
			UIHandler.SetMenu(null, false);
			bool registerAttempt = await WebHandler.RegisterRequest("NEEDEMAIL", this.loginInput.text, this.passInput.text);

			if(registerAttempt) {
				UIHandler.AddNotification(UIAlertType.Success, "Successfully Registered", "You have successfully joined Creo with the account `" + this.loginInput.text + "`.", 300);
			} else {
				UIHandler.AddNotification(UIAlertType.Error, "Registration Failed", WebHandler.ResponseMessage, 300);
			}
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
