using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ToggleBoxGY : ToggleBlock {

		public ToggleBoxGY() : base() {
			this.Texture = "/BoxGY";
			this.toggleBR = false;
			this.isOn = true;
			this.isToggleBox = true;
			this.tileId = (byte)TileEnum.ToggleBoxGY;
			this.title = "Green-Yellow Toggle Box";
			this.description = "Toggles the Green/Yellow color toggles.";
		}
	}
}
