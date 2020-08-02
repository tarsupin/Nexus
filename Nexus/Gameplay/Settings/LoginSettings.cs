using Newtonsoft.Json;
using Nexus.Engine;

namespace Nexus.Gameplay {

	public class LoginJson {
		public string User;
		public string Token;
		public byte HeadVal;
	}

	public class LoginSettings : LoginJson {

		public LoginSettings() {

			// Load Login Settings from Local File
			if(Systems.filesLocal.FileExists("Settings/Login.json")) {

				string fileContents = Systems.filesLocal.ReadFile("Settings/Login.json");

				LoginJson loginSettings = JsonConvert.DeserializeObject<LoginJson>(fileContents);

				// Assign Settings
				this.User = loginSettings.User;
				this.Token = loginSettings.Token;
				this.HeadVal = loginSettings.HeadVal;

			// Assign Generic Settings & Create Login Settings
			} else {

				// Assign Generic Settings
				this.User = "";
				this.Token = "";
				this.HeadVal = 0;

				// Create Login Settings
				this.SaveSettings();
			}
		}

		public void SaveSettings() {

			LoginJson loginSettings = new LoginJson {
				User = this.User,
				Token = this.Token,
				HeadVal = this.HeadVal,
			};

			string json = JsonConvert.SerializeObject(loginSettings);

			// Save JSON to Settings
			Systems.filesLocal.WriteFile("Settings/Login.json", json);
		}
	}
}
