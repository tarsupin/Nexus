using Nexus.Gameplay;
using System.Collections.Generic;

// The purpose of the Narrow Collisions is:
//	1. To determine if objects that were identified as potentially colliding do collide.
//	2. If they are colliding, send them to the appropriate Hit Comparison classes where they will determine what impacts to cause.

namespace Nexus.GameEngine {

	public interface IHitCompare {
		bool RunImpact(DynamicGameObject obj, DynamicGameObject obj2);
	}

	public class CollideNarrow {

		// List of Narrow Collision Mappings
		public static Dictionary<byte, IHitCompare> NarrowMap = new Dictionary<byte, IHitCompare>() {
			{ (byte) LoadOrder.Character, new HitCompareCharacter() },
			{ (byte) LoadOrder.Enemy, new HitCompareEnemy() },
			{ (byte) LoadOrder.Item, new HitCompareItem() },
			//{ (byte) LoadOrder.Platform, new HitComparePlatform() },
			{ (byte) LoadOrder.Projectile, new HitCompareProjectile() },
		};

		public static void ProcessCollision( DynamicGameObject obj, DynamicGameObject obj2 ) {

			// If an overlap is not detected, end the narrow testing here.
			if(!CollideDetect.IsOverlapping(obj, obj2)) { return; }

			// Order the objects by archetype, and run them through the collision map.
			if(obj.Meta.Archetype < obj2.Meta.Archetype) {
				CollideNarrow.RefineCollision(obj, obj2);
			} else {
				CollideNarrow.RefineCollision(obj2, obj);
			}
		}

		public static void RefineCollision( DynamicGameObject obj, DynamicGameObject obj2 ) {

			// Run Mapped Collisions
			if(CollideNarrow.NarrowMap.ContainsKey((byte) obj.Meta.LoadOrder)) {
				CollideNarrow.NarrowMap[(byte)obj.Meta.LoadOrder].RunImpact(obj, obj2);
			}

			// Run Reverse-Map Collisions
			else if(CollideNarrow.NarrowMap.ContainsKey((byte) obj2.Meta.LoadOrder)) {
				CollideNarrow.NarrowMap[(byte)obj2.Meta.LoadOrder].RunImpact(obj2, obj);
			}
		}
	}
}
