using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ButtonTimedBRDown : ButtonFixed {

		public ButtonTimedBRDown() : base() {
			this.tileId = (byte)TileEnum.ButtonTimedBRDown;
			this.Texture = "Button/Timed/BR";
			this.toggleBR = true;
			this.isDown = true;
			this.isTimer = true;
		}
	}
}
