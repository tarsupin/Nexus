using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.Objects;
using System;

namespace Nexus.ObjectComponents {

	// ActionMap.DeathEnemy.StartAction(actor, deathType);
	public class DeathEnemyAction : ActionEnemy {

		public DeathEnemyAction() {
			this.duration = 0;
			this.endsOnLanding = false;
		}

		public void StartAction(Enemy enemy, DeathResult deathResult) {

			enemy.SetActivity(Activity.NoCollide); // Enemy still requires updates (for rotations, etc), but cannot collide.

			EnemyStatus status = enemy.status;
			Physics physics = enemy.physics;

			status.action = ActionMap.DeathEnemy;
			status.actionEnds = Systems.timer.frame + 80;

			// Knockout
			if(deathResult == DeathResult.Knockout) {

				physics.SetGravity(FInt.Create(0.5));

				// Randomize Knockout Direction and Rotation
				Random rand = new Random();

				physics.velocity.Y = FInt.Create(-3 - rand.Next(0, 9));
				physics.velocity.X = FInt.Create(rand.Next(0, 9) - 4);

				// TODO RENDER: APPLY ROTATION TO KNOCKOUT
				//enemy.rotSpeed = 600 + Math.floor(Math.random() * 1200);
				//enemy.render = ...renderKnockoutRotation;
			}

			// Standard Squish
			else if(deathResult == DeathResult.Squish) {
				status.actionEnds = Systems.timer.frame + 25;

				// Lock the enemy in position. Since it's .activity is now set to NoCollide, it won't change its position.
				physics.SetGravity(FInt.Create(0));
				physics.velocity.X = FInt.Create(0);
				physics.velocity.Y = FInt.Create(0);
			}

			// Special Effect
			else if(deathResult == DeathResult.Special) {
				// Effect is determined by Die() method.
			}
		}

		public override void RunAction( Enemy enemy ) {

			// End the action after the designated number of frames has elapsed:
			if(this.HasTimeElapsed(enemy)) {
				this.EndAction(enemy);
				return;
			}
		}

		public override void EndAction( Enemy enemy ) {
			enemy.Destroy();
		}
	}
}
