using Nexus.Engine;
using Nexus.Objects;

namespace Nexus.GameEngine {

	public static class ProjectilePool {

		// Projectiles, Standard
		public static ObjectPool<ProjectileBall> ProjectileBall = new ObjectPool<ProjectileBall>(() => new ProjectileBall());
		public static ObjectPool<ProjectileBolt> ProjectileBolt = new ObjectPool<ProjectileBolt>(() => new ProjectileBolt());
		public static ObjectPool<ProjectileBullet> ProjectileBullet = new ObjectPool<ProjectileBullet>(() => new ProjectileBullet());
		public static ObjectPool<ProjectileEarth> ProjectileEarth = new ObjectPool<ProjectileEarth>(() => new ProjectileEarth());

		// Projectiles, Weapons
		public static ObjectPool<AxeProjectile> AxeProjectile = new ObjectPool<AxeProjectile>(() => new AxeProjectile());
		public static ObjectPool<ChakramProjectile> ChakramProjectile = new ObjectPool<ChakramProjectile>(() => new ChakramProjectile());
		public static ObjectPool<DaggerProjectile> DaggerProjectile = new ObjectPool<DaggerProjectile>(() => new DaggerProjectile());
		public static ObjectPool<GloveProjectile> GloveProjectile = new ObjectPool<GloveProjectile>(() => new GloveProjectile());
		public static ObjectPool<GrenadeProjectile> GrenadeProjectile = new ObjectPool<GrenadeProjectile>(() => new GrenadeProjectile());
		public static ObjectPool<HammerProjectile> HammerProjectile = new ObjectPool<HammerProjectile>(() => new HammerProjectile());
		public static ObjectPool<ShurikenProjectile> ShurikenProjectile = new ObjectPool<ShurikenProjectile>(() => new ShurikenProjectile());
		public static ObjectPool<SpearProjectile> SpearProjectile = new ObjectPool<SpearProjectile>(() => new SpearProjectile());
		public static ObjectPool<SwordProjectile> SwordProjectile = new ObjectPool<SwordProjectile>(() => new SwordProjectile());
	}
}
