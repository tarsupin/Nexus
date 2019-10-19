using Nexus.Gameplay;
using Nexus.ObjectComponents;
using Nexus.Objects;

namespace Nexus.GameEngine {

	public class HitCompareItem : IHitCompare {

		public virtual bool RunImpact( DynamicGameObject item, DynamicGameObject obj ) {

			// TODO: ALL OF THIS HAS TO BE BUILT. NEED DYNAMIC OBJECTS IN PLACE FIRST.
			// TODO: ALL OF THIS HAS TO BE BUILT. NEED DYNAMIC OBJECTS IN PLACE FIRST.

			Item it = (Item) item;

			// Specific Impact Types
			if(obj is Projectile) { return this.ItemHitsProjectile(it, (Projectile) obj); }

			// Make sure the item isn't being held
			//let char = item.scene.character;
			//if(char.item && (char.item.id === item.id || char.item.id === obj2.id)) { return false; }

			DirCardinal dir = CollideDetect.GetDirectionOfCollision(it, obj);

			// Standard Collision
			return Impact.StandardImpact(it, obj, dir);
		}

		public bool ItemHitsProjectile(Item item, Projectile projectile) {

			// The projectile ignores items if it ignores walls or breaks through objects:
			if(projectile.CollisionType == ProjectileCollisionType.IgnoreWalls || projectile.CollisionType == ProjectileCollisionType.BreakObjects) {
				return false;
			}

			// Destroy the projectile for other cases.
			projectile.Destroy();

			return true;
		}
	}
}
