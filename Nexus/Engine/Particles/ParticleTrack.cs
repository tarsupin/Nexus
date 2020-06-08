using Microsoft.Xna.Framework;

namespace Nexus.Engine {

	public class ParticleTrack : ParticleFree {

		public int[] posX;          // Tracks the X positions of the Emitter. Will re-loop around if life exceeds track size.
		public int[] posY;          // Tracks the Y positions of the Emitter. Will re-loop around if life exceeds track size.

		public void SetParticle(Atlas atlas, string spriteName, uint frameEnd, uint fadeStart = 0, float alphaStart = 1, float alphaEnd = 0, float rotation = 0, float rotationSpeed = 0, float gravity = 0) {
			this.posX = posX;
			this.posY = posY;
			this.rotation = rotation;
			this.rotationSpeed = rotationSpeed;
		}

		public override void RunParticleTick() {
			//this.pos += this.vel;
			this.rotation += this.rotationSpeed;
		}
	}
}
