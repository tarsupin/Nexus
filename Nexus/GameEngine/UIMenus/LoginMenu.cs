﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nexus.Engine;

namespace Nexus.GameEngine {

	public class LoginMenu : IMenu {

		// Login Components
		private readonly TextBox textBox;
		private readonly UICreoInput loginInput;
		private readonly UICreoInput emailInput;
		private readonly UICreoInput passInput;
		private readonly UICreoButton loginButton;
		private readonly UICreoButton registerButton;

		public LoginMenu(short width, short height) : base() {

			short centerX = (short)(Systems.screen.viewHalfWidth - (short)(width * 0.5));
			short centerY = (short)(Systems.screen.viewHalfHeight - (short)(height * 0.5));

			this.textBox = new TextBox(null, centerX, centerY, width, height);
			this.loginInput = new UICreoInput(this.textBox, 20, 50, 22);
			this.emailInput = new UICreoInput(this.textBox, 20, 134);
			this.passInput = new UICreoInput(this.textBox, 20, 218, 45, true);
			this.loginButton = new UICreoButton(this.textBox, "Login", 20, 302, null);
			this.registerButton = new UICreoButton(this.textBox, "Register", 152, 302, null);
		}

		public void ShowMenu() {
			UIComponent.ComponentSelected = this.loginInput;
		}

		public void RunTick() {

			// Handle Key Presses
			InputClient input = Systems.input;

			// Check if the menu should be closed:
			if(input.LocalKeyPressed(Keys.Escape)) {
				UIHandler.SetMenu(null, false);
				return;
			}

			// Tab Between Options
			if(input.LocalKeyPressed(Keys.Tab)) {
				if(UIComponent.ComponentSelected == this.loginInput) {
					UIComponent.ComponentSelected = this.emailInput;
				} else if(UIComponent.ComponentSelected == this.emailInput) {
					UIComponent.ComponentSelected = this.passInput;
				} else {
					UIComponent.ComponentSelected = this.loginInput;
				}
			}

			this.textBox.RunTick();
			this.loginInput.RunTick();
			this.emailInput.RunTick();
			this.passInput.RunTick();
			this.loginButton.RunTick();

			// Clicked on Login Button.
			if(UIComponent.ComponentWithFocus == this.loginButton && Cursor.LeftMouseState == Cursor.MouseDownState.Clicked) {
				UIHandler.AddNotification(UIAlertType.Warning, "Attempting Login", "Please wait while a login is attempted...", 180);
				this.RunLoginAttemmpt();
			}

			this.registerButton.RunTick();

			// Clicked on Register Button.
			if(UIComponent.ComponentWithFocus == this.registerButton && Cursor.LeftMouseState == Cursor.MouseDownState.Clicked) {
				UIHandler.AddNotification(UIAlertType.Warning, "Attempting Registration", "Please wait while registration is attempted...", 180);
				this.RunRegisterAttemmpt();
			}
		}

		// Close the menu, run a login attempt, and notify of results.
		public async void RunLoginAttemmpt() {
			UIHandler.SetMenu(null, false);

			bool loginAttempt = await WebHandler.LoginRequest(this.loginInput.text, this.passInput.text);

			if(loginAttempt) {
				UIHandler.AddNotification(UIAlertType.Success, "Logged In", "You have successfully logged into Creo.", 300);
			}

			else {
				UIHandler.AddNotification(UIAlertType.Error, "Login Failed", WebHandler.ResponseMessage, 300);
			}
		}

		// Close the menu, run a registration attempt, and notify of results.
		public async void RunRegisterAttemmpt() {
			UIHandler.SetMenu(null, false);
			bool registerAttempt = await WebHandler.RegisterRequest(this.emailInput.text, this.loginInput.text, this.passInput.text);

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
			
			Systems.fonts.baseText.Draw("Email", this.textBox.trueX + 28, this.textBox.trueY + 110, Color.Black);
			this.emailInput.Draw();

			// LoginMenu.passBlock.Substring(0, Math.Min(UIInput.charsVisible, this.passInput.text.Length)
			Systems.fonts.baseText.Draw("Password", this.textBox.trueX + 28, this.textBox.trueY + 194, Color.Black);
			this.passInput.Draw();

			this.loginButton.Draw();
			this.registerButton.Draw();
		}
	}
}
