using Microsoft.Xna.Framework;
using Nexus.Engine;

namespace Nexus.GameEngine {

	public class StationaryParticle : ParticleFree {

		public static ObjectPool<StationaryParticle> stationPool = new ObjectPool<StationaryParticle>(() => new StationaryParticle());

		public static StationaryParticle SetParticle(RoomScene room, Atlas atlas, string spriteName, Vector2 pos, int frameEnd) {

			// Retrieve an available particle from the pool.
			StationaryParticle particle = StationaryParticle.stationPool.GetObject();

			particle.atlas = atlas;
			particle.spriteName = spriteName;
			particle.pos = pos;
			particle.frameEnd = frameEnd;

			// Add the Particle to the Particle Handler
			room.particleHandler.AddParticle(particle);

			return particle;
		}

		public override void RunParticleTick() {
			if(this.HasExpired) { StationaryParticle.stationPool.ReturnObject(this); }
		}

		public override void Draw(int camX, int camY) {
			this.atlas.Draw(this.spriteName, (int)this.pos.X - camX, (int)this.pos.Y - camY);
		}
	}
}
