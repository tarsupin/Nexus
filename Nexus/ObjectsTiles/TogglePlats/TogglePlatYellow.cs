using Nexus.Gameplay;

namespace Nexus.Objects {

	public class TogglePlatYellow : TogglePlat {

		public TogglePlatYellow() : base() {
			this.Texture = "/Yellow/Plat";
			this.toggleBR = false;
			this.tileId = (byte)TileEnum.TogglePlatYellow;
			this.title = "Yellow Toggle Platform";
			this.description = "Acts like a platform when Yellow-Toggles are ON.";
		}
	}
}
