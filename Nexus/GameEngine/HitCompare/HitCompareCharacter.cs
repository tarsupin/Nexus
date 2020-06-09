using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using Nexus.Objects;

namespace Nexus.GameEngine {

	// TODO HIGH PRIORITY: LOTS MORE TO DO.
	// TODO HIGH PRIORITY: LOTS MORE TO DO.

	public class HitCompareCharacter : IHitCompare {

		public bool RunImpact( GameObject character, GameObject obj ) {

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

			// If this item is being held, skip.
			if(item.isHeld) { return false; }

			// If the entity is intangible, don't collide with the Character.
			if(item.intangible > Systems.timer.Frame) { return false; }

			DirCardinal dir = CollideDetect.GetDirectionOfCollision(character, item);

			// If item is hit from side:
			if(dir == DirCardinal.Left || dir == DirCardinal.Right) {
				
				// Check if Grabbing Item
				if(character.heldItem.WillPickupAttemptWork(item, dir)) {
					character.heldItem.PickUpItem(item);
					return true;
				}
			}

			// If item is hit from above:
			else if(dir == DirCardinal.Up) {

				// Check if Grabbing Item
				if(character.heldItem.WillPickupAttemptWork(item, character.FaceRight ? DirCardinal.Right : DirCardinal.Left)) {
					character.heldItem.PickUpItem(item);
					return true;
				}
			}

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

			// Skip Character if they cast the projectile.
			if(projectile.ByActorID == character.id) { return false; }

			DirCardinal dir = CollideDetect.GetDirectionOfCollision(character, projectile);
			return Impact.StandardImpact(character, projectile, dir);
		}
	}
}
