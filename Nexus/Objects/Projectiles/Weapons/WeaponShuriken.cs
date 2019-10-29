﻿using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public enum WeaponShurikenSubType : byte {
		Shuriken
	}

	public class WeaponShuriken : Projectile {

		private WeaponShuriken(LevelScene scene, byte subType, FVector pos, FVector velocity) : base(scene, subType, pos, velocity) {
			this.Damage = DamageStrength.Standard;
			this.CollisionType = ProjectileCollisionType.DestroyOnCollide;
			this.physics.SetGravity(FInt.Create(0.4));
		}

		public static WeaponShuriken Create(LevelScene scene, byte subType, FVector pos, FVector velocity) {
			WeaponShuriken projectile;

			// Retrieve a Projectile Ball from the ObjectPool, if one is available:
			if(ObjectPool.WeaponShuriken.Count > 0) {
				projectile = ObjectPool.WeaponShuriken.Pop();
				projectile.ResetProjectile(subType, pos, velocity);
			}

			// Create a New Projectile Ball
			else {
				projectile = new WeaponShuriken(scene, subType, pos, velocity);
			}

			projectile.AssignSubType(subType);
			projectile.AssignBoundsByAtlas(2, 2, -2, -2);

			// Add the Projectile to Scene
			scene.AddToScene(projectile, false);

			return projectile;
		}

		private void AssignSubType(byte subType) {
			if(subType == (byte) WeaponShurikenSubType.Shuriken) {
				this.SetSpriteName("Weapon/Shuriken");
			}
		}
	}
}
