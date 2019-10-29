using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class Shuriken : PowerThrown {

		public Shuriken( Character character, WeaponShurikenSubType subType ) : base( character ) {
			this.subType = (byte) subType;
			this.sound = Systems.sounds.axe;
			this.IconTexture = "Power/Shuriken";
			this.SetActivationSettings(132, 3, 15);

			// Power Settings
			this.multMomentum = FInt.Create(0.45);
			this.xVel = FInt.Create(17);
			this.yVel = FInt.Create(0);
			this.yVelUp = FInt.Create(-18);
			this.yVelDown = FInt.Create(18);
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
				velX = this.character.FaceRight ? FInt.Create(0.5) : FInt.Create(-0.5);
				velY = this.yVelUp;
				posX = this.character.posX + 8;
				posY = this.character.posY - 4;
			}
			
			else if(input.isDown(IKey.Down)) {
				velX = this.character.FaceRight ? FInt.Create(0.5) : FInt.Create(-0.5);
				velY = this.yVelDown;
				posX = this.character.posX + 8;
				posY = this.character.posY + 26;
			}

			// Launch Projectile
			this.Launch(posX, posY, velX, velY);

			return true;
		}
	}
}
