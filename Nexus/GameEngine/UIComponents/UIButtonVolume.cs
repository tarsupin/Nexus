using Nexus.Engine;
using System;

namespace Nexus.GameEngine {

	public class UIButtonVolume : UIButton {

		private static string volOff = "UI/Volume/Off";

		// onClick = delegate() { doSomething(); };
		public UIButtonVolume( UIComponent parent, string spriteName, short posX, short posY, Action onClick ) : base(parent, spriteName, posX, posY, onClick) {}

		public void Draw() {

			bool isMuted = Systems.settings.audio.Mute;

			if(UIComponent.ComponentWithFocus == this) {
				this.atlas.Draw(UIButtonVolume.ButtonSprite[1], this.trueX, this.trueY);
				this.atlas.Draw(isMuted == true ? UIButtonVolume.volOff : this.SpriteName, this.trueX + 1, this.trueY + 1);
			}

			else {
				this.atlas.Draw(UIButtonVolume.ButtonSprite[0], this.trueX, this.trueY);
				this.atlas.Draw(isMuted == true ? UIButtonVolume.volOff : this.SpriteName, this.trueX, this.trueY);
			}
		}
	}
}
