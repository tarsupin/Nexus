using Nexus.Engine;
using Nexus.Objects;
using System;

namespace Nexus.ObjectComponents {

	public class PowerThrust : PowerAttack {

		protected sbyte offsetX;
		protected sbyte offsetY;
		protected uint startFrame;
		protected int startX;
		protected int startY;
		protected int endX;
		protected int endY;
		protected float rotation;			// The rotation to face.
		
		// Weapon Stats
		protected short cycleDuration;		// The duration of the weapon's attack.
		protected short range;				// The range of the weapon's attack.
		protected byte weaponWidth;			// The width of the weapon, in pixels.
		protected byte weaponHeight;		// The height of the weapon, in pixels.
		
		public PowerThrust( Character character ) : base( character ) {
			this.sound = Systems.sounds.sword;
		}

		public override bool Activate() {

			// Make sure the power can be activated
			if(!this.CanActivate()) { return false; }

			// References
			Character character = this.character;

			// Determine Starting Position of Projectile relative to Character
			this.offsetX = (sbyte) Math.Round((this.character.FaceRight ? this.weaponWidth * 0.3 : -this.weaponWidth * 0.3) - this.weaponWidth * 0.5);
			this.offsetY = (sbyte) Math.Round(this.weaponHeight * 0.5);

			this.startX = character.posX + character.bounds.MidX + this.offsetX;
			this.startY = character.posY + character.bounds.Top + 15 + this.offsetY;

			this.endX = this.startX + this.range * (this.character.FaceRight ? 1 : -1);
			this.endY = this.startY;

			// Track Time of Attack
			this.startFrame = Systems.timer.Frame;

			return true;
		}
	}
}
