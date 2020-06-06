
namespace Nexus.Objects {

	public class ButtonTimedGYDown : ButtonFixed {

		public ButtonTimedGYDown() : base() {
			this.Texture = "Button/Timed/GY";
			this.toggleBR = false;
			this.isDown = true;
			this.isTimer = true;
		}
	}
}
