using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ButtonFixedBRUp : ButtonFixed {

		public ButtonFixedBRUp() : base() {
			this.tileId = (byte)TileEnum.ButtonFixedBRUp;
			this.Texture = "Button/Fixed/BR";
			this.toggleBR = true;
			this.isDown = false;
		}
	}
}
