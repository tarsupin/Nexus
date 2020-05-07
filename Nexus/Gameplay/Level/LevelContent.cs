using Newtonsoft.Json;
using Nexus.Engine;
using System.IO;

namespace Nexus.Gameplay {

	public class LevelContent {

		// Level Data
		public string levelId;          // Level ID (e.g. "QCALQOD16")
		public LevelFormat data;        // Level Data

		// Path
		public string levelPath;

		public LevelContent(string levelPath = null, string levelId = null) {
			this.SetLevelPath(levelPath);

			// Attempt to load Level Data
			if(levelId != null) {
				this.LoadLevelData(levelId);
			}
		}

		public void SetLevelPath(string levelPath = null) {

			// If a level directory is not provided, use the default local directory.
			if(levelPath == null) {
				Systems.filesLocal.MakeDirectory("Levels"); // Make sure the Levels directory exists.
				this.levelPath = Path.Combine(Systems.filesLocal.localDir, "Levels");
			}

			// Make sure the level directory provided exists.
			else {
				this.levelPath = Directory.Exists(levelPath) ? levelPath : null;
			}
		}

		public static string GetLocalLevelPath(string levelId) {
			return Path.Combine(levelId.Substring(0, 2), levelId + ".json");
		}

		public string GetFullLevelPath(string levelId) {
			return Path.Combine(this.levelPath, LevelContent.GetLocalLevelPath(levelId));
		}

		public string GetFullDestinationPath(string destPath, string levelId) {
			return Path.Combine(destPath, LevelContent.GetLocalLevelPath(levelId));
		}

		public bool LoadLevelData(string levelId = "") {

			// Update the Level ID, or use existing Level ID if applicable.
			if(levelId.Length > 0) { this.levelId = levelId; } else { return false; }

			string fullLevelPath = this.GetFullLevelPath(this.levelId);
			
			// Make sure the level exists:
			if(!File.Exists(fullLevelPath)) { return false; }

			string json = File.ReadAllText(fullLevelPath);

			// If there is no JSON content, end the attempt to load level:
			if(json == "") { return false; }

			// Load the Data
			this.data = JsonConvert.DeserializeObject<LevelFormat>(json);

			return true;
		}

		public bool LoadLevelData(LevelFormat levelData) {
			this.data = levelData;
			this.levelId = levelData.id;
			return true;
		}

		private LevelFormat BuildLevelStruct() {
			LevelFormat levelStructure = new LevelFormat();

			levelStructure.id = levelId;
			levelStructure.rooms["0"] = new RoomFormat();
			//levelStructure.room["0"].bgLayer;
			levelStructure.rooms["0"].main["16"]["0"].Add(1);
			levelStructure.rooms["0"].main["16"]["0"].Add(1);
			levelStructure.rooms["0"].main["16"]["1"].Add(1);
			levelStructure.rooms["0"].main["16"]["1"].Add(1);
			//levelStructure.room["0"].cosmeticLayer;

			return levelStructure;
		}

		public void SaveLevel( string destDir = null, string destLevelId = null ) {

			// Determine the Destination Path and Destination Level ID
			if(destDir == null) { destDir = this.levelPath; }
			if(destLevelId == null) { destLevelId = this.levelId; }

			// Can only save a level state if the level ID is assigned correctly.
			if(destLevelId == null || destLevelId.Length == 0) { return; }

			// TODO LOW PRIORITY: Verify that the levelId exists; not just that an ID is present.

			// Make sure the full destination path exists:
			string fullDestPath = this.GetFullDestinationPath(destDir, this.levelId);

			if(!File.Exists(fullDestPath)) {

				#if debug
				throw new Exception("Unable to locate full destination path: " + fullDestPath);
				#endif

				return;
			}

			// Save State
			string json = JsonConvert.SerializeObject(this.data);
			File.WriteAllText(fullDestPath, json);
		}
	}
}
