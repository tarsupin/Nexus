using Nexus.Gameplay;
using Nexus.ObjectComponents;
using Nexus.Objects;

namespace Nexus.GameEngine {

	public class HitCompareItem : IHitCompare {

		public bool RunImpact( GameObject item, GameObject obj ) {

			Item it = (Item) item;

			// Specific Impact Types
			if(obj is Projectile) { return this.ItemHitsProjectile(it, (Projectile) obj); }

			// Make sure the items aren't being held.
			if(it.isHeld) { return false; }

			// Platforms
			if(obj is Platform) { return this.ItemHitsPlatform(it, (Platform)obj); ; }

			// Item-Item Collision
			if(obj is Item) {
				if(((Item)obj).isHeld) { return false; }
			}

			DirCardinal dir = CollideDetect.GetDirectionOfCollision(it, obj);

			// Standard Collision
			return Impact.StandardImpact(it, obj, dir);
		}

		public bool ItemHitsProjectile(Item item, Projectile projectile) {

			// The projectile ignores items if it ignores walls or breaks through objects:
			if(projectile.CollisionType == ProjectileCollisionType.IgnoreWallsSurvive || projectile.CollisionType == ProjectileCollisionType.IgnoreWallsDestroy) {
				return false;
			}

			// Destroy the projectile for other cases.
			projectile.Destroy();

			return true;
		}

		public bool ItemHitsPlatform(Item item, Platform platform) {

			DirCardinal dir = CollideDetect.GetDirectionOfCollision(item, platform);
			if(dir != DirCardinal.Down) { return false; }

			// Item needs to track it's platform physics:
			item.physics.touch.TouchMover(platform);

			// Special Actor Collision for Platforms.
			// Only the Actor should run collision. Platform has no need to be touched, pushed, aligned, etc.
			// Additionally, it needs to skip the intend.Y test normally associated with CollideObjDown(platform).
			item.physics.touch.TouchDown();
			item.physics.AlignUp(platform);
			item.physics.StopY();

			return true;
		}
	}
}
