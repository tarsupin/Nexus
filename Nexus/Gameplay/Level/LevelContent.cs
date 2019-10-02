using Newtonsoft.Json;
using Nexus.Engine;

namespace Nexus.Gameplay {

	public class LevelContent {

		// References
		private readonly Systems systems;
		public readonly LevelGenerate generate;	

		// Level Data
		public string levelId;          // Level ID (e.g. "QCALQOD16")
		public LevelFormat data;		// Level Data

		public LevelContent(GameHandler gameHandler) {
			this.systems = gameHandler.systems;
			this.generate = new LevelGenerate(this, gameHandler);

			// Make sure the Levels directory exists.
			this.systems.filesLocal.MakeDirectory("Levels");
		}

		public bool LoadLevel(string levelId = "") {

			// Update the Level ID, or use existing Level ID if applicable.
			if(levelId.Length > 0) { this.levelId = levelId; } else if(this.levelId == "") { return false; }

			string localPath = LevelContent.GetLocalLevelPath(this.levelId);
			
			// Make sure the level exists:
			if(!this.systems.filesLocal.FileExists(localPath)) { return false; }

			string json = systems.filesLocal.ReadFile(localPath);
			
			// If there is no JSON content, end the attempt to load level:
			if(json == "") { return false; }

			// Load the Data
			this.data = JsonConvert.DeserializeObject<LevelFormat>(json);

			return true;
		}

		public static string GetLocalLevelPath( string levelId ) {
			return "Levels/" + levelId.Substring(0, 2) + "/" + levelId + ".json";
		}

		public static string GetFullLevelPath(Systems systems, string levelId) {
			return systems.filesLocal.LocalFilePath(LevelContent.GetLocalLevelPath(levelId));
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
			this.systems.filesLocal.WriteFile(LevelContent.GetLocalLevelPath(this.levelId), json);
		}
	}
}
