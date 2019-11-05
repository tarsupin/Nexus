using Nexus.GameEngine;
using System.Collections.Generic;

// This Handler tracks any Emitters or Loose Particles in the scene, and will draw them accordingly.

namespace Nexus.Engine {

	public class ParticleHandler {

		// Particle Pools
		public static ObjectPool<ParticleStandard> standardPool = new ObjectPool<ParticleStandard>(() => new ParticleStandard());

		// Handler Values
		private readonly RoomScene room;
		private List<EmitterSimple> emitterList;			// Current Emitters

		public ParticleHandler( RoomScene room ) {
			this.room = room;
			this.emitterList = new List<EmitterSimple>();
		}

		public void AddEmitter(EmitterSimple emitter) {
			this.emitterList.Add(emitter);
		}

		public void RunParticleTick() {

			// Loop through all the emitters and update accordingly:
			for( int i = 0; i < this.emitterList.Count; i++ ) {
				EmitterSimple emitter = this.emitterList[i];

				// Remove the emitter when it's time has expired.
				// NOTE: The Emitter removes itself from the pool on this frame in RunEmitterTick();
				if(emitter.frameEnd < Systems.timer.Frame) {
					this.emitterList.Remove(emitter);
					i--;
				}

				emitter.RunEmitterTick();
			}
		}

		public void Draw() {
			
			// Loop through all the emitters and render:
			foreach(EmitterSimple emitter in this.emitterList) {
				emitter.Draw(); // Emitter's draw method checks if it's within camera view.
			}
		}

		// Helper for Fading Particles
		public static float AlphaByFadeTime(uint currentFrame, uint fadeStart, uint fadeEnd, float alphaStart = 1, float alphaEnd = 0) {
			float duration = fadeEnd - fadeStart;
			float dist = currentFrame - fadeStart;
			float weight = dist / duration;
			return Interpolation.Number(alphaStart, alphaEnd, weight);
		}
	}
}
