using Nexus.GameEngine;

// Behaviors perform additional updates that relate to the actor, such as watching for a particular action.
// actor.behavior.RunTick();

namespace Nexus.ObjectComponents {

	public class Behavior {

		protected DynamicGameObject actor;

		public Behavior( DynamicGameObject actor ) {
			this.actor = actor;
		}
	}
}
