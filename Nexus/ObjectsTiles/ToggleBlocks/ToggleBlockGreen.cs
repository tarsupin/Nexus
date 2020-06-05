using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ToggleBlockGreen : ToggleBlock {

		public ToggleBlockGreen() : base() {
			this.Texture = "/Green/Block";
			this.toggleBR = false;
			this.isOn = true;
			this.tileId = (byte)TileEnum.ToggleBlockGreen;
			this.title = "Green Toggle Block";
			this.description = "Toggles on and off when Green/Yellow toggles are switched.";
		}
	}
}
