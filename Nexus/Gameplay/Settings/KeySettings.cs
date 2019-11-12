using Newtonsoft.Json;
using Nexus.Engine;

namespace Nexus.Gameplay {

	public class KeyJson {
		public char iKeyNonExist;
		public string macroF1;
		public string macroF2;
		public string macroF3;
		public string macroF4;
	}

	public class KeySettings : KeyJson {

		public KeySettings() {

			// Load Keys Settings from Local File
			if(Systems.filesLocal.FileExists("Settings/Keys.json")) {

				string fileContents = Systems.filesLocal.ReadFile("Settings/Keys.json");

				KeyJson keySettings = JsonConvert.DeserializeObject<KeyJson>(fileContents);

				this.iKeyNonExist = keySettings.iKeyNonExist;
				this.macroF1 = keySettings.macroF1;
				this.macroF2 = keySettings.macroF2;
				this.macroF3 = keySettings.macroF3;
				this.macroF4 = keySettings.macroF4;
			}

			// If Local File doesn't exist, Assign Generic Settings & Create Keys Settings
			else {

				// Assign Generic Settings
				this.iKeyNonExist = '=';
				this.macroF1 = "";
				this.macroF2 = "";
				this.macroF3 = "";
				this.macroF4 = "";

				// Create Keys Settings
				this.SaveSettings();
			}
		}

		public void SaveSettings() {

			KeyJson keySettings = new KeyJson {
				iKeyNonExist = this.iKeyNonExist,
				macroF1 = this.macroF1,
				macroF2 = this.macroF2,
				macroF3 = this.macroF3,
				macroF4 = this.macroF4,
			};

			string json = JsonConvert.SerializeObject(keySettings);

			// Save JSON to Settings
			Systems.filesLocal.WriteFile("Settings/Keys.json", json);
		}
	}
}
