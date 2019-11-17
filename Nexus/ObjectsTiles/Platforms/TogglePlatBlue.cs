using Nexus.Gameplay;

namespace Nexus.Objects {

	public class TogglePlatBlue : TogglePlat {

		public TogglePlatBlue(byte subTypeId) : base() {
			this.Texture = "/Blue/Plat";
			this.toggleBR = true;
			this.tileId = (byte)TileEnum.TogglePlatBlue;
		}
	}
}
