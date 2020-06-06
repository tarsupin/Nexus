
namespace Nexus.Objects {

	public class ButtonTimedBRDown : ButtonFixed {

		public ButtonTimedBRDown() : base() {
			this.Texture = "Button/Timed/BR";
			this.toggleBR = true;
			this.isDown = true;
			this.isTimer = true;
		}
	}
}
