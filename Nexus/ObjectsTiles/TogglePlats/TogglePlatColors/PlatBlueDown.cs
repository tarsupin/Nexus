using Nexus.Gameplay;

namespace Nexus.Objects {

	public class PlatBlueDown : TogglePlatDown {

		public PlatBlueDown() : base() {
			this.Texture = "/Blue/Plat";
			this.toggleBR = true;
			this.isOn = true;
			this.tileId = (byte)TileEnum.PlatBlueDown;
			this.title = "Blue Toggle Platform";
			this.description = "Acts like a platform when Blue-Toggles are ON.";
		}
	}
}
