using Nexus.Gameplay;

namespace Nexus.Objects {

	public class PlatformRockUp : PlatformFixedUp {

		public PlatformRockUp() : base() {
			this.BuildTexture("Platform/Rock/");
			this.tileId = (byte)TileEnum.PlatformRockUp;
			this.title = "Rock Platform";
		}
	}
}
