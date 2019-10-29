using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public enum WeaponChakramSubType : byte {
		Chakram
	}

	public class WeaponChakram : Projectile {

		private WeaponChakram(LevelScene scene, byte subType, FVector pos, FVector velocity) : base(scene, subType, pos, velocity) {
			this.Damage = DamageStrength.Major;
			this.CollisionType = ProjectileCollisionType.IgnoreWalls;
		}

		public static WeaponChakram Create(LevelScene scene, byte subType, FVector pos, FVector velocity) {
			WeaponChakram projectile;

			// Retrieve a Projectile Ball from the ObjectPool, if one is available:
			if(ObjectPool.WeaponChakram.Count > 0) {
				projectile = ObjectPool.WeaponChakram.Pop();
				projectile.ResetProjectile(subType, pos, velocity);
			}

			// Create a New Projectile Ball
			else {
				projectile = new WeaponChakram(scene, subType, pos, velocity);
			}

			projectile.AssignSubType(subType);
			projectile.AssignBoundsByAtlas(2, 2, -2, -2);

			// Add the Projectile to Scene
			scene.AddToScene(projectile, false);

			return projectile;
		}

		// Prevent collision destruction of Weapon; it can go through multiple objects.
		public override void Destroy( ) {}

		private void AssignSubType(byte subType) {
			if(subType == (byte) WeaponChakramSubType.Chakram) {
				this.SetSpriteName("Weapon/Chakram");
			}
		}
	}
}
