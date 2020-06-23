using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class PowerThrust : PowerAttack {

		// Weapon Stats
		protected short duration;			// The duration of the weapon's attack.
		protected short speed;				// The X-Velocity of the weapon's attack.
		protected byte weaponWidth;			// The width of the weapon, in pixels.
		protected byte offsetY;             // Height to offset the starting position. (Usually Half Weapon Height)
		protected byte behindVal;			// The amount to set the projectile behind the character when thrust (to collide with first block).
		
		public PowerThrust( Character character ) : base( character ) {
			this.sound = Systems.sounds.sword;
		}

		public override bool Activate() {

			// Make sure the power can be activated
			if(!this.CanActivate()) { return false; }

			// References
			Character character = this.character;

			// Determine Starting Position of Projectile relative to Character
			int startX = character.posX + character.bounds.MidX + (this.character.FaceRight ? -this.behindVal : -this.weaponWidth + this.behindVal);
			int startY = character.posY + character.bounds.MidY - this.offsetY;

			sbyte velX = (this.character.FaceRight ? (sbyte)this.speed : (sbyte)-this.speed);

			this.sound.Play();

			// Launch Projectile
			this.Launch(character, startX, startY, velX);

			return true;
		}

		public virtual void Launch(GameObject actor, int startX, int startY, sbyte velX) {}
	}
}
