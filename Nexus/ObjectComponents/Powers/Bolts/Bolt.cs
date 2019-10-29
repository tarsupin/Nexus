using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public enum BoltSubType : byte {
		Blue,
		Green,
		Gold,
	}

	public class Bolt : PowerAttack {

		protected BoltSubType subType;

		protected FInt xVel;                // X-Velocity baseline (gets reversed when facing left).
		protected FInt yVel;                // Y-Velocity strength baseline.
		protected FInt yVelUp;              // Y-Velocity strength if UP key is held. 0 is unused.
		protected FInt yVelDown;            // Y-Velocity strength if DOWN key is held. 0 is unused.

		public Bolt( Character character, BoltSubType subType ) : base( character ) {
			this.ApplySubType(subType);
		}

		public void ApplySubType( BoltSubType subType ) {
			this.subType = subType;

			if(subType == BoltSubType.Blue) {
				this.IconTexture = "Power/Bolt";
				this.SetActivationSettings(72, 2, 21);

				// Power Settings
				this.xVel = FInt.Create(11);
				this.yVel = FInt.Create(0);
				this.yVelUp = FInt.Create(-5);
				this.yVelDown = FInt.Create(5);
			}

			else if(subType == BoltSubType.Gold) {
				this.IconTexture = "Power/Gold";
				this.SetActivationSettings(90, 2, 24);

				// Power Settings
				this.xVel = FInt.Create(11);
				this.yVel = FInt.Create(0);
				this.yVelUp = FInt.Create(-5);
				this.yVelDown = FInt.Create(5);
			}

			else if(subType == BoltSubType.Green) {
				this.IconTexture = "Power/Green";
				this.SetActivationSettings(96, 2, 21);

				// TODO LOW PRIORITY: Green Bolts originally used random positioning and wave-like movement. MUST UPDATE. Right now, no unique feature.

				// Power Settings
				this.xVel = FInt.Create(11);
				this.yVel = FInt.Create(0);
				this.yVelUp = FInt.Create(-5);
				this.yVelDown = FInt.Create(5);
			}
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
			PlayerInput input = this.character.input;

			if(input.isDown(IKey.Up)) { velY = this.yVelUp; }
			else if(input.isDown(IKey.Down)) { velY = this.yVelDown; }

			// Launch Projectile
			this.Launch(posX, posY, velX, velY);

			return true;
		}

		public ProjectileBall Launch(int posX, int posY, FInt velX, FInt velY) {
			Systems.sounds.bolt.Play();
			return ProjectileBall.Create(this.character.scene, (byte) this.subType, FVector.Create(posX, posY), FVector.Create(velX, velY));
		}
	}
}
