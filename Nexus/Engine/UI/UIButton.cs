using Microsoft.Xna.Framework;

namespace Nexus.Engine {

	public class UIButton : UIComponent {

		// Essentials
		private UIConfirmType confirmType;
		private string title;
		private short titleWidth;

		// Colors
		private Color bg;
		private Color hover;

		public UIButton(UIComponent parent, UIConfirmType confirmType, string title) : base(parent) {

			// Type Theme
			UITheme theme = UIHandler.theme;
			UIThemeButton bTheme = theme.button;
			
			if(confirmType == UIConfirmType.Normal) {
				this.bg = bTheme.NormalBG;
				this.hover = bTheme.NormalHover;
			}

			else if(confirmType == UIConfirmType.Approve) {
				this.bg = bTheme.AcceptBG;
				this.hover = bTheme.AcceptHover;
			}

			else if(confirmType == UIConfirmType.Reject) {
				this.bg = bTheme.RejectBG;
				this.hover = bTheme.RejectHover;
			}

			// Essentials
			this.confirmType = confirmType;
			this.title = title;
			this.titleWidth = (short) UIHandler.theme.bigFont.font.MeasureString(title).X;

			// Size Setup
			this.SetHeight(bTheme.Height);
			this.SetWidth(bTheme.Width);
		}

		public void RunTick() {
			this.MouseOver = this.GetMouseOverState();

			// If clicked while hovering over the button:
			if(this.MouseOver == UIMouseOverState.On && Cursor.LeftMouseState == Cursor.MouseDownState.Clicked) {
				this.OnClick();
			}
		}

		protected virtual void OnClick() {
			if(this.Parent is UIConfirmBox) {
				UIConfirmBox confBox = (UIConfirmBox)this.Parent;
				confBox.OnSubmit(this.confirmType);
			}
		}

		public void Draw() {

			UITheme theme = UIHandler.theme;
			UIThemeButton bTheme = theme.button;

			// Draw Buttons
			int posX = this.trueX;
			int posY = this.trueY;
			
			// Confirm Button
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(posX, posY, bTheme.Width, bTheme.Height), this.MouseOver == UIMouseOverState.On ? this.hover : this.bg);
			
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(posX, posY, bTheme.Width, 2), Color.White);
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(posX, posY + bTheme.Height, bTheme.Width, 2), Color.White);
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(posX, posY, 2, bTheme.Height), Color.White);
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(posX + bTheme.Width, posY, 2, bTheme.Height + 2), Color.White);

			if(this.MouseOver == UIMouseOverState.On && Cursor.LeftMouseState >= Cursor.MouseDownState.HeldDown) {
				Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(posX + 2, posY + 2, bTheme.Width - 3, 3), Color.DarkSlateGray);
				Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(posX + 2, posY + 2, 3, bTheme.Height - 3), Color.DarkSlateGray);
				posX += 2;
				posY += 2;
			}

			// Draw Notice
			theme.bigFont.Draw(this.title, posX + (this.width / 2) - (this.titleWidth / 2), posY + 15, Color.White);
		}
	}
}
