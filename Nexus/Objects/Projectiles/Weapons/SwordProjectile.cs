using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class SwordProjectile : ThrustProjectile {

		private SwordProjectile(RoomScene room, byte subType, FVector pos, FVector endPos) : base(room, subType, pos, endPos) {
			this.Damage = DamageStrength.Major;
			this.CollisionType = ProjectileCollisionType.IgnoreWalls;
			this.cycleDuration = 24;
		}

		public static SwordProjectile Create(RoomScene room, byte subType, FVector pos, FVector endPos) {
			SwordProjectile projectile;

			// Retrieve a Projectile from the ObjectPool, if one is available:
			if(ProjectilePool.WeaponSwordPool.Count > 0) {
				projectile = ProjectilePool.WeaponSwordPool.Pop();
				projectile.ResetProjectile(room, subType, pos, FVector.Create(0, 0));
			}

			// Create a New Projectile
			else {
				projectile = new SwordProjectile(room, subType, pos, endPos);
			}

			projectile.ResetThrustProjectile();
			projectile.SetSpriteName("Weapon/Sword");
			projectile.AssignBoundsByAtlas(2, 2, -2, -2);
			projectile.rotation = projectile.endPos.X > projectile.posX ? 0 : Radians.Rotate180;

			// Add the Projectile to Scene
			room.AddToScene(projectile, false);

			return projectile;
		}
	}
}
