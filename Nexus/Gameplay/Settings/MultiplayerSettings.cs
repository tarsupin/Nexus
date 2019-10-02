using Newtonsoft.Json;
using Nexus.Engine;

namespace Nexus.Gameplay {

	public class MultiplayerJson {
		public bool StartAsGhostOnCoop;
	}

	public class MultiplayerSettings : MultiplayerJson {
		readonly Systems systems;

		public MultiplayerSettings(Systems systems) {
			this.systems = systems;

			// Load Multiplayer Settings from Local File
			if(systems.filesLocal.FileExists("Settings/Multiplayer.json")) {

				string fileContents = systems.filesLocal.ReadFile("Settings/Multiplayer.json");

				MultiplayerJson mpSettings = JsonConvert.DeserializeObject<MultiplayerJson>(fileContents);

				this.StartAsGhostOnCoop = mpSettings.StartAsGhostOnCoop;

			// Assign Generic Settings & Create Multiplayer Settings
			} else {

				// Assign Generic Settings
				this.StartAsGhostOnCoop = true;

				// Create Multiplayer Settings
				this.SaveSettings();
			}
		}

		public void SaveSettings() {

			MultiplayerJson mpSettings = new MultiplayerJson {
				StartAsGhostOnCoop = this.StartAsGhostOnCoop,
			};

			string json = JsonConvert.SerializeObject(mpSettings);

			// Save JSON to Settings
			this.systems.filesLocal.WriteFile("Settings/Multiplayer.json", json);
		}
	}
}
