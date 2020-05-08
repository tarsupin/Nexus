using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class TogglePlatYellow : TogglePlat {

		public TogglePlatYellow() : base() {
			this.Texture = "/Yellow/Plat";
			this.toggleBR = false;
			this.on = true;
			this.tileId = (byte)TileEnum.TogglePlatYellow;
		}
	}
}
