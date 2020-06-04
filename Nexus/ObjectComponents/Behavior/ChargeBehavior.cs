using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class ChargeBehavior : DetectCharBehavior {

		private byte chargeSpeed;           // The speed that the enemy should charge at.
		private byte hopSpeed;              // Charges can be accompanied by hops. This identifies the Y-Velocity to hop with, if applicable.
		private byte actionSpeed;           // The speed of the current charge (varies during the action).
		private bool walk;					// Whether the enemy walks or not.

		public ChargeBehavior( EnemyLand actor, byte chargeSpeed = 6, byte hopSpeed = 0, bool walk = false, byte viewDistance = 144, byte viewHeight = 32) : base(actor, viewDistance, viewHeight) {
			this.chargeSpeed = chargeSpeed;
			this.hopSpeed = hopSpeed;
			this.walk = walk;
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

			// End charge when touching ground and action has expired.
			if(touch.toBottom && this.actionEnd < this.timer.Frame) {
				this.actor.physics.StopX();
				this.EndAction((byte) CommonState.MotionEnd);
				return;
			}

			// End action and prevent further speed activation if running into something:
			if(this.dirRight) {
				if(touch.toRight) {
					this.actor.physics.StopX();
					this.EndAction((byte) CommonState.Wait);
					return;
				}
			} else {
				if(touch.toLeft) {
					this.actor.physics.StopX();
					this.EndAction((byte) CommonState.Wait);
					return;
				}
			}

			// Charge
			this.actor.physics.velocity.X = this.dirRight ? FInt.Create(this.actionSpeed) : FInt.Create(-this.actionSpeed);
		}

		protected override void EndAction(byte state = (byte)CommonState.Move) {
			base.EndAction(state);

			if(this.walk) {
				if(this.dirRight) {
					((EnemyLand)this.actor).WalkRight();
				} else {
					((EnemyLand)this.actor).WalkLeft();
				}
			}
		}
	}
}
