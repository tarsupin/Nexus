using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ToggleBlockYellowOff : ToggleBlock {

		public ToggleBlockYellowOff() : base() {
			this.Texture = "/Yellow/Block";
			this.toggleBR = false;
			this.on = false;
			this.tileId = (byte)TileEnum.ToggleBlockYellowOff;
		}
	}
}
