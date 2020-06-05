using Nexus.Gameplay;

namespace Nexus.Objects {

	public class PlatGreenLeft : TogglePlatLeft {

		public PlatGreenLeft() : base() {
			this.Texture = "/Green/Plat";
			this.toggleBR = false;
			this.isOn = true;
			this.tileId = (byte)TileEnum.PlatGreenLeft;
			this.title = "Green Toggle Platform";
			this.description = "Acts like a platform when Green-Toggles are ON.";
		}
	}
}
