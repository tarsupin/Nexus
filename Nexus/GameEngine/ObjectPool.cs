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
		public static Stack<WeaponAxe> WeaponAxe = new Stack<WeaponAxe>();
		public static Stack<WeaponChakram> WeaponChakram = new Stack<WeaponChakram>();
		public static Stack<WeaponDagger> WeaponDagger = new Stack<WeaponDagger>();
		public static Stack<WeaponGlove> WeaponGlove = new Stack<WeaponGlove>();
		public static Stack<WeaponGrenade> WeaponGrenade = new Stack<WeaponGrenade>();
		public static Stack<WeaponHammer> WeaponHammer = new Stack<WeaponHammer>();
		public static Stack<WeaponShuriken> WeaponShuriken = new Stack<WeaponShuriken>();
		public static Stack<WeaponSpear> WeaponSpear = new Stack<WeaponSpear>();
		public static Stack<WeaponSword> WeaponSword = new Stack<WeaponSword>();
	}
}
