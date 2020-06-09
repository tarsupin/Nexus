using Nexus.Engine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class FireBall : PowerBall {

		public FireBall( Character character ) : base( character ) {
			this.subType = (byte) ProjectileBallSubType.Fire;
			this.IconTexture = "Power/Fire";
			this.SetActivationSettings(60, 2, 15);

			// Power Settings
			this.multMomentum = FInt.Create(0.4);
			this.xVel = FInt.Create(8);
			this.yVel = FInt.Create(-3);
			this.yVelUp = FInt.Create(-3);
			this.yVelDown = FInt.Create(-3);
		}
	}
}
