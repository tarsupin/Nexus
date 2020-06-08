using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using Nexus.Objects;

/*
 * Tile Game Object:
 *		- Tile Game Objects are not placed in the game world. They represent MULTIPLE game objects of IDENTICAL MECHANICS.
 *		- TGO's are IMMUTABLE, but their grid position may have subtypes that represent different immutable versions.
 *		- TGO's do not have a RunTick() methods.
 *			- If a TGO needs to have behaviors (such as cannons firing, or a leaf being destroyed), it must use the Queue.
 *	
 *	The purpose of Tile Game Objects is to eliminate the need for large quantities of similar tiles.
 *		- For example, if there are 1500 Ground Tiles in a level, there is only one class needed to handle their mechanics.
 *	
 *	The Tilemap will make heavy use of Tile Game Objects.
 *		- The Tile Game Object will handle those statuses, mechanics, rendering, or whatever is request on behalf of that tile.
 *		
 */

namespace Nexus.GameEngine {

	public class TileObject {

		public IMetaData Meta { get; protected set; }
		public Atlas atlas;
		public byte tileId;
		public Params paramSet;

		// Helper Titles and Text
		public string title;
		public string description;
		public string[] titles;				// Gets used if there are multiple unique entries (subindexes) that need descriptions.
		public string[] descriptions;       // Gets used if there are multiple unique entries (subindexes) that need descriptions.

		// Collision Behaviors
		public bool collides;           // TRUE if this tile allows collisions.
		//public bool subTypeCollision;	// TRUE if subtypes are relevant for collisions.

		public TileObject() {
			this.atlas = Systems.mapper.atlas[(byte) AtlasGroup.Tiles];
		}

		public virtual void TriggerEvent( short gridX, short gridY, short val1 = 0, short val2 = 0 ) {

		}

		public virtual void Draw( RoomScene room, byte subType, int posX, int posY ) {
			//this.atlas.Draw(texture, posX, posY);
		}

		public virtual bool RunImpact( RoomScene room, DynamicObject actor, ushort gridX, ushort gridY, DirCardinal dir ) {
			TileSolidImpact.RunImpact(actor, gridX, gridY, dir);
			return true;
		}
	}
}
