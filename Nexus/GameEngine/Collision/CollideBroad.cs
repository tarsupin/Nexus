using Nexus.Gameplay;
using Nexus.Objects;
using System.Collections.Generic;

/*
 * This Collision System uses the "Sweep and Prune" style of behavior.
 * The reason I chose this, rather than other methods, is for a few reasons:
 *		1. I'm using a deterministic multiplayer system, and thus testing collisions on ALL objects in the scene.
 *			- Why? Because if there are 10 players in a level, pretty much everything would need collision testing anyway.
 *			- I'm therefore designing for maximum capacity, since I can't just test collisions from the camera's viewpoint.
 *			- If I did that, all deterministic behavior would evaporate. Thus... everything gets evaluated.
 *		2. Many levels are horizontal, which this would be excellent for, as opposed to square-based evaluations.
 *		3. Gut feeling and simplicity? Also because I can swap this out for other methods at any time. So yeah.
 */

//	1. Get every object in the room.
//	2. Order their X-position from left to right.
//	3. Loop through the list, measuring against any objects to the right that can still collide.
//		- If X position of scanned object is < current object's X + Bounds, then current object has no more possible collisions.
//	4. Send any pairs that could possibly collide to Collision Narrow.

namespace Nexus.GameEngine {

	class BroadComparePos : IComparer<GameObject> {
		public int Compare(GameObject obj1, GameObject obj2) {
			return obj1.posX.CompareTo(obj2.posX);
		}
	}
	
	public class CollideBroad {

		private readonly CollideDetect narrow;
		private List<GameObject>[] objectList;
		private BroadComparePos comparePos;

		public CollideBroad() {

			// Assign Narrow Collision Component (can swap out as desired)
			this.narrow = new CollideDetect();

			// Build List for Broad Collision
			this.objectList = new List<GameObject>[4];
			this.objectList[0] = new List<GameObject>();
			this.objectList[1] = new List<GameObject>();
			this.objectList[2] = new List<GameObject>();
			this.objectList[3] = new List<GameObject>();

			this.comparePos = new BroadComparePos();
		}

		public void RunBroadSequence( byte roomId, Dictionary<byte, Dictionary<uint, GameObject>> objects) {
			this.objectList[roomId].Clear();

			CollideBroad.SweepObjectGroup(this.objectList[roomId], objects[(byte)LoadOrder.Platform]);
			CollideBroad.SweepObjectGroup(this.objectList[roomId], objects[(byte)LoadOrder.Enemy]);
			CollideBroad.SweepObjectGroup(this.objectList[roomId], objects[(byte)LoadOrder.Item]);
			CollideBroad.SweepObjectGroup(this.objectList[roomId], objects[(byte)LoadOrder.Character]);
			CollideBroad.SweepObjectGroup(this.objectList[roomId], objects[(byte)LoadOrder.Projectile]);

			// Sort the Object List by X-Position
			this.objectList[roomId].Sort(this.comparePos);

			// Run PassToNarrow Sequence
			CollideBroad.PassToNarrow(this.objectList[roomId]);
		}

		private static void SweepObjectGroup( List<GameObject> objList, Dictionary<uint, GameObject> objects ) {

			// Loop through every object, add it to an array.
			foreach( var obj in objects ) {

				// Object must be capable of colliding, or don't add it to the array.
				if(obj.Value.Activity <= Activity.NoCollide) { return; }

				objList.Add(obj.Value);
			}
		}

		private static void PassToNarrow( List<GameObject> objList ) {
			ushort count = (ushort) objList.Count;

			// Loop through every object, starting from left.
			for( ushort left = 0; left < count; left++ ) {
				GameObject obj = objList[left];

				// Determine the right-boundary of the LEFT CURSOR object.
				int rBound = obj.posX + obj.bounds.Right;

				// Assign the RIGHT CURSOR as one to the right of LEFT CURSOR.
				ushort right = (ushort)(left + 1);

				// Compare the LEFT CURSOR to objects to its right until a short-circuit is found.
				while( right < count ) {
					GameObject obj2 = objList[right];
					right++;

					// Check if the RIGHT CURSOR object cannot collide (it's X position is too far right).
					// If so, short-circuit this loop; there is no reason to test against additional objects.
					if(rBound < obj2.posX) { break; }

					// The RIGHT CURSOR can potentially collide. Send it to NARROW COLLISION for testing.
					CollideNarrow.ProcessCollision(obj, obj2);
				}
			}
		}
	}
}
