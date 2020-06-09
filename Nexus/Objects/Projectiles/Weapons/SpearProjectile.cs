using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class SpearProjectile : ThrustProjectile {

		private SpearProjectile(RoomScene room, byte subType, FVector pos, FVector endPos, uint startFrame, uint endFrame) : base(room, subType, pos, endPos, startFrame, endFrame) {
			this.Damage = DamageStrength.Major;
			this.CollisionType = ProjectileCollisionType.IgnoreWalls;
		}

		public static SpearProjectile Create(RoomScene room, byte subType, FVector pos, FVector endPos, uint startFrame, uint endFrame) {
			SpearProjectile projectile;

			// Retrieve a Projectile from the ObjectPool, if one is available:
			if(ProjectilePool.WeaponSpear.Count > 0) {
				projectile = ProjectilePool.WeaponSpear.Pop();
				projectile.ResetProjectile(subType, pos, FVector.Create(0, 0));
				projectile.ResetThrustProjectile(startFrame, endFrame);
			}

			// Create a New Projectile
			else {
				projectile = new SpearProjectile(room, subType, pos, endPos, startFrame, endFrame);
			}

			projectile.SetSpriteName("Weapon/Spear");
			projectile.AssignBoundsByAtlas(2, 2, -2, -2);
			projectile.rotation = projectile.endPos.X > projectile.posX ? 0 : Radians.Rotate180;

			// Add the Projectile to Scene
			room.AddToScene(projectile, false);

			return projectile;
		}
	}
}
