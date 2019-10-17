using Newtonsoft.Json;
using Nexus.Engine;

namespace Nexus.Gameplay {

	public class AudioJson {
		public byte VolumeMaster;           // 0 to 100, 100 is maximum
		public byte VolumeSound;            // 0 to 100
		public byte VolumeMusic;            // 0 to 100
		public bool Mute;					// TRUE if currently muted.
	}

	public class AudioSettings : AudioJson {

		public AudioSettings() {

			// Load Audio Settings from Local File
			if(Systems.filesLocal.FileExists("Settings/Audio.json")) {

				string fileContents = Systems.filesLocal.ReadFile("Settings/Audio.json");

				AudioJson audioSettings = JsonConvert.DeserializeObject<AudioJson>(fileContents);

				this.VolumeMaster = audioSettings.VolumeMaster;
				this.VolumeSound = audioSettings.VolumeSound;
				this.VolumeMusic = audioSettings.VolumeMusic;
				this.Mute = audioSettings.Mute;

			// Assign Generic Settings & Create Audio Settings
			} else {

				// Assign Generic Settings
				this.VolumeMaster = 50;
				this.VolumeSound = 50;
				this.VolumeMusic = 50;
				this.Mute = false;

				// Create Audio Settings
				this.SaveSettings();
			}
		}

		public void SaveSettings() {

			AudioJson audioSettings = new AudioJson {
				VolumeMaster = this.VolumeMaster,
				VolumeSound = this.VolumeSound,
				VolumeMusic = this.VolumeMusic,
				Mute = this.Mute
			};

			string json = JsonConvert.SerializeObject(audioSettings);

			// Save JSON to Settings
			Systems.filesLocal.WriteFile("Settings/Audio.json", json);
		}
	}
}
