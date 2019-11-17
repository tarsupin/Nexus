using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ToggleBlockRed : ToggleBlock {

		public ToggleBlockRed() : base() {
			this.Texture = "/Red/Block";
			this.toggleBR = true;
			this.tileId = (byte)TileEnum.ToggleBlockRed;
		}
	}
}
