using Microsoft.Xna.Framework;

namespace Nexus.Engine {

	public class UIButton : UIComponent {

		// Essentials
		private string title;
		private short titleWidth;

		// Colors
		private Color bg;
		private Color hover;

		public UIButton(UIComponent parent, UIConfirmType type, string title) : base(parent) {

			// Type Theme
			UITheme theme = UIHandler.theme;
			UIThemeButton bTheme = theme.button;
			
			if(type == UIConfirmType.Normal) {
				this.bg = bTheme.NormalBG;
				this.hover = bTheme.NormalHover;
			}

			else if(type == UIConfirmType.Approve) {
				this.bg = bTheme.AcceptBG;
				this.hover = bTheme.AcceptHover;
			}

			else if(type == UIConfirmType.Reject) {
				this.bg = bTheme.RejectBG;
				this.hover = bTheme.RejectHover;
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
			UIThemeButton bTheme = theme.button;

			// Draw Buttons
			int posY = this.trueX;

			// Confirm Button
			int posX = this.trueX + 60;
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(posX, posY, bTheme.Width, bTheme.Height), this.bg);
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(posX, posY, bTheme.Width - 2, 2), Color.White);
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(posX, posY + bTheme.Height - 2, bTheme.Width - 2, 2), Color.White);
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(posX, posY, 2, bTheme.Height - 2), Color.White);
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(posX + bTheme.Width - 2, posY, 2, bTheme.Height - 2), Color.White);

			// Draw Notice
			Systems.fonts.baseText.Draw(this.title, posX + 10, posY + 10, theme.NormFG);
		}
	}
}
