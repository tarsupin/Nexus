using Microsoft.Xna.Framework;

namespace Nexus.Engine {

	public class UIButton : UIComponent {

		// Essentials
		private string title;
		private short titleWidth;

		// Colors
		private Color bg;
		private Color outline;

		public UIButton(UIComponent parent, UIAlertType type, string title) : base(parent) {

			// Type Theme
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

			// Essentials
			this.title = title;
			this.titleWidth = (short) UIHandler.theme.bigFont.font.MeasureString(title).X;

			// Size Setup
			this.SetHeight(80);
			this.SetWidth(120);
		}

		public void Draw() {

			UITheme theme = UIHandler.theme;
			UIThemeConfirmBox cTheme = theme.confirm;

			// Draw Buttons
			int posY = this.trueX;

			// Confirm Button
			int posX = this.trueX + 60;
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(posX, posY, cTheme.ButtonWidth, cTheme.ButtonHeight), theme.SuccessBG);
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(posX + 2, posY + 2, cTheme.ButtonWidth - 4, 1), theme.SuccessOutline);
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(posX + 2, posY + cTheme.ButtonHeight - 3, cTheme.ButtonWidth - 4, 1), theme.SuccessOutline);
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(posX + 2, posY + 2, 1, cTheme.ButtonHeight - 4), theme.SuccessOutline);
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(posX + cTheme.ButtonWidth - 3, posY + 2, 1, cTheme.ButtonHeight - 4), theme.SuccessOutline);

			// Reject Button
			posX = this.trueX + 240;
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(posX, posY, cTheme.ButtonWidth, cTheme.ButtonHeight), theme.ErrorBG);
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(posX + 2, posY + 2, cTheme.ButtonWidth - 4, 1), theme.ErrorOutline);
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(posX + 2, posY + cTheme.ButtonHeight - 3, cTheme.ButtonWidth - 4, 1), theme.ErrorOutline);
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(posX + 2, posY + 2, 1, cTheme.ButtonHeight - 4), theme.ErrorOutline);
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(posX + cTheme.ButtonWidth - 3, posY + 2, 1, cTheme.ButtonHeight - 4), theme.ErrorOutline);

			// Draw Notice
			Systems.fonts.baseText.Draw(this.title, posX + 10, posY + 10, theme.NormFG);
		}
	}
}
