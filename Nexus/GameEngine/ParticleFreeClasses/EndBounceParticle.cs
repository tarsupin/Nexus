using Microsoft.Xna.Framework;
using Nexus.Engine;

namespace Nexus.GameEngine {

	public class EndBounceParticle : ParticleFree {

		public static ObjectPool<EndBounceParticle> endBouncePool = new ObjectPool<EndBounceParticle>(() => new EndBounceParticle());

		public static EndBounceParticle SetParticle(RoomScene room, Atlas atlas, string spriteName, Vector2 pos, int frameEnd, float bounceHeight = 3, float gravity = 0.5f, float rotateSpeed = 0.12f) {

			// Retrieve an available particle from the pool.
			EndBounceParticle particle = EndBounceParticle.endBouncePool.GetObject();

			particle.atlas = atlas;
			particle.spriteName = spriteName;
			particle.pos = pos;
			particle.frameEnd = frameEnd;
			particle.gravity = gravity;
			particle.vel = new Vector2(CalcRandom.FloatBetween(-3, 3), CalcRandom.FloatBetween(-bounceHeight - 1.5f, -bounceHeight + 1.5f));
			particle.rotationSpeed = CalcRandom.FloatBetween(-rotateSpeed, rotateSpeed);

			// Add the Particle to the Particle Handler
			room.particleHandler.AddParticle(particle);

			return particle;
		}

		public override void RunParticleTick() {
			this.vel.Y += this.gravity;
			this.pos += this.vel;
			this.rotation += this.rotationSpeed;
			if(this.HasExpired) { EndBounceParticle.endBouncePool.ReturnObject(this); }
		}

		public override void Draw(int camX, int camY) {
			this.atlas.DrawAdvanced(this.spriteName, (int)this.pos.X - camX, (int)this.pos.Y - camY, null, this.rotation);
		}
	}
}
