using Nexus.Engine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class LevelScene : Scene {

		public TilemapBool tilemap;
		public Dictionary<byte, Dictionary<ushort, DynamicGameObject>> objects;		// objects[LoadOrder][ObjectID] = DynamicGameObject
		public Dictionary<byte, ClassGameObject> classObjects;

		public LevelScene( Systems systems ) : base( systems ) {

			// Tilemap
			this.tilemap = new TilemapBool(400, 100);		// TODO: Get X,Y grid sizes from the level data.

			// Game Objects
			this.objects = new Dictionary<byte, Dictionary<ushort, DynamicGameObject>> {
				[(byte) LoadOrder.Platform] = new Dictionary<ushort, DynamicGameObject>(),          // TODO: Change to Platform
				[(byte) LoadOrder.Enemy] = new Dictionary<ushort, DynamicGameObject>(),				// TODO: Change to Enemy
				[(byte) LoadOrder.Item] = new Dictionary<ushort, DynamicGameObject>(),				// TODO: Change to Item
				[(byte) LoadOrder.TrailingItem] = new Dictionary<ushort, DynamicGameObject>(),      // TODO: Change to TrailingItem
				[(byte) LoadOrder.Character] = new Dictionary<ushort, DynamicGameObject>(),			// TODO: Change to Character
				[(byte) LoadOrder.Projectile] = new Dictionary<ushort, DynamicGameObject>()         // TODO: Change to Projectile
			};

			// Game Class Objects
			this.classObjects = new Dictionary<byte, ClassGameObject>();

			// Generate Room 0
			systems.handler.level.generate.GenerateRoom(this, "0");
		}

		public void SpawnRoom() {

		}

		// Class Game Objects
		public bool IsClassGameObjectRegistered( byte classId ) {
			return (classObjects[classId] != null);
		}

		public void RegisterClassGameObject(ClassGameObjectId classId, ClassGameObject cgo ) {
			classObjects[(byte) classId] = cgo;
		}

		public override void Update() {

		}

		public override void Draw() {

			// Render Objects
			Atlas temp = this.systems.mapper.atlas[(byte)AtlasGroup.Blocks];
			temp.Draw("Grass/S", FVector.Create(100, 100));
			temp.Draw("Grass/H1", FVector.Create(160, 100));
		}
	}
}
