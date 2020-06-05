using Nexus.Gameplay;

namespace Nexus.Objects {

	public class PlatYellowLeft : TogglePlatLeft {

		public PlatYellowLeft() : base() {
			this.Texture = "/Yellow/Plat";
			this.toggleBR = false;
			this.isOn = false;
			this.tileId = (byte)TileEnum.PlatYellowLeft;
			this.title = "Yellow Toggle Platform";
			this.description = "Acts like a platform when Yellow-Toggles are ON.";
		}
	}
}
