using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ButtonFixedGYDown : ButtonFixed {

		public ButtonFixedGYDown() : base() {
			this.tileId = (byte)TileEnum.ButtonFixedGYDown;
			this.Texture = "Button/Fixed/GY";
			this.toggleBR = false;
			this.isDown = true;
		}
	}
}
