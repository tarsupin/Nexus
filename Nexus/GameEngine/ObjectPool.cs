using Nexus.Objects;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public static class ObjectPool {

		// Projectiles, Standard
		public static Stack<ProjectileBall> ProjectileBall = new Stack<ProjectileBall>();
		public static Stack<ProjectileBolt> ProjectileBolt = new Stack<ProjectileBolt>();
		public static Stack<ProjectileBullet> ProjectileBullet = new Stack<ProjectileBullet>();
		public static Stack<ProjectileEarth> ProjectileEarth = new Stack<ProjectileEarth>();
		public static Stack<ProjectileMagi> ProjectileMagi = new Stack<ProjectileMagi>();

		// Projectiles, Weapons
		public static Stack<AxeProjectile> WeaponAxe = new Stack<AxeProjectile>();
		public static Stack<ChakramProjectile> WeaponChakram = new Stack<ChakramProjectile>();
		public static Stack<DaggerProjectile> WeaponDagger = new Stack<DaggerProjectile>();
		public static Stack<GloveProjectile> WeaponGlove = new Stack<GloveProjectile>();
		public static Stack<GrenadeProjectile> WeaponGrenade = new Stack<GrenadeProjectile>();
		public static Stack<HammerProjectile> WeaponHammer = new Stack<HammerProjectile>();
		public static Stack<ShurikenProjectile> WeaponShuriken = new Stack<ShurikenProjectile>();
		public static Stack<SpearProjectile> WeaponSpear = new Stack<SpearProjectile>();
		public static Stack<SwordProjectile> WeaponSword = new Stack<SwordProjectile>();
	}
}
