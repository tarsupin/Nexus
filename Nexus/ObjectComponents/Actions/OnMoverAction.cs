using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {
	
	// Used when on a platform, conveyor, or other mover object.
	// This will maintain your momentum of the mover so that when you leave it, you will retain momentum.
	public class OnMoverAction : Action {

		public OnMoverAction() : base() {
			this.endsOnLanding = false;
		}

		public void StartAction( Character character ) {

			// If we're already in the OnMoverAction, ignore any repeat.
			if(character.status.action is OnMoverAction) { return; }

			this.EndLastActionIfActive(character);

			CharacterStatus status = character.status;
			status.action = ActionMap.OnMover;
		}

		public void TrackMoverMomentum(Character character) {
			GameObject objPhys = character.physics.touch.moveObj;
			if(objPhys == null) { return; }
			FVector objVel = objPhys.physics.velocity;
			character.status.actionNum1 = (sbyte)((float) objVel.X.ToDouble() * 0.8f);
			character.status.actionNum2 = (sbyte)((float) objVel.Y.ToDouble() * 0.8f);
		}

		public override void RunAction( Character character ) {

			// Track the character's momentum while on this mover:
			if(character.physics.touch.onMover) {
				this.TrackMoverMomentum(character);
			}

			// End the action if character is no longer on a mover (platform, conveyor, etc).
			else {
				this.EndAction(character);
			}
		}

		public override void EndAction(Character character) {

			// Transfer the mover's momentum to the character.
			character.physics.velocity.X += FInt.Create(character.status.actionNum1);
			character.physics.velocity.Y += FInt.Create(character.status.actionNum2);

			// Clear the Extra movement, because that will conflict with the existing momentum.
			character.physics.ClearExtraMovement();

			// Clear the Action
			character.status.action = null;
		}
	}
}
