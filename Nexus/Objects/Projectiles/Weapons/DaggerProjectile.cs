using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class DaggerProjectile : ThrustProjectile {

		private DaggerProjectile(RoomScene room, byte subType, FVector pos, FVector endPos) : base(room, subType, pos, endPos) {
			this.Damage = DamageStrength.Major;
			this.CollisionType = ProjectileCollisionType.IgnoreWalls;
			this.cycleDuration = 16;
		}

		public static DaggerProjectile Create(RoomScene room, byte subType, FVector pos, FVector endPos) {
			DaggerProjectile projectile;

			// Retrieve a Projectile from the ObjectPool, if one is available:
			if(ProjectilePool.WeaponDagger.Count > 0) {
				projectile = ProjectilePool.WeaponDagger.Pop();
				projectile.ResetProjectile(subType, pos, FVector.Create(0, 0));
			}

			// Create a New Projectile
			else {
				projectile = new DaggerProjectile(room, subType, pos, endPos);
			}

			projectile.ResetThrustProjectile();
			projectile.SetSpriteName("Weapon/Dagger");
			projectile.AssignBoundsByAtlas(2, 2, -2, -2);
			projectile.rotation = projectile.endPos.X > projectile.posX ? 0 : Radians.Rotate180;

			// Add the Projectile to Scene
			room.AddToScene(projectile, false);

			return projectile;
		}
	}
}
