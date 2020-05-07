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

		public LevelContent( string levelDir = null ) {

			// If a level directory is not provided, use the default local directory.
			if(levelDir == null) {
				Systems.filesLocal.MakeDirectory("Levels"); // Make sure the Levels directory exists.
				this.levelPath = Path.Combine(Systems.filesLocal.localDir, "Levels");
			}

			// Make sure the level directory provided exists.
			else if(Directory.Exists(levelDir)) {
				this.levelPath = levelDir;
			}
		}

		public static string GetLocalLevelPath(string levelId) {
			return Path.Combine(levelId.Substring(0, 2), levelId + ".json");
		}

		public string GetFullLevelPath(string levelId) {
			return Path.Combine(this.levelPath, LevelContent.GetLocalLevelPath(levelId));
		}

		public bool LoadLevel(string levelId = "") {

			// Update the Level ID, or use existing Level ID if applicable.
			if(levelId.Length > 0) { this.levelId = levelId; } else if(this.levelId == "") { return false; }

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

		public void SaveLevel() {

			// Can only save a level state if the level ID is assigned correctly.
			if(this.levelId.Length == 0) { return; }

			// TODO LOW PRIORITY: Verify that the levelId exists; not just that an ID is present.

			// Save State
			string json = JsonConvert.SerializeObject(this.data);
			File.WriteAllText(this.GetFullLevelPath(this.levelId), json);
		}
	}
}
