using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Nexus.Gameplay {

	public class FontAssets {

		// Fonts
		public readonly FontClass baseText;
		public readonly FontClass console;
		public readonly FontClass counter;

		public FontAssets(GameClient game) {
			this.baseText = new FontClass(game, "Fonts/BaseText");
			this.console = new FontClass(game, "Fonts/ConsoleText");
			this.counter = new FontClass(game, "Fonts/Counter");
		}
	}

	public class FontClass {
		private readonly SpriteBatch spriteBatch;
		public readonly SpriteFont font;

		public FontClass(GameClient game, string fileName) {
			var Content = game.Content;
			this.spriteBatch = game.spriteBatch;
			this.font = Content.Load<SpriteFont>(fileName);
		}

		public void Draw(string text, int posX, int posY, Color color) {
			this.spriteBatch.DrawString(this.font, text, new Vector2(posX, posY), color);
		}
	}
}
