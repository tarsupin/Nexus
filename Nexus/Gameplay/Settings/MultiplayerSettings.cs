using Newtonsoft.Json;
using Nexus.Engine;

namespace Nexus.Gameplay {

	public class MultiplayerJson {
		public bool StartAsGhostOnCoop;
	}

	public class MultiplayerSettings : MultiplayerJson {

		public MultiplayerSettings() {

			// Load Multiplayer Settings from Local File
			if(Systems.filesLocal.FileExists("Settings/Multiplayer.json")) {

				string fileContents = Systems.filesLocal.ReadFile("Settings/Multiplayer.json");

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
			Systems.filesLocal.WriteFile("Settings/Multiplayer.json", json);
		}
	}
}
