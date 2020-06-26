using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class Grenade : PowerThrown {

		public Grenade( Character character ) : base( character ) {
			this.subType = (byte)PowerSubType.Grenade;
			this.sound = Systems.sounds.axe;
			this.IconTexture = "Weapon/Grenade";
			this.baseStr = "ranged";
			this.subStr = "grenade";
			this.SetActivationSettings(30, 1, 30);

			// Power Settings
			this.multMomentum = FInt.Create(0.3);
			this.xVel = FInt.Create(14);
			this.yVel = FInt.Create(-7);
			this.yVelUp = FInt.Create(-14);
			this.yVelDown = FInt.Create(5);
		}

		public override void AffectByInput(ref FInt velX, ref FInt velY, ref int posX, ref int posY) {
			PlayerInput input = this.character.input;

			if(input.isDown(IKey.Up)) {
				velX = this.character.FaceRight ? FInt.Create(7) : FInt.Create(-7);
				velY = this.yVelUp;
			} else if(input.isDown(IKey.Down)) {
				velX = this.character.FaceRight ? FInt.Create(5) : FInt.Create(-5);
				velY = this.yVelDown;
			}
		}

		public override void Launch(int posX, int posY, FInt velX, FInt velY) {
			var projectile = GrenadeProjectile.Create(this.character.room, this.projSubType, FVector.Create(posX, posY), FVector.Create(velX, velY));
			projectile.SetActorID(this.character);
		}
	}
}
