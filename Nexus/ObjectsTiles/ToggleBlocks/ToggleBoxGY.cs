using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ToggleBoxGY : ToggleBlock {

		public ToggleBoxGY() : base() {
			this.Texture = "/BoxGY";
			this.toggleBR = false;
			this.on = true;
			this.tileId = (byte)TileEnum.ToggleBoxGY;
		}
	}
}
