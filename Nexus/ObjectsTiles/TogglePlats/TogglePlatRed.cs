using Nexus.Gameplay;

namespace Nexus.Objects {

	public class TogglePlatRed : TogglePlat {

		public TogglePlatRed() : base() {
			this.Texture = "/Red/Plat";
			this.toggleBR = true;
			this.tileId = (byte)TileEnum.TogglePlatRed;
			this.title = "Red Toggle Platform";
			this.description = "Acts like a platform when Red-Toggles are ON.";
		}
	}
}
