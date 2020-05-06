using Newtonsoft.Json;
using Nexus.Engine;
using System.IO;

namespace Nexus.Gameplay {

	public class LevelContent {

		// Level Data
		public string levelId;          // Level ID (e.g. "QCALQOD16")
		public LevelFormat data;		// Level Data

		public LevelContent() {

			// Make sure the Levels directory exists.
			Systems.filesLocal.MakeDirectory("Levels");
		}

		public bool LoadLevel(string levelId = "") {

			// Update the Level ID, or use existing Level ID if applicable.
			if(levelId.Length > 0) { this.levelId = levelId; } else if(this.levelId == "") { return false; }

			string localPath = Path.Combine("Levels", LevelContent.GetLocalLevelPath(this.levelId));
			
			// Make sure the level exists:
			if(!Systems.filesLocal.FileExists(localPath)) { return false; }

			string json = Systems.filesLocal.ReadFile(localPath);
			
			// If there is no JSON content, end the attempt to load level:
			if(json == "") { return false; }

			// Load the Data
			this.data = JsonConvert.DeserializeObject<LevelFormat>(json);

			return true;
		}

		public static string GetLocalLevelPath( string levelId ) {
			return Path.Combine(levelId.Substring(0, 2), levelId + ".json");
		}

		public static string GetFullLevelPath(string levelId) {
			return Systems.filesLocal.LocalFilePath(Path.Combine("Levels", LevelContent.GetLocalLevelPath(levelId)));
		}

		private LevelFormat BuildLevelStruct() {
			LevelFormat levelStructure = new LevelFormat();

			levelStructure.id = levelId;
			levelStructure.room["0"] = new RoomFormat();
			//levelStructure.room["0"].bgLayer;
			levelStructure.room["0"].main["16"]["0"].Add(1);
			levelStructure.room["0"].main["16"]["0"].Add(1);
			levelStructure.room["0"].main["16"]["1"].Add(1);
			levelStructure.room["0"].main["16"]["1"].Add(1);
			//levelStructure.room["0"].cosmeticLayer;

			return levelStructure;
		}

		public void SaveLevel() {

			// Can only save a level state if the level ID is assigned correctly.
			if(this.levelId.Length == 0) { return; }

			// TODO LOW PRIORITY: Verify that the levelId exists; not just that an ID is present.

			// Save State
			string json = JsonConvert.SerializeObject(this.data);
			Systems.filesLocal.WriteFile(Path.Combine("Levels", LevelContent.GetLocalLevelPath(this.levelId)), json);
		}
	}
}
