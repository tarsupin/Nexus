using Microsoft.Xna.Framework;
using Nexus.GameEngine;
using System.Collections.Generic;

namespace Nexus.Engine {

	public class EmitterSimple : IEmitter {

		// Implement Emitter Pool
		public static ObjectPool<EmitterSimple> emitterPool = new ObjectPool<EmitterSimple>(() => new EmitterSimple());

		// Particles attached to the emitter.
		public List<ParticleStandard> particles;

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
		
		public bool HasExpired { get { return this.frameEnd < Systems.timer.Frame; } }

		public static EmitterSimple NewEmitter( RoomScene room, Atlas atlas, string spriteName, Vector2 pos, Vector2 vel, float gravity, uint frameEnd, uint fadeStart = 0, float alphaStart = 1, float alphaEnd = 0 ) {

			// Retrieve an emitter from the pool.
			EmitterSimple emitter = EmitterSimple.emitterPool.GetObject();

			emitter.atlas = atlas;
			emitter.spriteName = spriteName;
			emitter.gravity = gravity;
			emitter.pos = pos;
			emitter.vel = vel;
			emitter.frameEnd = frameEnd;
			emitter.fadeStart = fadeStart == 0 ? frameEnd + 1 : fadeStart;
			emitter.alphaStart = alphaStart;
			emitter.alphaEnd = alphaEnd;

			emitter.particles = new List<ParticleStandard>();

			// Add the Emitter to the Particle Handler
			room.particleHandler.AddEmitter(emitter);

			return emitter;
		}

		public void AddParticle( Vector2 pos, Vector2 vel, float rotation = 0, float rotationSpeed = 0 ) {
			ParticleStandard particle = ParticleHandler.standardPool.GetObject();
			particle.SetParticle(pos, vel, rotation, rotationSpeed);
			this.particles.Add(particle);
		}

		public void ReturnEmitter() {

			// Loop through particles and return them to their pool.
			foreach(ParticleStandard particle in this.particles) {
				particle.ReturnParticleToPool();
			}

			// Clear all instances of the particle references.
			this.particles.Clear();

			// Return the Emitter to it's pool.
			EmitterSimple.emitterPool.ReturnObject(this);
		}

		public void RunEmitterTick() {

			// End the Emitter once it's duration has ended.
			if(this.HasExpired) {
				this.ReturnEmitter();
				return;
			}

			this.pos += vel;

			// Loop through particles. Update them relative to the Emitter's position.
			foreach(ParticleStandard particle in this.particles) {
				particle.vel.Y += gravity; // Apply Gravity
				particle.RunParticleTick();
			}
		}

		public bool IsOnScreen(Camera camera) {

			// Only draw the emitter if it's on the camera.
			if(this.pos.X < camera.posX - 250 || this.pos.X > camera.posX + camera.width + 250 || this.pos.Y < camera.posY - 250 || this.pos.Y > camera.posY + camera.height + 250) {
				return false;
			}

			return true;
		}

		public void Draw(int camX, int camY) {

			// Determine Alpha of Particle (can be affected by fading)
			uint frame = Systems.timer.Frame;
			float alpha = this.fadeStart < frame ? ParticleHandler.AlphaByFadeTime(frame, this.fadeStart, this.frameEnd, this.alphaStart, this.alphaEnd) : 1;

			// Loop Through Particles, Draw:
			foreach(ParticleStandard particle in this.particles) {
				this.atlas.DrawAdvanced(this.spriteName, (int)particle.pos.X - camX, (int)particle.pos.Y - camY, Color.White * alpha, particle.rotation);
			}
		}
	}
}
