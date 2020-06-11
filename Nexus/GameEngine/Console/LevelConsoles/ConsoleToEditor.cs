using Nexus.Engine;

namespace Nexus.GameEngine {

	public static class ConsoleToEditor {

		public static void ToEditor() {
			string currentIns = ConsoleTrack.GetArgAsString();

			ConsoleTrack.possibleTabs = "Example: `editor 10`";
			ConsoleTrack.helpText = "Load the level editor for one of your levels.";

			if(ConsoleTrack.activate) {

				// Prepare the current level as the default option.
				string levelId = currentIns == "" ? Systems.handler.levelContent.levelId : currentIns;

				// Transition to an Editor Scene
				SceneTransition.ToLevelEditor("", levelId);
			}
		}
	}
}
