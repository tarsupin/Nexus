﻿using Nexus.Engine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class ElectricBall : PowerBall {

		public ElectricBall( Character character ) : base( character ) {
			this.subType = (byte)PowerSubType.Electric;
			this.projSubType = (byte) ProjectileBallSubType.Electric;
			this.IconTexture = "Power/Electric";
			this.baseStr = "magic";
			this.subStr = "electric";
			this.SetActivationSettings(60, 2, 15);

			// Power Settings
			this.multMomentum = FInt.Create(0.8);
			this.xVel = FInt.Create(8);
			this.yVel = FInt.Create(-3);
			this.yVelUp = FInt.Create(-13);
			this.yVelDown = FInt.Create(6);
		}
	}
}
