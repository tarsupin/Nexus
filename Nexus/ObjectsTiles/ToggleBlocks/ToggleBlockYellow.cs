using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ToggleBlockYellow : ToggleBlock {

		public ToggleBlockYellow() : base() {
			this.Texture = "/Yellow/Block";
			this.toggleBR = false;
			this.on = true;
			this.tileId = (byte)TileEnum.ToggleBlockYellow;
		}
	}
}
