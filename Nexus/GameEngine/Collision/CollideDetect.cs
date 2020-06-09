using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System;
using System.Collections.Generic;

/*
 * CollideDetect is the Narrow Phase of Collision Detection.
 * This phase tests between two objects, to see if they are overlapping / colliding.
 * It can also optionally provide details about the direction of collision, amount of overlap, etc.
 */

namespace Nexus.GameEngine {

	public class CollideDetect {

		/***************
		*** Overlaps ***
		***************/

		// Check if two objects overlap each other:
		public static bool IsOverlapping(GameObject obj, GameObject obj2) {

			// Since our Broad-Phase tracked by X-Position, quickly eliminate Y as an option first:
			int y1 = obj.posY;
			int y2 = obj2.posY;

			// Test for Y-Overlap.
			// Note the use of ||, which is different from && for X-Overlap return.
			if(y1 + obj.bounds.Top >= y2 + obj2.bounds.Bottom || y1 + obj.bounds.Bottom <= y2 + obj2.bounds.Top) { return false; }

			int x1 = obj.posX;
			int x2 = obj2.posX;

			return (x1 + obj.bounds.Left < x2 + obj2.bounds.Right && x1 + obj.bounds.Right > x2 + obj2.bounds.Left);
		}

		// This tests if the first object completely overlaps the smaller object.
		public static bool IsOverlappingTotal(GameObject largeObject, GameObject smallObject) {

			// Since our Broad-Phase tracked by X-Position, quickly eliminate Y as an option first:
			int y1 = largeObject.posY;
			int y2 = smallObject.posY;

			// Test for Y-Overlap.
			// Note the use of ||, which is different from && for X-Overlap return.
			if(y1 + largeObject.bounds.Bottom > y2 + smallObject.bounds.Bottom || y1 + largeObject.bounds.Top < y2 + smallObject.bounds.Top) { return false; }

			int x1 = largeObject.posX;
			int x2 = smallObject.posX;

			return (x1 + largeObject.bounds.Right <= x2 + smallObject.bounds.Right && x1 + largeObject.bounds.Left > x2 + smallObject.bounds.Left);
		}

		// GetOverlapX retrieves the current X overlap. Negative means not overlapping.
		public static int GetOverlapX(GameObject obj, GameObject obj2, bool obj1IsLeft) {

			// Object 1 is to the left of Object 2
			if(obj1IsLeft) {
				return (obj.posX + obj.bounds.Right) - (obj2.posX + obj2.bounds.Left);
			}

			// Object 1 is to the right of Object 2
			return (obj.posX + obj.bounds.Left) - (obj2.posX + obj2.bounds.Right);
		}

		// GetOverlapX retrieves the current Y overlap. Negative means not overlapping.
		public static int GetOverlapY(GameObject obj, GameObject obj2, bool obj1IsAbove) {

			// Object 1 is below Object 2
			if(obj1IsAbove) {
				return (obj.posY + obj.bounds.Bottom) - (obj2.posY + obj2.bounds.Top);
			}

			// Object 1 is above Object 2
			return (obj.posY + obj.bounds.Top) - (obj2.posY + obj2.bounds.Bottom);
		}

		// GetMaxOverlapX provides the amount of total X-Overlap that should occur between two objects based on relative movement.
		// Collisions that exceed this will cause false positives.
		// TODO LOW PRIORITY: Test how/why this causes false positive? Does it anymore? We changed a lot. Could be useful to remove this if possible.
		// TODO LOW PRIORITY: Eliminate this test if possible. Adds overhead (though it might be necessary overhead).
		private static int GetMaxOverlapX( Physics obj1Phys, Physics obj2Phys = null ) {
			int obj2Move = obj2Phys != null ? obj2Phys.AmountMovedX : 0;

			// If object #2 is stationary (didn't move):
			if(obj2Move == 0) { return Math.Abs(obj1Phys.AmountMovedX); }

			int obj1Move = obj1Phys.AmountMovedX;

			// If the objects are moving in the same direction:
			if(Math.Sign(obj2Move) == Math.Sign(obj1Move)) {
				return Math.Abs(obj1Move - obj2Move);
			}

			// If the objects are moving in opposite directions:
			return Math.Abs(obj1Move) + Math.Abs(obj2Move);
		}

