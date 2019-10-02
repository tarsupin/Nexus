using Nexus.Engine;
using Nexus.Gameplay;

/*
 * The purpose of SceneTransition is to:
 *		1. Unload any existing Scene data that isn't needed.
 *		2. Retrieve needed data from the server (such as a new map or level).
 *		3. Wait until all data has been retrieved.
 *		4. Load the new Scene.
 */

namespace Nexus.GameEngine {

	public class SceneTransition {

		public SceneTransition( Systems systems ) {
			
		}

		// Go to World Scene
		public static void ToWorld( Systems systems, string worldId, bool runMenu = false, bool runEditor = false ) {

		}

		// Go to Level Scene (In World)
		public static bool ToLevel( Systems systems, string worldId, string levelId, bool runMenu = false, bool runEditor = false ) {
			GameHandler handler = systems.handler;

			// Verify that we're loading a level that's different from our current one:
			if(levelId == handler.level.levelId) { return false; }
			
			// Get Level Path & Retrieve Level Data
			if(!handler.level.LoadLevel(levelId)) { return false; }

			// Update the Level State
			handler.levelState.FullLevelReset();

			// Create New Level Scene
			systems.scene = new LevelScene(systems);

			return true;
		}

	}
}
