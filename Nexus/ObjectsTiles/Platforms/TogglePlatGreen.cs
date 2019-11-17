using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class TogglePlatGreen : TogglePlat {

		public TogglePlatGreen(byte subTypeId) : base() {
			this.Texture = "/Green/Plat";
			this.toggleBR = false;
			this.tileId = (byte)TileEnum.TogglePlatGreen;
		}
	}
}
