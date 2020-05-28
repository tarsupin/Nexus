using Nexus.Engine;

namespace Nexus.GameEngine {

	public class WorldFuncButPlay : WorldFuncBut {

		public WorldFuncButPlay() : base() {
			this.keyChar = "p";
			this.spriteName = "Icons/Small/Play";
			this.title = "Play";
			this.description = "Saves the level, then initiates a playthrough.";
		}

		public override void ActivateWorldFuncButton() {
			Systems.handler.worldContent.SaveWorld();
			SceneTransition.ToLevel("", ((WorldEditorScene) Systems.scene).worldContent.worldId);
		}
	}
}
