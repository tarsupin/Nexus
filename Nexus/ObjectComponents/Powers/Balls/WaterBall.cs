using Nexus.Engine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class WaterBall : PowerBall {

		public WaterBall( Character character ) : base( character ) {
			this.subType = (byte) ProjectileBallSubType.Water;
			this.IconTexture = "Power/Water";
			this.baseStr = "magic";
			this.subStr = "water";
			this.SetActivationSettings(144, 2, 39);

			// Power Settings
			this.multMomentum = FInt.Create(0.2);
			this.xVel = FInt.Create(9);
			this.yVel = FInt.Create(-6);
			this.yVelUp = FInt.Create(-8);
			this.yVelDown = FInt.Create(1);
		}

		public override void Launch(int posX, int posY, FInt velX, FInt velY) {

			var projectile = ProjectileBall.Create(this.character.room, this.subType, FVector.Create(posX, posY), FVector.Create(velX, velY));
			projectile.SetActorID(this.character);

			var projectile2 = ProjectileBall.Create(this.character.room, this.subType, FVector.Create(posX, posY), FVector.Create(velX * FInt.Create(1.4), velY * FInt.Create(1.2)));
			projectile2.SetActorID(this.character);
		}
	}
}
