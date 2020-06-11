using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class PowerBall : PowerAttack {

		protected FInt multMomentum;        // Multiplier of character's momentum to add. 0 or null is unused.
		protected FInt xVel;                // X-Velocity baseline (gets reversed when facing left).
		protected FInt yVel;                // Y-Velocity strength baseline.
		protected FInt yVelUp;              // Y-Velocity strength if UP key is held. 0 is unused.
		protected FInt yVelDown;            // Y-Velocity strength if DOWN key is held. 0 is unused.

		public PowerBall( Character character ) : base( character ) {
			this.sound = Systems.sounds.flame;
		}

		public override bool Activate() {

			// Make sure the power can be activated
			if(!this.CanActivate()) { return false; }

			this.sound.Play();

			// References
			Character character = this.character;

			// Determine Starting Position of Projectile relative to Character
			int posX = character.posX + character.bounds.MidX + (character.FaceRight ? 10 : -30);
			int posY = character.posY + character.bounds.Top + 5;

			// Check if the tile placement is blocked:
			TilemapLevel tilemap = this.character.room.tilemap;

			bool isBlocked = CollideTile.IsBlockingCoord(tilemap, posX + (character.FaceRight ? 10 : 4), posY, character.FaceRight ? DirCardinal.Right : DirCardinal.Left);

			// Prevent Throw
			if(isBlocked) { return false; }

			// Prepare Velocity
			FInt velX = character.FaceRight ? this.xVel : this.xVel.Inverse;
			FInt velY = this.yVel;

			// Affect the Y-Velocity of the projectile if holding UP or DOWN
			this.AffectByInput(ref velX, ref velY);

			// Apply Character's Momentum (if applicable)
			if(this.multMomentum > 0) {
				velX += character.physics.velocity.X * this.multMomentum;
				velY += character.physics.velocity.Y * this.multMomentum * FInt.Create(0.5);
			}

			// Launch Projectile
			this.Launch(posX, posY, velX, velY);

			return true;
		}

		public virtual void AffectByInput(ref FInt velX, ref FInt velY) {
			PlayerInput input = this.character.input;
			if(input.isDown(IKey.Up)) { velY = this.yVelUp; }
			else if(input.isDown(IKey.Down)) { velY = this.yVelDown; }
		}

		public virtual void Launch(int posX, int posY, FInt velX, FInt velY) {
			ProjectileBall projectile = ProjectileBall.Create(this.character.room, this.subType, FVector.Create(posX, posY), FVector.Create(velX.RoundInt, velY.RoundInt));
			projectile.SetActorID(this.character);
		}
	}
}
