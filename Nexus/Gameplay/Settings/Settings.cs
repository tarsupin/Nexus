using Nexus.Engine;
using Nexus.GameEngine;

namespace Nexus.Gameplay {
	public class Settings {

		public readonly AudioSettings audio;
		public readonly GraphicSettings graphics;
		public readonly KeySettings input;
		public readonly MultiplayerSettings multiplayer;

		public Settings(Systems systems) {

			// Make sure the Settings directory exists.
			systems.filesLocal.MakeDirectory("Settings");

			this.audio = new AudioSettings(systems);
			this.graphics = new GraphicSettings(systems);
			this.input = new KeySettings(systems);
			this.multiplayer = new MultiplayerSettings(systems);
		}
	}
}
