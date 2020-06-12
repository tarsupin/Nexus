﻿using Nexus.Engine;
using Nexus.GameEngine;

namespace Nexus.Objects {

	public class ProjectileBullet : Projectile {

		public ProjectileBullet() : base(null, 0, FVector.Create(0, 0), FVector.Create(0, 0)) { }

		public static ProjectileBullet Create(RoomScene room, byte subType, FVector pos, FVector velocity) {

			// Retrieve an available projectile from the pool.
			ProjectileBullet projectile = ProjectilePool.ProjectileBullet.GetObject();

			projectile.ResetProjectile(room, subType, pos, velocity);

			projectile.SetActivity(Activity.NoTileCollide);
			projectile.SetCollisionType(ProjectileCollisionType.IgnoreWalls);
			projectile.SetSafelyJumpOnTop(true);
			projectile.SetEndLife(Systems.timer.Frame + 720);

			projectile.SetSpriteName("Projectiles/Bullet");
			projectile.AssignBoundsByAtlas(2, 2, -2, -2);

			// Add the Projectile to Scene
			room.AddToScene(projectile, false);

			return projectile;
		}
	}
}
