using Nexus.Engine;

namespace Nexus.GameEngine {

	public class WEFuncButPlay : WEFuncBut {

		public WEFuncButPlay() : base() {
			this.keyChar = "p";
			this.spriteName = "Small/Play";
			this.title = "Play";
			this.description = "Saves the world, then initiates a playthrough.";
		}

		public override void ActivateWorldFuncButton() {
			Systems.handler.worldContent.SaveWorld();
			SceneTransition.ToWorld(((WEScene)Systems.scene).worldContent.worldId);
		}
	}
}
