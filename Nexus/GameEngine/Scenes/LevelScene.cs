﻿using Nexus.Engine;
using Nexus.Gameplay;
using System;
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
		public Dictionary<byte, Dictionary<ushort, DynamicGameObject>> objects;		// objects[LoadOrder][ObjectID] = DynamicGameObject
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
			this.stopwatch.Start();

			ushort startX = (ushort) Math.Max(this.camera.GridX - 1, 0);
			ushort startY = (ushort) Math.Max(this.camera.GridY - 1, 0);

			ushort gridX = (ushort) (startX + 29 + 2);
			ushort gridY = (ushort) (startY + 16 + 2);

			int camX = this.camera.pos.X.IntValue;
			int camY = this.camera.pos.Y.IntValue;
			
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
						this.classObjects[(byte)idData[0]].Draw((byte)idData[1], (ushort) (x * (byte) TilemapEnum.TileWidth - camX), (ushort) (y * (byte) TilemapEnum.TileHeight - camY));
					};
				};
			}

			// Debugging
			this.stopwatch.Stop();
			//System.Console.WriteLine("Benchmark: " + this.stopwatch.ElapsedTicks + ", " + this.stopwatch.ElapsedMilliseconds);
		}
	}
}
