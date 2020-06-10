using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class PowerThrust : PowerAttack {

		// Weapon Stats
		protected short range;				// The range of the weapon's attack.
		protected byte weaponWidth;			// The width of the weapon, in pixels.
		protected byte offsetY;				// Height to offset the starting position. (Usually Half Weapon Height)
		
		public PowerThrust( Character character ) : base( character ) {
			this.sound = Systems.sounds.sword;
		}

		public override bool Activate() {

			// Make sure the power can be activated
			if(!this.CanActivate()) { return false; }

			// References
			Character character = this.character;

			// Determine Starting Position of Projectile relative to Character
			int startX = character.posX + character.bounds.MidX + (this.character.FaceRight ? 0 : -this.weaponWidth);
			int startY = character.posY + character.bounds.MidY - this.offsetY;

			int endX = startX + this.range * (this.character.FaceRight ? 1 : -1);
			int endY = startY;

			// Apply Character's Momentum (if applicable)
			//if(this.multMomentum > 0) {
			//	velX += character.physics.velocity.X * this.multMomentum;
			//	velY += character.physics.velocity.Y * this.multMomentum * FInt.Create(0.5);
			//}

			this.sound.Play();

			// Launch Projectile
			this.Launch(character, startX, startY, endX, endY);

			return true;
		}

		public virtual void Launch(GameObject actor, int startX, int startY, int endX, int endY) {}
	}
}
