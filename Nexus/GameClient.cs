using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nexus.Config;
using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.Objects;
using System;

namespace Nexus
{
    public class GameClient : Game
    {
		// XNA Graphics
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

		public SimpleEmitter TESTEMITTER;		// TODO CLEANUP: DELETE

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

			// Add Audio to System
			this.systems.AddAudio(this);

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

			// TODO CLEANUP: Remove this gravity gen
			//ParticleGen.GenGravityBurst(0.50f, -11.0f);
			//ParticleGen.GenGravityBurst(0.75f, -10.0f);
			//ParticleGen.GenGravityBurst(1.00f, -9.5f);
			//ParticleGen.GenGravityBurst(1.25f, -9.0f);
			//ParticleGen.GenGravityBurst(1.50f, -8.5f);
			//ParticleGen.GenGravityBurst(1.75f, -8.0f);

			this.TESTEMITTER = SimpleEmitter.NewEmitter(this.systems.mapper.atlas[(byte) AtlasGroup.Objects], "Items/Key", new Vector2(500, 300), new Vector2(2, 0), 0.5f, 300);
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
			this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

			this.systems.scene.Draw();

			// TODO CLEANUP: DELETE PARTICLE SPIRAL STEP THING
			//Vector2 particlePos = ParticleGen.SpiralStep(0f, 40f, 1.5f, 0.05f, (int) this.systems.scene.timer.frame);
			//float alpha = ParticleGen.AlphaByFade(1f, 0f, 0.5f, this.systems.scene.timer.frame / 180f);
			
			////this.systems.mapper.atlas[(byte)AtlasGroup.Tiles].Draw("Grass/FU", (int) Math.Round(particlePos.X + 500), (int) Math.Round(particlePos.Y + 400));

			//this.systems.mapper.atlas[(byte)AtlasGroup.Tiles].DrawAdvanced(
			//	"Grass/FU", (int) Math.Round(particlePos.X + 500), (int) Math.Round(particlePos.Y + 400),
			//	Color.White * alpha
			//);

			this.TESTEMITTER.Draw();

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