
using Newtonsoft.Json;
using Nexus.Engine;

namespace Nexus.Gameplay {

	public class WorldContent {

		// World Data
		public string worldId;          // World ID (e.g. "QCALQOD16")
		public WorldFormat data;		// World Data

		public WorldContent(GameHandler gameHandler) {

			// Make sure the Worlds directory exists.
			Systems.filesLocal.MakeDirectory("Worlds");
		}

		public void LoadWorld(string worldId) {

			// Update the World ID, or use existing World ID if applicable.
			if(worldId.Length > 0) { this.worldId = worldId; } else if(this.worldId == "") { return; }

			string localPath = WorldContent.GetLocalWorldPath(this.worldId);

			// Make sure the world exists:
			if(!Systems.filesLocal.FileExists(localPath)) { return; }

			string json = Systems.filesLocal.ReadFile(localPath);

			// If there is no JSON content, end the attempt to load world:
			if(json == "") { return; }

			// Load the Data
			this.data = JsonConvert.DeserializeObject<WorldFormat>(json);
		}

		public static string GetLocalWorldPath(string worldId) {
			return "Worlds/" + worldId.Substring(0, 2) + "/" + worldId + ".json";
		}

		private WorldFormat BuildWorldStruct() {
			WorldFormat worldStructure = new WorldFormat();
			return worldStructure;
		}

		public void SaveWorld() {

			// Can only save a world state if the world ID is assigned correctly.
			if(this.worldId.Length == 0) { return; }

			// TODO LOW PRIORITY: Verify that the worldId exists; not just that an ID is present.

			// Save State
			string json = JsonConvert.SerializeObject(this.data);
			Systems.filesLocal.WriteFile(WorldContent.GetLocalWorldPath(this.worldId), json);
		}
	}
}
