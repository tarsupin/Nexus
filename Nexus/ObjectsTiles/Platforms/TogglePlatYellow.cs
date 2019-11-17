﻿using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class TogglePlatYellow : TogglePlat {

		public TogglePlatYellow(byte subTypeId) : base() {
			this.Texture = "/Yellow/Plat";
			this.toggleBR = false;
			this.tileId = (byte)TileEnum.TogglePlatYellow;
		}
	}
}
