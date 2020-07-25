using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nexus.Engine;

namespace Nexus.GameEngine {

	public class LoadWorldMenu : IMenu {

		// Login Components
		private readonly TextBox textBox;
		private readonly UICreoInput worldIdInput;
		private readonly UICreoButton loadButton;

		private int lastBack = 0;

		public LoadWorldMenu(short width, short height) : base() {

			short centerX = (short)(Systems.screen.windowHalfWidth - (short)(width * 0.5));
			short centerY = (short)(Systems.screen.windowHalfHeight - (short)(height * 0.5));

			this.textBox = new TextBox(null, centerX, centerY, width, height);
			this.worldIdInput = new UICreoInput(this.textBox, 20, 50);
			this.loadButton = new UICreoButton(this.textBox, "Load World", 20, 102, null);

			UIComponent.ComponentSelected = this.worldIdInput;
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
			if(UIComponent.ComponentSelected is UICreoInput) {

				// Get Characters Pressed (doesn't assist with order)
				string charsPressed = input.GetCharactersPressed();

				UICreoInput comp = (UICreoInput)UIComponent.ComponentSelected;

				if(charsPressed.Length > 0) {
					comp.SetInputText(comp.text + charsPressed);
				}

				// Backspace (+Shift, +Control)
				if(input.LocalKeyDown(Keys.Back) && this.lastBack < Systems.timer.UniFrame) {
					
					if(input.LocalKeyPressed(Keys.Back)) {
						this.lastBack = Systems.timer.UniFrame + 10;
					} else {
						this.lastBack = Systems.timer.UniFrame + 4;
					}

					if(input.LocalKeyDown(Keys.LeftShift) || input.LocalKeyDown(Keys.RightShift) || input.LocalKeyDown(Keys.LeftControl) || input.LocalKeyDown(Keys.RightControl)) {
						comp.SetInputText("");
					} else if(comp.text.Length > 0) {
						comp.SetInputText(comp.text.Substring(0, comp.text.Length - 1));
					}
				}

				if(comp.text.Length > 22) {
					comp.SetInputText(comp.text.Substring(0, 22));
				}
			}

			// Tab Between Options
			if(input.LocalKeyPressed(Keys.Tab)) {
				UIComponent.ComponentSelected = this.worldIdInput;
			}

			this.textBox.RunTick();
			this.worldIdInput.RunTick();
			this.loadButton.RunTick();

			// Press Enter
			if(input.LocalKeyPressed(Keys.Enter)) {
				this.LoadWorldAttempt();
				return;
			}

			// Clicked on Load World Button.
			if(UIComponent.ComponentWithFocus == this.loadButton && Cursor.LeftMouseState == Cursor.MouseDownState.Clicked) {
				this.LoadWorldAttempt();
			}
		}

		// Close the menu and attempt to load the designated world.
		public void LoadWorldAttempt() {
			UIHandler.SetMenu(null, false);
			string worldID = this.worldIdInput.text.ToUpper();
			SceneTransition.ToWorld(worldID);
		}

		public void Draw() {
			this.textBox.Draw();

			Systems.fonts.baseText.Draw("World ID", this.textBox.trueX + 28, this.textBox.trueY + 26, Color.Black);
			this.worldIdInput.Draw();

			this.loadButton.Draw();
		}
	}
}
