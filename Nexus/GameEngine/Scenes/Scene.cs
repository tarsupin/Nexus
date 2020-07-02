using Nexus.Engine;

namespace Nexus.GameEngine {

	public class Scene {

		// UI Handler
		public bool mouseAlwaysVisible = false;
		public UIState uiState = UIState.Playing;

		public enum UIState : byte {
			Playing,
			MainMenu,
			SubMenu,
			Console,
		}

		public int idCounter;           // Tracks the last ID that was generated in the scene.

		public Scene() {}

		public virtual int Width { get { return 0; } } // Placeholder for Camera
		public virtual int Height { get { return 0; } } // Placeholder for Camera

		public virtual void StartScene() { }
		public virtual void ResetScene() { }
		public virtual void EndScene() { }

		public void SetUIState(UIState uiState) {
			this.uiState = uiState;
			UIComponent.ComponentSelected = null;

			if(uiState == UIState.SubMenu || uiState == UIState.MainMenu || this.mouseAlwaysVisible) { Systems.SetMouseVisible(true); }
			else { Systems.SetMouseVisible(false); }
		}

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
