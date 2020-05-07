using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class TogglePlatGreen : TogglePlat {

		public TogglePlatGreen() : base() {
			this.Texture = "/Green/Plat";
			this.toggleBR = false;
			this.tileId = (byte)TileEnum.TogglePlatGreen;
		}
	}
}
