using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class TogglePlatGreenOff : TogglePlat {

		public TogglePlatGreenOff() : base() {
			this.Texture = "/Green/Plat";
			this.toggleBR = false;
			this.on = false;
			this.tileId = (byte)TileEnum.TogglePlatGreenOff;
		}
	}
}
