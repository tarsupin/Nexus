using Nexus.Gameplay;

namespace Nexus.Objects {

	public class SlabGray : HorizontalWall {

		public SlabGray() : base() {
			this.BuildTextures("Slab/Gray/");
			this.tileId = (byte)TileEnum.SlabGray;
			this.title = "Gray Slab Block";
			this.description = "Hold Control to Auto-Tile";
		}
	}
}
