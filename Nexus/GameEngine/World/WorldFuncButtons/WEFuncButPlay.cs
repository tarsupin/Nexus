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
			WEScene scene = (WEScene)Systems.scene;

			Systems.handler.worldContent.SaveWorld();
			GameValues.LastAction = "WEPlayButton";

			if(scene.tutorial.tutorialStep == TutorialWorldEdit.finalStep) {
				UIHandler.AddNotification(UIAlertType.Warning, "Playtest Fixes", "If something goes wrong, don't worry. Return to map editing or reset your position through the tilde (~) console.", 1500);
			}

			SceneTransition.ToWorld(scene.worldContent.worldId);
		}
	}
}
