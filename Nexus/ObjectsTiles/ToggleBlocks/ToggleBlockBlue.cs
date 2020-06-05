using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ToggleBlockBlue : ToggleBlock {
		
		public ToggleBlockBlue() : base() {
			this.Texture = "/Blue/Block";
			this.toggleBR = true;
			this.isOn = true;
			this.tileId = (byte)TileEnum.ToggleBlockBlue;
			this.title = "Blue Toggle Block";
			this.description = "Toggles on and off when Blue/Red toggles are switched.";
		}
	}
}
