using Nexus.Gameplay;

namespace Nexus.Objects {

	public class GroundSlime : Ground {

		public GroundSlime() : base() {
			this.BuildTextures("Slime/");
			this.tileId = (byte)TileEnum.GroundSlime;
			this.title = "Slime Block";
			this.description = "Hold Control to Auto-Tile";
		}
	}
}
