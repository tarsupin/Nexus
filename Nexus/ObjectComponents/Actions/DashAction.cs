using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;
using System;

namespace Nexus.ObjectComponents {

	// status.actionNum1 (physics.velocity.X)
	// status.actionNum2 (physics.velocity.Y)
	// status.actionNum3 (launch frame)				// The frame that the dash was activated.
	// status.actionBool1 (switch used)				// TRUE if the character has used their "switch" direction during the dash.

	public class DashAction : Action {

		public DashAction() : base() {
			this.endsOnLanding = false;
		}

		public void StartAction( Character character, sbyte directionHor, sbyte directionVert ) {
			
			// Don't allow this action to work if the character doesn't have shoes.
			if(character.shoes == null) { return; }

			CharacterStatus status = character.status;
			Action lastAction = status.action;

			this.EndLastActionIfActive(character);

			// Begin Action
			status.action = ActionMap.Dash;
			status.actionEnds = Systems.timer.Frame + character.shoes.duration;
			status.actionBool1 = false;		// Switch Consumed

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
					character.physics.velocity.Y = FInt.Create(-6);
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
			status.actionNum3 = Systems.timer.Frame;

			this.RestrictXMovement(character.status);

			// Switch Movement if Wall Grabbing
			if(lastAction is WallGrabAction) {
				if(status.grabDir == DirCardinal.Left) {
					if(status.actionNum1 < 0) { status.actionNum1 = -status.actionNum1; }
				} else if(status.grabDir == DirCardinal.Right) {
					if(status.actionNum1 > 0) { status.actionNum1 = -status.actionNum1; }
				}
			}

			// Update Physics
			character.physics.touch.ResetTouch();

			// Assign a Character Trail
			character.nameplate.SetCharacterTrail(0.5f, 0.02f, 1, 10);
		}

		public void RestrictXMovement(CharacterStatus status) {

			// Possibly Temporary: This prevents character from dashing through blocks. Could be fixed with collision changes?
			// Might be due to character bounds being 16 pixels? 15 works exactly. 16 doesn't. Something with MidX relative values?
			if(status.actionNum1 > 15) { status.actionNum1 = 15; }
			if(status.actionNum1 < -15) { status.actionNum1 = -15; }
		}

		public static void CauseSlam(Character character, DirCardinal dir) {
			if(Systems.camera.IsShaking(10)) { return; }
			Systems.camera.BeginCameraShake(8, 5);
			Systems.sounds.thudWhomp.Play(0.3f, 0, 0);

			// Reverse Horizontal
			if(dir == DirCardinal.Left || dir == DirCardinal.Right) {
				character.status.actionNum1 = (int) Math.Round((float)(-character.status.actionNum1) * 0.75f);
				//character.physics.velocity.X = character.physics.velocity.X.Inverse * FInt.Create(0.75);
			}

			// Reverse Vertical
			else if(dir == DirCardinal.Up || dir == DirCardinal.Down) {
				character.status.actionNum2 = (int)Math.Round((float)(-character.status.actionNum2) * 0.75f);
				//character.physics.velocity.Y = character.physics.velocity.Y.Inverse * FInt.Create(0.75);
			}
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
			if(!status.actionBool1 && Systems.timer.Frame != status.actionNum3) {

				// Add Vertical Shift
				if(character.input.isReleased(IKey.Right) || character.input.isPressed(IKey.Left)) {
					status.actionNum1 -= 7;
					status.actionBool1 = true;
					this.RestrictXMovement(character.status);
				} else if(character.input.isReleased(IKey.Left) || character.input.isPressed(IKey.Right)) {
					status.actionNum1 += 7;
					status.actionBool1 = true;
					this.RestrictXMovement(character.status);
				}

				// Add Horizontal Shift
				if(character.input.isReleased(IKey.Up) || character.input.isPressed(IKey.Down)) {
					status.actionNum2 += 7;
					status.actionBool1 = true;
					this.RestrictXMovement(character.status);
				} else if(character.input.isReleased(IKey.Down) || character.input.isPressed(IKey.Up)) {
					status.actionNum2 -= 7;
					status.actionBool1 = true;
					this.RestrictXMovement(character.status);
				}
			}

			// Update Physics
			Physics physics = character.physics;
			physics.velocity.X = FInt.Create(status.actionNum1);
			physics.velocity.Y = FInt.Create(status.actionNum2) + physics.gravity.Inverse;
		}

		public override void EndAction(Character character) {
			base.EndAction(character);

			// Reduce the speed of the character after the dash effect.
			Physics physics = character.physics;
			physics.velocity.X *= FInt.Create(0.5f);
			physics.velocity.Y *= FInt.Create(0.5f);
		}
	}
}
