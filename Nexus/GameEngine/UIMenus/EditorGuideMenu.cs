using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nexus.Engine;

namespace Nexus.GameEngine {

	public class EditorGuideMenu : IMenu {

		private readonly TextBox textBox;
		//private readonly string[] textLines;
		private short yLine = 0;

		public EditorGuideMenu(short width, short height) : base() {

			short centerX = (short)(Systems.screen.viewHalfWidth - (short)(width * 0.5));
			short centerY = (short)(Systems.screen.viewHalfHeight - (short)(height * 0.5));

			this.textBox = new TextBox(null, centerX, centerY, width, height);

			// Create Multiline Text
			//this.textLines = TextHelper.WrapTextSplit(Systems.fonts.console.font, "", width - 100);
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
			if(Cursor.LeftMouseState == Cursor.MouseDownState.Clicked && this.textBox.MouseOver != UIMouseOverState.On) {
				UIHandler.SetMenu(null, false);
			}

			this.textBox.RunTick();
		}

		private void NextLine(string str, short nextY = 0) {
			this.yLine = nextY != 0 ? nextY : (short)(this.yLine + 20);
			Systems.fonts.console.Draw(str, this.textBox.trueX + 28, this.textBox.trueY + this.yLine, Color.Black);
		}

		public void Draw() {
			this.textBox.Draw();

			// Placing Tiles
			Systems.fonts.baseText.Draw("Placing Tiles", this.textBox.trueX + 28, this.textBox.trueY + 25, Color.Black);
			this.NextLine("1. Hold `Tab` to select your tile archetype (e.g. \"Ground\", \"Enemy\", etc).", 50);
			this.NextLine("2. The Utility Bar (bottom) displays available tile groups (e.g. Boxes, Bricks, etc).");
			this.NextLine("3. The Scroller Bar (right) indicates your active tile (e.g. Brown Box, Gray Box).");
			this.NextLine("4. Press a number 1-10 to select a tile group on the Utility Bar.");
			this.NextLine("5. Use the middle mouse key to scroll through your active tile.");
			this.NextLine("6. Click on the screen to place a tile.");

			// Advanced Tiles
			Systems.fonts.baseText.Draw("Advanced Tiles", this.textBox.trueX + 28, this.textBox.trueY + 190, Color.Black);
			this.NextLine("1. Some tiles have advanced options. You can access them with the wand tool.", 215);
			this.NextLine("2. Select the wand tool or hold \"e\" and try clicking on a tile.");
			this.NextLine("3. If you don't see options appear, it doesn't have custom options. Try a flying enemy.");

			// Level Console
			Systems.fonts.baseText.Draw("Editor Console", this.textBox.trueX + 28, this.textBox.trueY + 300, Color.Black);
			this.NextLine("1. Some options are only accessible through the Editor Console.", 325);
			this.NextLine("2. Press the tilde key (~) to make the console appear.");
			this.NextLine("3. With the Editor Console you can resize the level, choose level settings, etc.");
		}
	}
}
