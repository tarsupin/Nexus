using Newtonsoft.Json;
using Nexus.Engine;

namespace Nexus.Gameplay {

	public class KeyJson {
		public char iKeyNonExist;
	}

	public class KeySettings : KeyJson {

		public KeySettings() {

			// Load Keys Settings from Local File
			if(Systems.filesLocal.FileExists("Settings/Keys.json")) {

				string fileContents = Systems.filesLocal.ReadFile("Settings/Keys.json");

				KeyJson keySettings = JsonConvert.DeserializeObject<KeyJson>(fileContents);

				this.iKeyNonExist = keySettings.iKeyNonExist;

			// Assign Generic Settings & Create Keys Settings
			} else {

				// Assign Generic Settings
				this.iKeyNonExist = '=';

				// Create Keys Settings
				this.SaveSettings();
			}
		}

		public void SaveSettings() {

			KeyJson keySettings = new KeyJson {
				iKeyNonExist = this.iKeyNonExist,
			};

			string json = JsonConvert.SerializeObject(keySettings);

			// Save JSON to Settings
			Systems.filesLocal.WriteFile("Settings/Keys.json", json);
		}
	}
}
