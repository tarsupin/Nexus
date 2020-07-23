using Newtonsoft.Json;
using Nexus.Engine;

namespace Nexus.Gameplay {

	public class TutorialJson {
		public int Editor;
		public int WorldEditor;
	}

	public class TutorialSettings : TutorialJson {

		public TutorialSettings() {

			// Load Tutorial Settings from Local File
			if(Systems.filesLocal.FileExists("Settings/Tutorial.json")) {

				string fileContents = Systems.filesLocal.ReadFile("Settings/Tutorial.json");

				TutorialJson tutSettings = JsonConvert.DeserializeObject<TutorialJson>(fileContents);

				// Assign Settings
				this.Editor = tutSettings.Editor;
				this.WorldEditor = tutSettings.WorldEditor;

			// Assign Generic Settings & Create Tutorial Settings
			} else {

				// Assign Generic Settings
				this.Editor = 0;
				this.WorldEditor = 0;

				// Create Tutorial Settings
				this.SaveSettings();
			}
		}

		public void UpdateEditorStep(short editorStep) {
			this.Editor = editorStep;
			this.SaveSettings();
		}
		
		public void UpdateWorldEditorStep(short weStep) {
			this.WorldEditor = weStep;
			this.SaveSettings();
		}

		public void SaveSettings() {

			TutorialJson tutSettings = new TutorialJson {
				Editor = this.Editor,
				WorldEditor = this.WorldEditor,
			};

			string json = JsonConvert.SerializeObject(tutSettings);

			// Save JSON to Settings
			Systems.filesLocal.WriteFile("Settings/Tutorial.json", json);
		}
	}
}
