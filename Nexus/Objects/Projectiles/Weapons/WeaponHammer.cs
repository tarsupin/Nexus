using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public enum WeaponHammerSubType : byte {
		Hammer
	}

	public class WeaponHammer : Projectile {

		private WeaponHammer(LevelScene scene, byte subType, FVector pos, FVector velocity) : base(scene, subType, pos, velocity) {
			this.Damage = DamageStrength.Major;
			this.CollisionType = ProjectileCollisionType.IgnoreWalls;
			this.physics.SetGravity(FInt.Create(0.45));
		}

		public static WeaponHammer Create(LevelScene scene, byte subType, FVector pos, FVector velocity) {
			WeaponHammer projectile;

			// Retrieve a Projectile Ball from the ObjectPool, if one is available:
			if(ObjectPool.WeaponHammer.Count > 0) {
				projectile = ObjectPool.WeaponHammer.Pop();
				projectile.ResetProjectile(subType, pos, velocity);
			}

			// Create a New Projectile Ball
			else {
				projectile = new WeaponHammer(scene, subType, pos, velocity);
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
			if(subType == (byte) WeaponHammerSubType.Hammer) {
				this.SetSpriteName("Weapon/Hammer");
			}
		}
	}
}
