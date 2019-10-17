using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.GameEngine {

	public class HitCompareEnemy : IHitCompare {

		public virtual bool RunImpact( DynamicGameObject enemy, DynamicGameObject obj ) {

			Enemy en = (Enemy) enemy;

			// Specific Impact Types
			if(obj is Enemy) { return this.EnemyHitsEnemy(en, (Enemy) obj); }
			if(obj is Item) { return this.EnemyHitsItem(en, (Item) obj); }
			if(obj is Block) { return this.EnemyHitsBlock(en, (Block) obj); }
			if(obj is Platform) { return this.EnemyHitsPlatform(en, (Platform) obj); }
			if(obj is Projectile) { return this.EnemyHitsProjectile(en, (Projectile) obj); }

			DirCardinal dir = CollideDetect.GetDirectionOfCollision(en, obj);

			// Standard Collision
			return en.impact.StandardImpact(obj, dir);
		}

		public bool EnemyHitsItem(Enemy enemy, Item item) {

			// If the entity is intangible, don't collide with the item.
			if(item.intangible > Systems.timer.frame) { return false; }

			DirCardinal dir = CollideDetect.GetDirectionOfCollision(enemy, item);

			// TODO: LOTS OF STUFF HERE.
			// TODO: GRABBING, HOLDING, PICK UP ITEMS, ETC.

			// If we've made it this far, run standard collision with item:
			return enemy.impact.StandardImpact(item);
		}

		public bool EnemyHitsEnemy(Enemy enemy, Enemy enemy2) {

			DirCardinal dir = CollideDetect.GetDirectionOfCollision(enemy, enemy2);

			return enemy.impact.StandardImpact(enemy2, dir);
		}

		public bool EnemyHitsPlatform(Enemy enemy, Platform platform) {

			DirCardinal dir = CollideDetect.GetDirectionOfCollision(enemy, platform);

			// TODO: LOTS OF STUFF HERE.
			// TODO: CONVEYORS, WALL JUMPS, ETC

			return enemy.impact.StandardImpact(platform, dir);
		}

		public bool EnemyHitsBlock(Enemy enemy, Block block) {

			DirCardinal dir = CollideDetect.GetDirectionOfCollision(enemy, block);

			// TODO: LOTS OF STUFF HERE.
			// TODO: CONVEYORS, WALL JUMPS, ETC

			return enemy.impact.StandardImpact(block, dir);
		}

		public bool EnemyHitsProjectile(Enemy enemy, Projectile projectile) {
			DirCardinal dir = CollideDetect.GetDirectionOfCollision(enemy, projectile);
			return enemy.impact.StandardImpact(projectile, dir);
		}
	}
}
