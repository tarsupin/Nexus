using Nexus.Engine;
using Nexus.Gameplay;
using System;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class EditorRoomScene : Scene {

		// References
		public readonly EditorScene scene;

		// Editor Data
		public TilemapBool tilemap;
		public Dictionary<byte, Dictionary<uint, DynamicGameObject>> objects;		// objects[LoadOrder][ObjectID] = DynamicGameObject
		public Dictionary<byte, TileGameObject> tileObjects;						// Tracks the tiles in the room (1 class per type)

		public EditorRoomScene(EditorScene scene, string roomID) : base() {

			// References
			this.scene = scene;

			// TODO HIGH PRIORITY: Don't need game objects.

			// Game Objects
			this.objects = new Dictionary<byte, Dictionary<uint, DynamicGameObject>> {
				[(byte) LoadOrder.Platform] = new Dictionary<uint, DynamicGameObject>(),
				[(byte) LoadOrder.Enemy] = new Dictionary<uint, DynamicGameObject>(),
				[(byte) LoadOrder.Item] = new Dictionary<uint, DynamicGameObject>(),
				[(byte) LoadOrder.TrailingItem] = new Dictionary<uint, DynamicGameObject>(),
				[(byte) LoadOrder.Character] = new Dictionary<uint, DynamicGameObject>(),
				[(byte) LoadOrder.Projectile] = new Dictionary<uint, DynamicGameObject>()
			};

			// Game Class Objects
			this.tileObjects = new Dictionary<byte, TileGameObject>();

			// Build Tilemap with Correct Dimensions
			ushort xCount, yCount;
			EditorRoomGenerate.DetermineRoomSize(Systems.handler.levelContent.data.room[roomID], out xCount, out yCount);

			this.tilemap = new TilemapBool(xCount, yCount);

			// Generate Room Content (Tiles, Objects)
			EditorRoomGenerate.GenerateRoom(this, Systems.handler.levelContent, roomID);
		}

		public override int Width { get { return this.tilemap.Width; } }
		public override int Height { get { return this.tilemap.Height; } }

		public override void RunTick() {

			// Camera Movement
			Systems.camera.MoveWithInput(Systems.localServer.MyPlayer.input);
		}

		public override void Draw() {
			Camera cam = Systems.camera;

			int startX = Math.Max(0, cam.GridX);
			int startY = Math.Max(0, cam.GridY);

			ushort gridX = (ushort) (startX + 29 + 1); // 29 is view size. +1 is to render the edge.
			ushort gridY = (ushort) (startY + 18 + 1); // 18 is view size. +1 is to render the edge.

			if(gridX > this.tilemap.XCount) { gridX = this.tilemap.XCount; } // Must limit to room size (due to the +1)
			if(gridY > this.tilemap.YCount) { gridY = this.tilemap.YCount; } // Must limit to room size (due to the +1)

			// Camera Position
			bool isShaking = cam.IsShaking();
			int camX = cam.posX + (isShaking ? cam.GetCameraShakeOffsetX() : 0);
			int camY = cam.posY + (isShaking ? cam.GetCameraShakeOffsetY() : 0); ;
			int camRight = camX + cam.width;
			int camBottom = camY + cam.height;

			// Loop through the tilemap data:
			for(int y = startY; y <= gridY; y++) {
				int tileYPos = y * (byte)TilemapEnum.TileHeight - camY;

				for(int x = startX; x <= gridX; x++) {

					// Scan the Tiles Data at this grid square:
					uint gridId = this.tilemap.GetGridID((ushort) x, (ushort) y);

					byte[] tileData = this.tilemap.GetTileDataAtGridID(gridId);

					// Render the tile with its designated Class Object:
					if(tileData is null == false) {
						byte subType = tileData[1];

						// TODO HIGH PRIORITY: Draw tile based on its tile ID.
						//tileData.Draw(subType, x * (byte) TilemapEnum.TileWidth - camX, tileYPos);
					};
				};
			}
		}
	}
}
