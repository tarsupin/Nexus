using Nexus.GameEngine;
using System.Collections.Generic;

// This Handler tracks any Emitters or Loose Particles in the scene, and will draw them accordingly.

namespace Nexus.Engine {

	public class ParticleHandler {

		private readonly RoomScene room;
		private List<EmitterSimple> emitterList;

		public ParticleHandler( RoomScene room ) {
			this.room = room;
			this.emitterList = new List<EmitterSimple>();
		}

		public void AddEmitter(EmitterSimple emitter) {
			emitterList.Add(emitter);
		}

		public void RunParticleTick() {

			// Loop through all the emitters and update accordingly:
			for( int i = 0; i < this.emitterList.Count; i++ ) {
				EmitterSimple emitter = this.emitterList[i];

				// Remove the emitter when it's time has expired.
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
	}
}
