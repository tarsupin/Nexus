using Nexus.Engine;

namespace Nexus.GameEngine {

	public class FuncButtonPlay : FuncButton {

		public FuncButtonPlay() : base() {
			this.keyChar = "p";
			this.spriteName = "Play";
			this.title = "Play";
			this.description = "Saves the level, then initiates a playthrough.";
		}

		public override void ActivateFuncButton() {
			EditorScene scene = (EditorScene)Systems.scene;

			Systems.handler.levelContent.SaveLevel();
			GameValues.LastAction = "EditorPlayButton";

			if(scene.tutorial.tutorialStep == TutorialEditor.finalStep) {
				UIHandler.AddNotification(UIAlertType.Warning, "Playtest Fixes", "If something goes wrong, don't worry. Return to map editing or reset your position through the tilde (~) console.", 1500);
			}

			SceneTransition.ToLevel("", scene.levelContent.levelId);
		}
	}
}
