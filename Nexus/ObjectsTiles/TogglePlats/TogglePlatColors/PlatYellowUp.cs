using Nexus.Gameplay;

namespace Nexus.Objects {

	public class PlatYellowUp : TogglePlatUp {

		public PlatYellowUp() : base() {
			this.Texture = "/Yellow/Plat";
			this.toggleBR = false;
			this.isOn = false;
			this.tileId = (byte)TileEnum.PlatYellowUp;
			this.title = "Yellow Toggle Platform";
			this.description = "Acts like a platform when Yellow-Toggles are ON.";
		}
	}
}
