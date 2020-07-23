﻿using Nexus.Engine;

namespace Nexus.Gameplay {

	public class Settings {

		public readonly AudioSettings audio;
		public readonly GraphicSettings graphics;
		public readonly KeySettings input;
		public readonly LoginSettings login;
		public readonly MultiplayerSettings multiplayer;
		public readonly TutorialSettings tutorial;

		public Settings() {

			// Make sure the Settings directory exists.
			Systems.filesLocal.MakeDirectory("Settings");

			this.audio = new AudioSettings();
			this.graphics = new GraphicSettings();
			this.input = new KeySettings();
			this.login = new LoginSettings();
			this.multiplayer = new MultiplayerSettings();
			this.tutorial = new TutorialSettings();
		}
	}
}
