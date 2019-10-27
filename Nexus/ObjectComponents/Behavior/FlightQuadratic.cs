using Newtonsoft.Json.Linq;
using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.ObjectComponents {

	public class FlightQuadratic : FlightBehavior {

		Physics physics;        // Reference to the actor's physics.

		protected int midX;                 // Middle X position of a motion, such as for curves (quadratic motion)
		protected int midY;                 // Middle Y position of a motion, such as for curves (quadratic motion).

		public FlightQuadratic(DynamicGameObject actor, JObject paramList) : base(actor, paramList) {
			this.physics = actor.physics;
			this.midX = this.startX + (paramList["midX"] != null ? paramList["midX"].Value<int>() * (byte) TilemapEnum.TileWidth : 0);
			this.midY = this.startY + (paramList["midY"] != null ? paramList["midY"].Value<int>() * (byte) TilemapEnum.TileHeight : 0);
		}

		// TODO OPTIMIZE: This section has a huge opportunity for optimization, since we're currently using QuadBezierEaseBothDir() twice.
		public override void RunTick() {

			// Identify Position based on Global Timing
			float weight = (Systems.timer.Frame + this.offset) % this.duration;
			weight /= this.duration;

			// Assign Next Velocity
			float posX = Interpolation.QuadBezierEaseBothDir(this.startX, this.midX, this.endX, weight);
			float posY = Interpolation.QuadBezierEaseBothDir(this.startY, this.midY, this.endY, weight);

			this.physics.velocity.X = FInt.Create(posX - this.actor.posX);
			this.physics.velocity.Y = FInt.Create(posY - this.actor.posY);
		}
	}
}
