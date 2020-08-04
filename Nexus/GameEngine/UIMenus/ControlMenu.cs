using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nexus.Engine;
using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class ControlMenu : IMenu {

		private readonly TextBox textBox;

		public ControlMenu(short width, short height) : base() {

			short centerX = (short)(Systems.screen.viewHalfWidth - (short)(width * 0.5));
			short centerY = (short)(Systems.screen.viewHalfHeight - (short)(height * 0.5));

			this.textBox = new TextBox(null, centerX, centerY, width, height);
		}

		public void RunTick() {

			// Handle Key Presses
			InputClient input = Systems.input;
			PlayerInput playerInput = Systems.localServer.MyPlayer.input;

			// Check if the menu should be closed:
			if(input.LocalKeyPressed(Keys.Escape) || input.LocalKeyPressed(Keys.Tab) || playerInput.isPressed(IKey.Start)) {
				UIHandler.SetMenu(null, false);
				return;
			}

			// If we clicked off of the menu, exit.
			//if(Cursor.LeftMouseState == Cursor.MouseDownState.Clicked && this.textBox.MouseOver != UIMouseOverState.On) {
			//	UIHandler.SetMenu(null, false);
			//}

			this.textBox.RunTick();
		}

		public void Draw() {
			this.textBox.Draw();

			this.DrawNavigationPad(160, 70);
			this.DrawActionPad(500, 70);

			this.DrawNavigationKeys(160, 230);
			this.DrawActionKeys(500, 230);

			Systems.fonts.console.Draw("Movement Keys", this.textBox.trueX + 140, this.textBox.trueY + 360, Color.Black);
			Systems.fonts.console.Draw("Action Keys", this.textBox.trueX + 495, this.textBox.trueY + 360, Color.Black);
		}

		public void DrawNavigationPad(short groupX, short groupY) {
			Atlas tileAtlas = Systems.mapper.atlas[(byte)AtlasGroup.Tiles];
			tileAtlas.Draw("Prompt/DPad/Up", this.textBox.trueX + groupX, this.textBox.trueY + groupY);
			tileAtlas.Draw("Prompt/Button/L1", this.textBox.trueX + groupX - 50, this.textBox.trueY + groupY);
			tileAtlas.Draw("Prompt/DPad/Left", this.textBox.trueX + groupX - 50 + 6, this.textBox.trueY + groupY + 50);
			tileAtlas.Draw("Prompt/DPad/Down", this.textBox.trueX + groupX + 6, this.textBox.trueY + groupY + 50);
			tileAtlas.Draw("Prompt/DPad/Right", this.textBox.trueX + groupX + 50 + 6, this.textBox.trueY + groupY + 50);
		}

		public void DrawActionPad(short groupX, short groupY) {
			Atlas tileAtlas = Systems.mapper.atlas[(byte)AtlasGroup.Tiles];
			tileAtlas.Draw("Prompt/Button/Y", this.textBox.trueX + groupX, this.textBox.trueY + groupY);
			tileAtlas.Draw("Prompt/Button/R1", this.textBox.trueX + groupX + 50, this.textBox.trueY + groupY);
			tileAtlas.Draw("Prompt/Button/X", this.textBox.trueX + groupX - 50 + 6, this.textBox.trueY + groupY + 50);
			tileAtlas.Draw("Prompt/Button/A", this.textBox.trueX + groupX + 6, this.textBox.trueY + groupY + 50);
			tileAtlas.Draw("Prompt/Button/B", this.textBox.trueX + groupX + 50 + 6, this.textBox.trueY + groupY + 50);
		}

		public void DrawNavigationKeys(short groupX, short groupY) {
			UIHandler.atlas.Draw("Key", this.textBox.trueX + groupX, this.textBox.trueY + groupY);
			UIHandler.atlas.DrawAdvanced("Key", this.textBox.trueX + groupX - 50, this.textBox.trueY + groupY, Color.White * 0.5f);
			UIHandler.atlas.Draw("Key", this.textBox.trueX + groupX - 50 + 6, this.textBox.trueY + groupY + 50);
			UIHandler.atlas.Draw("Key", this.textBox.trueX + groupX + 6, this.textBox.trueY + groupY + 50);
			UIHandler.atlas.Draw("Key", this.textBox.trueX + groupX + 50 + 6, this.textBox.trueY + groupY + 50);

			Systems.fonts.baseText.Draw("W", this.textBox.trueX + groupX + 14, this.textBox.trueY + groupY + 15, Color.Black);
			Systems.fonts.baseText.Draw("Q", this.textBox.trueX + groupX - 50 + 16, this.textBox.trueY + groupY + 15, Color.Black * 0.5f);
			Systems.fonts.baseText.Draw("A", this.textBox.trueX + groupX - 50 + 6 + 15, this.textBox.trueY + groupY + 50 + 15, Color.Black);
			Systems.fonts.baseText.Draw("S", this.textBox.trueX + groupX + 6 + 16, this.textBox.trueY + groupY + 50 + 15, Color.Black);
			Systems.fonts.baseText.Draw("D", this.textBox.trueX + groupX + 50 + 6 + 16, this.textBox.trueY + groupY + 50 + 15, Color.Black);
		}

		public void DrawActionKeys(short groupX, short groupY) {
			UIHandler.atlas.Draw("Key", this.textBox.trueX + groupX, this.textBox.trueY + groupY);
			UIHandler.atlas.DrawAdvanced("Key", this.textBox.trueX + groupX + 50, this.textBox.trueY + groupY, Color.White * 0.5f);
			UIHandler.atlas.Draw("Key", this.textBox.trueX + groupX - 50 + 6, this.textBox.trueY + groupY + 50);
			UIHandler.atlas.Draw("Key", this.textBox.trueX + groupX + 6, this.textBox.trueY + groupY + 50);
			UIHandler.atlas.Draw("Key", this.textBox.trueX + groupX + 50 + 6, this.textBox.trueY + groupY + 50);

			Systems.fonts.baseText.Draw("I", this.textBox.trueX + groupX + 21, this.textBox.trueY + groupY + 15, Color.Black);
			Systems.fonts.baseText.Draw("O", this.textBox.trueX + groupX + 50 + 16, this.textBox.trueY + groupY + 15, Color.Black * 0.5f);
			Systems.fonts.baseText.Draw("J", this.textBox.trueX + groupX - 50 + 6 + 18, this.textBox.trueY + groupY + 50 + 15, Color.Black);
			Systems.fonts.baseText.Draw("K", this.textBox.trueX + groupX + 6 + 16, this.textBox.trueY + groupY + 50 + 15, Color.Black);
			Systems.fonts.baseText.Draw("L", this.textBox.trueX + groupX + 50 + 6 + 18, this.textBox.trueY + groupY + 50 + 15, Color.Black);
		}
	}
}
