using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class Axe : PowerThrown {

		public Axe( Character character, WeaponAxeSubType subType ) : base( character ) {
			this.subType = (byte)PowerSubType.Axe;
			this.baseStr = "ranged";
			this.subStr = "axe";
			this.sound = Systems.sounds.axe;
			this.SetActivationSettings(72, 1, 36);

			// Power Settings
			this.multMomentum = FInt.Create(0.65);
			this.xVel = FInt.Create(5);
			this.yVel = FInt.Create(-12);
			this.yVelUp = FInt.Create(-18);
			this.yVelDown = FInt.Create(1);

			this.ApplySubType(subType);
		}

		public void ApplySubType(WeaponAxeSubType subType) {
			this.projSubType = (byte)subType;

			if(subType == WeaponAxeSubType.Axe) {
				this.IconTexture = "Weapon/Axe";
			} else if(subType == WeaponAxeSubType.Axe2) {
				this.IconTexture = "Weapon/Axe2";
			} else if(subType == WeaponAxeSubType.Axe3) {
				this.IconTexture = "Weapon/Axe3";
			}
		}

		public override void AffectByInput(ref FInt velX, ref FInt velY, ref int posX, ref int posY) {
			PlayerInput input = this.character.input;

			if(velX > 0) { posX += 7; }

			if(input.isDown(IKey.Up)) {
				velX = this.character.FaceRight ? FInt.Create(2.5) : FInt.Create(-2.5); // Axe
				velY = this.yVelUp;
			}

			else if(input.isDown(IKey.Down)) {
				velY = this.yVelDown;
			}
		}

		public override void Launch(int posX, int posY, FInt velX, FInt velY) {
			var projectile = AxeProjectile.Create(this.character.room, this.projSubType, FVector.Create(posX, posY), FVector.Create(velX, velY));
			projectile.SetActorID(this.character);
		}
	}
}
