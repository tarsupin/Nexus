using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// status.actionNum1 (earlyDuration)		:: the number of frames earlier than the ending that acts as the minimum duration.
	// status.actionBool1 (jumpActive)			:: turns FALSE when the character has released the JUMP key (once off, cannot turn on).
	// status.actionBool2 (runActive)			:: turns FALSE when the character has released the RUN key (once off, cannot turn on).

	public class WallJumpAction : Action {

		public WallJumpAction() : base() {
			this.endsOnLanding = true;
		}

		public void StartAction( Character character, DirCardinal dir, sbyte extraStrength = 0, byte extraDuration = 0, sbyte minimumDuration = 0 ) {
			this.EndLastActionIfActive(character);
			CharacterStatus status = character.status;
			CharacterStats stats = character.stats;

			// Jumping off of a wall automatically counts as your first jump.
			status.jumpsUsed = 1;

			status.action = ActionMap.WallJump;
			status.actionEnds = Systems.timer.Frame + stats.WallJumpDuration + extraDuration;
			status.actionNum1 = (sbyte) (stats.WallJumpDuration + extraDuration - minimumDuration);     // Minimum Duration
			status.actionBool1 = true; // TRUE if the Jump Key is down
			status.actionBool2 = character.input.isDown(IKey.XButton); // TRUE if the Run Key is down

			// Apply X-Axis Jump Strength
			//character.physics.physPos.X += dir == DirCardinal.Right ? -2 : 2;
			character.physics.velocity.X += ((stats.WallJumpXStrength + extraStrength) * (dir == DirCardinal.Right ? -1 : 1));
			character.physics.velocity.Y -= stats.WallJumpYStrength;

			// TODO SOUND: Wall Jump Sound
		}

		public override void RunAction( Character character ) {

			// End the action after the designated number of frames has elapsed:
			if(this.HasTimeElapsed(character)) {
				this.EndAction(character);
				return;
			}

			CharacterStatus status = character.status;
			PlayerInput input = character.input;

			// Deactivate "JUMP" marker if the character has released the jump button.
			if(status.actionBool1 && !input.isDown(IKey.AButton)) { status.actionBool1 = false; }

			// Deactivate "RUN" marker if the character has released the run button.
			if(status.actionBool2 && !input.isDown(IKey.XButton)) { status.actionBool2 = false; }

			CharacterStats stats = character.stats;
			Physics physics = character.physics;

			// JUMP STRENGTH x0.6 (if not jumping), xSlowSpeed (if not running)
			FInt jumpStrength = stats.WallJumpYStrength * (status.actionBool1 ? (FInt) 1 : FInt.Create(0.6)) * (status.actionBool2 ? (FInt) 1 : stats.SlowSpeedMult);

			// Vertical Movement
			physics.velocity.Y = 0 - jumpStrength;
			
			// If the jump button has been released and the minimum duration has ended, end the jump:
			if(!status.actionBool1 && Systems.timer.Frame > character.status.actionEnds - status.actionNum1) {
				this.EndAction(character);
				return;
			}
		}
	}
}
