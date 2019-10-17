using Microsoft.Xna.Framework;
using Nexus.Engine;

namespace Nexus.GameEngine {

	public class SimpleEmitter : IEmitter {

		// Implement Emitter Pool
		public static ObjectPool<SimpleEmitter> emitterPool = new ObjectPool<SimpleEmitter>(() => new SimpleEmitter());

		// Particles attached to the emitter.
		public ParticleStandard[] particles;

		public Atlas atlas;         // Reference to the atlas used (for texturing particles).
		public string spriteName;	// Name of the sprite to draw all particles with.
		public float gravity;       // Gravity to apply to particles each tick.

		public Vector2 pos;			// Position of the Emitter
		public Vector2 vel;         // Velocity of the Emitter, if applicable.

		// Lifespan & Fading
		public uint frameEnd;		// The frame # that indicates the end of the emitter's life.
		public uint fadeStart;		// The frame # that indicates the emitter's particles should begin to fade.
		public float alphaStart;    // The amount of alpha to apply at max visibility (0 to 1). Typically 1.
		public float alphaEnd;		// The amount of alpha to apply at min visibility (0 to 1). Typically 0.

		public static SimpleEmitter NewEmitter( Atlas atlas, string spriteName, Vector2 pos, Vector2 vel, float gravity, uint frameEnd, uint fadeBegin = 0, float alphaStart = 1, float alphaEnd = 0 ) {

			// Retrieve an emitter from the pool.
			SimpleEmitter emitter = SimpleEmitter.emitterPool.GetObject();

			emitter.atlas = atlas;
			emitter.spriteName = spriteName;
			emitter.gravity = gravity;
			emitter.pos = pos;
			emitter.vel = vel;
			emitter.frameEnd = frameEnd;
			emitter.fadeStart = fadeBegin == 0 ? frameEnd : fadeBegin;
			emitter.alphaStart = alphaStart;
			emitter.alphaEnd = alphaEnd;

			return emitter;
		}

		public void AddParticle( Vector2 pos, Vector2 vel, float rotation = 0, float rotationSpeed = 0 ) {
			ParticleStandard particle = ParticleManager.standardParticles.GetObject();
			particle.SetParticle(pos, vel, rotation, rotationSpeed);
		}

		public void ReturnEmitter() {

			// Loop through particles and return them to their pool.
			foreach(ParticleStandard particle in this.particles) {
				particle.ReturnParticleToPool();
			}
		}

		public void RunEmitterTick() {
			
			// End the Emitter once it's duration has ended.
			// TODO HIGH PRIORITY: EMITTER TIME DURATION END
			// TODO HIGH PRIORITY: EMITTER TIME DURATION END
			// TODO HIGH PRIORITY: EMITTER TIME DURATION END
			// TODO HIGH PRIORITY: EMITTER TIME DURATION END
			// TODO HIGH PRIORITY: EMITTER TIME DURATION END
			// TODO HIGH PRIORITY: EMITTER TIME DURATION END
			// TODO HIGH PRIORITY: EMITTER TIME DURATION END

			this.pos += vel;

			// Loop through particles. Update them relative to the Emitter's position.
			foreach(ParticleStandard particle in this.particles) {
				particle.vel.Y += gravity; // Apply Gravity
				particle.RunParticleTick();
			}
		}

		public void Draw() {
			this.atlas.Draw(this.spriteName, 500, 300);
		}
	}
}
