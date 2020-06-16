using Nexus.GameEngine;
using System.Collections.Generic;

// This Handler tracks any Emitters or Free Particles in the scene, and will draw them accordingly.

namespace Nexus.Engine {

	public class ParticleHandler {

		// Particle Pools
		public static ObjectPool<ParticleStandard> standardPool = new ObjectPool<ParticleStandard>(() => new ParticleStandard());

		// Handler Values
		private readonly RoomScene room;
		private List<IEmitter> emitterList;			// Current Emitters
		private List<ParticleFree> freeParticles;	// Free Particles (not attached to emitters)

		public ParticleHandler( RoomScene room ) {
			this.room = room;
			this.emitterList = new List<IEmitter>();
			this.freeParticles = new List<ParticleFree>();
		}

		public void AddEmitter(IEmitter emitter) {
			this.emitterList.Add(emitter);
		}

		public void AddParticle(ParticleFree particle) {
			this.freeParticles.Add(particle);
		}

		public void RunParticleTick() {

			// Loop through all the emitters and update accordingly:
			for( int i = 0; i < this.emitterList.Count; i++ ) {
				IEmitter emitter = this.emitterList[i];

				// Remove the emitter when it's time has expired.
				// NOTE: The Emitter removes itself from the pool on this frame in RunEmitterTick();
				if(emitter.HasExpired) {
					this.emitterList.Remove(emitter);
					i--;
				}

				emitter.RunEmitterTick();
			}

			// Loop through all free particles (ones that aren't attached to emitters)
			for( int i = 0; i < this.freeParticles.Count; i++ ) {
				ParticleFree particle = this.freeParticles[i];

				// Remove Free Particles as applicable:
				if(particle.HasExpired) {
					this.freeParticles.Remove(particle);
					i--;
				}

				particle.RunParticleTick();
			}
		}

		public void Draw() {

			Camera camera = Systems.camera;
			int camX = camera.posX;
			int camY = camera.posY;

			// Loop through all the emitters and render:
			foreach(IEmitter emitter in this.emitterList) {
				if(!emitter.IsOnScreen(camera)) { continue; }
				emitter.Draw(camX, camY); // Emitter's draw method checks if it's within camera view.
			}

			// Loop through all free particles and render:
			foreach(ParticleFree particle in this.freeParticles) {
				if(!particle.IsOnScreen(camera)) { continue; }
				particle.Draw(camX, camY);
			}
		}

		// Helper for Fading Particles
		public static float AlphaByFadeTime(int currentFrame, int fadeStart, int fadeEnd, float alphaStart = 1, float alphaEnd = 0) {
			float duration = fadeEnd - fadeStart;
			float dist = currentFrame - fadeStart;
			float weight = dist / duration;
			return Interpolation.Number(alphaStart, alphaEnd, weight);
		}
	}
}
