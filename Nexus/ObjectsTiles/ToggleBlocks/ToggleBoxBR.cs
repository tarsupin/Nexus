using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ToggleBoxBR : ToggleBlock {

		public ToggleBoxBR() : base() {
			this.Texture = "/BoxBR";
			this.toggleBR = true;
			this.isOn = true;
			this.isToggleBox = true;
			this.tileId = (byte)TileEnum.ToggleBoxBR;
			this.title = "Blue-Red Toggle Box";
			this.description = "Toggles the Blue/Red color toggles.";
		}
	}
}
