using Microsoft.Xna.Framework.Graphics;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Engine {

	// Placeholder Scene
	public class ScenePlaceholder : Scene {
		public ScenePlaceholder(Systems systems) : base( systems ) { }
		public override void RunTick() { }
	}

	public class Systems {

		public Scene scene;

		// Systems
		public readonly InputClient input;
		public readonly TimerGlobal timer;
		public readonly FilesLocal filesLocal;
		public readonly ScreenSys screen;
		public readonly LocalServer localServer;
		public GameMapper mapper { get; protected set; }

		// Settings & States
		public readonly Settings settings;
		public readonly GameHandler handler;

		public Systems(GameClient game) {

			// Load Systems
			input = new InputClient(this);
			timer = new TimerGlobal();
			filesLocal = new FilesLocal();
			screen = new ScreenSys(game);
			localServer = new LocalServer(this);

			// Load Settings & States
			settings = new Settings(this);
			handler = new GameHandler(this, "Current");

			// Load Scene Placeholder
			scene = new ScenePlaceholder(this);
		}

		public void AddGraphics(GameClient game, SpriteBatch spriteBatch) {
			this.mapper = new GameMapper(game, spriteBatch);
		}
	}
}
