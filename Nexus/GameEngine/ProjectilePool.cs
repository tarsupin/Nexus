using Nexus.Engine;
using Nexus.Objects;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public static class ProjectilePool {

		// Projectiles, Standard
		public static ObjectPool<ProjectileBall> ProjectileBall = new ObjectPool<ProjectileBall>(() => new ProjectileBall());
		public static ObjectPool<ProjectileBolt> ProjectileBolt = new ObjectPool<ProjectileBolt>(() => new ProjectileBolt());
		public static ObjectPool<ProjectileBullet> ProjectileBullet = new ObjectPool<ProjectileBullet>(() => new ProjectileBullet());
		public static ObjectPool<ProjectileEarth> ProjectileEarth = new ObjectPool<ProjectileEarth>(() => new ProjectileEarth());
		public static ObjectPool<ProjectileMagi> ProjectileMagi = new ObjectPool<ProjectileMagi>(() => new ProjectileMagi());

		//// Projectiles, Weapons
		//public static ObjectPool<AxeProjectile> WeaponAxe = new ObjectPool<AxeProjectile>(() => new AxeProjectile());
		//public static ObjectPool<ChakramProjectile> WeaponChakram = new ObjectPool<ChakramProjectile>(() => new ChakramProjectile());
		//public static ObjectPool<DaggerProjectile> WeaponDagger = new ObjectPool<DaggerProjectile>(() => new DaggerProjectile());
		//public static ObjectPool<GloveProjectile> WeaponGlove = new ObjectPool<GloveProjectile>(() => new GloveProjectile());
		//public static ObjectPool<GrenadeProjectile> WeaponGrenade = new ObjectPool<GrenadeProjectile>(() => new GrenadeProjectile());
		//public static ObjectPool<HammerProjectile> WeaponHammer = new ObjectPool<HammerProjectile>(() => new HammerProjectile());
		//public static ObjectPool<ShurikenProjectile> WeaponShuriken = new ObjectPool<ShurikenProjectile>(() => new ShurikenProjectile());
		//public static ObjectPool<SpearProjectile> WeaponSpear = new ObjectPool<SpearProjectile>(() => new SpearProjectile());
		//public static ObjectPool<SwordProjectile> WeaponSword = new ObjectPool<SwordProjectile>(() => new SwordProjectile());

		// Projectiles, Standard
		//public static Stack<ProjectileBolt> ProjectileBoltPool = new Stack<ProjectileBolt>();
		//public static Stack<ProjectileBullet> ProjectileBulletPool = new Stack<ProjectileBullet>();
		//public static Stack<ProjectileEarth> ProjectileEarthPool = new Stack<ProjectileEarth>();
		//public static Stack<ProjectileMagi> ProjectileMagiPool = new Stack<ProjectileMagi>();

		// Projectiles, Weapons
		public static Stack<AxeProjectile> WeaponAxePool = new Stack<AxeProjectile>();
		public static Stack<ChakramProjectile> WeaponChakramPool = new Stack<ChakramProjectile>();
		public static Stack<DaggerProjectile> WeaponDaggerPool = new Stack<DaggerProjectile>();
		public static Stack<GloveProjectile> WeaponGlovePool = new Stack<GloveProjectile>();
		public static Stack<GrenadeProjectile> WeaponGrenadePool = new Stack<GrenadeProjectile>();
		public static Stack<HammerProjectile> WeaponHammerPool = new Stack<HammerProjectile>();
		public static Stack<ShurikenProjectile> WeaponShurikenPool = new Stack<ShurikenProjectile>();
		public static Stack<SpearProjectile> WeaponSpearPool = new Stack<SpearProjectile>();
		public static Stack<SwordProjectile> WeaponSwordPool = new Stack<SwordProjectile>();
	}
}
