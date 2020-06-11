using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class Nameplate {

		private readonly Character character;
		private readonly Atlas atlas;

		private string name;
		private bool nameVisible = false;
		private bool hpVisible = false;

		private sbyte xName = 0;
		private sbyte yName = 0;

		private sbyte xHP = 0;
		private sbyte yHP = 0;

		public Nameplate(Character character, string name, bool nameVisible = false, bool hpVisible = false) {
			this.character = character;
			this.atlas = Systems.mapper.atlas[(byte)AtlasGroup.Tiles];
			this.SetName(name);
			this.SetVisible(nameVisible, hpVisible);
		}

		public byte HealthShown { get { return this.hpVisible ? (byte)(this.character.wounds.Armor + this.character.wounds.Health) : (byte) 0; } }

		public void SetName(string name) { this.name = name; }

		public void SetVisible(bool nameVisible = false, bool hpVisible = false) {
			this.nameVisible = nameVisible;
			this.hpVisible = hpVisible;

			if(!this.nameVisible && !this.hpVisible) { return; }

			if(this.hpVisible) {
				this.xHP = (sbyte)(this.character.bounds.MidX + 1);
				this.yHP = (sbyte)(this.character.bounds.Top - 8 - 18);
			}

			if(this.nameVisible) {
				Vector2 fontSize = Systems.fonts.console.font.MeasureString(this.name);
				this.xName = (sbyte)(character.bounds.MidX - (fontSize.X * 0.5));
				this.yName = (sbyte)(character.bounds.Top - fontSize.Y - 7);
			}
		}

		//public void SetOffsetDown() {
		//	Vector2 fontSize = Systems.fonts.console.font.MeasureString(this.name);
		//	this.xName = (sbyte)(character.bounds.MidX - (fontSize.X * 0.5));
		//	this.yName = (sbyte)(character.bounds.Bottom + 5);
		//}

		public void Draw(int camX, int camY) {
			byte hpShown = this.HealthShown;
			if(this.character.hat is Hat) { camY += 4; }

			if(this.nameVisible) {
				Systems.fonts.console.Draw(this.name, this.character.posX + this.xName - camX, this.character.posY + this.yName - camY - (hpShown > 0 ? 24 : 0), Color.White);
			}

			// Get Shields + Health Counts
			if(hpShown > 0) {
				byte armor = this.character.wounds.Armor;
				camX += (byte)(hpShown * 9);

				// Draw Shield and Health Bits
				for(byte i = 0; i < hpShown; i++) {
					this.atlas.Draw(armor > i ? "Icon/ShieldBit" : "Icon/HPBit", this.character.posX + this.xHP - camX + (i * 18), this.character.posY + this.yHP - camY);
				}
			}
		}
	}
}
