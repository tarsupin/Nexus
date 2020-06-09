using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using Nexus.Objects;

namespace Nexus.GameEngine {

	public class HitCompareEnemy : IHitCompare {

		public bool RunImpact( GameObject enemy, GameObject obj ) {

			Enemy en = (Enemy) enemy;
			
			// If the object is a platform, run special collision tests first.
			if(obj is Platform) { return this.EnemyHitsPlatform(en, (Platform) obj); }

			// For projectiles, direction may be unnecessary.
			if(obj is Projectile) { return en.RunProjectileImpact(obj as Projectile); }

			// Don't collide with Flight Enemies
			if(obj is EnemyFlight) { return false; }

			DirCardinal dir = CollideDetect.GetDirectionOfCollision(en, obj);

			// Specific Impact Types
			if(obj is Enemy) { return this.EnemyHitsEnemy(en, (Enemy) obj, dir); }
			if(obj is Item) { return this.EnemyHitsItem(en, (Item) obj, dir); }
			if(obj is Block) { return this.EnemyHitsBlock(en, (Block) obj, dir); }

			// Standard Collision
			return Impact.StandardImpact(en, obj, dir);
		}

		public bool EnemyHitsItem(Enemy enemy, Item item, DirCardinal dir) {

			// If the item is intangible, don't collide with the item.
			if(item.intangible > Systems.timer.Frame) { return false; }

			// TODO: LOTS OF STUFF HERE.
			// TODO: GRABBING, HOLDING, PICK UP ITEMS, ETC.

			// If we've made it this far, run standard collision with item:
			return Impact.StandardImpact(enemy, item);
		}

		public bool EnemyHitsEnemy(Enemy enemy, Enemy enemy2, DirCardinal dir) {
			return Impact.StandardImpact(enemy, enemy2, dir);
		}

		public bool EnemyHitsPlatform(Enemy enemy, Platform platform) {

			DirCardinal dir = CollideDetect.GetDirectionOfCollision(enemy, platform);

			// TODO: LOTS OF STUFF HERE.
			// TODO: CONVEYORS, WALL JUMPS, ETC

			return Impact.StandardImpact(enemy, platform, dir);
		}

		public bool EnemyHitsBlock(Enemy enemy, Block block, DirCardinal dir) {

			// TODO: LOTS OF STUFF HERE.
			// TODO: CONVEYORS, WALL JUMPS, ETC

			return Impact.StandardImpact(enemy, block, dir);
		}
	}
}
