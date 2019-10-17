
using Microsoft.Xna.Framework;

namespace Nexus.Engine {

	public class ParticleSimple {

		public Vector2 pos;
		public Vector2 vel;

		public void SetParticle( Vector2 pos, Vector2 vel ) {
			this.pos = pos;
			this.vel = vel;
		}

		public void ReturnParticleToPool() {
			ParticleManager.simpleParticles.ReturnObject(this);
		}

		public void RunParticleTick() {
			this.pos += this.vel;
		}
	}
}
