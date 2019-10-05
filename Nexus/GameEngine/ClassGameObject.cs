using Nexus.Engine;
using Nexus.Gameplay;

/*
 * Class Game Object:
 *	- Class Game Objects are not placed in the game world. They represent MULTIPLE game objects of IDENTICAL MECHANICS.
 *	- IMMUTABLE values and mechanics. All objects represented by a Class Game Object must have identical and persistent properties.
 *	- CANNOT have Update() methods, as there would be nothing mutable to update.
 *	- Have NO associated position.
 *	- Will render their objects with a dynamically assigned texture and position.
 *	
 *	The purpose of Class Game Objects is to eliminate the need for large quantities of similar tiles.
 *		- For example, if there are 600 Ground Tiles in a level, there is only one class needed to handle their mechanics.
 *	
 *	The Tilemap will make heavy use of Class Game Objects.
 *		- A tile can identify a ClassID that it wants to render through, or use behaviors of.
 *		- The Class Game Object will handle those statuses, mechanics, rendering, or whatever is request on behalf of that tile.
 *		
 *		
 * How Class Game Objects are Stored & Referenced:
 *		Scene.classObjects[classID] = ClassGameObject;
 */

namespace Nexus.GameEngine {

	public class ClassGameObject {

		public LevelScene scene;
		public Atlas atlas;

		public ClassGameObject(LevelScene scene, ClassGameObjectId classId, AtlasGroup atlasGroup) {
			this.scene = scene;
			this.atlas = scene.mapper.atlas[(byte) atlasGroup];

			this.scene.RegisterClassGameObject(classId, this);
		}

		public virtual void Draw( byte subType, int posX, int posY ) {
			//this.atlas.Draw(texture, FVector.Create(posX, posY));
		}
	}
}
