using Nexus.Gameplay;

namespace Nexus.Objects {

	public class GroundSlime : Ground {

		public GroundSlime() : base() {
			this.BuildTextures("Slime/");
			this.tileId = (byte)TileEnum.GroundSlime;
		}
	}
}
