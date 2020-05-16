using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System;
using System.Collections.Generic;

namespace Nexus.ObjectComponents {

	public class FlightCircle : FlightBehavior {

		Physics physics;		// Reference to the actor's physics.
		float radius;			// The radius of the flight circle.

		public FlightCircle(DynamicObject actor, Dictionary<string, short> paramList) : base(actor, paramList) {
			this.physics = actor.physics;
			byte diameter = paramList.ContainsKey("diameter") ? (byte) paramList["diameter"] : (byte) 3;
			this.radius = (float) Math.Round((double) diameter / 2, 1) * (byte) TilemapEnum.TileWidth;
		}

		// TODO OPTIMIZE: This section has a huge opportunity for optimization, since we're currently using large calculations twice.
		public override void RunTick() {

			// Identify Position based on Global Timing
			float weight = (Systems.timer.Frame + this.offset) % this.duration;
			weight = weight / this.duration * 17;

			// Clockwise or Counter-Clockwise
			if(this.reverse) { weight = -weight; }

			// Set Position
			float posX = Circle.GetEdgePointByRadianX(this.startX, this.radius, (float) (weight * Math.PI * 2));
			float posY = Circle.GetEdgePointByRadianY(this.startY, this.radius, (float) (weight * Math.PI * 2));

			this.physics.velocity.X = FInt.Create(posX - this.actor.posX);
			this.physics.velocity.Y = FInt.Create(posY - this.actor.posY);
		}
	}
}
