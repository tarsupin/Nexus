using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ToggleBoxBR : ToggleBlock {

		public ToggleBoxBR() : base() {
			this.Texture = "/BoxBR";
			this.toggleBR = true;
			this.on = true;
			this.tileId = (byte)TileEnum.ToggleBoxBR;
		}
	}
}
