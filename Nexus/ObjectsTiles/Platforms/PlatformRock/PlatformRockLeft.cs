using Nexus.Gameplay;

namespace Nexus.Objects {

	public class PlatformRockLeft : PlatformFixedLeft {

		public PlatformRockLeft() : base() {
			this.BuildTexture("Platform/Rock/");
			this.tileId = (byte)TileEnum.PlatformRockLeft;
			this.title = "Rock Platform";
		}
	}
}
