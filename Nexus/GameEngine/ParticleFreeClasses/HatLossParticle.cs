using Microsoft.Xna.Framework;
using Nexus.Engine;

namespace Nexus.GameEngine {

	public class HatLossParticle : ParticleFree {

		public static ObjectPool<HatLossParticle> hatPool = new ObjectPool<HatLossParticle>(() => new HatLossParticle());

		public static HatLossParticle SetParticle(RoomScene room, Atlas atlas, string spriteName, Vector2 pos, uint frameEnd) {

			// Retrieve an available particle from the pool.
			HatLossParticle particle = HatLossParticle.hatPool.GetObject();

			particle.atlas = atlas;
			particle.spriteName = spriteName;
			particle.pos = pos;
			particle.frameEnd = frameEnd;
			particle.gravity = 0.5f;
			particle.vel = new Vector2(CalcRandom.IntBetween(-3, 3), CalcRandom.IntBetween(-6, -3));
			particle.rotationSpeed = CalcRandom.FloatBetween(-0.12f, 0.12f);

			// Add the Particle to the Particle Handler
			room.particleHandler.AddParticle(particle);

			return particle;
		}

		public override void RunParticleTick() {
			this.vel.Y += this.gravity;
			this.pos += this.vel;
			this.rotation += this.rotationSpeed;
			if(this.HasExpired) { HatLossParticle.hatPool.ReturnObject(this); }
		}

		public override void Draw(int camX, int camY) {
			this.atlas.DrawAdvanced(this.spriteName, (int)this.pos.X - camX, (int)this.pos.Y - camY, null, this.rotation);
		}
	}
}
