using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class Shuriken : PowerThrown {

		public enum ShurikenSubType : byte {
			Green = 0,
			Red = 1,
			Blue = 2,
			Yellow = 3,
		}

		public Shuriken( Character character, byte subType ) : base( character ) {
			this.subType = (byte) subType;
			this.sound = Systems.sounds.axe;
			this.baseStr = "ranged";
			this.subStr = "shuriken";
			this.SetActivationSettings(132, 3, 15);

			// Power Settings
			this.multMomentum = FInt.Create(0.45);
			this.xVel = FInt.Create(17);
			this.yVel = FInt.Create(0);
			this.yVelUp = FInt.Create(-18);
			this.yVelDown = FInt.Create(18);
		}

		public void ApplySubType(ShurikenSubType subType) {
			this.subType = (byte)subType;

			if(subType == ShurikenSubType.Green) {
				this.IconTexture = "Weapon/ShurikenGreen";
			} else if(subType == ShurikenSubType.Red) {
				this.IconTexture = "Weapon/ShurikenRed";
			} else if(subType == ShurikenSubType.Blue) {
				this.IconTexture = "Weapon/ShurikenBlue";
			} else if(subType == ShurikenSubType.Yellow) {
				this.IconTexture = "Weapon/ShurikenYellow";
			}
		}

		public override void AffectByInput(ref FInt velX, ref FInt velY, ref int posX, ref int posY) {

			// Affect the Y-Velocity of the projectile if holding UP or DOWN
			PlayerInput input = this.character.input;

			if(input.isDown(IKey.Up)) {
				velX = this.character.FaceRight ? FInt.Create(0.5) : FInt.Create(-0.5);
				velY = this.yVelUp;
				posX = this.character.posX + 2;
				posY = this.character.posY - 4;
			}
			
			else if(input.isDown(IKey.Down)) {
				velX = this.character.FaceRight ? FInt.Create(0.5) : FInt.Create(-0.5);
				velY = this.yVelDown;
				posX = this.character.posX + 2;
				posY = this.character.posY + 26;
			}
		}

		public override void Launch(int posX, int posY, FInt velX, FInt velY) {
			var projectile = ShurikenProjectile.Create(this.character.room, this.subType, FVector.Create(posX, posY), FVector.Create(velX, velY));
			projectile.SetActorID(this.character);
		}
	}
}
