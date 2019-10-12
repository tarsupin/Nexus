using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class HopUpAction : ActionEnemy {

		// status.actionBool1 (prepHop)			:: TRUE if the enemy is preparing for hop. FALSE if the enemy has already hopped.

		private readonly byte stallDuration;

		public HopUpAction() : base() {
			this.stallDuration = 30;			// The Stall Duration is how long the enemy waits before hopping. Will rest on ground until then.
			this.duration = 60;					// The duration is how long the enemy waits after hopping to reset the timer.
		}

		public void StartAction( Enemy enemy ) {

			// Enemy must be on the floor to watch for hop:
			if(!enemy.physics.touch.toBottom) { return; }

			EnemyStatus status = enemy.status;

			// If the action is paused, wait until sufficient time has passed.
			if(enemy.action is HopUpAction) {
				if(!this.HasTimeElapsed(enemy)) { return; }
				if(status.actionBool1 == true) { return; }
			}

			status.action = ActionMap.HopUp;
			status.actionEnds = enemy.scene.timer.frame + this.stallDuration;
			status.actionBool1 = true;
		}

		public void RunAction( Enemy enemy ) {

			// End the action after the designated number of frames has elapsed:
			if(this.HasTimeElapsed(enemy)) {

				EnemyStatus status = enemy.status;

				// If the hop was being prepared, begin the hop.
				if(status.actionBool1) {
					status.actionEnds = enemy.scene.timer.frame + this.duration;

					enemy.physics.velocity.Y = (FInt)(-10);

					// TODO SOUND: Enemy Hop (quiet)
					return;
				}

				// If the hop was preformed and time passed, we can end the hop action.
				this.EndAction(enemy);
				return;
			}
		}
	}
}
