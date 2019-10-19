using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// ActorState.MovementStandard		// Enemy is performing standard movement and watching for characters.
	// ActorState.ReactionStall			// Enemy has spotted a character, and is charging up (frozen, stalling).
	// ActorState.ReactionCharacter		// Enemy is actively performing the behavior action. 

	public class DetectCharBehavior : Behavior {

		protected TimerGlobal timer;

		protected readonly byte viewDist;		// The distance (X-axis) that the enemy can "see" in the direction it faces.
		protected readonly byte viewHeight;		// The height for the enemy's view.

		protected byte testFrequency;			// The frequency (in frames) of how often the enemy will "look" for characters.
		protected byte cooldownDuration;		// The duration of the cooldown (how long until the actor can act again).
		protected byte stallDuration;			// The stalling duration; how long the actor will stall movement before charging.
		protected byte actionDuration;			// The duration of the charge.

		// Action
		protected uint actionEnd;				// The next frame # end for a given duration.
		protected bool dirRight;				// If the charge is facing right (or false if left).

		public DetectCharBehavior( EnemyLand actor, byte viewDistance = 144, byte viewHeight = 32 ) : base(actor) {

			// Reaction
			this.viewDist = viewDistance;
			this.viewHeight = viewHeight;
			this.dirRight = false;

			// Timer
			this.timer = Systems.timer;
			this.actionEnd = 0;

			this.SetBehavePassives();
		}

		public void SetBehavePassives( byte cooldownDuration = 120, byte stallDuration = 30, byte actionDuration = 15, byte testFrequency = 13 ) {
			this.cooldownDuration = cooldownDuration;
			this.stallDuration = stallDuration;
			this.actionDuration = actionDuration;
			this.testFrequency = testFrequency;
		}

		protected void WatchForCharacter() {

			// Only run the watch behavior every 13 frames (prime number to reduce overlap).
			if(this.timer.frame % this.testFrequency != 0) { return; }

			// Make sure the cooldown is complete.
			if(this.actionEnd > this.timer.frame) { return; }

			Bounds bounds = this.actor.bounds;

			// Determine view bounds based on direction actor is facing.
			int scanX = actor.posX + (actor.FaceRight ? bounds.Right : bounds.Left - this.viewDist);
			int scanY = actor.posY + bounds.Bottom - this.viewHeight;

			uint objectId = CollideDetect.FindObjectsTouchingArea(
				this.actor.scene.objects[(byte)LoadOrder.Character],
				(uint)scanX,
				(uint)scanY,
				this.viewDist,
				this.viewHeight
			);

			// If a character was located in the enemy's view range, enemy begins their stall (before their action).
			if(objectId != 0) {
				this.BeginStall();
			}
		}

		protected virtual void BeginStall() {
			this.actor.SetState(ActorState.ReactionStall);
			this.actionEnd = this.timer.frame + this.stallDuration;
			this.dirRight = this.actor.FaceRight;
		}

		protected virtual void RunStall() {

			this.actor.physics.StopX();

			// Wait until the stall is complete:
			if(this.actionEnd > this.timer.frame) { return; }

			// Activate Charge
			this.StartAction();
		}

		protected virtual void StartAction() {
			this.actor.SetState(ActorState.ReactionCharacter);
			this.actionEnd = this.timer.frame + this.actionDuration;
		}
		
		protected virtual void RunAction() {
			if(this.actionEnd > this.timer.frame) {
				this.EndAction();
				return;
			}
		}

		protected virtual void EndAction( ActorState state = ActorState.BehaviorStandard ) {
			this.actor.SetState(state);
			this.actionEnd = this.timer.frame + this.cooldownDuration;
		}

		public override void RunTick() {
			if(this.actor.State == ActorState.ReactionStall) { this.RunStall(); }
			else if(this.actor.State == ActorState.ReactionCharacter) { this.RunAction(); }
			else { this.WatchForCharacter(); }
		}
	}
}
