using Nexus.Gameplay;

namespace Nexus.Objects {

	public class SlabBrown : HorizontalWall {

		public SlabBrown() : base() {
			this.BuildTextures("Slab/Brown/");
			this.tileId = (byte)TileEnum.SlabBrown;
			this.title = "Brown Slab Block";
			this.description = "Hold `control` while clicking and dragging the mouse to Auto-Tile";
		}
	}
}
