
namespace Nexus.GameEngine {

	public static class WCToEditor {

		public static void ToEditor() {
			ConsoleTrack.possibleTabs = "Example: `editor`";
			ConsoleTrack.helpText = "Load the world editor for this world. No additional parameters required.";

			if(ConsoleTrack.activate) {

				// Transition to an Editor Scene
				SceneTransition.ToWorldEditor();
			}
		}
	}
}
