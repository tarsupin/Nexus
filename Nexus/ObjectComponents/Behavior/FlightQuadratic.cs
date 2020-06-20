using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.ObjectComponents {

	public class FlightQuadratic : FlightBehavior {

		private int midX;		// Middle X position of a motion, such as for curves (quadratic motion)
		private int midY;		// Middle Y position of a motion, such as for curves (quadratic motion).

		public FlightQuadratic(GameObject actor, Dictionary<string, short> paramList) : base(actor, paramList) {
			this.midX = this.startX + (paramList.ContainsKey("midX") ? paramList["midX"] * (byte) TilemapEnum.TileWidth : 0);
			this.midY = this.startY + (paramList.ContainsKey("midY") ? paramList["midY"] * (byte) TilemapEnum.TileHeight : 0);
		}

		// TODO OPTIMIZE: This section has a huge opportunity for optimization, since we're currently using QuadBezierEaseBothDir() twice.
		public override void RunTick() {

			// Identify Position based on Global Timing
			float weight = (float) ((float)(Systems.timer.Frame + this.offset) % this.duration) / (float)this.duration;

			// Assign Next Velocity
			float posX = Interpolation.QuadBezierEaseBothDir(this.startX, this.midX, this.endX, (float)weight);
			float posY = Interpolation.QuadBezierEaseBothDir(this.startY, this.midY, this.endY, (float)weight);

			if(this.cluster is Cluster) {
				posX += this.cluster.actor.posX - this.cluster.startX;
				posY += this.cluster.actor.posY - this.cluster.startY;
			}

			this.physics.velocity.X = FInt.Create(posX - this.actor.posX);
			this.physics.velocity.Y = FInt.Create(posY - this.actor.posY);
		}
	}
}
