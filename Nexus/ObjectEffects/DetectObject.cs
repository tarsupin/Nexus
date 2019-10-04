
// This effect checks a particular object at an assigned location.
// Objects might use this to determine if they should act on something.
// Example: Detect if there is an enemy above a brick when it gets hit (thus damaging the enemy).

using Nexus.Engine;
using Nexus.GameEngine;
using System.Collections.Generic;

namespace Nexus.ObjectEffects {

	public class DetectObject {

		// Retrieve the object ID of a GameObject that is within the area designated.
		// You can supply an existing ID to scan for IDs above a previous value.
		// For example, if you retrieved ID 10 from this, you could search again with ID 10 as a minimum, and it will only return newer objects.
		// uint enemyFoundId = DetectObjects.FindObjectWithinArea( objectList, 480, 96, 48, 48, minimumId );
		public static uint FindObjectWithinArea( Dictionary<uint, GameObject> objectList, uint posX, uint posY, ushort width, ushort height, ushort minId = 0 ) {
			uint right = posX + width;
			uint bottom = posY + height;

			foreach( KeyValuePair<uint, GameObject> actor in objectList ) {
				FVector actorPos = actor.Value.pos;

				// If the Actor is within the bounds described.
				if(actorPos.X.IntValue >= posX && actorPos.X.IntValue + actor.Value.bounds.Right <= right && actorPos.Y.IntValue >= posY && actorPos.Y.IntValue + actor.Value.bounds.Bottom <= bottom) {

					// It is possible to skip over IDs, in case you're looking for multiple objects somewhere.
					if(actor.Value.id > minId) { return actor.Value.id; }
				}
			}

			// No GameObject was located in the list provided. Return 0, which is an invalid GameObject ID.
			return 0;
		}

		// Retrieve the object ID of a GameObject that is touching the area designated. Otherwise identical to FindObjectWithinArea().
		public static uint FindObjectsTouchingArea( Dictionary<uint, GameObject> objectList, uint posX, uint posY, ushort width, ushort height, ushort minId = 0 ) {
			uint right = posX + width;
			uint bottom = posY + height;

			foreach( KeyValuePair<uint, GameObject> actor in objectList ) {
				FVector actorPos = actor.Value.pos;

				// If the Actor is within the bounds described.
				if(actorPos.X.IntValue < right && actorPos.X.IntValue + actor.Value.bounds.Right >= posX && actorPos.Y.IntValue <= bottom && actorPos.Y.IntValue + actor.Value.bounds.Bottom >= posY) {

					// It is possible to skip over IDs, in case you're looking for multiple objects somewhere.
					if(actor.Value.id > minId) { return actor.Value.id; }
				}
			}

			// No GameObject was located in the list provided. Return 0, which is an invalid GameObject ID.
			return 0;
		}

		// TODO HIGH PRIORITY: Damage Above Block
		public void DamageAbove() {

			//// Destroy land enemies above this brick:
			//let enemiesFound: any = findObjectsTouchingArea(this.scene.activeObjects[LoadOrder.Enemy], this.pos.x + 16, this.pos.y - 4, 16, 4);

			//for(let i in enemiesFound) {
			//	let enemy: Enemy = enemiesFound[i];

			//	if(enemy.isLandCreature) {
			//		enemy.dieKnockout();
			//	}
			//}
		}
	}
}
