using Nexus.Objects;

namespace Nexus.GameEngine {

	// TODO HIGH PRIORITY: LOTS MORE TO DO.
	// TODO HIGH PRIORITY: LOTS MORE TO DO.

	public class HitCompareCharacter : IHitCompare {

		public virtual bool RunImpact( DynamicGameObject character, DynamicGameObject obj ) {

			Character ch = (Character) character;

			// Specific Impact Types
			if(obj is Enemy) { return this.CharHitsEnemy(ch, (Enemy) obj); }
			if(obj is Item) { return this.CharHitsItem(ch, (Item) obj); }
			if(obj is Block) { return this.CharHitsBlock(ch, (Block) obj); }
			if(obj is Platform) { return this.CharHitsPlatform(ch, (Platform) obj); }
			if(obj is Projectile) { return this.CharHitsProjectile(ch, (Projectile) obj); }

			// Standard Collision
			return ch.impact.StandardCollision(obj);
		}

		public bool CharHitsItem( Character character, Item item ) {

			// If the entity is intangible, don't collide with the Character.
			if(item.intangible > item.scene.timer.frame) { return false; }

			// TODO: LOTS OF STUFF HERE.
			// TODO: GRABBING, HOLDING, PICK UP ITEMS, ETC.

			// If we've made it this far, run standard collision with item:
			return character.impact.StandardCollision(item);
		}

		public bool CharHitsEnemy( Character character, Enemy enemy ) {

			// TODO: LOTS OF STUFF HERE.
			// TODO: CONVEYORS, WALL JUMPS, ETC

			return character.impact.StandardCollision(enemy);
		}
		
		public bool CharHitsPlatform( Character character, Platform platform ) {

			// TODO: LOTS OF STUFF HERE.
			// TODO: CONVEYORS, WALL JUMPS, ETC

			return character.impact.StandardCollision(platform);
		}

		public bool CharHitsBlock( Character character, Block block ) {

			// TODO: LOTS OF STUFF HERE.
			// TODO: CONVEYORS, WALL JUMPS, ETC

			return character.impact.StandardCollision(block);
		}

		public bool CharHitsProjectile( Character character, Projectile projectile ) {
			return character.impact.StandardCollision(projectile);
		}
	}
}
