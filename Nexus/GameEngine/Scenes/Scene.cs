using System;

namespace Nexus.GameEngine {

	public class Scene {

		public uint idCounter;           // Tracks the last ID that was generated in the scene.

		public Scene() {}

		public virtual int Width { get { return 0; } } // Placeholder for Camera
		public virtual int Height { get { return 0; } } // Placeholder for Camera

		public virtual void StartScene() { }
		public virtual void ResetScene() { }
		public virtual void EndScene() { }

		public virtual void RunTick() { }
		public virtual void Draw() { }

		public uint nextId {
			get {
				this.idCounter++;
				return this.idCounter;
			}
		}
	}
}
