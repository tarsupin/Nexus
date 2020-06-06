
namespace Nexus.Objects {

	public class ButtonTimedGYUp : ButtonFixed {

		public ButtonTimedGYUp() : base() {
			this.Texture = "Button/Timed/GY";
			this.toggleBR = false;
			this.isDown = false;
			this.isTimer = true;
		}
	}
}
