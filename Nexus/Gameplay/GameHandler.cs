
using Nexus.Engine;

namespace Nexus.Gameplay {

	public class GameHandler {

		// ID of Game State & Save
		public readonly string saveId;

		// Game Content Classes
		public readonly LevelContent levelContent;
		public readonly WorldContent worldContent;

		// Game State Classes
		public readonly CampaignState campaignState;
		public readonly EditorState editorState;
		public readonly LevelState levelState;
		public readonly PlaylistState playlistState;
		public readonly WorldState worldState;

		public GameHandler(string saveId) {
			this.saveId = saveId;

			// Make sure the Saves directory exists.
			Systems.filesLocal.MakeDirectory("Saves/" + saveId);
			Systems.filesLocal.MakeDirectory("Saves/" + saveId + "/Campaign");

			// Content
			this.levelContent = new LevelContent();
			this.worldContent = new WorldContent();

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
			Systems.filesLocal.WriteFile("Saves/" + this.saveId + "/" + stateName + ".json", json);
		}

		public string GameStateRead(string stateName) {
			string statePath = "Saves/" + this.saveId + "/" + stateName + ".json";

			// Load Save Content from Local File
			if(Systems.filesLocal.FileExists(statePath)) {
				return Systems.filesLocal.ReadFile(statePath);
			}

			return "";
		}
	}
}
