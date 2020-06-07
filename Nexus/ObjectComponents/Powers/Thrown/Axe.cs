using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class Axe : PowerThrown {

		public Axe( Character character, WeaponAxeSubType subType ) : base( character ) {
			this.ApplySubType(subType);
			this.sound = Systems.sounds.axe;
		}

		public override void AffectByInput(ref FInt velX, ref FInt velY) {
			PlayerInput input = this.character.input;

			if(input.isDown(IKey.Up)) {
				velX = this.character.FaceRight ? FInt.Create(2.5) : FInt.Create(-2.5); // Axe
				velY = this.yVelUp;
			}

			else if(input.isDown(IKey.Down)) {
				velY = this.yVelDown;
			}
		}

		public void ApplySubType(WeaponAxeSubType subType) {
			this.subType = (byte) subType;

			if(subType == WeaponAxeSubType.Axe) {
				this.IconTexture = "Weapon/Axe";
				this.SetActivationSettings(72, 1, 36);

				// Power Settings
				this.multMomentum = FInt.Create(0.8);
				this.xVel = FInt.Create(5);
				this.yVel = FInt.Create(-12);
				this.yVelUp = FInt.Create(-18);
				this.yVelDown = FInt.Create(1);
			}
			
			// TODO: Right now, Axe2 is identical to Axe. Change it's behavior, at least slightly.
			else if(subType == WeaponAxeSubType.Axe2) {
				this.IconTexture = "Weapon/Axe2";
				this.SetActivationSettings(72, 1, 36);

				// Power Settings
				this.multMomentum = FInt.Create(0.8);
				this.xVel = FInt.Create(5);
				this.yVel = FInt.Create(-12);
				this.yVelUp = FInt.Create(-18);
				this.yVelDown = FInt.Create(1);
			}

			// TODO: Right now, Axe3 is identical to Axe. Change it's behavior, at least slightly.
			else if(subType == WeaponAxeSubType.Axe3) {
				this.IconTexture = "Weapon/Axe3";
				this.SetActivationSettings(72, 1, 36);

				// Power Settings
				this.multMomentum = FInt.Create(0.8);
				this.xVel = FInt.Create(5);
				this.yVel = FInt.Create(-12);
				this.yVelUp = FInt.Create(-18);
				this.yVelDown = FInt.Create(1);
			}
		}
	}
}
