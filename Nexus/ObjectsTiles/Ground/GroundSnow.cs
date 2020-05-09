using Nexus.Gameplay;

namespace Nexus.Objects {

	public class GroundSnow : Ground {

		public GroundSnow() : base() {
			this.BuildTextures("Snow/");
			this.tileId = (byte)TileEnum.GroundSnow;
			this.title = "Snow Block";
			this.description = "";
		}
	}
}
