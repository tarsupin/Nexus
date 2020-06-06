
namespace Nexus.Objects {

	public class ButtonFixedBRDown : ButtonFixed {

		public ButtonFixedBRDown() : base() {
			this.Texture = "Button/Fixed/BR";
			this.toggleBR = true;
			this.isDown = true;
		}
	}
}
