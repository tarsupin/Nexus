﻿using Nexus.Engine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class ChargeBehavior : DetectCharBehavior {

		private byte chargeSpeed;           // The speed that the enemy should charge at.
		private byte hopSpeed;              // Charges can be accompanied by hops. This identifies the Y-Velocity to hop with, if applicable.

		private byte actionSpeed;           // The speed of the current charge (varies during the action).

		public ChargeBehavior( EnemyLand actor, byte chargeSpeed = 6, byte hopSpeed = 0, byte viewDistance = 144, byte viewHeight = 32) : base(actor, viewDistance, viewHeight) {
			this.chargeSpeed = chargeSpeed;
			this.hopSpeed = hopSpeed;
		}

		protected override void StartAction() {
			base.StartAction();
			this.actionSpeed = this.chargeSpeed;

			// Trigger Hop (if applicable) only when the charge occurs:
			if(this.hopSpeed != 0) {
				this.actor.physics.velocity.Y = FInt.Create(-this.hopSpeed);
			}
		}

		protected override void RunAction() {

			Touch touch = this.actor.physics.touch;

			// End charge if no longer touching ground, or if action has expired.
			if(!touch.toBottom || this.actionEnd > this.timer.frame) {
				this.EndAction();
				return;
			}

			// Prevent further speed activation if running into something:
			if(this.dirRight) {
				if(touch.toRight) { this.actionSpeed = 0; }
			} else {
				if(touch.toLeft) { this.actionSpeed = 0; }
			}

			// Charge
			this.actor.physics.velocity.X = this.dirRight ? FInt.Create(this.actionSpeed) : FInt.Create(-this.actionSpeed);
		}
	}
}
