using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class AlertText : UIComponent {

		// Text
		private string title = "";
		private string text = "";
		private Vector2 measureTitle;
		private Vector2 measureText;

		// Colors
		private Color bgColor = Color.DarkSlateGray;
		private Color textColor = Color.White;

		// Fading
		private uint fadeStart = 0;
		private uint fadeEnd = 0;
		private float alpha = 1;

		public AlertText( UIComponent parent ) : base(parent) {
			this.SetRelativePosition((short) Systems.screen.windowHalfWidth, 5);
		}

		public void SetBGColor(Color bgColor) {
			this.bgColor = bgColor;
		}

		public void SetTextColor(Color textColor) {
			this.textColor = textColor;
		}

		public void SetAlert(string title, string text) {
			this.SetTitle(title);
			this.SetText(text);
		}

		public void ClearAlert() {
			this.title = "";
		}

		public void SetTitle(string title) {
			this.title = title;
			this.measureTitle = Systems.fonts.baseText.font.MeasureString(this.title);
		}

		public void SetText(string text) {
			this.text = text;
			this.measureText = Systems.fonts.console.font.MeasureString(this.text);
		}

		public void SetFade( uint frameToStart, uint duration = 60 ) {
			this.fadeStart = frameToStart;
			this.fadeEnd = this.fadeStart + duration;
		}

		public void Draw( uint frame = 0 ) {

			// Make sure there is content to draw:
			if(this.title.Length == 0) { return; }

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

			// Draw Alert
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.x - ((ushort) this.measureTitle.X / 2) - 2, this.y - 2, (int)(this.measureTitle.X + 4), (int)(this.measureTitle.Y + 4)), this.bgColor * alpha);
			Systems.fonts.baseText.Draw(this.title, this.x - ((ushort) this.measureTitle.X / 2), this.y, this.textColor * alpha);

			if(this.text.Length > 0) {
				Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.x - ((ushort) this.measureText.X / 2) - 2, this.y + 25 - 2, (int) this.measureText.X + 4, (int) this.measureText.Y + 4), this.bgColor * alpha);
				Systems.fonts.console.Draw(this.text, this.x - ((ushort) this.measureText.X / 2), this.y + 25, this.textColor * alpha);
			}
		}
	}
}
