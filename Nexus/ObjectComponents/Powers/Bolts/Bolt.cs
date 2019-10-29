﻿using Nexus.Engine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class Bolt : PowerAttack {

		public Bolt( Character character, ProjectileBoltSubType subType ) : base( character ) {
			this.ApplySubType(subType);
			this.sound = Systems.sounds.bolt;
			this.multMomentum = FInt.Create(0);
		}

		public void ApplySubType(ProjectileBoltSubType subType ) {
			this.subType = (byte) subType;

			if(subType == ProjectileBoltSubType.Blue) {
				this.IconTexture = "Power/Bolt";
				this.SetActivationSettings(72, 2, 21);

				// Power Settings
				this.xVel = FInt.Create(11);
				this.yVel = FInt.Create(0);
				this.yVelUp = FInt.Create(-5);
				this.yVelDown = FInt.Create(5);
			}

			else if(subType == ProjectileBoltSubType.Gold) {
				this.IconTexture = "Power/Gold";
				this.SetActivationSettings(90, 2, 24);

				// Power Settings
				this.xVel = FInt.Create(11);
				this.yVel = FInt.Create(0);
				this.yVelUp = FInt.Create(-5);
				this.yVelDown = FInt.Create(5);
			}

			else if(subType == ProjectileBoltSubType.Green) {
				this.IconTexture = "Power/Green";
				this.SetActivationSettings(96, 2, 21);

				// TODO LOW PRIORITY: Green Bolts originally used random positioning and wave-like movement. MUST UPDATE. Right now, no unique feature.

				// Power Settings
				this.xVel = FInt.Create(11);
				this.yVel = FInt.Create(0);
				this.yVelUp = FInt.Create(-5);
				this.yVelDown = FInt.Create(5);
			}
		}
	}
}
