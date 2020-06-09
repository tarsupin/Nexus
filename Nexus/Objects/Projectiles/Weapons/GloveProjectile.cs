using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public enum WeaponGloveSubType : byte {
		Red,
		White
	}

	public class GloveProjectile : ThrustProjectile {

		private GloveProjectile(RoomScene room, byte subType, FVector pos, FVector endPos) : base(room, subType, pos, endPos) {
			this.Damage = DamageStrength.Standard;
			this.CollisionType = ProjectileCollisionType.BreakObjects;
			this.cycleDuration = 24;
		}

		public static GloveProjectile Create(RoomScene room, byte subType, FVector pos, FVector endPos) {
			GloveProjectile projectile;

			// Retrieve a Projectile from the ObjectPool, if one is available:
			if(ProjectilePool.WeaponGlove.Count > 0) {
				projectile = ProjectilePool.WeaponGlove.Pop();
				projectile.ResetProjectile(subType, pos, FVector.Create(0, 0));
			}

			// Create a New Projectile
			else {
				projectile = new GloveProjectile(room, subType, pos, endPos);
			}

			projectile.ResetThrustProjectile();
			projectile.AssignSubType(subType);
			projectile.AssignBoundsByAtlas(5, 5, -5, -5); // Reduce Bounds (otherwise it appears to hit too much, too quickly)
			projectile.rotation = projectile.endPos.X > projectile.posX ? 0 : Radians.Rotate180;

			// Add the Projectile to Scene
			room.AddToScene(projectile, false);

			return projectile;
		}

		private void AssignSubType(byte subType) {
			if(subType == (byte) WeaponGloveSubType.Red) {
				this.SetSpriteName("Weapon/BoxingRed");
			} else if(subType == (byte) WeaponGloveSubType.White) {
				this.SetSpriteName("Weapon/BoxingWhite");
			}
		}
	}
}
