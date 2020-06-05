using Nexus.Gameplay;

namespace Nexus.Objects {

	public class PlatYellowDown : TogglePlatDown {

		public PlatYellowDown() : base() {
			this.Texture = "/Yellow/Plat";
			this.toggleBR = false;
			this.isOn = false;
			this.tileId = (byte)TileEnum.PlatYellowDown;
			this.title = "Yellow Toggle Platform";
			this.description = "Acts like a platform when Yellow-Toggles are ON.";
		}
	}
}
