using Microsoft.Xna.Framework.Graphics;
using TexturePackerLoader;

namespace Nexus.Engine {
	public class AtlasOld {

		// Texture Package
		readonly SpriteSheet sheet;
		readonly SpriteRender render;

		// Setup Atlas for Texture Packager
		public AtlasOld(GameClient game, SpriteBatch spriteBatch, string filePath) {
			SpriteSheetLoader spriteSheetLoader = new SpriteSheetLoader(game.Content, game.GraphicsDevice);
			this.sheet = spriteSheetLoader.Load(filePath);
			this.render = new SpriteRender(spriteBatch);
		}

		// Draw Atlas
		public void Draw( string spriteName, FVector pos ) {
			this.render.Draw(
				this.sheet.Sprite(spriteName),
				pos
			);
		}
	}
}
