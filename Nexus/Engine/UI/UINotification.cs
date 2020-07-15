
using Microsoft.Xna.Framework;

namespace Nexus.Engine {

	public class UINotification : UIComponent {

		// Essentials
		private string title;           // The title or main text for the notification.
		private string[] text;			// Description for the notification.

		// Colors
		private Color bg;
		private Color outline;

		// Ending Effects (Specifics of the exit are up to the Notification Handler)
		public int exitFrame { get; private set; }		// Marks the frame where the notification will begin to exit (fade, exit right, etc).
		public float alpha = 1;

		public UINotification(UIComponent parent, UIAlertType type, string title, string text, int exitFrame) : base(parent) {

			// Type Themee
			var theme = UIHandler.theme;

			if(type == UIAlertType.Error) {
				this.bg = theme.ErrorBG;
				this.outline = theme.ErrorOutline;
			}

			else if(type == UIAlertType.Success) {
				this.bg = theme.SuccessBG;
				this.outline = theme.SuccessOutline;
			}

			else if(type == UIAlertType.Warning) {
				this.bg = theme.WarningBG;
				this.outline = theme.WarningOutline;
			}

			else if(type == UIAlertType.Normal) {
				this.bg = theme.NormBG;
				this.outline = theme.NormOutline;
			}

			// Measure Strings
			Vector2 measureTitle = UIHandler.theme.smallHeaderFont.font.MeasureString(title);
			Vector2 measureText = UIHandler.theme.normalFont.font.MeasureString(text);

			// Text + Multi-Line Handler
			this.title = title;
			this.text = TextHelper.WrapTextSplit(UIHandler.theme.normalFont.font, text, parent.width - 16 - 16);

			// Behaviors
			this.exitFrame = exitFrame;

			// Size Setup
			this.SetHeight((short)(measureTitle.Y + measureText.Y * this.text.Length + 10 + 10 + 6));
			this.SetWidth(parent.width);
		}

		public void Draw(int posY) {

			int posX = this.Parent.trueX;

			// Draw Background
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(posX, posY, this.width, this.height), this.bg * alpha);

			// Draw Outlines
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(posX + 2, posY + 2, this.width - 4, 1), this.outline * alpha);
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(posX + 2, posY + this.height - 3, this.width - 4, 1), this.outline * alpha);
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(posX + 2, posY + 2, 1, this.height - 4), this.outline * alpha);
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(posX + this.width - 3, posY + 2, 1, this.height - 4), this.outline * alpha);

			// Draw Notice
			var theme = UIHandler.theme;
			Systems.fonts.baseText.Draw(this.title, posX + 10, posY + 10, theme.NormFG * alpha);

			// Draw Each Text Line
			for(byte i = 0; i < this.text.Length; i++) {
				Systems.fonts.console.Draw(this.text[i], posX + 10, posY + 35 + (i * 17), theme.NormFG * alpha);
			}
		}
	}
}
