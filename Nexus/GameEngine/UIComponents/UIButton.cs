﻿using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;
using System;

namespace Nexus.GameEngine {

	public class UIButton : UIComponent {

		protected const string active = "UI/Button/TextOn";
		protected const string inactive = "UI/Button/Text";

		protected Atlas atlas;
		protected string text;
		private short xOffset;
		public Action onClick { get; protected set; }

		// onClick = delegate() { doSomething(); };
		public UIButton( UIComponent parent, string text, short posX, short posY, Action onClick ) : base(parent) {
			this.atlas = Systems.mapper.atlas[(byte)AtlasGroup.Tiles];
			this.text = text;
			this.onClick = onClick;

			this.SetRelativePosition(posX, posY);
			this.SetWidth(124);
			this.SetHeight(48);

			// Prepare Center Text (X Offset)
			Vector2 textSize = Systems.fonts.baseText.font.MeasureString(this.text);
			this.xOffset = (short) Math.Floor(textSize.X * 0.5f);
		}

		public void RunTick() {
			if(this.IsMouseOver()) {
				UIComponent.ComponentWithFocus = this;

				// Mouse Clicked
				if(Cursor.LeftMouseState == Cursor.MouseDownState.Clicked) {
					this.onClick();
				}
			}
			
			// If the Mouse just exited this component:
			//else if(this.MouseOver == UIMouseOverState.Exited) {}
		}

		public void Draw() {
			
			if(UIComponent.ComponentWithFocus == this) {
				this.atlas.Draw(UIButton.active, this.trueX, this.trueY);
				Systems.fonts.baseText.Draw(this.text, this.trueX + 62 + 1 - this.xOffset, this.trueY + 16 + 1, Color.DarkSlateGray);
			}

			else {
				this.atlas.Draw(UIButton.inactive, this.trueX, this.trueY);
				Systems.fonts.baseText.Draw(this.text, this.trueX + 62 - this.xOffset, this.trueY + 16, Color.DarkSlateGray);
			}
		}
	}
}
