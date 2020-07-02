using Nexus.Engine;
using System;

namespace Nexus.GameEngine {

	public class ToggleVolume : UIIcon {

		private static string volOn = "UI/Volume/On";
		private static string volOff = "UI/Volume/Off";

		// onClick = delegate() { doSomething(); };
		public ToggleVolume( UIComponent parent, short posX, short posY, Action onClick ) : base(parent, ToggleVolume.volOn, posX, posY, onClick) {}

		public new void Draw() {

			bool isMuted = Systems.settings.audio.Mute;

			if(UIComponent.ComponentWithFocus == this) {
				this.atlas.Draw(ToggleVolume.ButtonSprite[1], this.trueX, this.trueY);
				this.atlas.Draw(isMuted == true ? ToggleVolume.volOff : this.SpriteName, this.trueX + 1, this.trueY + 1);
			}

			else {
				this.atlas.Draw(ToggleVolume.ButtonSprite[0], this.trueX, this.trueY);
				this.atlas.Draw(isMuted == true ? ToggleVolume.volOff : this.SpriteName, this.trueX, this.trueY);
			}
		}
	}
}
