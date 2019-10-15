using Nexus.Engine;
using Nexus.Gameplay;

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

		public LevelScene scene;
		public Atlas atlas;

		// Collision Behaviors
		public bool collides;           // TRUE if this tile allows collisions.
		//public bool subTypeCollision;	// TRUE if subtypes are relevant for collisions.

		public TileGameObject(LevelScene scene, TileGameObjectId classId, AtlasGroup atlasGroup) {
			this.scene = scene;
			this.atlas = scene.mapper.atlas[(byte) atlasGroup];
			scene.RegisterClassGameObject(classId, this);
		}

		public virtual void Draw( byte subType, int posX, int posY ) {
			//this.atlas.Draw(texture, posX, posY);
		}

		public virtual bool RunCollision( DynamicGameObject actor, DirCardinal dir ) {
			return false;
		}

		public bool CollideWithBlock(DynamicGameObject actor, DirCardinal dir) {

			//// If we're dealing with a full block collision, we already know it's a confirmed hit.
			//if(tileObj.facing == DirCardinal.Center) {

			//	// TODO: CONFIRMED HIT HERE. PROCESS IT.
			//	// tileData[(byte) TMBTiles.SpecialCollisionTest]  // NOTE: It already considers the collision to "hit"
			//	// tileData[(byte) TMBTiles.SpecialCollisionEffect]

			//	// TODO: NEED TO DO TMBTiles.SpecialCollisionTest if applicable. // NOTE: It already considers the collision to "hit"

			//	// Run Collision & Alignment based on the direction moved:
			//	if(dir == DirCardinal.Down) {
			//		CollideTileAffect.CollideDown(actor, gridY * (byte)TilemapEnum.TileHeight - actor.bounds.Bottom);
			//	} else if(dir == DirCardinal.Right) {
			//		CollideTileAffect.CollideRight(actor, gridX * (byte)TilemapEnum.TileWidth - actor.bounds.Right);
			//	} else if(dir == DirCardinal.Left) {
			//		CollideTileAffect.CollideLeft(actor, gridX * (byte)TilemapEnum.TileWidth - actor.bounds.Left);
			//	} else if(dir == DirCardinal.Up) {
			//		CollideTileAffect.CollideUp(actor, gridY * (byte)TilemapEnum.TileHeight - actor.bounds.Top);
			//	}

			//	// TODO: NEED TO DO TMBTiles.SpecialCollisionEffect if applicable.
			//	return true;
			//}

			//// Colliding with a Platform:
			//else if(tileObj is PlatformFixed) {

			//	// The Platform Faces Up. Collide if the Actor is moving is Down.
			//	if(tileObj.facing == DirCardinal.Up) {
			//		if(dir == DirCardinal.Down) {
			//			// TODO: NEED TO DO TMBTiles.SpecialCollisionTest if applicable. // NOTE: It already considers the collision to "hit"

			//			CollideTileAffect.CollideDown(actor, gridY * (byte)TilemapEnum.TileHeight - actor.bounds.Bottom);

			//			// TODO: NEED TO DO TMBTiles.SpecialCollisionEffect if applicable.
			//			return true;
			//		}

			//		return false;
			//	}

			//	// The Platform Faces Down. Collide if the Actor is moving is Up.
			//	else if(tileObj.facing == DirCardinal.Down) {
			//		if(dir == DirCardinal.Up) {
			//			// TODO: NEED TO DO TMBTiles.SpecialCollisionTest if applicable. // NOTE: It already considers the collision to "hit"

			//			CollideTileAffect.CollideUp(actor, gridY * (byte)TilemapEnum.TileHeight - actor.bounds.Top);

			//			// TODO: NEED TO DO TMBTiles.SpecialCollisionEffect if applicable.

			//			return true;
			//		}
			//	}

			//	// The Platform Faces Left. Collide if the Actor is moving Right.
			//	else if(tileObj.facing == DirCardinal.Left) {
			//		if(dir == DirCardinal.Right) {
			//			// TODO: NEED TO DO TMBTiles.SpecialCollisionTest if applicable. // NOTE: It already considers the collision to "hit"

			//			CollideTileAffect.CollideRight(actor, gridX * (byte)TilemapEnum.TileWidth - actor.bounds.Right);

			//			// TODO: NEED TO DO TMBTiles.SpecialCollisionEffect if applicable.
			//			return true;
			//		}

			//		return false;
			//	}

			//	// The Platform Faces Right. Collide if the Actor is moving is Left.
			//	else if(tileObj.facing == DirCardinal.Right) {
			//		if(dir == DirCardinal.Left) {
			//			// TODO: NEED TO DO TMBTiles.SpecialCollisionTest if applicable. // NOTE: It already considers the collision to "hit"

			//			CollideTileAffect.CollideLeft(actor, gridX * (byte)TilemapEnum.TileWidth - actor.bounds.Left);

			//			// TODO: NEED TO DO TMBTiles.SpecialCollisionEffect if applicable.
			//			return false;
			//		}
			//	}
			//}

			// Colliding with a Slope:
			// TODO: SLOPE COLLISION
			return false;
		}
	}
}
