using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ToggleBlockRedOff : ToggleBlock {

		public ToggleBlockRedOff() : base() {
			this.Texture = "/Red/Block";
			this.toggleBR = true;
			this.on = false;
			this.tileId = (byte)TileEnum.ToggleBlockRedOff;
		}
	}
}
