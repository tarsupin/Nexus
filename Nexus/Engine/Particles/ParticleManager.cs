
namespace Nexus.Engine {

	public class ParticleManager {

		// Implement Particle Pools
		public static ObjectPool<ParticleSimple> simpleParticles = new ObjectPool<ParticleSimple>(() => new ParticleSimple());
		public static ObjectPool<ParticleStandard> standardParticles = new ObjectPool<ParticleStandard>(() => new ParticleStandard());

		// Helper for Fading Particles
		public static float AlphaByFadeTime(uint currentFrame, uint fadeStart, uint fadeEnd, float alphaStart = 1, float alphaEnd = 0) {
			float duration = fadeEnd - fadeStart;
			float dist = currentFrame - fadeStart;
			float weight = dist / duration;
			return Interpolation.Number(alphaStart, alphaEnd, weight);
		}
	}
}
