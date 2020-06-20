using Nexus.Engine;
using Nexus.GameEngine;
using System.Collections.Generic;

namespace Nexus.ObjectComponents {

	public class FlightAxis : FlightBehavior {

		Physics physics;		// Reference to the actor's physics.

		public FlightAxis(GameObject actor, Dictionary<string, short> paramList) : base(actor, paramList) {
			this.physics = actor.physics;
		}

		// TODO OPTIMIZE: This section has a huge opportunity for optimization, since we're currently using EaseBothDir() twice.
		public override void RunTick() {

			// Identify Position based on Global Timing
			float weight = (Systems.timer.Frame + this.offset) % this.duration;
			weight /= this.duration;

			// Assign Next Velocity
			float posX = Interpolation.EaseBothDir(this.startX, this.endX, weight) + (this.cluster is Cluster ? this.cluster.actor.posX : 0);
			float posY = Interpolation.EaseBothDir(this.startY, this.endY, weight) + (this.cluster is Cluster ? this.cluster.actor.posY : 0);

			this.physics.velocity.X = FInt.Create(posX - this.actor.posX);
			this.physics.velocity.Y = FInt.Create(posY - this.actor.posY);
		}
	}
}
