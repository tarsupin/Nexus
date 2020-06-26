using Nexus.Engine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class RockBall : PowerBall {

		public RockBall( Character character ) : base( character ) {
			this.subType = (byte)PowerSubType.Rock;
			this.projSubType = (byte) ProjectileBallSubType.Rock;
			this.sound = Systems.sounds.rock;
			this.IconTexture = "Power/Rock";
			this.baseStr = "magic";
			this.subStr = "rock";
			this.SetActivationSettings(60, 1, 30);

			// Power Settings
			this.multMomentum = FInt.Create(1);
			this.xVel = FInt.Create(1);
			this.yVel = FInt.Create(-1);
			this.yVelUp = FInt.Create(-4);
			this.yVelDown = FInt.Create(2);
		}

		public override void Launch(int posX, int posY, FInt velX, FInt velY) {
			if(velX < 0) { posX -= 8; }
			var projectile = ProjectileBall.Create(this.character.room, this.projSubType, FVector.Create(posX, posY), FVector.Create(velX, velY));
			projectile.SetActorID(this.character);
		}
	}
}
