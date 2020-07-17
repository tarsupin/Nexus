using Microsoft.Xna.Framework;

namespace Nexus.Engine {

	public class UIConfirmBox : UIComponent {

		// Essentials
		private string eventTag;		// Tag for the event.
		private string title;           // The title or main text for the confirmation box.
		private string[] text;          // Description for the confirmation box.

		private short midLine;
		private short titleHalfWidth;

		public UIConfirmBox(UIComponent parent) : base(parent) {}

		public void SetConfirmBox(string eventTag, string title, string text) {

			// Confirm Box Theme
			UITheme theme = UIHandler.theme;
			UIThemeConfirmBox cTheme = UIHandler.theme.confirm;

			// Measure Strings
			Vector2 measureTitle = UIHandler.theme.smallHeaderFont.font.MeasureString(title);
			Vector2 measureText = UIHandler.theme.normalFont.font.MeasureString(text);

			this.titleHalfWidth = (short)(measureTitle.X / 2);

			// Essentials
			this.eventTag = eventTag;
			this.title = title;
			this.text = TextHelper.WrapTextSplit(UIHandler.theme.normalFont.font, text, cTheme.Width - 16 - 16);

			// Size Setup
			this.midLine = (short)(cTheme.HeightGaps * 3 + (measureText.Y + 5) * this.text.Length);
			short height = (short)(theme.button.Height + cTheme.HeightGaps * 2 + this.midLine);
			if(height < cTheme.MinHeight) { height = cTheme.MinHeight; }

			this.SetHeight(height);
			this.SetWidth(cTheme.Width);

			// TODO: REMOVE. TEMPORARY TEST.
			this.SetButtonsConfirmDeny("AgreeSay", "RejectItYeah");
		}

		public void SetButtonsConfirmDeny(string yesTitle = "Yes", string noTitle = "No") {
			UIThemeConfirmBox cTheme = UIHandler.theme.confirm;

			short midHor = (short)(this.width / 2);

			UIButton button1 = new UIButton(this, this.eventTag + "_yes", UIConfirmType.Approve, yesTitle);
			button1.SetRelativePosition((short)(midHor - button1.width - 10), (short)(this.midLine + cTheme.HeightGaps));

			UIButton button2 = new UIButton(this, this.eventTag + "_no", UIConfirmType.Reject, noTitle);
			button2.SetRelativePosition((short)(midHor + 10), (short)(this.midLine + cTheme.HeightGaps));
		}

		public void SetButtonOk(string okTitle = "OK") {
			UIButton button = new UIButton(this, this.eventTag + "_ok", UIConfirmType.Normal, okTitle);
		}

		public void RunTick() {

			// Draw Children
			foreach(UIComponent child in this.Children) {
				if(child is UIButton) {
					((UIButton)child).RunTick();
				}
			}
		}

		public void Draw() {
			UIThemeConfirmBox theme = UIHandler.theme.confirm;

			// Draw Background
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.trueX, this.trueY, this.width, this.height), theme.bg);

			// Draw Outlines
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.trueX + 2, this.trueY + 2, this.width - 4, 1), theme.fg);
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.trueX + 2, this.trueY + this.height - 3, this.width - 4, 1), theme.fg);
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.trueX + 2, this.trueY + 2, 1, this.height - 4), theme.fg);
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.trueX + this.width - 3, this.trueY + 2, 1, this.height - 4), theme.fg);

			// Draw Notice
			Systems.fonts.baseText.Draw(this.title, (short)(this.width / 2 - this.titleHalfWidth), this.trueY + theme.HeightGaps, theme.fg);

			// Draw Each Text Line
			for(byte i = 0; i < this.text.Length; i++) {
				Systems.fonts.console.Draw(this.text[i], this.trueX + 20, this.trueY + theme.HeightGaps * 3 + (i * 17), theme.fg);
			}

			// Draw Midline
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.trueX + 60, this.midLine, this.width - 120, 1), theme.fg);

			// Draw Children
			foreach(UIComponent child in this.Children) {
				if(child is UIButton) {
					((UIButton)child).Draw();
				}
			}
		}
	}
}
