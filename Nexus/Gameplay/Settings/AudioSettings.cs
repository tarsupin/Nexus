using Newtonsoft.Json;
using Nexus.Engine;

namespace Nexus.Gameplay {

	public class AudioJson {
		public byte MasterValue;		// 0 to 100, 100 is maximum
		public byte SoundValue;			// 0 to 100
		public byte MusicValue;			// 0 to 100
		public bool Mute;               // TRUE if currently muted.

		public float SoundVolume = 0f;
		public float MusicVolume = 0f;
	}

	public class AudioSettings : AudioJson {

		public AudioSettings() {

			// Load Audio Settings from Local File
			if(Systems.filesLocal.FileExists("Settings/Audio.json")) {

				string fileContents = Systems.filesLocal.ReadFile("Settings/Audio.json");

				AudioJson audioSettings = JsonConvert.DeserializeObject<AudioJson>(fileContents);

				this.MasterValue = audioSettings.MasterValue;
				this.SoundValue = audioSettings.SoundValue;
				this.MusicValue = audioSettings.MusicValue;
				this.Mute = audioSettings.Mute;

			// Assign Generic Settings & Create Audio Settings
			} else {

				// Assign Generic Settings
				this.MasterValue = 50;
				this.SoundValue = 50;
				this.MusicValue = 50;
				this.Mute = false;

				// Create Audio Settings
				this.SaveSettings();
			}

			this.UpdatedAudioSettings();
		}

		private void UpdatedAudioSettings() {

			if(this.Mute) {
				this.SoundVolume = 0f;
				this.MusicVolume = 0f;
				return;
			}

			this.SoundVolume = (float)this.SoundValue / 100f * (float)this.MasterValue / 100f;
			this.MusicVolume = (float)this.MusicValue / 100f * (float)this.MasterValue / 100f;
		}

		public void SaveSettings() {

			AudioJson audioSettings = new AudioJson {
				MasterValue = this.MasterValue,
				SoundValue = this.SoundValue,
				MusicValue = this.MusicValue,
				Mute = this.Mute
			};

			string json = JsonConvert.SerializeObject(audioSettings);

			// Save JSON to Settings
			Systems.filesLocal.WriteFile("Settings/Audio.json", json);
		}
	}
}
