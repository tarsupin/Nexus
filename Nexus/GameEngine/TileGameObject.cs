using Newtonsoft.Json.Linq;
using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using Nexus.Objects;

/*
 * Tile Game Object:
 *	- Tile Game Objects are not placed in the game world. They represent MULTIPLE game objects of IDENTICAL MECHANICS.
 *	- IMMUTABLE values and mechanics. All objects represented by a Tile Game Object must have identical and persistent properties.
 *	- CANNOT have Update() methods, as there would be nothing mutable to update.
 *	- Have NO associated position.
 *	- Will render their objects with a dynamically assigned texture and position.
 *	
 *	The purpose of Tile Game Objects is to eliminate the need for large quantities of similar tiles.
 *		- For example, if there are 600 Ground Tiles in a level, there is only one class needed to handle their mechanics.
 *	
 *	The Tilemap will make heavy use of Tile Game Objects.
 *		- A tile can identify a ClassID that it wants to render through, or use behaviors of.
 *		- The Tile Game Object will handle those statuses, mechanics, rendering, or whatever is request on behalf of that tile.
 *		
 *		
 * How Tile Game Objects are Stored & Referenced:
 *		Scene.classObjects[classID] = ClassGameObject;
 */

namespace Nexus.GameEngine {

	public class TileGameObject {

		public RoomScene room;
		public Atlas atlas;

		// Collision Behaviors
		public bool collides;           // TRUE if this tile allows collisions.
		//public bool subTypeCollision;	// TRUE if subtypes are relevant for collisions.

		public TileGameObject(RoomScene room, TileGameObjectId classId, AtlasGroup atlasGroup) {
			this.room = room;
			this.atlas = Systems.mapper.atlas[(byte) atlasGroup];
			room.RegisterClassGameObject(classId, this);
		}

		public virtual void UpdateParams( JObject paramList ) {
			// System.Console.WriteLine(paramList);
			// System.Console.WriteLine(paramList.GetType().ToString());

			// JToken token = paramList["attGrav"];
			// if(token != null) {
			//	int a = (int) paramList.GetValue("attGrav");
			//	System.Console.WriteLine("GRAV: " + a.ToString());
			// }
		}

		public virtual void Draw( byte subType, int posX, int posY ) {
			//this.atlas.Draw(texture, posX, posY);
		}

		public virtual bool RunImpact( DynamicGameObject actor, ushort gridX, ushort gridY, DirCardinal dir ) {
			TileSolidImpact.RunImpact(actor, gridX, gridY, dir);

			if(actor is EnemyLand) {

				// Enemy should turn if colliding with a side.
				if(dir == DirCardinal.Left) {
					HitCompareEnemy.LandEnemyHitsSide((EnemyLand) actor, false);

				} else if(dir == DirCardinal.Right) {
					HitCompareEnemy.LandEnemyHitsSide((EnemyLand) actor, true);
				}
			}

			return true;
		}
	}
}
