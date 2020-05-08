using Nexus.Gameplay;

namespace Nexus.Objects {

	public class TogglePlatBlueOff : TogglePlat {

		public TogglePlatBlueOff() : base() {
			this.Texture = "/Blue/Plat";
			this.toggleBR = true;
			this.on = false;
			this.tileId = (byte)TileEnum.TogglePlatBlueOff;
		}
	}
}
