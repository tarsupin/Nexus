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

		private readonly LocalServer localServer;

		public Stopwatch stopwatch;

		public TilemapBool tilemap;
		public Dictionary<byte, Dictionary<uint, DynamicGameObject>> objects;		// objects[LoadOrder][ObjectID] = DynamicGameObject
		public Dictionary<byte, ClassGameObject> classObjects;

		// Level Data
		public LevelFlags flags = new LevelFlags();

		public LevelScene( Systems systems ) : base( systems ) {

			// TODO CLEANUP: Debugging stopwatch should be removed.
			this.stopwatch = new Stopwatch();

			// References
			this.localServer = systems.localServer;

			// Tilemap
			this.tilemap = new TilemapBool(400, 100);		// TODO: Get X,Y grid sizes from the level data.

			// Game Objects
			this.objects = new Dictionary<byte, Dictionary<uint, DynamicGameObject>> {
				[(byte) LoadOrder.Platform] = new Dictionary<uint, DynamicGameObject>(),          // TODO: Change to Platform
				[(byte) LoadOrder.Enemy] = new Dictionary<uint, DynamicGameObject>(),				// TODO: Change to Enemy
				[(byte) LoadOrder.Item] = new Dictionary<uint, DynamicGameObject>(),				// TODO: Change to Item
				[(byte) LoadOrder.TrailingItem] = new Dictionary<uint, DynamicGameObject>(),      // TODO: Change to TrailingItem
				[(byte) LoadOrder.Character] = new Dictionary<uint, DynamicGameObject>(),			// TODO: Change to Character
				[(byte) LoadOrder.Projectile] = new Dictionary<uint, DynamicGameObject>()         // TODO: Change to Projectile
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

		public override void RunTick() {

			// TODO: Change this to the actual frame for this tick.
			byte frame = 0;

			// Update Timer
			this.time.RunTick();

			// Camera Movement
			this.camera.MoveWithInput(this.localServer.MyPlayer.input);

			// Loop through every player and update inputs for this frame tick:
			foreach( var player in this.localServer.players ) {
				player.Value.input.UpdateKeyStates(0);
			}
		}

		public override void Draw() {
			//this.stopwatch.Start();

			ushort startX = this.camera.GridX;
			ushort startY = this.camera.GridY;

			ushort gridX = (ushort) (startX + 29 + 1);
			ushort gridY = (ushort) (startY + 16 + 1);

			int camX = this.camera.posX;
			int camY = this.camera.posY;
			int camRight = camX + this.camera.width;
			int camBottom = camY + this.camera.height;
			
			// Loop through the tilemap data:
			for(ushort y = startY; y <= gridY; y++) {
				for(ushort x = startX; x <= gridX; x++) {

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
						this.classObjects[(byte)idData[0]].Draw((byte)idData[1], x * (byte) TilemapEnum.TileWidth - camX, y * (byte) TilemapEnum.TileHeight - camY);
					};
				};
			}

			// Draw object data:
			this.DrawObjectGroup( this.objects[(byte) LoadOrder.Platform], camX, camY, camRight, camBottom );
			this.DrawObjectGroup( this.objects[(byte) LoadOrder.Enemy], camX, camY, camRight, camBottom );
			this.DrawObjectGroup( this.objects[(byte) LoadOrder.Item], camX, camY, camRight, camBottom );
			this.DrawObjectGroup( this.objects[(byte) LoadOrder.TrailingItem], camX, camY, camRight, camBottom );
			this.DrawObjectGroup( this.objects[(byte) LoadOrder.Character], camX, camY, camRight, camBottom );
			this.DrawObjectGroup( this.objects[(byte) LoadOrder.Projectile], camX, camY, camRight, camBottom );

			// Debugging
			//this.stopwatch.Stop();
			//System.Console.WriteLine("Benchmark: " + this.stopwatch.ElapsedTicks + ", " + this.stopwatch.ElapsedMilliseconds);
		}

		public void DrawObjectGroup( Dictionary<uint, DynamicGameObject> objectGroup, int camX, int camY, int camRight, int camBottom ) {

			// Loop through each object ID in the dictionary:
			foreach( var obj in objectGroup ) {
				FVector pos = obj.Value.pos;

				// Make sure the frame is visible:
				if(pos.X.IntValue < camRight && pos.Y.IntValue < camBottom && pos.X.IntValue + 48 > camX && pos.Y.IntValue > camY) {

					// Custom Rendering Rules
					// TODO HIGH PRIOIRTY: if CUSTOM RENDER RULES, DO CUSTOM RENDER RULES

					// Render Standard Objects
					obj.Value.Draw( camX, camY );
				}
			}
		}

		public void AddToObjects( DynamicGameObject gameObject ) {
			this.objects[(byte)gameObject.Meta.LoadOrder][gameObject.id] = gameObject;
		}
	}
}
