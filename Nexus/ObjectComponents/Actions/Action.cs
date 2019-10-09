using Nexus.GameEngine;

namespace Nexus.ObjectComponents {

	public class Action {

		public byte duration;
		protected bool endsOnLanding;			// TRUE if the action will end when the actor lands.

		public Action() {
			this.duration = 0;
			this.endsOnLanding = false;
		}

		public virtual void RunAction( DynamicGameObject actor ) {}

		public virtual void LandsOnGround( DynamicGameObject actor ) {
			if(this.endsOnLanding) { this.EndAction( actor ); }
		}

		public virtual void EndAction( DynamicGameObject actor ) {
			
		}

	}
}
