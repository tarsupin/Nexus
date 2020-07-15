using Microsoft.Xna.Framework;

namespace Nexus.Engine {

	// The correct way to maintain (and create) a tool tip:
	// UIHandler.RunToolTip("testId", "Testing", "This is a test.", UIPrimaryDirection.Top);

	// Note: There should only be one of these created.
	public class UIToolTip : UIComponent {

		// Essentials
		private string uid;					// The unique identifier for a tool tip.
		private string title;				// The title or main text for the notification.
		private string[] text;				// Description for the notification.
		private UIPrimaryDirection dir;		// Direction that the tooltip should appear, relative to mouse.

		// Survival
		// The ToolTip will fade quickly if it doesn't get "re-touched" from the local system.
		public int endFrame { get; private set; } // ToolTip will soon fade after reaching this frame.
		public float alpha = 1;

		public UIToolTip(UIComponent parent) : base(parent) {}

		// Use this method to see if the tool tip needs to be rebuilt (is new). If it already exists, update it's end frame.
		// Use UIHandler.RunToolTip() instead of this method.
		public bool _MaintainToolTip( string uid ) {

			// Don't rebuild a tool tip that already exists. Instead, update the end frame.
			if(this.uid == uid && this.title is string) {
				this.endFrame = Systems.timer.UniFrame + 4;
				this.UpdatePosition();
				if(this.alpha < 1) { this.alpha = 1; }
				return true;
			}

			return false;
		}

		// The uid is a unique identifier to prevent re-building a tool tip on every frame.
		// This method will NOT update the endFrame (to avoid programmers trying to run it without "_MaintainToolTip"). Use "_MaintainToolTip" to determine if this is needed.
		// Use UIHandler.RunToolTip() instead of this method.
		public void _CreateToolTip( string uid, string title, string text, UIPrimaryDirection dir = UIPrimaryDirection.None ) {
			this.uid = uid;

			// Measure Strings
			Vector2 measureTitle = UIHandler.theme.smallHeaderFont.font.MeasureString(title);
			Vector2 measureText = UIHandler.theme.normalFont.font.MeasureString(text);

			// Text + Multi-Line Handler
			this.title = title;
			this.text = TextHelper.WrapTextSplit(UIHandler.theme.normalFont.font, text, UIHandler.theme.tooltips.ItemWidth - 16 - 16);
			
			// This will prevent the ToolTip from appearing on this frame.
			// THIS IS INTENTIONAL. It's to avoid trying to use "_CreateToolTip" when you're supposed to use "_MaintainToolTip".
			this.endFrame = 0;

			// Size Setup
			this.SetHeight((short)(measureTitle.Y + measureText.Y * this.text.Length + 10 + 10 + 6));
			this.SetWidth(UIHandler.theme.tooltips.ItemWidth);

			// Position
			this.dir = dir;
		}

		public void UpdatePosition() {
			UIThemeToolTip theme = UIHandler.theme.tooltips;

			int posX = Cursor.MouseX;
			int posY = Cursor.MouseY;

			switch(dir) {

				case UIPrimaryDirection.Top:
					posX -= (short)(this.width / 2f);
					posY -= (this.height + theme.CursorGap);
					break;

				case UIPrimaryDirection.Left:
					posX -= (this.width + theme.CursorGap);
					posY += (short)(this.height / 2f);
					break;
				
				case UIPrimaryDirection.Right:
					posX += theme.CursorGap;
					posY += (short)(this.height / 2f);
					break;
				
				case UIPrimaryDirection.Bottom:
					posX -= (short)(this.width / 2f);
					posY += theme.CursorGap;
					break;
			}

			// Limit to Screen
			if(posX < 10) {
				posX = 10;
			}

			else if(posX > Systems.screen.windowWidth - this.width - 10) {
				posX = Systems.screen.windowWidth - this.width - 10;
			}

			if(posY < 10) {
				posY = 10;
			}

			else if(posY > Systems.screen.windowHeight - this.height - 10) {
				posY = Systems.screen.windowHeight - this.height - 10;
			}

			// Set Position
			this.SetRelativePosition((short)posX, (short)posY);
		}

		public void RunTick() {

			// Only Run Tool Tip Mechanics if it's been maintained.
			if(this.endFrame == 0) { return; }

			// Check Notification Exit Mechanics
			if(this.endFrame <= Systems.timer.UniFrame) {
				int finalFrame = this.endFrame + UIHandler.theme.tooltips.EndDuration;

				// Draw Fade Effect during the fade itself.
				this.alpha = 1 - Spectrum.GetPercentFromValue(Systems.timer.UniFrame, this.endFrame, finalFrame);

				// Clear the ToolTip mechanics if it's been ended.
				if(Systems.timer.UniFrame > finalFrame) {
					this.endFrame = 0;
				}
			}
		}

		public void Draw() {

			// Only Draw Tool Tip if it's been maintained.
			if(this.endFrame == 0) { return; }

			UITheme theme = UIHandler.theme;

			// Draw Background
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.trueX, this.trueY, this.width, this.height), theme.NormBG * alpha);

			// Draw Outlines
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.trueX + 2, this.trueY + 2, this.width - 4, 1), theme.NormOutline * alpha);
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.trueX + 2, this.trueY + this.height - 3, this.width - 4, 1), theme.NormOutline * alpha);
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.trueX + 2, this.trueY + 2, 1, this.height - 4), theme.NormOutline * alpha);
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.trueX + this.width - 3, this.trueY + 2, 1, this.height - 4), theme.NormOutline * alpha);

			// Draw Notice
			Systems.fonts.baseText.Draw(this.title, this.trueX + 10, this.trueY + 10, theme.NormFG * alpha);

			// Draw Each Text Line
			for(byte i = 0; i < this.text.Length; i++) {
				Systems.fonts.console.Draw(this.text[i], this.trueX + 10, this.trueY + 35 + (i * 17), theme.NormFG * alpha);
			}
		}

		public void RunThemeUpdate() {
			UIThemeToolTip tipTheme = UIHandler.theme.tooltips;
			this.SetWidth(tipTheme.ItemWidth);
			//this.SetRelativePosition(tipTheme.xOffset, tipTheme.yOffset, tipTheme.xRel, tipTheme.yRel);
		}
	}
}
