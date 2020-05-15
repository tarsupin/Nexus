using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class Block : DynamicGameObject {

		public Block(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {

		}

		public virtual bool RunProjectileImpact(Projectile projectile) {

			// TODO:
			// Must be a projectile created by a character.
			// if(!projectile.ignoreCharacter) { return false; }

			// Some projectiles get destroyed on collision.
			if(projectile.CollisionType == ProjectileCollisionType.DestroyOnCollide) {
				projectile.Destroy();
				return true;
			}

			// Some projectiles can break objects (like boxes).
			else if(projectile.CollisionType == ProjectileCollisionType.BreakObjects) {

				// TODO: Probably not needed, since no dynamic blocks would explode?
				//this.ProjectileBreakBlock(projectile, this);
				projectile.Destroy();
				return true;
			}

			// Some projectiles have special behaviors.
			else if(projectile.CollisionType == ProjectileCollisionType.Special) {
				projectile.Destroy();
				return true;
			}

			// Some projectiles bounce.
			if(projectile.CollisionType == ProjectileCollisionType.BounceOnFloor) {
				DirCardinal dir = CollideDetect.GetDirectionOfCollision(projectile, this);

				if(dir == DirCardinal.Down) {
					CollideAffect.AlignUp(projectile, this);
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
