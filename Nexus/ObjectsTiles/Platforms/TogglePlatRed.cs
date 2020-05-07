using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class TogglePlatRed : TogglePlat {

		public TogglePlatRed() : base() {
			this.Texture = "/Red/Plat";
			this.toggleBR = true;
			this.tileId = (byte)TileEnum.TogglePlatRed;
		}
	}
}
