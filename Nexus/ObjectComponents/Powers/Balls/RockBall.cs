using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class RockBall : PowerArc {

		public RockBall( Character character ) : base( character ) {
			this.subType = (byte) ProjectileBallSubType.Rock;
			this.sound = Systems.sounds.rock;
			this.IconTexture = "Power/Rock";
			this.SetActivationSettings(60, 1, 30);

			// Power Settings
			this.multMomentum = FInt.Create(1);
			this.xVel = FInt.Create(1);
			this.yVel = FInt.Create(-1);
			this.yVelUp = FInt.Create(-4);
			this.yVelDown = FInt.Create(2);
		}

		public override bool Activate() {

			// Make sure the power can be activated
			if(!this.CanActivate()) { return false; }

			// References
			Character character = this.character;

			// Determine Starting Position of Projectile relative to Character
			int posX = character.posX + character.bounds.MidX + (character.FaceRight ? 10 : -10);
			int posY = character.posY + character.bounds.Top + 5;

			FInt velX = character.FaceRight ? this.xVel : this.xVel.Inverse;
			FInt velY = this.yVel;

			// Affect the Y-Velocity of the projectile if holding UP or DOWN
			PlayerInput input = this.character.input;

			if(input.isDown(IKey.Up)) {
				velX = this.character.FaceRight ? FInt.Create(2) : FInt.Create(-2);
				velY = this.yVelUp;
			}
			
			else if(input.isDown(IKey.Down)) {
				velX = FInt.Create(0);
				velY = this.yVelDown;
				posX = this.character.posX + 6;
				posY = this.character.posY + 24;
			}

			// Launch Projectile
			this.Launch(posX, posY, velX, velY);

			return true;
		}
	}
}
