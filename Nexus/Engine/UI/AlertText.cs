﻿using Microsoft.Xna.Framework;
using Nexus.Engine;

namespace Nexus.GameEngine {

	public class AlertText : UIComponent {

		private string title = "";
		private string text = "";
		private Vector2 measureTitle;
		private Vector2 measureText;
		private Color bgColor = Color.DarkSlateGray;
		private Color textColor = Color.White;

		// Fading
		private int fadeStart = 0;
		private int fadeEnd = 0;
		private float alpha = 1;

		public AlertText( UIComponent parent ) : base(parent) {
			this.SetRelativePosition((short) Systems.screen.windowHalfWidth, 5);
		}

		public void SetNotice(string title, string text, int duration = 0) {

			this.title = title;
			this.text = text;
			this.measureTitle = Systems.fonts.baseText.font.MeasureString(this.title);
			this.measureText = Systems.fonts.console.font.MeasureString(this.text);
			
			if(duration > 0) {
				this.fadeStart = Systems.timer.Frame;
				this.fadeEnd = this.fadeStart + duration;
			} else {
				this.fadeStart = 0;
				this.fadeEnd = 0;
			}
		}

		public void ClearNotice() { this.title = ""; }
		
		public void Draw( int frame = 0 ) {

			// Draw Alert (if applicable)
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

			// Draw Notice
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.x - ((short) this.measureTitle.X / 2) - 2, this.y - 2, (int)(this.measureTitle.X + 4), (int)(this.measureTitle.Y + 4)), this.bgColor * alpha);
			Systems.fonts.baseText.Draw(this.title, this.x - ((short) this.measureTitle.X / 2), this.y, this.textColor * alpha);

			if(this.text.Length > 0) {
				Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.x - ((short) this.measureText.X / 2) - 2, this.y + 25 - 2, (int) this.measureText.X + 4, (int) this.measureText.Y + 4), this.bgColor * alpha);
				Systems.fonts.console.Draw(this.text, this.x - ((short) this.measureText.X / 2), this.y + 25, this.textColor * alpha);
			}
		}
	}
}
