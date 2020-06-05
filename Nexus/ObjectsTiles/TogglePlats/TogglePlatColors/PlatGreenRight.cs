﻿using Nexus.Gameplay;

namespace Nexus.Objects {

	public class PlatGreenRight : TogglePlatRight {

		public PlatGreenRight() : base() {
			this.Texture = "/Green/Plat";
			this.toggleBR = false;
			this.isOn = true;
			this.tileId = (byte)TileEnum.PlatGreenRight;
			this.title = "Green Toggle Platform";
			this.description = "Acts like a platform when Green-Toggles are ON.";
		}
	}
}
