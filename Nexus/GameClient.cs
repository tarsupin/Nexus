using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nexus.Config;
using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.Scripts;
using System;

namespace Nexus
{
    public class GameClient : Game
    {
		// XNA Graphics
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
		private readonly Action loadInstructions;

		public GameClient(Action LoadInstructions = null) {
			graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
			loadInstructions = LoadInstructions;
		}

		/// Load any non-graphic content. Screen sizes, systems, etc.
		protected override void Initialize() {

			//TargetElapsedTime = TimeSpan.FromMilliseconds(1000d / 30);
			//this.IsFixedTimeStep = false;

			// NOTE: Important to set this to false. Game can be stuttery if vSync enabled, because of monitor speed, tearing, etc. Read more:
			// https://hardforum.com/threads/how-vsync-works-and-why-people-loathe-it.928593/
			// https://www.geforce.com/hardware/technology/adaptive-vsync/technology
			graphics.SynchronizeWithVerticalRetrace = false; // Vsync; may cause stutter if not used.

			// Enumerate through components and initialize them as well.
			base.Initialize();
        }

		/// Loaded once per game. Load all Game Content.
		protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
			Systems.spriteBatch = this.spriteBatch;

			// Load Systems
			Systems.AddGame(this);
			Systems.AddGraphics(this, this.graphics, this.spriteBatch);
			Systems.AddFonts(this);
			Systems.screen.ResizeWindowToBestFit();
			Systems.AddAudio(this);

			// Resize Window
			Window.AllowUserResizing = true;
			Window.ClientSizeChanged += new EventHandler<EventArgs>(Systems.screen.OnResizeWindow);
			//Window.Position = new Point(0, 24);

			// Process Extra Loading Instructions
			if(loadInstructions != null) { loadInstructions(); }
		}

		/// UnloadContent will be called once per game and is the place to unload game-specific content.
		protected override void UnloadContent() { }

        /// Run Game Logic; e.g. updating the world, checking for collisions, gathering input, playing audio, etc.
        protected override void Update(GameTime gameTime) {
			//Systems.timer.stopwatch.Start();

			Systems.input.PreProcess(!Systems.levelConsole.visible);
			Systems.scene.RunTick();

			base.Update(gameTime);

			// Debugging
			//Systems.timer.stopwatch.Stop();
			//System.Console.WriteLine("Benchmark: " + Systems.timer.stopwatch.ElapsedTicks + ", " + Systems.timer.stopwatch.ElapsedMilliseconds);
		}

		/// This is called when the game should draw itself.
		protected override void Draw(GameTime gameTime) {

			GraphicsDevice.Clear(Color.CornflowerBlue);
			this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

			Systems.scene.Draw();

			this.spriteBatch.End();
			base.Draw(gameTime);
		}
    }

	public static class Program {

		/// The main entry point for the application.
		static void Main() {

			//byte[] byteVals = new byte[] { 0, 0, 1, 10, 1, 0, 0 };
			//int frameIndex = 0;
			//int frame;
			
			//// Some architectures reverse the BitConverter.ToInt32() method.
			//if(BitConverter.IsLittleEndian) {
			//	byte[] frameSlice = new byte[] { byteVals[frameIndex + 3], byteVals[frameIndex + 2], byteVals[frameIndex + 1], byteVals[frameIndex] };
			//	frame = BitConverter.ToInt32(frameSlice, 0);
			//} else {
			//	frame = BitConverter.ToInt32(byteVals, frameIndex);
			//}

			//System.Console.WriteLine(frame.ToString());

			//return;

			//SocketClient sc = new SocketClient();

			//while(true) {
			//	sc.SendText(":loadRoom");
			//	byte[] bytes = new byte[] { 5, 10, 234, 3 };
			//	sc.SendData(bytes);
			//	System.Threading.Thread.Sleep(2000);
			//}

			Action gameLoadInstructions = () => {

				// Try converting levels
				//new LevelConvertV1();               // TODO CLEANUP: Remove this line.

				//DebugConfig.SetTickSpeed(DebugTickSpeed.HalfSpeed);
				//DebugConfig.SetTickSpeed(DebugTickSpeed.WhenYPressed);

				// TODO: Change playtesting level to correct setup.
				// TODO: If no scene is transitioned correctly, this will fail. We need a default solution here.
				// TODO: We also need levels to be somehow loaded into local data during installation. Unfamiliar with that process atm.
				// Load a default level.
				//SceneTransition.ToLevel("", "QCALQOD16");
				//Systems.camera.CenterAtPosition(1200, 0);

				//SceneTransition.ToWorld("Tarsupin");
				SceneTransition.ToWorldEditor("Tarsupin");

				//Console.WriteLine("-----------------DATA--------------");
				//Console.WriteLine(Systems.handler.level.data.id);
			};

			using(var game = new GameClient(gameLoadInstructions)) {
				game.Run();
			}
		}
	}
}