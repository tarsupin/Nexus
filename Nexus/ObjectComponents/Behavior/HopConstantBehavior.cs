using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class HopConstantBehavior : Behavior {

		protected new EnemyLand actor;		// Reference to the enemy.

		public HopConstantBehavior( EnemyLand actor ) : base(actor) {
			this.actor = actor;
		}

		protected void StartHop() {

			this.actor.physics.velocity.X = FInt.Create(this.actor.FaceRight ? 2 : -2);
			this.actor.physics.velocity.Y = FInt.Create(-8);

			this.actor.SetState(ActorState.MoveStandard);
		}

		public override void RunTick() {

			// If we're on the two beats, the hop can be triggered and sustained (6 frame duration).
			// Note that any enemies with Activity.Inactive will not hop, but will resyncronize.
			if(Systems.timer.beat <= 1) {

				// Only start hop if touching the ground.
				if(this.actor.physics.touch.toBottom) { this.StartHop(); }

				// If the hop has already started, keep refreshing the velocity (maintains jump until beats have expired)
				else if(this.actor.State == ActorState.MoveStandard) {
					this.actor.physics.velocity.Y = FInt.Create(-8);
				}
			}
			
			// If the beat is untriggered:
			else {

				// If the actor is currently grounded (and waiting)
				if(this.actor.physics.touch.toBottom) {
					this.actor.physics.StopX();
					this.actor.SetState(ActorState.RestStall);
				} else {
					this.actor.SetState(ActorState.MoveAir);
				}
			}
		}
	}
}
