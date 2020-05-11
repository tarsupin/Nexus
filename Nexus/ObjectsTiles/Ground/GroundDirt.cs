using Nexus.Gameplay;

namespace Nexus.Objects {

	public class GroundDirt : Ground {

		public GroundDirt() : base() {
			this.BuildTextures("Grass/");
			this.tileId = (byte)TileEnum.GroundDirt;
			this.title = "Dirt Block";
			this.description = "Hold Control to Auto-Tile";
		}
	}
}
