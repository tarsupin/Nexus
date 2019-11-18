﻿using Nexus.Engine;
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
		public static void ToWorld( string worldId, bool runMenu = false, bool runEditor = false ) {

		}

		// Go to Level Scene (In World)
		public static bool ToLevel( string worldId, string levelId, bool runMenu = false, bool runEditor = false ) {
			GameHandler handler = Systems.handler;

			// Verify that we're loading a level that's different from our current one:
			if(levelId == handler.levelContent.levelId) { return false; }
			
			// Get Level Path & Retrieve Level Data
			if(!handler.levelContent.LoadLevel(levelId)) { return false; }

			// Update the Level State
			handler.levelState.FullLevelReset();

			// End Old Level Scene
			Systems.scene.EndScene();

			// TODO HIGH PRIORITY: Editable Levels should be 1 to 100 on localhost. Then, can send those to server, which changes to a special hash.

			// Editor Scene
			if(runEditor) { Systems.scene = new EditorScene(); }

			// Create New Level Scene
			else { Systems.scene = new LevelScene(); }

			return true;
		}

	}
}
