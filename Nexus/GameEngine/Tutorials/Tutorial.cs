using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class Tutorial {

		public short tutorialStep = 0;

		public UINotification notify;

		public string arrowTexture = "";
		public int arrowX;
		public int arrowY;

		public Tutorial() {}

		public void SetTutorialNote(short posX, short posY, string title, string text, DirRotate dir = DirRotate.Center) {

			// Don't overwrite a notify that's already been drawn.
			if(this.notify is UINotification && this.notify.alpha > 0) { return; }

			// UI Notification
			this.notify = new UINotification(UIHandler.globalUI, UIAlertType.Success, title, text, 0, UIHandler.theme.notifs.ItemWidth);
			this.notify.SetRelativePosition(posX, posY);

			// Set Arrow (if applicable)
			this.SetArrow(dir);
		}

		public void SetArrow(DirRotate dir) {
			if(dir == DirRotate.Left) {
				this.arrowTexture = "Arrow/Left";
				this.arrowX = this.notify.trueX - (byte) TilemapEnum.TileWidth - 8;
				this.arrowY = this.notify.trueY + (this.notify.height / 2) - 8;
			} else if(dir == DirRotate.Right) {
				this.arrowTexture = "Arrow/Right";
				this.arrowX = this.notify.trueX + this.notify.width + 8;
				this.arrowY = this.notify.trueY + (this.notify.height / 2) - 8;
			} else if(dir == DirRotate.Up) {
				this.arrowTexture = "Arrow/Up";
				this.arrowX = this.notify.trueX + (this.notify.width / 2) - (byte) TilemapEnum.HalfWidth;
				this.arrowY = this.notify.trueY - (byte) TilemapEnum.TileHeight - 8;
			} else if(dir == DirRotate.Down) {
				this.arrowTexture = "Arrow/Down";
				this.arrowX = this.notify.trueX + (this.notify.width / 2) - (byte) TilemapEnum.HalfWidth;
				this.arrowY = this.notify.trueY + this.notify.height + 8;
			} else {
				this.arrowTexture = "";
			}
		}

		public virtual void IncrementTutorialStep() {
			this.tutorialStep = (short)(this.tutorialStep + 1);
			this.ClearTutorialNote();
		}

		public void ClearTutorialNote() {
			this.notify.SetExitFrame(Systems.timer.UniFrame + 10);
		}

		public void Draw() {
			UINotification notify = this.notify;
			if(notify is UINotification) {
				this.notify.Draw();

				// Draw Arrow
				if(this.arrowTexture.Length > 0) {
					Systems.mapper.MetaList[MetaGroup.Decor].Atlas.DrawAdvanced(this.arrowTexture, this.arrowX, this.arrowY, Color.White * this.notify.alpha);
				}
			}
		}
	}
}
