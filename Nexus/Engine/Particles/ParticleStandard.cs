using Microsoft.Xna.Framework;

namespace Nexus.Engine {

	public class ParticleStandard {

		public Vector2 pos;
		public Vector2 vel;
		public float rotation;
		public float rotationSpeed;

		public void SetParticle( Vector2 pos, Vector2 vel, float rotation = 0, float rotationSpeed = 0 ) {
			this.pos = pos;
			this.vel = vel;
			this.rotation = rotation;
			this.rotationSpeed = rotationSpeed;
		}

		public static ParticleStandard GetParticleFromPool() {
			return ParticleHandler.standardPool.GetObject();
		}

		public void ReturnParticleToPool() {
			ParticleHandler.standardPool.ReturnObject(this);
		}

		public void RunParticleTick() {
			this.pos += this.vel;
			this.rotation += this.rotationSpeed;
		}
	}
}
