using Nexus.Gameplay;

namespace Nexus.Objects {

	public class PlatGreenUp : TogglePlatUp {

		public PlatGreenUp() : base() {
			this.Texture = "/Green/Plat";
			this.toggleBR = false;
			this.isOn = true;
			this.tileId = (byte)TileEnum.PlatGreenUp;
			this.title = "Green Toggle Platform";
			this.description = "Acts like a platform when Green-Toggles are ON.";
		}
	}
}
