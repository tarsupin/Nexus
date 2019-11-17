using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ToggleBlockGreen : ToggleBlock {

		public ToggleBlockGreen() : base() {
			this.Texture = "/Green/Block";
			this.toggleBR = false;
			this.tileId = (byte)TileEnum.ToggleBlockGreen;
		}
	}
}
