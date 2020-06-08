using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ButtonTimedGYUp : ButtonFixed {

		public ButtonTimedGYUp() : base() {
			this.tileId = (byte)TileEnum.ButtonTimedGYUp;
			this.Texture = "Button/Timed/GY";
			this.toggleBR = false;
			this.isDown = false;
			this.isTimer = true;
		}
	}
}
