using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ToggleBlockBlueOff : ToggleBlock {
		
		public ToggleBlockBlueOff() : base() {
			this.Texture = "/Blue/Block";
			this.toggleBR = true;
			this.on = false;
			this.tileId = (byte)TileEnum.ToggleBlockBlueOff;
		}
	}
}
