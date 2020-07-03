using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// status.actionNum1 (directionHor)		:: the Horizontal Direction of the Air Burst (e.g. -1, 0, or 1)
	// status.actionNum2 (directionVert)	:: the Vertical Direction of the Air Burst (e.g. -1, 0, or 1)

	public class AirBurst : Action {

		public AirBurst() : base() {
			this.duration = 7;
			this.endsOnLanding = true;
		}

		public void StartAction( Character character, sbyte directionHor, sbyte directionVert, bool endMomentum = false, sbyte extraDuration = 0 ) {
			this.EndLastActionIfActive(character);

			CharacterStatus status = character.status;

			status.action = ActionMap.AirBurst;
			status.actionNum1 = directionHor;
			status.actionNum2 = directionVert;
			status.actionEnds = Systems.timer.Frame + this.duration + extraDuration;

			// Horizontal Movements have a longer duration, since we're trying to ignore gravity for that duration.
			if(directionVert == 0) {
				status.actionEnds += 8;
			}

			// End Momentum if instructed to (such as for Puff Blocks)
			if(endMomentum) {
				character.physics.velocity.X = FInt.Create(0);
				character.physics.velocity.Y = FInt.Create(0);
			}

			// If we're doing a horizontal burst, set the character so that they have no Y Momentum:
			else if(directionVert == 0) {
				character.physics.velocity.Y = FInt.Create(0);
			}

			character.status.actionNum1 = (directionVert != 0 ? 16 : 19) * directionHor;
			character.status.actionNum2 = (directionHor != 0 ? 14 : 18) * directionVert;

			// Update Physics
			character.physics.touch.ResetTouch();
			character.physics.SetGravity(FInt.Create(0));
		}

		public override void RunAction( Character character ) {

			// End the action after the designated number of frames has elapsed:
			if(Systems.timer.Frame > character.status.actionEnds) {
				this.EndAction( character );
				return;
			}

			Physics physics = character.physics;
			physics.velocity.X = FInt.Create(character.status.actionNum1);
			physics.velocity.Y = FInt.Create(character.status.actionNum2);
		}

		public override void EndAction(Character character) {
			base.EndAction(character);
			character.physics.SetGravity(character.stats.BaseGravity);
		}
	}
}
