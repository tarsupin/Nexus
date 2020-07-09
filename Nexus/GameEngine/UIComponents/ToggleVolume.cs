using Nexus.Engine;
using System;

namespace Nexus.GameEngine {

	public class ToggleVolume : UIIcon {

		private static string volOn = "Volume/On";
		private static string volOff = "Volume/Off";

		// onClick = delegate() { doSomething(); };
		public ToggleVolume( UIComponent parent, short posX, short posY, Action onClick ) : base(parent, ToggleVolume.volOn, posX, posY, onClick) {}

		public new void Draw() {

			bool isMuted = Systems.settings.audio.Mute;

			if(UIComponent.ComponentWithFocus == this) {
				UIHandler.atlas.Draw(UIIcon.Down, this.trueX, this.trueY);
				UIHandler.atlas.Draw(isMuted == true ? ToggleVolume.volOff : this.SpriteName, this.trueX + 5, this.trueY + 5);
			}

			else {
				UIHandler.atlas.Draw(UIIcon.Up, this.trueX, this.trueY);
				UIHandler.atlas.Draw(isMuted == true ? ToggleVolume.volOff : this.SpriteName, this.trueX + 4, this.trueY + 4);
			}
		}
	}
}
