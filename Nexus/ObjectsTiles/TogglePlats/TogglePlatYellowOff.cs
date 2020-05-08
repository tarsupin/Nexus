using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class TogglePlatYellowOff : TogglePlat {

		public TogglePlatYellowOff() : base() {
			this.Texture = "/Yellow/Plat";
			this.toggleBR = false;
			this.on = false;
			this.tileId = (byte)TileEnum.TogglePlatYellowOff;
		}
	}
}
