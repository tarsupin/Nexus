using Nexus.Engine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// To start an action, use the "StartAction()" methods through the ActionMap.
	// Example: ActionMap.Slide.StartAction(this, this.faceRight);
	public class ActionCharacter : Action {

		public bool HasTimeElapsed( Character character ) {
			return Systems.timer.frame > character.status.actionEnds;
		}

		public virtual void RunAction( Character character ) {}

		public virtual void LandsOnGround( Character character ) {
			if(this.endsOnLanding) { this.EndAction( character ); }
		}

		public virtual void EndAction( Character character ) {
			character.status.action = null;
		}
	}
}
