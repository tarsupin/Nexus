using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.Objects;
using System;

namespace Nexus.ObjectComponents {

	public class Nameplate {

		private readonly Character character;
		private readonly Atlas atlas;

		// Draw Name and HP
		private string name;
		private bool nameVisible = false;
		private bool hpVisible = false;

		private sbyte xName = 0;
		private sbyte yName = 0;

		private sbyte xHP = 0;
		private sbyte yHP = 0;

		// Draw Character Trail
		private float trailAlpha = 0f;
		private float trailAlphaDecay = 0f;
		private int trailLast = 0;			// The frame that a particle was most recently placed.
		private byte trailRate = 0;         // The number of frames to wait before placing a new particle.
		private byte trailDuration = 0;		// The # of frames that the effect will last.

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

		public void SetCharacterTrail(float alphaStart, float alphaDecay, byte trailRate, byte trailDuration) {
			this.trailAlpha = alphaStart;
			this.trailAlphaDecay = -Math.Abs(alphaDecay);
			this.trailRate = trailRate;
			this.trailLast = 0;
			this.trailDuration = trailDuration;
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

			// Draw Character Trail
			if(this.trailAlpha > 0 && this.trailLast < Systems.timer.Frame + this.trailRate) {
				StayFadeParticle.SetCharFadeParticle(character.room, character, Systems.timer.Frame + this.trailDuration, this.trailAlpha, this.trailAlphaDecay);
				this.trailLast = Systems.timer.Frame;
				this.trailAlpha += this.trailAlphaDecay;
			}
		}
	}
}
