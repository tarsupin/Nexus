using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class PowerBolt : PowerAttack {

		protected byte multMomentum = 0;	// Multiplier of character's momentum to add. 0 or null is unused.
		protected sbyte xVel;				// X-Velocity baseline (gets reversed when facing left).
		protected sbyte yVel;				// Y-Velocity strength baseline.
		protected sbyte yVelUp;				// Y-Velocity strength if UP key is held. 0 is unused.
		protected sbyte yVelDown;			// Y-Velocity strength if DOWN key is held. 0 is unused.

		public PowerBolt( Character character ) : base( character ) {
			this.sound = Systems.sounds.bolt;
			this.multMomentum = 0;
			this.baseStr = "wand";

			// Power Settings
			this.xVel = 11;
			this.yVel = 0;
			this.yVelUp = -2;
			this.yVelDown = 2;
		}

		public override bool Activate() {

			// Make sure the power can be activated
			if(!this.CanActivate()) { return false; }

			// References
			Character character = this.character;

			// Determine Starting Position of Projectile relative to Character
			int posX = character.posX + character.bounds.MidX + (character.FaceRight ? 6 : -30);
			int posY = character.posY + character.bounds.Top + 5;

			// Play Sound
			character.room.PlaySound(this.sound, 1f, posX, posY);

			// Check if the tile placement is blocked (only applies to bolts that collide with tiles).
			if(this is BoltBlue) {
				TilemapLevel tilemap = this.character.room.tilemap;

				bool isBlocked = CollideTile.IsBlockingCoord(tilemap, posX + (character.FaceRight ? 10 : 4), posY, character.FaceRight ? DirCardinal.Right : DirCardinal.Left);

				// Prevent Throw
				if(isBlocked) { return false; }
			}

			// Prepare Velocity
			FInt velX = character.FaceRight ? FInt.Create(this.xVel) : FInt.Create(-this.xVel);
			FInt velY = FInt.Create(this.yVel);

			// Affect the Y-Velocity of the projectile if holding UP or DOWN
			this.AffectByInput(ref velX, ref velY);

			// Launch Projectile
			this.Launch(character, posX, posY, velX, velY);

			return true;
		}

		public virtual void AffectByInput(ref FInt velX, ref FInt velY) {
			PlayerInput input = this.character.input;
			if(input.isDown(IKey.Up)) { velY = FInt.Create(this.yVelUp); }
			else if(input.isDown(IKey.Down)) { velY = FInt.Create(this.yVelDown); }
		}

		public virtual void Launch(GameObject actor, int posX, int posY, FInt velX, FInt velY) { }
	}
}
