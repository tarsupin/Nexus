using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class TogglePlatRedOff : TogglePlat {

		public TogglePlatRedOff() : base() {
			this.Texture = "/Red/Plat";
			this.toggleBR = true;
			this.on = false;
			this.tileId = (byte)TileEnum.TogglePlatRedOff;
		}
	}
}
