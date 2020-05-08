using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ToggleBlockGreenOff : ToggleBlock {

		public ToggleBlockGreenOff() : base() {
			this.Texture = "/Green/Block";
			this.toggleBR = false;
			this.on = false;
			this.tileId = (byte)TileEnum.ToggleBlockGreenOff;
		}
	}
}
