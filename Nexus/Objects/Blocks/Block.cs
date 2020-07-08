using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class Block : GameObject {

		public Block(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {

		}

		public virtual bool RunProjectileImpact(Projectile projectile) {

			// Some projectiles get destroyed on collision.
			if(projectile.CollisionType == ProjectileCollisionType.DestroyOnCollide) {
				projectile.Destroy();
				return true;
			}

			// Some projectiles bounce.
			if(projectile.CollisionType == ProjectileCollisionType.BounceOnFloor) {
				DirCardinal dir = CollideDetect.GetDirectionOfCollision(projectile, this);

				if(dir == DirCardinal.Down) {
					projectile.physics.AlignUp(this);
					projectile.BounceOnGround();
					return false;
				}

				projectile.Destroy(dir);
				return true;
			}

			// Standard Collision
			return false;
		}
	}
}
