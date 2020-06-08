using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class HealthBits {

		private readonly Character character;
		private readonly Atlas atlas;
		private bool visible = false;
		private sbyte xOffset = 0;
		private sbyte yOffset = -10;

		public HealthBits(Character actor, DirCardinal position = DirCardinal.Up, bool setVisible = true) {
			this.character = actor;
			this.atlas = Systems.mapper.atlas[(byte)AtlasGroup.Tiles];
			this.SetVisible(setVisible);
			if(position == DirCardinal.Up) { this.SetOffsetUp(); }
			else if(position == DirCardinal.Down) { this.SetOffsetDown(); }
		}

		public void SetVisible(bool visible = true) {
			this.visible = visible;
		}

		public void SetOffsetDown() {
			this.xOffset = (sbyte)(character.bounds.MidX + 1);
			this.yOffset = (sbyte)(character.bounds.Bottom + 4);
		}

		public void SetOffsetUp() {
			this.xOffset = (sbyte)(character.bounds.MidX + 1);
			this.yOffset = (sbyte)(character.bounds.Top - 8 - 16);
		}

		public void SetOffset(sbyte xOffset = 0 , sbyte yOffset = 0) {
			this.xOffset = xOffset;
			this.yOffset = yOffset;
		}

		public void Draw(int camX, int camY) {
			if(!this.visible) { return; }

			// Get Shields + Health Counts
			byte armor = this.character.wounds.Armor;
			byte hp = (byte)(armor + this.character.wounds.Health);
			camX += (byte)(hp * 9);

			// Draw Shield and Health Bits
			for(byte i = 0; i < hp; i++) {
				this.atlas.Draw(armor > i ? "Icon/ShieldBit" : "Icon/HPBit", this.character.posX + this.xOffset - camX + (i * 18), this.character.posY + this.yOffset - camY);
			}
		}
	}
}
