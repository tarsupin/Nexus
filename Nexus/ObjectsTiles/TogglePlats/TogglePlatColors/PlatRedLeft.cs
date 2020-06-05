using Nexus.Gameplay;

namespace Nexus.Objects {

	public class PlatRedLeft : TogglePlatLeft {

		public PlatRedLeft() : base() {
			this.Texture = "/Red/Plat";
			this.toggleBR = true;
			this.isOn = false;
			this.tileId = (byte)TileEnum.PlatRedLeft;
			this.title = "Red Toggle Platform";
			this.description = "Acts like a platform when Red-Toggles are ON.";
		}
	}
}
