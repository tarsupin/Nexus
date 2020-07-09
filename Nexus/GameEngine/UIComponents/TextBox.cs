using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class TextBox : UIComponent {

		protected const string Top = "TextBox/Top";
		protected const string Left = "TextBox/Left";
		protected const string Right = "TextBox/Right";
		protected const string Bottom = "TextBox/Bottom";
		protected const string TopLeft = "TextBox/TopLeft";
		protected const string TopRight = "TextBox/TopRight";
		protected const string BottomLeft = "TextBox/BottomLeft";
		protected const string BottomRight = "TextBox/BottomRight";

		protected const byte leftPad = 8;
		protected const byte rightPad = 8;
		protected const byte topPad = 8;
		protected const byte botPad = 8;

		protected Rectangle[] patches;
		protected static Color bgColor;

		// onSubmit = delegate() { doSomething(); };
		public TextBox( UIComponent parent, short posX, short posY, short width, short height ) : base(parent) {

			this.SetRelativePosition(posX, posY);
			this.SetWidth(width);
			this.SetHeight(height);

			TextBox.bgColor = new Color(148, 169, 170);

			this.patches = this.CreatePatches(posX, posY, width, height);
		}

		private Rectangle[] CreatePatches(short x, short y, short width, short height) {

			int middleWidth = width - leftPad - rightPad;
			int middleHeight = height - topPad - botPad;
			int bottomY = y + height - botPad;
			int rightX = x + width - rightPad;
			int leftX = x + leftPad;
			int topY = y + topPad;

			Rectangle[] patches = new[]
			{
				new Rectangle(x,      y,        leftPad,  topPad),				// top left
				new Rectangle(leftX,  y,        middleWidth,  topPad),			// top middle
				new Rectangle(rightX, y,        rightPad, topPad),				// top right
				new Rectangle(x,      topY,     leftPad,  middleHeight),		// left middle
				new Rectangle(leftX,  topY,     middleWidth,  middleHeight),	// middle
				new Rectangle(rightX, topY,     rightPad, middleHeight),		// right middle
				new Rectangle(x,      bottomY,  leftPad,  botPad),				// bottom left
				new Rectangle(leftX,  bottomY,  middleWidth,  botPad),			// bottom middle
				new Rectangle(rightX, bottomY,  rightPad, botPad)				// bottom right
			};

			return patches;
		}

		public void RunTick() {
			if(this.IsMouseOver()) {
				UIComponent.ComponentWithFocus = this;

				//// Mouse Clicked
				//if(Cursor.LeftMouseState == Cursor.MouseDownState.Clicked) {
					
				//}
			}
			
			// If the Mouse just exited this component:
			//else if(this.MouseOver == UIMouseOverState.Exited) {}
		}

		public void Draw() {

			// Draw Background
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.trueX + leftPad - 4, this.trueY + topPad - 4, this.width + 8, this.height + 8), TextBox.bgColor);

			// Draw Top
			UIHandler.atlas.DrawStretch(TextBox.Top, this.trueX + leftPad, this.trueY, this.width, topPad);
			UIHandler.atlas.DrawStretch(TextBox.Bottom, this.trueX + leftPad, this.trueY + this.height + topPad, this.width, botPad);
			UIHandler.atlas.DrawStretch(TextBox.Left, this.trueX, this.trueY + topPad, leftPad, this.height);
			UIHandler.atlas.DrawStretch(TextBox.Right, this.trueX + this.width + rightPad, this.trueY + topPad, rightPad, this.height);

			// Draw Corners
			UIHandler.atlas.Draw(TextBox.TopLeft, this.trueX, this.trueY);
			UIHandler.atlas.Draw(TextBox.TopRight, this.trueX + this.width + rightPad, this.trueY);
			UIHandler.atlas.Draw(TextBox.BottomLeft, this.trueX, this.trueY + this.height + botPad);
			UIHandler.atlas.Draw(TextBox.BottomRight, this.trueX + this.width + rightPad, this.trueY + this.height + botPad);
		}
	}
}
