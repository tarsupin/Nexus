
namespace Nexus.Engine {

	public class ParticleManager {

		// Implement Particle Pools
		public static ObjectPool<ParticleSimple> simpleParticles = new ObjectPool<ParticleSimple>(() => new ParticleSimple());
		public static ObjectPool<ParticleStandard> standardParticles = new ObjectPool<ParticleStandard>(() => new ParticleStandard());
	}
}
