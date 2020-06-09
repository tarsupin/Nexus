﻿using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class PowerThrown : PowerAttack {

		protected FInt multMomentum;        // Multiplier of character's momentum to add. 0 or null is unused.
		protected FInt xVel;                // X-Velocity baseline (gets reversed when facing left).
		protected FInt yVel;                // Y-Velocity strength baseline.
		protected FInt yVelUp;              // Y-Velocity strength if UP key is held. 0 is unused.
		protected FInt yVelDown;            // Y-Velocity strength if DOWN key is held. 0 is unused.

		public PowerThrown( Character character ) : base( character ) {
			this.sound = Systems.sounds.axe;
		}

		public override bool Activate() {

			// Make sure the power can be activated
			if(!this.CanActivate()) { return false; }

			// References
			Character character = this.character;

			// Determine Starting Position of Projectile relative to Character
			int posX = character.posX + character.bounds.MidX + (character.FaceRight ? 10 : -10) - 17;
			int posY = character.posY + character.bounds.Top;

			// Check if the tile placement is blocked:
			if(this is Shuriken) {
				TilemapLevel tilemap = this.character.room.tilemap;

				bool isBlocked = CollideTile.IsBlockingCoord(tilemap, posX + (character.FaceRight ? 10 : -10), posY, character.FaceRight ? DirCardinal.Right : DirCardinal.Left);

				// Prevent Throw
				if(isBlocked) { return false; }
			}

			// Prepare Velocity
			FInt velX = character.FaceRight ? this.xVel : this.xVel.Inverse;
			FInt velY = this.yVel;

			// Affect the Y-Velocity of the projectile if holding UP or DOWN
			this.AffectByInput(ref velX, ref velY, ref posX, ref posY);

			// Apply Character's Momentum (if applicable)
			if(this.multMomentum > 0) {
				velX += character.physics.velocity.X * this.multMomentum;
				velY += character.physics.velocity.Y * this.multMomentum * FInt.Create(0.5);
			}

			this.sound.Play();

			// Launch Projectile
			this.Launch(posX, posY, velX, velY);

			return true;
		}

		public virtual void AffectByInput(ref FInt velX, ref FInt velY, ref int posX, ref int posY) {
			PlayerInput input = this.character.input;
			if(input.isDown(IKey.Up)) { velY = this.yVelUp; } else if(input.isDown(IKey.Down)) { velY = this.yVelDown; }
		}

		public virtual Projectile Launch(int posX, int posY, FInt velX, FInt velY) {
			throw new System.Exception("Method must be overwritten. See WeaponShuriken.");
			//projectile.SetActorID(this.character);
			//return projectile;
		}
	}
}
