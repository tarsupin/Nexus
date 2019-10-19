using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using Nexus.Objects;

namespace Nexus.GameEngine {

	// TODO HIGH PRIORITY: LOTS MORE TO DO.
	// TODO HIGH PRIORITY: LOTS MORE TO DO.

	public class HitCompareCharacter : IHitCompare {

		public virtual bool RunImpact( DynamicGameObject character, DynamicGameObject obj ) {

			Character ch = (Character) character;

			// Specific Impact Types
			if(obj is Enemy) { return ((Enemy)obj).RunCharacterImpact(ch); }
			if(obj is Item) { return this.CharHitsItem(ch, (Item) obj); }
			if(obj is Block) { return this.CharHitsBlock(ch, (Block) obj); }
			if(obj is Platform) { return this.CharHitsPlatform(ch, (Platform) obj); }
			if(obj is Projectile) { return this.CharHitsProjectile(ch, (Projectile) obj); }

			DirCardinal dir = CollideDetect.GetDirectionOfCollision(ch, obj);

			// Standard Collision
			return Impact.StandardImpact(ch, obj, dir);
		}

		public bool CharHitsItem( Character character, Item item ) {

			// If the entity is intangible, don't collide with the Character.
			if(item.intangible > Systems.timer.frame) { return false; }

			DirCardinal dir = CollideDetect.GetDirectionOfCollision(character, item);

			// TODO: LOTS OF STUFF HERE.
			// TODO: GRABBING, HOLDING, PICK UP ITEMS, ETC.

			// If we've made it this far, run standard collision with item:
			return Impact.StandardImpact(character, item, dir);
		}

		//public bool CharHitsEnemy( Character character, Enemy enemy ) {
		//	DirCardinal dir = CollideDetect.GetDirectionOfCollision(character, enemy);
		//	return Impact.StandardImpact(character, enemy, dir);
		//}
		
		public bool CharHitsPlatform( Character character, Platform platform ) {

			DirCardinal dir = CollideDetect.GetDirectionOfCollision(character, platform);

			// TODO: LOTS OF STUFF HERE.
			// TODO: CONVEYORS, WALL JUMPS, ETC

			return Impact.StandardImpact(character, platform, dir);
		}

		public bool CharHitsBlock( Character character, Block block ) {

			DirCardinal dir = CollideDetect.GetDirectionOfCollision(character, block);

			// TODO: LOTS OF STUFF HERE.
			// TODO: CONVEYORS, WALL JUMPS, ETC

			return Impact.StandardImpact(character, block, dir);
		}

		public bool CharHitsProjectile( Character character, Projectile projectile ) {
			DirCardinal dir = CollideDetect.GetDirectionOfCollision(character, projectile);
			return Impact.StandardImpact(character, projectile, dir);
		}
	}
}
