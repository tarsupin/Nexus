
namespace Nexus.Objects {

	public class ButtonTimedBRUp : ButtonFixed {

		public ButtonTimedBRUp() : base() {
			this.Texture = "Button/Timed/BR";
			this.toggleBR = true;
			this.isDown = false;
			this.isTimer = true;
		}
	}
}
