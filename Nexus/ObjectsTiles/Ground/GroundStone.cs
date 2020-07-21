using Nexus.Gameplay;

namespace Nexus.Objects {

	public class GroundStone : Ground {

		public GroundStone() : base() {
			this.BuildTextures("Stone/");
			this.tileId = (byte)TileEnum.GroundStone;
			this.title = "Stone Block";
			this.description = "Hold `control` while clicking and dragging the mouse to Auto-Tile";
		}
	}
}
