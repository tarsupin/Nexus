using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ToggleBlockRed : ToggleBlock {

		public ToggleBlockRed() : base() {
			this.Texture = "/Red/Block";
			this.toggleBR = true;
			this.tileId = (byte)TileEnum.ToggleBlockRed;
			this.title = "Red Toggle Block";
			this.description = "Toggles on and off when Blue/Red toggles are switched.";
		}
	}
}
