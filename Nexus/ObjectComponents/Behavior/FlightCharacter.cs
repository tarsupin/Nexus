using Nexus.GameEngine;
using System.Collections.Generic;

namespace Nexus.ObjectComponents {

	public class FlightCharacter : FlightBehavior {

		Physics physics;		// Reference to the actor's physics.

		public FlightCharacter(DynamicObject actor, Dictionary<string, short> paramList) : base(actor, paramList) {
			this.physics = actor.physics;

			// TODO: MUST ADD THIS
			// TODO: MUST ADD THIS
			// TODO: MUST ADD THIS
			// TODO: MUST ADD THIS
		}

		public override void RunTick() {

		}
	}
}
