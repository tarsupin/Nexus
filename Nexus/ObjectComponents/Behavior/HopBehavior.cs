using Nexus.Engine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class HopBehavior : DetectCharBehavior {

		private byte hopSpeed;              // This identifies the Y-Velocity to hop with.

		public HopBehavior( EnemyLand actor, byte hopSpeed = 0, byte viewDistance = 144, byte viewHeight = 32) : base(actor, viewDistance, viewHeight) {
			this.hopSpeed = hopSpeed;
			this.SetBehavePassives(150, 30, 60, 11);
		}

		// The hop begins and ends immediately when we start the action. No need to run each frame.
		protected override void StartAction() {
			this.actor.physics.velocity.Y = FInt.Create(-this.hopSpeed);
			this.EndAction();
		}
	}
}
