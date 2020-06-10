using Nexus.Engine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class FrostBall : PowerBall {

		public FrostBall( Character character ) : base( character ) {
			this.subType = (byte) ProjectileBallSubType.Frost;
			this.IconTexture = "Power/Frost";
			this.baseStr = "magic";
			this.subStr = "frost";
			this.SetActivationSettings(60, 3, 12);

			// Power Settings
			this.multMomentum = FInt.Create(0.8);
			this.xVel = FInt.Create(11);
			this.yVel = FInt.Create(-6);
			this.yVelUp = FInt.Create(-10);
			this.yVelDown = FInt.Create(-2);
		}
	}
}
