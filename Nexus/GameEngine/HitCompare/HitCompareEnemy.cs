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

			// Standard Collision
			return en.impact.StandardCollision(obj);
		}

		public bool EnemyHitsItem(Enemy enemy, Item item) {

			// If the entity is intangible, don't collide with the Enemy.
			if(item.intangible > item.scene.timer.frame) { return false; }

			// TODO: LOTS OF STUFF HERE.
			// TODO: GRABBING, HOLDING, PICK UP ITEMS, ETC.

			// If we've made it this far, run standard collision with item:
			return enemy.impact.StandardCollision(item);
		}

		public bool EnemyHitsEnemy(Enemy enemy, Enemy enemy2) {

			return enemy.impact.StandardCollision(enemy2);
		}

		public bool EnemyHitsPlatform(Enemy enemy, Platform platform) {

			// TODO: LOTS OF STUFF HERE.
			// TODO: CONVEYORS, WALL JUMPS, ETC

			return enemy.impact.StandardCollision(platform);
		}

		public bool EnemyHitsBlock(Enemy enemy, Block block) {

			// TODO: LOTS OF STUFF HERE.
			// TODO: CONVEYORS, WALL JUMPS, ETC

			return enemy.impact.StandardCollision(block);
		}

		public bool EnemyHitsProjectile(Enemy enemy, Projectile projectile) {
			return enemy.impact.StandardCollision(projectile);
		}
	}
}
