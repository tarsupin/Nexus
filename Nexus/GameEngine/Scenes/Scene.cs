using Nexus.Engine;

namespace Nexus.GameEngine {

	public class Scene {

		// Scene Counter
		public int idCounter;           // Tracks the last ID that was generated in the scene.

		public Scene() {}

		public virtual int Width { get { return 0; } } // Placeholder for Camera
		public virtual int Height { get { return 0; } } // Placeholder for Camera

		public virtual void StartScene() { }
		public virtual void ResetScene() { }
		public virtual void EndScene() { }

		public virtual void RunTick() { }
		public virtual void Draw() { }

		public int nextId {
			get {
				this.idCounter++;
				return this.idCounter;
			}
		}
	}
}
