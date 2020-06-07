using Nexus.Gameplay;

namespace Nexus.Objects {

	public class PlatformRockRight : PlatformFixedRight {

		public PlatformRockRight() : base() {
			this.BuildTexture("Platform/Rock/");
			this.tileId = (byte)TileEnum.PlatformRockRight;
			this.title = "Rock Platform";
		}
	}
}
