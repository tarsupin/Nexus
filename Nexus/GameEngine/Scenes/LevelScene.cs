using Nexus.Engine;
using Nexus.Gameplay;
using System.Collections.Generic;
using System.Diagnostics;

namespace Nexus.GameEngine {

	public class LevelFlags {
		public bool toggleBR = true;
		public bool toggleGY = true;
	}

	public class LevelScene : Scene {

		public Stopwatch stopwatch;

		public TilemapBool tilemap;
		public Dictionary<byte, Dictionary<ushort, DynamicGameObject>> objects;		// objects[LoadOrder][ObjectID] = DynamicGameObject
		public Dictionary<byte, ClassGameObject> classObjects;

		// Level Data
		public LevelFlags flags = new LevelFlags();

		public LevelScene( Systems systems ) : base( systems ) {

			// TODO CLEANUP: Debugging stopwatch should be removed.
			this.stopwatch = new Stopwatch();

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
			return classObjects.ContainsKey(classId);
		}

		public void RegisterClassGameObject(ClassGameObjectId classId, ClassGameObject cgo ) {
			classObjects[(byte) classId] = cgo;
		}

		public override void Update() {

		}

		public override void Draw() {
			this.stopwatch.Start();

			uint gridX = 29;
			uint gridY = 16;
			
			// Loop through the tilemap data:
			for(ushort y = 0; y <= gridY; y++) {
				for(ushort x = 0; x <= gridX; x++) {

					// Skip if there is no tile present at this tile:
					if(!this.tilemap.IsTilePresent(x, y)) { continue; }
					
					// Scan the Tiles Data at this grid square:
					uint gridId = this.tilemap.GetGridID(x, y);

					// If the tile is a Tile ID
					bool[] tileData = this.tilemap.tiles[gridId];

					if(tileData[0] == true) {

						// Check the .ids dictionary for the Tile Data (e.g. class object, class subType)
						ushort[] idData = this.tilemap.ids[gridId];
						
						// Render the tile with its designated Class Object:
						this.classObjects[(byte)idData[0]].Draw((byte)idData[1], (ushort) (x * (byte) TilemapEnum.TileWidth), (ushort) (y * (byte) TilemapEnum.TileHeight));
					};
				};
			}

			// Debugging
			this.stopwatch.Stop();
			System.Console.WriteLine("Benchmark: " + this.stopwatch.ElapsedTicks + ", " + this.stopwatch.ElapsedMilliseconds);
		}
	}
}
