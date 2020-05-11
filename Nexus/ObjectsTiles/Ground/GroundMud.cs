using Nexus.Gameplay;

namespace Nexus.Objects {

	public class GroundMud : Ground {

		public GroundMud() : base() {
			this.BuildTextures("Mud/");
			this.tileId = (byte)TileEnum.GroundMud;
			this.title = "Mud Block";
			this.description = "Hold Control to Auto-Tile";
		}
	}
}
