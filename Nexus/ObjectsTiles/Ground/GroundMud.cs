using Nexus.Gameplay;

namespace Nexus.Objects {

	public class GroundMud : Ground {

		public GroundMud() : base() {
			this.BuildTextures("Mud/");
			this.tileId = (byte)TileEnum.GroundMud;
		}
	}
}
