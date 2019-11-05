using Nexus.Engine;
using Nexus.Objects;

// To start an action, use the "StartAction()" methods through the ActionMap.
// Example: ActionMap.Slide.StartAction(this, this.faceRight);

namespace Nexus.ObjectComponents {

	public class Action {

		protected byte duration;
		protected bool endsOnLanding;			// TRUE if the action will end when the actor lands.

		public Action() {
			this.duration = 0;
			this.endsOnLanding = false;
		}

		public bool HasTimeElapsed(Character character) {
			return Systems.timer.Frame > character.status.actionEnds;
		}

		public virtual void RunAction(Character character) { }

		public virtual void LandsOnGround(Character character) {
			if(this.endsOnLanding) { this.EndAction(character); }
		}

		public virtual void EndAction(Character character) {
			character.status.action = null;
		}
	}
}
