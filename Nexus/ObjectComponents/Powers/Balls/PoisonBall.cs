using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class PoisonBall : PowerBall {

		public PoisonBall( Character character ) : base( character ) {
			this.subType = (byte)PowerSubType.Poison;
			this.projSubType = (byte) ProjectileBallSubType.Poison;
			this.IconTexture = "Power/Slime";
			this.baseStr = "magic";
			this.subStr = "poison";
			this.SetActivationSettings(96, 2, 30);

			// Power Settings
			this.multMomentum = FInt.Create(0.2);
			this.xVel = FInt.Create(9);
			this.yVel = FInt.Create(-6);
			this.yVelUp = FInt.Create(-16);
			this.yVelDown = FInt.Create(4);
		}

		public override void AffectByInput(ref FInt velX, ref FInt velY) {
			PlayerInput input = this.character.input;

			if(input.isDown(IKey.Up)) {
				velX = this.character.FaceRight ? FInt.Create(0.5) : FInt.Create(-0.5);
				velY = this.yVelUp;
			}
			
			else if(input.isDown(IKey.Down)) {
				velX = this.character.FaceRight ? FInt.Create(0.5) : FInt.Create(-0.5);
				velY = this.yVelDown;
			}
		}
	}
}
