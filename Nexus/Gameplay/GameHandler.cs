
using Nexus.Engine;

namespace Nexus.Gameplay {

	public class GameHandler {

		// Systems
		public readonly Systems systems;

		// ID of Game State & Save
		public readonly string saveId;

		// Game Content Classes
		public readonly LevelContent level;
		public readonly WorldContent world;

		// Game State Classes
		public readonly CampaignState campaignState;
		public readonly EditorState editorState;
		public readonly LevelState levelState;
		public readonly PlaylistState playlistState;
		public readonly WorldState worldState;

		public GameHandler(Systems systems, string saveId) {
			this.systems = systems;
			this.saveId = saveId;

			// Make sure the Saves directory exists.
			systems.filesLocal.MakeDirectory("Saves/" + saveId);

			// Content
			this.level = new LevelContent(this);
			this.world = new WorldContent(this);

			// State
			this.campaignState = new CampaignState(this);
			this.editorState = new EditorState(this);
			this.levelState = new LevelState(this);
			this.playlistState = new PlaylistState(this);
			this.worldState = new WorldState(this);
		}

		// Save Game State
		public void SaveGameState() {
			this.campaignState.SaveCampaign();
			this.editorState.SaveEditor();
			this.levelState.SaveLevelState();
		}

		public void GameStateWrite(string stateName, string json) {
			this.systems.filesLocal.WriteFile("Saves/" + this.saveId + "/" + stateName + ".json", json);
		}

		public string GameStateRead(string stateName) {
			string statePath = "Saves/" + this.saveId + "/" + stateName + ".json";

			// Load Save Content from Local File
			if(this.systems.filesLocal.FileExists(statePath)) {
				return systems.filesLocal.ReadFile(statePath);
			}

			return "";
		}
	}
}
