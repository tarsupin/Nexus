using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ButtonFixedBRDown : ButtonFixed {

		public ButtonFixedBRDown() : base() {
			this.tileId = (byte)TileEnum.ButtonFixedBRDown;
			this.Texture = "Button/Fixed/BR";
			this.toggleBR = true;
			this.isDown = true;
		}
	}
}
