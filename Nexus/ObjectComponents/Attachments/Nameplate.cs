using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.ObjectComponents {

	public class Nameplate {

		private readonly GameObject actor;
		private string nameplate;
		private bool visible = false;
		private sbyte xOffset = 0;
		private sbyte yOffset = -10;

		public Nameplate(GameObject actor, string nameplate, DirCardinal position = DirCardinal.Center, bool setVisible = true) {
			this.actor = actor;
			this.SetVisible(setVisible);
			this.SetNameplate(nameplate);
			if(position == DirCardinal.Center) { this.SetOffsetCenter(); }
			else if(position == DirCardinal.Up) { this.SetOffsetUp(); }
			else if(position == DirCardinal.Down) { this.SetOffsetDown(); }
		}

		public void SetNameplate(string nameplate) {
			this.nameplate = nameplate;
		}

		public void SetVisible(bool visible = true) {
			this.visible = visible;
		}

		public void SetOffsetUp() {
			Vector2 fontSize = Systems.fonts.console.font.MeasureString(this.nameplate);
			this.xOffset = (sbyte)(actor.bounds.MidX - (fontSize.X * 0.5));
			this.yOffset = (sbyte)(actor.bounds.Top - fontSize.Y - 7 - 16 - 6);
		}

		public void SetOffsetCenter() {
			Vector2 fontSize = Systems.fonts.console.font.MeasureString(this.nameplate);
			this.xOffset = (sbyte)(actor.bounds.MidX - (fontSize.X * 0.5));
			this.yOffset = (sbyte)(actor.bounds.Top - fontSize.Y - 7);
		}

		public void SetOffsetDown() {
			Vector2 fontSize = Systems.fonts.console.font.MeasureString(this.nameplate);
			this.xOffset = (sbyte)(actor.bounds.MidX - (fontSize.X * 0.5));
			this.yOffset = (sbyte)(actor.bounds.Bottom + 5);
		}

		public void SetOffset(sbyte xOffset = 0 , sbyte yOffset = 0) {
			Vector2 fontSize = Systems.fonts.console.font.MeasureString(this.nameplate);
			this.xOffset = (sbyte)(xOffset - (fontSize.X * 0.5));
			this.yOffset = yOffset;
		}

		public void Draw(int camX, int camY) {
			if(!this.visible) { return; }
			Systems.fonts.console.Draw(this.nameplate, this.actor.posX + this.xOffset - camX, this.actor.posY + this.yOffset - camY, Color.White);
		}
	}
}
