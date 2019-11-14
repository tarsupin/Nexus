using Newtonsoft.Json;
using Nexus.Engine;

namespace Nexus.Gameplay {

	public class KeyJson {
		public char iKeyNonExist;

		// Macro Keys (F1 - F8)
		public string macroF1;
		public string macroF2;
		public string macroF3;
		public string macroF4;
		public string macroF5;
		public string macroF6;
		public string macroF7;
		public string macroF8;

		// Console "Down" List (saved between sessions)
		public string[] consoleDown;
	}

	public class KeySettings : KeyJson {

		public KeySettings() {

			// Load Keys Settings from Local File
			if(Systems.filesLocal.FileExists("Settings/Keys.json")) {

				string fileContents = Systems.filesLocal.ReadFile("Settings/Keys.json");

				KeyJson keySettings = JsonConvert.DeserializeObject<KeyJson>(fileContents);

				this.iKeyNonExist = keySettings.iKeyNonExist;

				// Macro Keys (F1 - F8)
				this.macroF1 = keySettings.macroF1;
				this.macroF2 = keySettings.macroF2;
				this.macroF3 = keySettings.macroF3;
				this.macroF4 = keySettings.macroF4;
				this.macroF5 = keySettings.macroF5;
				this.macroF6 = keySettings.macroF6;
				this.macroF7 = keySettings.macroF7;
				this.macroF8 = keySettings.macroF8;

				// Console "Down" List (saved between sessions)
				this.consoleDown = keySettings.consoleDown;
			}

			// If Local File doesn't exist, Assign Generic Settings & Create Keys Settings
			else {

				// Assign Generic Settings
				this.iKeyNonExist = '=';

				// Macro Keys (F1 - F8)
				this.macroF1 = "";
				this.macroF2 = "";
				this.macroF3 = "";
				this.macroF4 = "";
				this.macroF5 = "";
				this.macroF6 = "";
				this.macroF7 = "";
				this.macroF8 = "";

				// Console "Down" List (saved between sessions)
				this.consoleDown = new string[5] { "", "", "", "", "" };

				// Create Keys Settings
				this.SaveSettings();
			}
		}

		public void SaveSettings() {

			KeyJson keySettings = new KeyJson {
				iKeyNonExist = this.iKeyNonExist,

				// Macro Keys (F1 - F8)
				macroF1 = this.macroF1,
				macroF2 = this.macroF2,
				macroF3 = this.macroF3,
				macroF4 = this.macroF4,
				macroF5 = this.macroF5,
				macroF6 = this.macroF6,
				macroF7 = this.macroF7,
				macroF8 = this.macroF8,

				// Console "Down" List (saved between sessions)
				consoleDown = this.consoleDown,
			};

			string json = JsonConvert.SerializeObject(keySettings);

			// Save JSON to Settings
			Systems.filesLocal.WriteFile("Settings/Keys.json", json);
		}
	}
}
