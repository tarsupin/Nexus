using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public enum WeaponAxeSubType : byte {
		Axe,
		Axe2,
		Axe3
	}

	public class AxeProjectile : Projectile {

		private AxeProjectile(RoomScene room, byte subType, FVector pos, FVector velocity) : base(room, subType, pos, velocity) {
			this.Damage = DamageStrength.Lethal;
			this.physics.SetGravity(FInt.Create(0.45));
			this.CollisionType = ProjectileCollisionType.IgnoreWalls;
		}

		public static AxeProjectile Create(RoomScene room, byte subType, FVector pos, FVector velocity) {
			AxeProjectile projectile;

			// Retrieve a Projectile Ball from the ObjectPool, if one is available:
			if(ProjectilePool.WeaponAxe.Count > 0) {
				projectile = ProjectilePool.WeaponAxe.Pop();
				projectile.ResetProjectile(subType, pos, velocity);
			}

			// Create a New Projectile Ball
			else {
				projectile = new AxeProjectile(room, subType, pos, velocity);
			}

			projectile.AssignSubType(subType);
			projectile.AssignBoundsByAtlas(2, 2, -2, -2);

			// Add the Projectile to Scene
			room.AddToScene(projectile, false);

			return projectile;
		}

		private void AssignSubType(byte subType) {
			if(subType == (byte) WeaponAxeSubType.Axe) {
				this.SetSpriteName("Weapon/Axe");
			}

			else if(subType == (byte) WeaponAxeSubType.Axe2) {
				this.SetSpriteName("Weapon/Axe2");
			}

			else if(subType == (byte) WeaponAxeSubType.Axe3) {
				this.SetSpriteName("Weapon/Axe3");
			}
		}
	}
}
