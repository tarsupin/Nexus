using Nexus.Engine;
using System;

namespace Nexus.GameEngine {

	public class ToggleMusic : UIIcon {

		private static string on = "Music/On";
		private static string off = "Music/Off";

		// onClick = delegate() { doSomething(); };
		public ToggleMusic( UIComponent parent, short posX, short posY, Action onClick ) : base(parent, ToggleMusic.on, posX, posY, onClick, "Music Toggle", "Toggles the music on or off.") {}

		public new void Draw() {

			bool isMuted = Systems.settings.audio.MusicMute;

			if(UIComponent.ComponentWithFocus == this) {
				UIHandler.atlas.Draw(UIIcon.Down, this.trueX, this.trueY);
				UIHandler.atlas.Draw(isMuted == true ? ToggleMusic.off : this.SpriteName, this.trueX + 1, this.trueY + 1);
			}

			else {
				UIHandler.atlas.Draw(UIIcon.Up, this.trueX, this.trueY);
				UIHandler.atlas.Draw(isMuted == true ? ToggleMusic.off : this.SpriteName, this.trueX, this.trueY);
			}
		}
	}
}
