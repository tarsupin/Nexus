using Newtonsoft.Json;
using Nexus.Engine;

namespace Nexus.Gameplay {

	public class GraphicsJson {
		public bool DisplayParticles;
		public ushort MaxParticles;
	}

	public class GraphicSettings : GraphicsJson {

		public GraphicSettings() {

			// Load Graphics Settings from Local File
			if(Systems.filesLocal.FileExists("Settings/Graphics.json")) {

				string fileContents = Systems.filesLocal.ReadFile("Settings/Graphics.json");

				GraphicsJson graphicsSettings = JsonConvert.DeserializeObject<GraphicsJson>(fileContents);

				this.DisplayParticles = graphicsSettings.DisplayParticles;
				this.MaxParticles = graphicsSettings.MaxParticles;

			// Assign Generic Settings & Create Graphics Settings
			} else {

				// Assign Generic Settings
				this.DisplayParticles = true;
				this.MaxParticles = 100;

				// Create Graphics Settings
				this.SaveSettings();
			}
		}

		public void SaveSettings() {

			GraphicsJson graphicsSettings = new GraphicsJson {
				DisplayParticles = this.DisplayParticles,
				MaxParticles = this.MaxParticles,
			};

			string json = JsonConvert.SerializeObject(graphicsSettings);

			// Save JSON to Settings
			Systems.filesLocal.WriteFile("Settings/Graphics.json", json);
		}
	}
}
