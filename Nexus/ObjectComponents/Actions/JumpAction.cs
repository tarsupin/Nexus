using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// status.actionNum1 (strength)				:: the strength of the jump.
	// status.actionNum2 (earlyDuration)		:: the number of frames earlier than the ending that acts as the minimum duration.
	// status.actionBool1 (jumpActive)			:: turns FALSE when the character has released the JUMP key (once off, cannot turn on).
	// status.actionBool2 (runActive)			:: turns FALSE when the character has released the RUN key (once off, cannot turn on).

	public class JumpAction : ActionCharacter {

		public JumpAction() : base() {
			this.endsOnLanding = true;
		}

		public void StartAction( Character character, sbyte extraStrength = 0, byte extraDuration = 0, sbyte minimumDuration = 0 ) {
			CharacterStatus status = character.status;
			CharacterStats stats = character.stats;

			// If falling, must have at least one jump strain (to account for extra jump otherwise)
			if(!character.physics.touch.toBottom && status.jumpsUsed < 1) { status.jumpsUsed = 1; }

			// If you've spent more jumps than you have available, cannot jump again.
			if(status.jumpsUsed > stats.JumpMaxTimes) { return; }

			status.action = ActionMap.Jump;
			status.actionEnds = Systems.timer.frame + stats.JumpDuration + extraDuration;
			status.actionNum1 = (sbyte) (stats.JumpStrength + extraStrength);
			status.actionNum2 = (sbyte) (stats.JumpDuration + extraDuration - minimumDuration);
			status.actionBool1 = true; // TRUE if the Jump Key is down
			status.actionBool2 = character.input.isDown(IKey.XButton); // TRUE if the Run Key is down

			// Jump Sound
			Systems.sounds.jump.Play();
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
			FInt jumpStrength = status.actionNum1 * (status.actionBool1 ? (FInt) 1 : FInt.Create(0.6)) * (status.actionBool2 ? (FInt) 1 : stats.SlowSpeedMult);

			// Vertical Movement
			physics.velocity.Y = 0 - jumpStrength;
			
			// If the jump button has been released and the minimum duration has ended, end the jump:
			if(!status.actionBool1 && Systems.timer.frame > character.status.actionEnds - status.actionNum2) {
				this.EndAction(character);
				return;
			}
		}
	}
}
