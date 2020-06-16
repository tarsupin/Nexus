using Microsoft.Xna.Framework;
using Nexus.Engine;

namespace Nexus.GameEngine {

	public class AlertText : UIComponent {

		// Notice Text - For helpful insights, rollovers, etc.
		private string noticeTitle = "";
		private string noticeText = "";
		private Vector2 measureTitle;
		private Vector2 measureText;
		private Color bgColor = Color.DarkSlateGray;
		private Color textColor = Color.White;

		// Alert Text - Appears momentarily for a brief purpose, such as having saved something.
		private string alertTitle = "";
		private string alertText = "";
		private Vector2 measureAlertTitle;
		private Vector2 measureAlertText;
		private Color bgAlertColor = Color.DarkSlateGray;
		private Color textAlertColor = Color.White;

		// Alert Fading
		private uint fadeStart = 0;
		private uint fadeEnd = 0;
		private float alpha = 1;

		public AlertText( UIComponent parent ) : base(parent) {
			this.SetRelativePosition((short) Systems.screen.windowHalfWidth, 5);
		}

		public void SetNotice(string title, string text) {
			this.noticeTitle = title;
			this.noticeText = text;
			this.measureTitle = Systems.fonts.baseText.font.MeasureString(this.noticeTitle);
			this.measureText = Systems.fonts.console.font.MeasureString(this.noticeText);
		}

		public void ClearNotice() { this.noticeTitle = ""; }
		
		public void SetAlert(string title, string text, uint frameToStart, uint duration = 120) {
			this.alertTitle = title;
			this.alertText = text;
			this.measureAlertTitle = Systems.fonts.baseText.font.MeasureString(this.alertTitle);
			this.measureAlertText = Systems.fonts.console.font.MeasureString(this.alertText);
			this.fadeStart = frameToStart;
			this.fadeEnd = this.fadeStart + duration;
		}

		public void ClearAlert() { this.alertTitle = ""; }

		public void Draw( uint frame = 0 ) {

			// Draw Notice (if applicable)
			if(this.noticeTitle.Length > 0) {

				Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.x - ((ushort)this.measureTitle.X / 2) - 2, this.y - 2, (int)(this.measureTitle.X + 4), (int)(this.measureTitle.Y + 4)), this.bgColor);
				Systems.fonts.baseText.Draw(this.noticeTitle, this.x - ((ushort)this.measureTitle.X / 2), this.y, this.textColor);

				if(this.noticeText.Length > 0) {
					Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.x - ((ushort)this.measureText.X / 2) - 2, this.y + 25 - 2, (int)this.measureText.X + 4, (int)this.measureText.Y + 4), this.bgColor);
					Systems.fonts.console.Draw(this.noticeText, this.x - ((ushort)this.measureText.X / 2), this.y + 25, this.textColor);
				}
			}

			// Draw Alert (if applicable)
			if(this.alertText.Length == 0) { return; }

			// Fade Mechanic
			// If we're sending frames to this draw method, it's because it might fade over time. Run a fade test.
			if(frame > 0 && fadeEnd > 0) {

				// End Draw if frame is after frame end.
				if(frame > fadeEnd) { return; }

				// Draw Fade Effect during the fade itself.
				if(fadeStart >= frame) {
					this.alpha = 1 - Spectrum.GetPercentFromValue(frame, fadeStart, fadeEnd);
					if(this.alpha < 0) { return; }
				}
			}

			// Draw Notice
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.x - ((ushort) this.measureTitle.X / 2) - 2, this.y - 2, (int)(this.measureTitle.X + 4), (int)(this.measureTitle.Y + 4)), this.bgColor * alpha);
			Systems.fonts.baseText.Draw(this.noticeTitle, this.x - ((ushort) this.measureTitle.X / 2), this.y, this.textColor * alpha);

			if(this.noticeText.Length > 0) {
				Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.x - ((ushort) this.measureText.X / 2) - 2, this.y + 25 - 2, (int) this.measureText.X + 4, (int) this.measureText.Y + 4), this.bgColor * alpha);
				Systems.fonts.console.Draw(this.noticeText, this.x - ((ushort) this.measureText.X / 2), this.y + 25, this.textColor * alpha);
			}
		}
	}
}
