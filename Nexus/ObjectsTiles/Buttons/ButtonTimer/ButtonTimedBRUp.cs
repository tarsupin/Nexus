using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ButtonTimedBRUp : ButtonFixed {

		public ButtonTimedBRUp() : base() {
			this.tileId = (byte)TileEnum.ButtonTimedBRUp;
			this.Texture = "Button/Timed/BR";
			this.toggleBR = true;
			this.isDown = false;
			this.isTimer = true;
		}
	}
}
