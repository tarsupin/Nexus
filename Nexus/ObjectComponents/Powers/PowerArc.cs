﻿using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class PowerArc : PowerAttack {

		protected byte subType;

		protected FInt multMomentum;		// Multiplier of character's momentum to add. 0 or null is unused.
		protected FInt xVel;				// X-Velocity baseline (gets reversed when facing left).
		protected FInt yVel;				// Y-Velocity strength baseline.
		protected FInt yVelUp;				// Y-Velocity strength if UP key is held. 0 is unused.
		protected FInt yVelDown;            // Y-Velocity strength if DOWN key is held. 0 is unused.

		public PowerArc( Character character ) : base( character ) {

		}

		public override bool Activate() {

			// Make sure the power can be activated
			if(!this.CanActivate()) { return false; }

			// References
			Character character = this.character;

			// Determine Starting Position of Projectile relative to Character
			int posX = character.posX + character.bounds.MidX + (character.FaceRight ? 10 : -10);
			int posY = character.posY + character.bounds.Top + 5;

			FInt velX = character.FaceRight ? this.xVel : xVel.Inverse;
			FInt velY = yVel.Inverse;

			// Affect the Y-Velocity of the projectile if holding UP or DOWN
			this.AffectByInput(ref velX, ref velY);

			// Launch Projectile
			this.Launch( posX, posY, velX, velY );

			return true;
		}

		public virtual void AffectByInput( ref FInt velX, ref FInt velY ) {
			PlayerInput input = this.character.input;

			if(input.isDown(IKey.Up)) { velY = this.yVelUp; }
			else if(input.isDown(IKey.Down)) { velY = this.yVelDown; }
		}

		public virtual ProjectileBall Launch( int posX, int posY, FInt velX, FInt velY ) {

			// Apply Character's Momentum (if applicable)
			if(this.multMomentum > 0) {
				velX += character.physics.velocity.X * this.multMomentum;
				velY += character.physics.velocity.Y * this.multMomentum;
			}

			Systems.sounds.flame.Play();

			return ProjectileBall.Create(this.character.scene, this.subType, FVector.Create(posX, posY), FVector.Create(velX, velY));
		}
	}
}
