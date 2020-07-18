using Nexus.Engine;
using Nexus.Gameplay;
using System.Threading.Tasks;

/*
 * The purpose of SceneTransition is to:
 *		1. Unload any existing Scene data that isn't needed.
 *		2. Retrieve needed data from the server (such as a new map or level).
 *		3. Wait until all data has been retrieved.
 *		4. Load the new Scene.
 */

namespace Nexus.GameEngine {

	public class SceneTransition {

		public static Scene nextScene;

		public static void RunTransition() {
			if(SceneTransition.nextScene is Scene == false) { return; }
			Systems.scene.EndScene();
			Systems.scene = SceneTransition.nextScene;
			SceneTransition.nextScene = null;
			Systems.scene.StartScene();
		}

		// Go to World Scene
		public static void ToWorld( string worldId, bool finalTest = false ) {
			GameHandler handler = Systems.handler;

			// If we're already in a World Scene, verify that we're loading a different world from our current one.
			if(Systems.scene is WorldScene && worldId == handler.worldContent.worldId) { return; }

			// Get World Path & Retrieve World Data
			if(!handler.worldContent.LoadWorldData(worldId)) {
				if(finalTest == false) { _ = SceneTransition.DownloadWorld(worldId); }
				return;
			}

			// Prepare Next Scene
			SceneTransition.nextScene = new WorldScene();
		}

		// Download a World
		private static async Task<bool> DownloadWorld(string worldId) {
			bool success = await WebHandler.WorldRequest(worldId);
			if(success) { SceneTransition.ToWorld(worldId, true); }
			return true;
		}

		// Go to Level Scene (In World)
		public static void ToLevel( string worldId, string levelId, bool grantCampaignEquipment = false, bool finalTest = false ) {
			GameHandler handler = Systems.handler;

			// If we're already in a Level Scene, verify that we're loading a level that's different from our current one.
			if(Systems.scene is LevelScene && levelId == handler.levelContent.levelId) { return; }
			
			// Get Level Path & Retrieve Level Data
			if(!handler.levelContent.LoadLevelData(levelId)) {
				if(finalTest == false) { _ = SceneTransition.DownloadLevel(worldId, levelId); }
				return;
			}

			// Update the Level State
			handler.levelState.SetLevel(levelId, 0);

			// Prepare Next Scene
			SceneTransition.nextScene = new LevelScene(grantCampaignEquipment);
		}
		
		// Download a Level
		private static async Task<bool> DownloadLevel( string worldId, string levelId ) {
			bool success = await WebHandler.LevelRequest(levelId);
			if(success) { SceneTransition.ToLevel(worldId, levelId, false, true); }
			return true;
		}

		// Go to Level Editor Scene
		public static void ToLevelEditor( string worldId, string levelId, short myLevelNum = 0 ) {
			GameHandler handler = Systems.handler;

			// Load Level to Edit (unless it's already loaded)
			if(levelId != handler.levelContent.levelId) {

				// Get Level Path & Retrieve Level Data
				if(!handler.levelContent.LoadLevelData(levelId)) {

					// If this is a personal level, allow it to be created.
					if(myLevelNum >= 0 && myLevelNum < GameValues.MaxLevelsAllowedPerUser) {
						handler.levelContent.data = LevelContent.BuildEmptyLevel(levelId);
						handler.levelContent.levelId = levelId;
					}
					
					else {

						#if debug
						throw new Exception("Unable to load level data.");
						#endif
						return;
					}
				}
			}

			// Update the Level State
			handler.levelState.FullReset();

			// Prepare Next Scene
			SceneTransition.nextScene = new EditorScene();
		}

		// Go to World Editor Scene
		public static void ToWorldEditor( string worldId = "" ) {
			GameHandler handler = Systems.handler;

			// TODO: Limit worldId to the user's username.

			// Load World to Edit (unless it's already loaded)
			if(worldId != handler.worldContent.worldId) {

				// Get World Path & Retrieve World Data
				if(!handler.worldContent.LoadWorldData(worldId)) {
					
					// If this is a personal world, allow it to be created.
					if(worldId == "__World") {
						handler.worldContent.data = WorldContent.BuildEmptyWorld(worldId);
						handler.worldContent.worldId = worldId;
					}
					
					else {
						#if debug
						throw new Exception("Unable to load world data.");
						#endif
						return;
					}
				}
			}

			// Prepare Next Scene
			SceneTransition.nextScene = new WEScene();
		}

		// Go to Planet Selection Scene
		public static void ToPlanetSelection() {
			SceneTransition.nextScene = new PlanetSelectScene();
		}

		// Go to My Levels Scene
		public static void ToMyLevels() {
			SceneTransition.nextScene = new MyLevelsScene();
		}
	}
}
