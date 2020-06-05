using Nexus.Gameplay;

namespace Nexus.Objects {

	public class PlatRedDown : TogglePlatDown {

		public PlatRedDown() : base() {
			this.Texture = "/Red/Plat";
			this.toggleBR = true;
			this.isOn = false;
			this.tileId = (byte)TileEnum.PlatRedDown;
			this.title = "Red Toggle Platform";
			this.description = "Acts like a platform when Red-Toggles are ON.";
		}
	}
}
