﻿using Nexus.Engine;
using Nexus.GameEngine;
using System.Collections.Generic;

namespace Nexus.ObjectComponents {

	public class FlightAxis : FlightBehavior {

		public FlightAxis(GameObject actor, Dictionary<string, short> paramList) : base(actor, paramList) {}

		// TODO OPTIMIZE: This section has a huge opportunity for optimization, since we're currently using EaseBothDir() twice.
		public override void RunTick() {

			// Identify Position based on Global Timing
			float weight = (Systems.timer.Frame + this.offset) % this.duration;
			weight /= this.duration;

			// Assign Next Velocity
			float posX = Interpolation.EaseBothDir(this.startX, this.endX, weight);
			float posY = Interpolation.EaseBothDir(this.startY, this.endY, weight);

			if(this.cluster is Cluster) {
				posX += this.cluster.actor.posX - this.cluster.startX;
				posY += this.cluster.actor.posY - this.cluster.startY;
			}

			this.physics.velocity.X = FInt.Create(posX - this.actor.posX);
			this.physics.velocity.Y = FInt.Create(posY - this.actor.posY);
		}
	}
}
