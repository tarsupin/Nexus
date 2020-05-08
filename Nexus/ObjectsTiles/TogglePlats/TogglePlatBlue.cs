using Nexus.Gameplay;

namespace Nexus.Objects {

	public class TogglePlatBlue : TogglePlat {

		public TogglePlatBlue() : base() {
			this.Texture = "/Blue/Plat";
			this.toggleBR = true;
			this.on = true;
			this.tileId = (byte)TileEnum.TogglePlatBlue;
		}
	}
}
