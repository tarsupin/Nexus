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

		// Go to World Scene
		public static bool ToWorld( string worldId, bool runMenu = false, bool runEditor = false ) {
			GameHandler handler = Systems.handler;

			// If we're already in a World Scene, verify that we're loading a different world from our current one.
			if(Systems.scene is WorldScene && worldId == handler.worldContent.worldId) { return false; }

			// Get World Path & Retrieve World Data
			if(!handler.worldContent.LoadWorldData(worldId)) { return false; }

			// Update the World State
			handler.worldState.FullWorldReset();

			// End Old World Scene
			Systems.scene.EndScene();

			// Create New World Scene
			Systems.scene = new WorldScene();
			Systems.scene.StartScene();

			return true;
		}

		// Go to Level Scene (In World)
		public static bool ToLevel( string worldId, string levelId, bool runMenu = false ) {
			GameHandler handler = Systems.handler;

			// If we're already in a Level Scene, verify that we're loading a level that's different from our current one.
			if(Systems.scene is LevelScene && levelId == handler.levelContent.levelId) { return false; }
			
			// Get Level Path & Retrieve Level Data
			if(!handler.levelContent.LoadLevelData(levelId)) { return false; }

			// Update the Level State
			handler.levelState.SetLevel(levelId, 0);

			// End Old Level Scene
			Systems.scene.EndScene();

			// Create New Level Scene
			Systems.scene = new LevelScene();
			Systems.scene.StartScene();

			return true;
		}

		// Go to Level Editor Scene
		public static bool ToLevelEditor( string worldId, string levelId, short myLevelNum = 0 ) {
			GameHandler handler = Systems.handler;

			// Load Level to Edit (unless it's already loaded)
			if(levelId != handler.levelContent.levelId) {

				// Get Level Path & Retrieve Level Data
				if(!handler.levelContent.LoadLevelData(levelId)) {

					// If this is a personal level, allow it to be created.
					if(myLevelNum > 0 && myLevelNum < GameValues.MaxLevelsAllowedPerUser) {
						handler.levelContent.data = LevelContent.BuildEmptyLevel(levelId);
					}
					
					else {

						#if debug
						throw new Exception("Unable to load level data.");
						#endif

						return false;
					}
				}
			}

			// Update the Level State
			handler.levelState.FullReset();

			// End Old Level Scene
			Systems.scene.EndScene();

			// Editor Scene
			Systems.scene = new EditorScene();
			Systems.scene.StartScene();

			return true;
		}

		// Go to World Editor Scene
		public static bool ToWorldEditor( string worldId = "" ) {
			GameHandler handler = Systems.handler;

			// TODO: Limit worldId to the user's username.

			// Load World to Edit (unless it's already loaded)
			if(worldId != handler.worldContent.worldId) {

				// Get World Path & Retrieve World Data
				if(!handler.worldContent.LoadWorldData(worldId)) {
					
					// If this is a personal world, allow it to be created.
					if(worldId == "__World") {
						handler.worldContent.data = WorldContent.BuildEmptyWorld(worldId);
					}
					
					else {
						#if debug
						throw new Exception("Unable to load world data.");
						#endif

						return false;
					}
				}
			}

			// Update the World State
			handler.worldState.FullWorldReset();

			// End Old World Scene
			Systems.scene.EndScene();

			// Editor Scene
			Systems.scene = new WEScene();
			Systems.scene.StartScene();

			return true;
		}

		// Go to Planet Selection Scene
		public static bool ToPlanetSelection() {
			Systems.scene.EndScene();
			Systems.scene = new PlanetSelectScene();
			Systems.scene.StartScene();
			return true;
		}

		// Go to My Levels Scene
		public static bool ToMyLevels() {
			Systems.scene.EndScene();
			Systems.scene = new MyLevelsScene();
			Systems.scene.StartScene();
			return true;
		}
	}
}
