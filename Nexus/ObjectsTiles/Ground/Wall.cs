using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Wall : Ground {

		public Wall() : base() {
			this.BuildTextures("Slab/Gray/");   // TODO: Change to Wall
			this.tileId = (byte)TileEnum.Wall;
		}
	}
}
