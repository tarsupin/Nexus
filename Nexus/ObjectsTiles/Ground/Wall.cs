using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Wall : Ground {

		public Wall() : base() {
			this.BuildTextures("Slab/Brown/");   // TODO: Change to Wall
			this.tileId = (byte)TileEnum.Wall;
			this.title = "Wall Block";
			this.description = "";
		}
	}
}
