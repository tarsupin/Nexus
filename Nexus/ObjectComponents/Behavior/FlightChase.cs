using Nexus.GameEngine;
using System.Collections.Generic;

namespace Nexus.ObjectComponents {

	public class FlightChase : FlightBehavior {

		Physics physics;		// Reference to the actor's physics.

		public FlightChase(GameObject actor, Dictionary<string, short> paramList) : base(actor, paramList) {
			this.physics = actor.physics;

			// TODO: MUST ADD THIS
			// TODO: MUST ADD THIS
			// TODO: MUST ADD THIS
			// TODO: MUST ADD THIS
		}

		public override void RunTick() {

			// TODO: Once in a while (every X frames), needs to check for character presence.
		}
	}
}
