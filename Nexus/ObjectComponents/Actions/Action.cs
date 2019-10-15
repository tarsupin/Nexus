using Nexus.GameEngine;

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

		public virtual void LandsOnGround( DynamicGameObject actor ) {
			if(this.endsOnLanding) { this.EndAction( actor ); }
		}

		public virtual void EndAction( DynamicGameObject actor ) {
			
		}
	}
}
