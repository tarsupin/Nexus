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

		public override ProjectileBall Launch(int posX, int posY, FInt velX, FInt velY) {

			// Apply Character's Momentum (if applicable)
			if(this.multMomentum > 0) {
				velX += character.physics.velocity.X * this.multMomentum;
				velY += character.physics.velocity.Y * this.multMomentum;
			}

			Systems.sounds.flame.Play();

			ProjectileBall.Create(this.character.room, this.subType, FVector.Create(posX, posY), FVector.Create(velX, velY));

			return ProjectileBall.Create(this.character.room, this.subType, FVector.Create(posX, posY), FVector.Create(velX * FInt.Create(1.2), velY * FInt.Create(1.1)));
		}
	}
}
