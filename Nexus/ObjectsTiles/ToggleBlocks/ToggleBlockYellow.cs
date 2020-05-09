using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ToggleBlockYellow : ToggleBlock {

		public ToggleBlockYellow() : base() {
			this.Texture = "/Yellow/Block";
			this.toggleBR = false;
			this.tileId = (byte)TileEnum.ToggleBlockYellow;
			this.title = "Yellow Toggle Block";
			this.description = "Toggles on and off when Green/Yellow toggles are switched.";
		}
	}
}
