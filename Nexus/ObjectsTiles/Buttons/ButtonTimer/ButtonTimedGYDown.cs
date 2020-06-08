using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ButtonTimedGYDown : ButtonFixed {

		public ButtonTimedGYDown() : base() {
			this.tileId = (byte)TileEnum.ButtonTimedGYDown;
			this.Texture = "Button/Timed/GY";
			this.toggleBR = false;
			this.isDown = true;
			this.isTimer = true;
		}
	}
}
