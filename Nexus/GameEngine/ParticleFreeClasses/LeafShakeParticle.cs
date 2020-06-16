using Microsoft.Xna.Framework;
using Nexus.Engine;

namespace Nexus.GameEngine {

	public class LeafShakeParticle : ParticleFree {

		public static ObjectPool<LeafShakeParticle> leafPool = new ObjectPool<LeafShakeParticle>(() => new LeafShakeParticle());

		public static LeafShakeParticle SetParticle(RoomScene room, Atlas atlas, string spriteName, Vector2 pos, int frameEnd) {

			// Retrieve an available particle from the pool.
			LeafShakeParticle particle = LeafShakeParticle.leafPool.GetObject();

			particle.atlas = atlas;
			particle.spriteName = spriteName;
			particle.pos = pos;
			particle.frameEnd = frameEnd;

			// Add the Particle to the Particle Handler
			room.particleHandler.AddParticle(particle);

			return particle;
		}

		public short ShakeOffset() {
			float weight = (float) ((this.frameEnd - Systems.timer.Frame) % 100) / 12f;
			return (short) Interpolation.EaseBothDir(-3f, 3f, weight);
		}

		public override void RunParticleTick() {
			if(this.HasExpired) { LeafShakeParticle.leafPool.ReturnObject(this); }
		}

		public override void Draw(int camX, int camY) {
			this.atlas.Draw(this.spriteName, (int)this.pos.X - camX + this.ShakeOffset(), (int)this.pos.Y - camY);
		}
	}
}
