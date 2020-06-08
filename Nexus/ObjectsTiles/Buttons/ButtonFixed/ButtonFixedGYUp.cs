using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ButtonFixedGYUp : ButtonFixed {

		public ButtonFixedGYUp() : base() {
			this.tileId = (byte)TileEnum.ButtonFixedGYUp;
			this.Texture = "Button/Fixed/GY";
			this.toggleBR = false;
			this.isDown = false;
		}
	}
}
