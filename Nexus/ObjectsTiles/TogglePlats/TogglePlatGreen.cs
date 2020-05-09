using Nexus.Gameplay;

namespace Nexus.Objects {

	public class TogglePlatGreen : TogglePlat {

		public TogglePlatGreen() : base() {
			this.Texture = "/Green/Plat";
			this.toggleBR = false;
			this.tileId = (byte)TileEnum.TogglePlatGreen;
			this.title = "Green Toggle Platform";
			this.description = "Acts like a platform when Green-Toggles are ON.";
		}
	}
}
