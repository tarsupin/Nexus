using Nexus.Gameplay;

namespace Nexus.Objects {

	public class GroundStone : Ground {

		public GroundStone() : base() {
			this.BuildTextures("Stone/");
			this.tileId = (byte)TileEnum.GroundStone;
		}
	}
}
