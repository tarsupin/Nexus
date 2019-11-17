using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class GroundGrass : Ground {

		public GroundGrass() : base() {
			this.BuildTextures("Grass/");
			this.tileId = (byte)TileEnum.GroundGrass;
		}
	}
}
