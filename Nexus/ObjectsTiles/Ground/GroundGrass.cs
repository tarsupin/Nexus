﻿using Nexus.Gameplay;

namespace Nexus.Objects {

	public class GroundGrass : Ground {

		public GroundGrass() : base() {
			this.BuildTextures("Grass/");
			this.tileId = (byte)TileEnum.GroundGrass;
			this.title = "Grass Block";
			this.description = "Hold `control` while clicking and dragging the mouse to Auto-Tile";
		}
	}
}
