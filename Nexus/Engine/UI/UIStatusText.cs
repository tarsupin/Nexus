using Microsoft.Xna.Framework;
using Nexus.Engine;

namespace Nexus.GameEngine {

	public class UIStatusText : UIComponent {

		private string title = "";
		private string text = "";
		private Vector2 measureTitle;
		private Vector2 measureText;

		// Fading
		public int endFrame { get; private set; } // Status Text will soon fade after reaching this frame.
		public float alpha = 1;

		public UIStatusText( UIComponent parent, short xPos, short yPos, UIHorPosition xRel = UIHorPosition.Left, UIVertPosition yRel = UIVertPosition.Top ) : base(parent) {
			this.SetRelativePosition(xPos, yPos, xRel, yRel);
		}

		public void SetText(string title, string text, short endFrame = 0) {
			this.alpha = 1;
			this.title = title;
			this.text = text;
			this.measureTitle = Systems.fonts.baseText.font.MeasureString(this.title);
			this.measureText = Systems.fonts.console.font.MeasureString(this.text);
			this.endFrame = endFrame;
		}

		public void ClearStatus() { this.title = ""; }

		public void RunTick() {

			// Only Run Tool Tip Mechanics if it's been maintained.
			if(this.endFrame == 0) { return; }

			// Check Notification Exit Mechanics
			if(this.endFrame <= Systems.timer.UniFrame) {
				int finalFrame = this.endFrame + UIHandler.theme.status.EndDuration;

				// Draw Fade Effect during the fade itself.
				this.alpha = 1 - Spectrum.GetPercentFromValue(Systems.timer.UniFrame, this.endFrame, finalFrame);

				// Clear the ToolTip mechanics if it's been ended.
				if(Systems.timer.UniFrame > finalFrame) {
					this.endFrame = 0;
				}
			}
		}

		public void DrawAlertFrame() {

			// Draw Alert (if applicable)
			if(this.title.Length == 0) { return; }

			UIThemeStatus theme = UIHandler.theme.status;

			// Draw Status Title
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.x - ((short)this.measureTitle.X / 2) - 2, this.y - 2, (int)(this.measureTitle.X + 4), (int)(this.measureTitle.Y + 4)), theme.bg * alpha);
			Systems.fonts.baseText.Draw(this.title, this.x - ((short)this.measureTitle.X / 2), this.y, theme.fg * alpha);

			// Draw Status Text
			if(this.text.Length > 0) {
				Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.x - ((short) this.measureText.X / 2) - 2, this.y + 25 - 2, (int) this.measureText.X + 4, (int) this.measureText.Y + 4), theme.bg * alpha);
				Systems.fonts.console.Draw(this.text, this.x - ((short) this.measureText.X / 2), this.y + 25, theme.fg * alpha);
			}
		}
	}
}
