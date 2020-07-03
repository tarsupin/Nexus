using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;
using System;

namespace Nexus.ObjectComponents {

	// status.actionNum1 (physics.velocity.X)
	// status.actionNum2 (physics.velocity.Y)
	// status.actionBool1 (switch used)				// TRUE if the character has used their "switch" direction during the dash.

	public class DashAction : Action {

		public DashAction() : base() {
			this.endsOnLanding = true;
		}

		public void StartAction( Character character, sbyte directionHor, sbyte directionVert ) {
			this.EndLastActionIfActive(character);

			CharacterStatus status = character.status;

			status.action = ActionMap.Dash;
			status.actionEnds = Systems.timer.Frame + Shoes.duration;
			status.actionBool1 = false;

			int velX = character.physics.velocity.X.RoundInt;

			// End Momentum
			character.physics.velocity.X = FInt.Create(0);
			character.physics.velocity.Y = FInt.Create(0);

			// Horizontal Dash
			if(directionHor != 0) {
				character.physics.velocity.X = FInt.Create((directionHor == 1) ? 12 : -12);

				// Add Run Momentum
				if(directionHor == 1) {
					if(velX > 0) { character.physics.velocity.X += (int)Math.Floor(velX * 0.3f); }
					if(directionVert == 0) { character.physics.velocity.X += 4; }
				} else if(directionHor == -1) {
					if(velX < 0) { character.physics.velocity.X += (int)Math.Floor(velX * 0.3f); }
					if(directionVert == 0) { character.physics.velocity.X -= 4; }
				}
			}

			// Vertical Dashing Mechanics
			// If we're on the ground, add some lift.
			if(character.physics.touch.toFloor) {
				if(directionVert == -1) {
					character.physics.velocity.Y = FInt.Create(-15);
				} else if(directionVert == 1) {
					character.physics.velocity.Y = FInt.Create(-4);
				} else {
					character.physics.velocity.Y = FInt.Create(-8);
				}
			}
			
			// If we're in the air, vertical movement should be more dash-like.
			else {
				if(directionVert == -1) {
					character.physics.velocity.Y = FInt.Create(-12);
				} else if(directionVert == 1) {
					character.physics.velocity.Y = FInt.Create(16);
				} else {
					character.physics.velocity.Y = FInt.Create(0);
				}
			}
			
			status.actionNum1 = character.physics.velocity.X.ToInt();
			status.actionNum2 = character.physics.velocity.Y.ToInt();

			// Update Physics
			character.physics.touch.ResetTouch();
		}

		public override void RunAction( Character character ) {

			// End the action after the designated number of frames has elapsed:
			if(Systems.timer.Frame > character.status.actionEnds) {
				this.EndAction( character );
				return;
			}

			// Update the last held time.
			if(character.shoes is Shoes) {
				character.shoes.SetLastHeld(Systems.timer.Frame);
			}

			CharacterStatus status = character.status;

			// If the character hasn't used their "switch" in the dash yet.
			// "Switch" allows them to shift directions (somewhat) during the dash.
			// Making a switch also increases the duration by a very small amount.
			if(!status.actionBool1 && status.actionEnds != Systems.timer.Frame + Shoes.duration) {

				// Add Vertical Shift
				if(character.input.isReleased(IKey.Right) || character.input.isPressed(IKey.Left)) {
					if(status.actionNum2 < 0) {
						status.actionNum2 -= 4;
					} else if(status.actionNum2 > 0) {
						status.actionNum2 += 4;
					}
					status.actionNum1 -= 4;
					status.actionBool1 = true;
				} else if(character.input.isReleased(IKey.Left) || character.input.isPressed(IKey.Right)) {
					if(status.actionNum2 < 0) {
						status.actionNum2 -= 4;
					} else if(status.actionNum2 > 0) {
						status.actionNum2 += 4;
					}
					status.actionNum1 += 4;
					status.actionBool1 = true;
				}

				// Add Horizontal Shift
				if(character.input.isReleased(IKey.Up) || character.input.isPressed(IKey.Down)) {
					if(status.actionNum1 < 0) {
						status.actionNum1 -= 4;
					} else if(status.actionNum1 > 0) {
						status.actionNum1 += 4;
					}
					status.actionNum2 += 4;
					status.actionBool1 = true;
				} else if(character.input.isReleased(IKey.Down) || character.input.isPressed(IKey.Up)) {
					if(status.actionNum1 < 0) {
						status.actionNum1 -= 4;
					} else if(status.actionNum1 > 0) {
						status.actionNum1 += 4;
					}
					status.actionNum2 -= 4;
					status.actionBool1 = true;
				}
			}

			Physics physics = character.physics;
			physics.velocity.X = FInt.Create(status.actionNum1);
			physics.velocity.Y = FInt.Create(status.actionNum2) + physics.gravity.Inverse;
		}
	}
}
