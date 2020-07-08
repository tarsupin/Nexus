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
			if(en is EnemyFlight || obj is EnemyFlight) { return false; }

			DirCardinal dir = CollideDetect.GetDirectionOfCollision(en, obj);

			// Specific Impact Types
			if(obj is Enemy) { return this.EnemyHitsEnemy(en, (Enemy) obj, dir); }
			if(obj is Item) { return this.EnemyHitsItem(en, (Item) obj, dir); }
			if(obj is Block) { return this.EnemyHitsBlock(en, (Block) obj, dir); }

			// Standard Collision
			return Impact.StandardImpact(en, obj, dir);
		}

		public bool EnemyHitsItem(Enemy enemy, Item item, DirCardinal dir) {
			if(item.isHeld) { return false; }

			// Most Flight Enemies ignore item collisions.
			if(enemy is EnemyFlight) {
				if(!((EnemyFlight)enemy).shellCollision && item is Shell) { return false; }
			}

			// If we've made it this far, run standard collision with item:
			return Impact.StandardImpact(enemy, item, dir);
		}

		public bool EnemyHitsEnemy(Enemy enemy, Enemy enemy2, DirCardinal dir) {
			if(enemy.CollideVal <= CollideEnum.NoTileCollide) { return false; }
			if(enemy2.CollideVal <= CollideEnum.NoTileCollide) { return false; }
			return Impact.StandardImpact(enemy, enemy2, dir);
		}

		public bool EnemyHitsPlatform(Enemy enemy, Platform platform) {

			// If the enemy doesn't collide with tiles, they don't collide with platforms.
			if(enemy.CollideVal <= CollideEnum.NoTileCollide) { return false; }

			DirCardinal dir = CollideDetect.GetDirectionOfCollision(enemy, platform);
			if(dir != DirCardinal.Down) { return false; }

			// Enemy needs to track it's platform physics:
			enemy.physics.touch.TouchMover(platform);

			// Special Actor Collision for Platforms.
			// Only the Actor should run collision. Platform has no need to be touched, pushed, aligned, etc.
			// Additionally, it needs to skip the intend.Y test normally associated with CollideObjDown(platform).
			enemy.physics.touch.TouchDown();
			enemy.physics.AlignUp(platform);
			enemy.physics.StopY();

			return true;
		}

		public bool EnemyHitsBlock(Enemy enemy, Block block, DirCardinal dir) {

			// TODO: LOTS OF STUFF HERE.
			// TODO: CONVEYORS, WALL JUMPS, ETC

			return Impact.StandardImpact(enemy, block, dir);
		}
	}
}
