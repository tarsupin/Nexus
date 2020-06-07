using Nexus.Gameplay;

namespace Nexus.Objects {

	public class PlatformRockDown : PlatformFixedDown {

		public PlatformRockDown() : base() {
			this.BuildTexture("Platform/Rock/");
			this.tileId = (byte)TileEnum.PlatformRockDown;
			this.title = "Rock Platform";
		}
	}
}
