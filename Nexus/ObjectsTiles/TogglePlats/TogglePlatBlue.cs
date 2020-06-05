using Nexus.Gameplay;

namespace Nexus.Objects {

	public class TogglePlatBlue : TogglePlat {

		public TogglePlatBlue() : base() {
			this.Texture = "/Blue/Plat";
			this.toggleBR = true;
			this.isOn = true;
			this.tileId = (byte)TileEnum.TogglePlatBlue;
			this.title = "Blue Toggle Platform";
			this.description = "Acts like a platform when Blue-Toggles are ON.";
		}
	}
}
