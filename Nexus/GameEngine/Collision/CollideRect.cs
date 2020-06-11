using Nexus.Gameplay;
using System;
using System.Collections.Generic;

/*
 * CollideRect allows you to arbitrarily test against rectangle boundaries, such as to assist with additional checks on tiles.
 */

namespace Nexus.GameEngine {

	public class CollideRect {

		/************************
		*** Generic Detection ***
		************************/

		// Retrieve the object ID of a GameObject that is within the area designated.
		// You can supply an existing ID to scan for IDs above a previous value.
		// For example, if you retrieved ID 10 from this, you could search again with ID 10 as a minimum, and it will only return newer objects.
		// uint enemyFoundId = CollideRect.FindObjectWithinArea( objectList, 480, 96, 48, 48, minimumId );
		public static uint FindObjectWithinArea(Dictionary<uint, GameObject> objectList, uint posX, uint posY, ushort width, ushort height, ushort minId = 0) {
			uint right = posX + width;
			uint bottom = posY + height;

			foreach(KeyValuePair<uint, GameObject> actorEntry in objectList) {
				GameObject actor = actorEntry.Value;

				// If the Actor is within the bounds described.
				if(actor.posX >= posX && actor.posX + actor.bounds.Right <= right && actor.posY >= posY && actor.posY + actor.bounds.Bottom <= bottom) {

					// It is possible to skip over IDs, in case you're looking for multiple objects somewhere.
					if(actor.id > minId) { return actor.id; }
				}
			}

			// No GameObject was located in the list provided. Return 0, which is an invalid GameObject ID.
			return 0;
		}

		// Retrieve the object ID of a GameObject that is touching the area designated. Otherwise identical to FindObjectWithinArea().
		// uint enemyFoundId = CollideRect.FindObjectTouchingArea( objectList, 480, 96, 48, 48, minimumId );
		public static uint FindOneObjectTouchingArea(Dictionary<uint, GameObject> objectList, uint posX, uint posY, ushort width, ushort height, ushort minId = 0) {
			uint right = posX + width;
			uint bottom = posY + height;

			foreach(KeyValuePair<uint, GameObject> actorEntry in objectList) {
				GameObject actor = actorEntry.Value;

				// If the Actor is within the bounds described.
				if(actor.posX < right && actor.posX + actor.bounds.Right >= posX && actor.posY <= bottom && actor.posY + actor.bounds.Bottom >= posY) {

					// It is possible to skip over IDs, in case you're looking for multiple objects somewhere.
					if(actor.id > minId) { return actor.id; }
				}
			}

			// No GameObject was located in the list provided. Return 0, which is an invalid GameObject ID.
			return 0;
		}

		// Retrieve the object ID of a GameObject that is touching the area designated. Otherwise identical to FindObjectWithinArea().
		// uint enemyFoundId = CollideRect.FindObjectsTouchingArea( objectList, 480, 96, 48, 48, minimumId );
		public static List<GameObject> FindAllObjectsTouchingArea(Dictionary<uint, GameObject> objectList, uint posX, uint posY, ushort width, ushort height, ushort minId = 0) {
			List<GameObject> objList = new List<GameObject>();
			uint right = posX + width;
			uint bottom = posY + height;

			foreach(KeyValuePair<uint, GameObject> actorEntry in objectList) {
				GameObject actor = actorEntry.Value;

				// Include the actor if it is within the bounds described.
				if(actor.posX < right && actor.posX + actor.bounds.Right >= posX && actor.posY <= bottom && actor.posY + actor.bounds.Bottom >= posY) {
					objList.Add(actor);
				}
			}

			// Return a list of all valid GameObject IDs within the area.
			return objList;
		}

		// -------------------------- //
		// --- Rectangle Overlaps --- //
		// -------------------------- //

		// Check if an object is overlapping / touching a rectangle.
		public static bool IsTouchingRect(GameObject obj, int x1, int x2, int y1, int y2) {

			// Test for Y-Overlap.
			// Note the use of ||, which is different from && for X-Overlap return.
			if(obj.posY + obj.bounds.Top > y2 || obj.posY + obj.bounds.Bottom < y1) { return false; }

			return (obj.posX + obj.bounds.Left < x2 && obj.posX + obj.bounds.Right > x1);
		}

		// This tests if the object is completely overlapped by the rectangle.
		public static bool IsWithinRectangle(GameObject obj, int x1, int x2, int y1, int y2) {

			// Test for Y-Overlap.
			// Note the use of ||, which is different from && for X-Overlap return.
			if(y2 >= obj.posY + obj.bounds.Bottom || y1 <= obj.posY + obj.bounds.Top) { return false; }

			return (x2 >= obj.posX + obj.bounds.Right && x1 <= obj.posX + obj.bounds.Left);
		}

		// GetOverlapX retrieves the current X overlap. Negative means not overlapping.
		public static int GetOverlapX(GameObject obj, int x1, int x2, bool obj1IsLeft) {

			// Object 1 is to the left of Object 2
			if(obj1IsLeft) {
				return (obj.posX + obj.bounds.Right) - x1;
			}

			// Object 1 is to the right of Object 2
			return x2 - (obj.posX + obj.bounds.Left);
		}

		// GetOverlapX retrieves the current Y overlap. Negative means not overlapping.
		public static int GetOverlapY(GameObject obj, int y1, int y2, bool obj1IsAbove) {

			// Object 1 is below Object 2
			if(obj1IsAbove) {
				return (obj.posY + obj.bounds.Bottom) - y1;
			}

			// Object 1 is above Object 2
			return y2 - (obj.posY + obj.bounds.Top);
		}

		/*************************
		*** Relative Positions ***
		*************************/

		public static short GetRelativeX(GameObject obj, int midX) {
			return (short)(obj.posX + obj.bounds.MidX - midX);
		}
		
		public static short GetRelativeY(GameObject obj, int midY) {
			return (short)(obj.posY + obj.bounds.MidY - midY);
		}

		/****************************
		*** Directional Detection ***
		****************************/

		// Identifies the direction of a collision relative to a rectangle.
		// WARNING: Heavy process. Only run this AFTER you've tested for if the rectangle overlaps.
		public static DirCardinal GetDirectionOfCollision( GameObject obj, int x1, int x2, int y1, int y2) {

			// If the movement between the objects > the amount overlapped, ignore the overlap.
			// This prevents problems like inaccurate hitboxes from the wrong side.
			int maxOverlapY = Math.Abs(obj.physics.AmountMovedY);
			int relativeY = 0 - obj.physics.AmountMovedY;
			int overlapY = CollideRect.GetOverlapY(obj, y1, y2, relativeY <= 0);

			if(overlapY <= maxOverlapY) { return relativeY > 0 ? DirCardinal.Up : DirCardinal.Down; }

			// Same as above, but for X coordinates.
			int maxOverlapX = Math.Abs(obj.physics.AmountMovedX);
			int relativeX = 0 - obj.physics.AmountMovedX;
			int overlapX = CollideRect.GetOverlapX(obj, x1, x2, relativeX <= 0);

			if(overlapX <= maxOverlapX) { return relativeX > 0 ? DirCardinal.Left : DirCardinal.Right; }

			// If we've made it this far, the object is overlapping, but already passed the edge.
			// We return false to avoid unusual behavior, such as 'popping' up on a platform when you're slightly beneath it.
			return DirCardinal.None;
		}
	}
}
