
using Microsoft.Xna.Framework;

namespace Nexus.Engine {

	public class UINotification : UIComponent {

		// Essentials
		private UIAlertType type;		// The type of alert, which can affect colors and behavior.
		private string title;           // The title or main text for the notification.
		private string text;            // Less important text or minor details to support the notification title.

		// Colors
		private Color bg;
		private Color outline;

		// Measurements of Notification
		private short titleHeight;
		private short titleWidth;
		private short textHeight;
		private short textWidth;

		// Ending Effects (Specifics of the exit are up to the Notification Handler)
		public int exitFrame { get; private set; }		// Marks the frame where the notification will begin to exit (fade, exit right, etc).
		public float transition = 1;

		// Action Parameters
		// If the notification is clicked, it can provide parameters for the resulting action called.
		public short[] paramNums;

		public UINotification(UIComponent parent, UIAlertType type, string title, string text, int exitFrame, short[] paramNums = null) : base(parent) {

			// Essentials
			this.type = type;
			this.title = title;
			this.text = text;

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
			Vector2 measureTitle = UIHandler.theme.smallHeaderFont.font.MeasureString(this.title);
			Vector2 measureText = UIHandler.theme.normalFont.font.MeasureString(this.text);

			this.titleWidth = (short) measureTitle.X;
			this.titleHeight = (short) measureTitle.Y;
			this.textWidth = (short) measureText.X;
			this.textHeight = (short) measureText.Y;

			this.exitFrame = exitFrame;
			this.paramNums = paramNums;

			this.SetHeight((short)(this.titleHeight + this.textHeight + 10 + 10 + 10));
			this.SetWidth(parent.width);
		}

		public void Draw(int posY) {

			int posX = this.Parent.trueX;

			// Draw Background
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(posX, posY, this.width, this.height), this.bg * transition);

			// Draw Outlines
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(posX + 2, posY + 2, this.width - 4, 1), this.outline * transition);
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(posX + 2, posY + this.height - 3, this.width - 4, 1), this.outline * transition);
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(posX + 2, posY + 2, 1, this.height - 4), this.outline * transition);
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(posX + this.width - 3, posY + 2, 1, this.height - 4), this.outline * transition);

			// Draw Notice
			var theme = UIHandler.theme;
			Systems.fonts.baseText.Draw(this.title, posX + 10, posY + 10, theme.notifs.FontColor * transition);
			Systems.fonts.console.Draw(this.text, posX + 10, posY + 35, theme.notifs.FontColor * transition);
		}
	}
}