		// GetMaxOverlapY provides the amount of total Y-Overlap that should occur between two objects based on relative movement.
		// Collisions that exceed this will cause false positives.
		private static int GetMaxOverlapY( Physics obj1Phys, Physics obj2Phys = null ) {
			int obj2Move = obj2Phys != null ? obj2Phys.AmountMovedY : 0;

			// If Object 2 did not move:
			if(obj2Move == 0) { return Math.Abs(obj1Phys.AmountMovedY); }

			int obj1Move = obj1Phys.AmountMovedY;

			// If the objects are moving in the same direction:
			if(Math.Sign(obj2Move) == Math.Sign(obj1Move)) {
				return Math.Abs(obj1Move - obj2Move);
			}

			// If the objects are moving in opposite directions:
			return Math.Abs(obj1Move) + Math.Abs(obj2Move);
		}

		/*************************
		*** Relative Positions ***
		*************************/

		public static short GetRelativeX(GameObject obj, GameObject obj2) {
			return (short)(obj2.bounds.MidX - obj.bounds.MidX);
		}

		private static int GetRelativeDX(Physics phys1, Physics phys2) {
			return phys2.AmountMovedX - phys1.AmountMovedX;
		}

		private static int GetRelativeDY(Physics phys1, Physics phys2) {
			return phys2.AmountMovedY - phys1.AmountMovedY;
		}

		/****************************
		*** Directional Detection ***
		****************************/

		// Identifies the direction of a collision.
		// WARNING: Heavy use. Only run this AFTER you've tested for if the objects overlap.
		public static DirCardinal GetDirectionOfCollision( GameObject obj, GameObject obj2 ) {

			// If the movement between the objects > the amount overlapped, ignore the overlap.
			// This prevents problems like inaccurate hitboxes from the wrong side.
			int maxOverlapY = CollideDetect.GetMaxOverlapY(obj.physics, obj2.physics);
			int relativeY = CollideDetect.GetRelativeDY(obj.physics, obj2.physics);
			int overlapY = CollideDetect.GetOverlapY(obj, obj2, relativeY <= 0);

			if(Math.Abs(overlapY) <= maxOverlapY) { return relativeY > 0 ? DirCardinal.Up : DirCardinal.Down; }

			// Same as above, but for X coordinates.
			int maxOverlapX = CollideDetect.GetMaxOverlapX(obj.physics, obj2.physics);
			int relativeX = CollideDetect.GetRelativeDX(obj.physics, obj2.physics);
			int overlapX = CollideDetect.GetOverlapX(obj, obj2, relativeX <= 0);

			if(Math.Abs(overlapX) <= maxOverlapX) { return relativeX > 0 ? DirCardinal.Left : DirCardinal.Right; }

			// If we've made it this far, the object is overlapping, but already passed the edge.
			// We return false to avoid unusual behavior, such as 'popping' up on a platform when you're slightly beneath it.
			return DirCardinal.None;
		}

		// TODO HIGH PRIORITY: Might not need this if we rebuild GetDirectionOfCollision, due to no longer needing floats, etc.
		// Forces a direction to be assumed in a collision based on relative motion of the objects.
		public static DirCardinal AssumeDirectionOfCollision(Physics obj1Phys, Physics obj2Phys) {
			int relativeY = CollideDetect.GetRelativeDY(obj1Phys, obj2Phys);
			int relativeX = CollideDetect.GetRelativeDX(obj1Phys, obj2Phys);

			// If the relative motion of Y is greater than the relative motion of X, return a vertical collision.
			if(relativeY > relativeX) { return relativeY > 0 ? DirCardinal.Up : DirCardinal.Down; }

			return relativeX > 0 ? DirCardinal.Left : DirCardinal.Right;
		}
	}
}
