using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ToggleBlockBlue : ToggleBlock {
		
		public ToggleBlockBlue() : base() {
			this.Texture = "/Blue/Block";
			this.toggleBR = true;
			this.tileId = (byte)TileEnum.ToggleBlockBlue;
		}
	}
}
