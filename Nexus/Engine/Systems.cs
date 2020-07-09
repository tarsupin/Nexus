using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Engine {

	// Placeholder Scene
	public class ScenePlaceholder : Scene {
		public ScenePlaceholder() : base() { }
		public override void RunTick() { }
	}

	public static class Systems {

		// Important References
		public static GameClient game;
		public static GraphicsDeviceManager graphics;
		public static SpriteBatch spriteBatch;
		public static Scene scene = new ScenePlaceholder();

		// Systems
		public static readonly InputClient input = new InputClient();
		public static readonly TimerGlobal timer = new TimerGlobal();
		public static readonly FilesLocal filesLocal = new FilesLocal();
		public static readonly LocalServer localServer = new LocalServer();

		// Graphics, Audio, and Assets
		public static ScreenSys screen;
		public static GameMapper mapper;
		public static SoundAssets sounds;
		public static MusicAssets music;
		public static FontAssets fonts;
		public static Camera camera;

		// Graphic Colors
		public static Texture2D tex2dBlack;
		public static Texture2D tex2dWhite;
		public static Texture2D tex2dDarkRed;
		public static Texture2D tex2dDarkGreen;

		// Settings & States
		public static readonly Settings settings = new Settings();
		public static readonly GameHandler handler = new GameHandler("Main");

		public static void AddGame( GameClient game ) {
			Systems.game = game;
		}

		public static void AddGraphics( GameClient game, GraphicsDeviceManager graphics, SpriteBatch spriteBatch ) {
			Systems.graphics = graphics;
			Systems.spriteBatch = spriteBatch;
			Systems.screen = new ScreenSys(game);
			Systems.mapper = new GameMapper(game, spriteBatch);
			Systems.mapper.PostLoad();
			Systems.camera = new Camera(Systems.scene);

			// Add Textures
			Systems.tex2dBlack = new Texture2D(Systems.graphics.GraphicsDevice, 1, 1);
			Systems.tex2dBlack.SetData(new[] { Color.Black });

			Systems.tex2dWhite = new Texture2D(Systems.graphics.GraphicsDevice, 1, 1);
			Systems.tex2dWhite.SetData(new[] { Color.White });

			Systems.tex2dDarkRed = new Texture2D(Systems.graphics.GraphicsDevice, 1, 1);
			Systems.tex2dDarkRed.SetData(new[] { Color.DarkRed });

			Systems.tex2dDarkGreen = new Texture2D(Systems.graphics.GraphicsDevice, 1, 1);
			Systems.tex2dDarkGreen.SetData(new[] { Color.DarkGreen });
		}

		public static void AddAudio( GameClient game ) {
			Systems.sounds = new SoundAssets(game);
			Systems.music = new MusicAssets(game);
		}

		public static void AddFonts( GameClient game ) {
			Systems.fonts = new FontAssets(game);
		}

		public static void AddUIHandler() {
			UIHandler.atlas = Systems.mapper.atlas[(byte)AtlasGroup.UI];
		}
	}
}
