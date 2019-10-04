
using Nexus.Engine;
using Nexus.Gameplay;
using System;

namespace Nexus.GameEngine {

	public class Scene {

		public uint idCounter;           // Tracks the last ID that was generated in the scene.

		public readonly Systems systems;
		public readonly TimerGlobal time;
		public readonly Camera camera;
		public readonly ScreenSys screen;
		public readonly GameMapper mapper;

		public Scene( Systems systems ) {

			// Systems
			this.systems = systems;
			this.time = systems.timer;
			this.screen = systems.screen;
			this.camera = new Camera(this);
			this.mapper = systems.mapper;
		}

		public virtual void Update() { throw new ArgumentNullException("Must implement Update() in Child Scene."); }
		public virtual void Draw() { throw new ArgumentNullException("Must implement Draw() in Child Scene."); }

		public uint nextId {
			get {
				this.idCounter++;
				return this.idCounter;
			}
		}

		public virtual void ReceivePlayerInput( uint frame, byte playerId, IKey[] iKeysPressed, IKey[] iKeysReleased ) {

			// Display Input on Console
			Console.WriteLine("Input, Frame " + frame + ", Player " + playerId + ": " + iKeysPressed.ToString() + " & " + iKeysReleased.ToString());
		}
	}
}
