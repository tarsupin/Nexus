using Nexus.Gameplay;

namespace Nexus.Objects {

	public class GroundCloud : Ground {

		public GroundCloud() : base() {
			this.tileId = (byte)TileEnum.GroundCloud;
			this.Texture = new string[1];
			this.Texture[(byte)GroundSubTypes.S] = "Cloud/S";
		}
	}
}
