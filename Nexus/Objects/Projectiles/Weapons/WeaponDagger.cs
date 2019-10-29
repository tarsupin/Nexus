﻿using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public enum WeaponDaggerSubType : byte {
		Dagger
	}

	public class WeaponDagger : Projectile {

		private WeaponDagger(LevelScene scene, byte subType, FVector pos, FVector velocity) : base(scene, subType, pos, velocity) {
			this.Damage = DamageStrength.Major;
			this.CollisionType = ProjectileCollisionType.IgnoreWalls;
		}

		public static WeaponDagger Create(LevelScene scene, byte subType, FVector pos, FVector velocity) {
			WeaponDagger projectile;

			// Retrieve a Projectile Ball from the ObjectPool, if one is available:
			if(ObjectPool.WeaponDagger.Count > 0) {
				projectile = ObjectPool.WeaponDagger.Pop();
				projectile.ResetProjectile(subType, pos, velocity);
			}

			// Create a New Projectile Ball
			else {
				projectile = new WeaponDagger(scene, subType, pos, velocity);
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
			if(subType == (byte) WeaponDaggerSubType.Dagger) {
				this.SetSpriteName("Weapon/Dagger");
			}
		}
	}
}
