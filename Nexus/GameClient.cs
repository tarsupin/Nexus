using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nexus.Config;
using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Objects;
using System;

namespace Nexus
{
    public class GameClient : Game
    {
		// XNA Graphics
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

		// Nexus Classes
		public Systems systems;

		public GameClient() {
			graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

		/// Load any non-graphic content. Screen sizes, systems, etc.
		protected override void Initialize() {

			// Prepare Engine Systems
			this.systems = new Systems(this);

			// Initialize Configurations
			new DebugConfig();

			// Resize Window
			Window.AllowUserResizing = true;
			Window.ClientSizeChanged += new EventHandler<EventArgs>(this.systems.screen.OnResizeWindow);
			//Window.Position = new Point(0, 24);
			//TargetElapsedTime = TimeSpan.FromMilliseconds(1000d / 30);
			//this.IsFixedTimeStep = false;

			// NOTE: Important to set this to false. Game can be stuttery if vSync enabled, because of monitor speed, tearing, etc. Read more:
			// https://hardforum.com/threads/how-vsync-works-and-why-people-loathe-it.928593/
			// https://www.geforce.com/hardware/technology/adaptive-vsync/technology
			//graphics.SynchronizeWithVerticalRetrace = false; // Vsync; may cause stutter if not used.

			// Enumerate through components and initialize them as well.
			base.Initialize();
        }

		/// Loaded once per game. Load all Game Content.
		protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            this.spriteBatch = new SpriteBatch(GraphicsDevice);

			// Add Graphics to System
			this.systems.AddGraphics(this, this.spriteBatch);
			this.systems.screen.ResizeWindowToBestFit();

			// TODO: use this.Content to load your game content here

			// TODO: Change playtesting level to correct setup.
			// Load a default level.
			SceneTransition.ToLevel(systems, "", "QCALQOD16");
			(this.systems.scene).camera.CenterAtPosition(1200, 0);

			// TODO CLEANUP: Remove Character from being inserted like this:
			Character character = new Character((LevelScene) this.systems.scene, 0, FVector.Create(950, 600), null);
			((LevelScene)(this.systems.scene)).AddToObjects(character);

			this.systems.localServer.MyPlayer.AssignCharacter(character);

			//Console.WriteLine("-----------------DATA--------------");
			//Console.WriteLine(this.systems.handler.level.data.id);
		}

		/// UnloadContent will be called once per game and is the place to unload game-specific content.
		protected override void UnloadContent() { }

        /// Run Game Logic; e.g. updating the world, checking for collisions, gathering input, playing audio, etc.
        protected override void Update(GameTime gameTime) {
			//var stopwatch = new Stopwatch();
			//stopwatch.Start();

			this.systems.input.PreProcess();
			this.systems.scene.RunTick();

			base.Update(gameTime);

			// Debugging
			//stopwatch.Stop();
			//System.Console.WriteLine("Benchmark: " + stopwatch.ElapsedTicks + ", " + stopwatch.ElapsedMilliseconds);
		}

		/// This is called when the game should draw itself.
		protected override void Draw(GameTime gameTime) {

			GraphicsDevice.Clear(Color.CornflowerBlue);
			this.spriteBatch.Begin();

			this.systems.scene.Draw();

			this.spriteBatch.End();
			base.Draw(gameTime);
		}
    }

	public static class Program {

		/// The main entry point for the application.
		static void Main() {

			//SocketClient sc = new SocketClient();

			//while(true) {
			//	Thread.Sleep(1000);
			//	//sc.SendText("asdf! How are things?");

			//	//byte[] bytes = Encoding.ASCII.GetBytes("Testing a hello.");
			//	//sc.SendData(bytes);
			//}

			using(var game = new GameClient()) {
				game.Run();
			}
		}
	}
}