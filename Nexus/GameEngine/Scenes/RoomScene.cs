using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nexus.Config;
using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;
using System;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class RoomFlags {
		public bool toggleBR = true;
		public bool toggleGY = true;
	}

	public class RoomScene : Scene {

		// References
		public readonly LevelScene scene;
		protected readonly CollideSequence collideSequence;

		// Components
		private ParallaxHandler parallax;
		public ParticleHandler particles;

		// Level Data
		public TilemapBool tilemap;
		public Dictionary<byte, Dictionary<uint, DynamicGameObject>> objects;		// objects[LoadOrder][ObjectID] = DynamicGameObject
		public Dictionary<byte, TileGameObject> tileObjects;						// Tracks the tiles in the room (1 class per type)

		public RoomFlags flags = new RoomFlags();

		// Object Coordination and Cleanup
		private readonly List<DynamicGameObject> markedForAddition;		// A list of objects to be added after the frame's loops have ended.
		private readonly List<DynamicGameObject> markedForRemoval;		// A list of objects that will be removed after the frame's loops have ended.

		public RoomScene(LevelScene scene, string roomID) : base() {

			// References
			this.scene = scene;
			this.collideSequence = new CollideSequence(this);

			// Game Objects
			this.objects = new Dictionary<byte, Dictionary<uint, DynamicGameObject>> {
				[(byte) LoadOrder.Platform] = new Dictionary<uint, DynamicGameObject>(),
				[(byte) LoadOrder.Enemy] = new Dictionary<uint, DynamicGameObject>(),
				[(byte) LoadOrder.Item] = new Dictionary<uint, DynamicGameObject>(),
				[(byte) LoadOrder.TrailingItem] = new Dictionary<uint, DynamicGameObject>(),
				[(byte) LoadOrder.Character] = new Dictionary<uint, DynamicGameObject>(),
				[(byte) LoadOrder.Projectile] = new Dictionary<uint, DynamicGameObject>()
			};

			// Object Coordination and Cleanup
			this.markedForAddition = new List<DynamicGameObject>();
			this.markedForRemoval = new List<DynamicGameObject>();

			// Game Class Objects
			this.tileObjects = new Dictionary<byte, TileGameObject>();

			// Build Tilemap with Correct Dimensions
			ushort xCount, yCount;
			RoomGenerate.DetermineRoomSize(Systems.handler.levelContent.data.room[roomID], out xCount, out yCount);

			this.tilemap = new TilemapBool(xCount, yCount);

			// Generate Room Content (Tiles, Objects)
			RoomGenerate.GenerateRoom(this, Systems.handler.levelContent, roomID);

			// Additional Components
			this.parallax = ParallaxOcean.CreateOceanParallax(this);
			this.particles = new ParticleHandler(this);
		}

		public override int Width { get { return this.tilemap.Width; } }
		public override int Height { get { return this.tilemap.Height; } }

		public override void RunTick() {

			// Update All Objects
			this.RunTickForObjectGroup(this.objects[(byte)LoadOrder.Platform]);
			this.RunTickForObjectGroup(this.objects[(byte)LoadOrder.Enemy]);
			this.RunTickForObjectGroup(this.objects[(byte)LoadOrder.Item]);
			this.RunTickForObjectGroup(this.objects[(byte)LoadOrder.TrailingItem]);
			this.RunTickForObjectGroup(this.objects[(byte)LoadOrder.Character]);
			this.RunTickForObjectGroup(this.objects[(byte)LoadOrder.Projectile]);

			// Object Coordination & Cleanup
			this.AddObjectsMarkedForAddition();
			this.DestroyObjectsMarkedForRemoval();

			// Update Components
			this.parallax.RunParallaxTick();
			this.particles.RunParticleTick();

			// Camera Movement
			Character MyCharacter = Systems.localServer.MyCharacter;

			if(MyCharacter is Character) {
				Systems.camera.Follow(MyCharacter.posX + 80, MyCharacter.posY, 50, 50);
			}

			// Run Collisions
			this.collideSequence.RunCollisionSequence();

			// Run Important Sprite Changes and Animations (like Character)
			// We do this here because collisions may have influenced how they change.
			this.RunCharacterSpriteTick(this.objects[(byte)LoadOrder.Character]);
		}

		public void RunTickForObjectGroup(Dictionary<uint, DynamicGameObject> objectGroup) {

			// Loop through each object in the dictionary, run it's tick:
			foreach(var obj in objectGroup) {

				obj.Value.RunTick();

				// Run Tile Detection for the Object
				// TODO: Eventually check 1x1 size, and replace with a tile detection that can account for larger sets if it's not 1x1 tile sized.
				CollideTile.RunQuadrantDetection(this, obj.Value);
			}
		}

		public void RunCharacterSpriteTick(Dictionary<uint, DynamicGameObject> objectGroup) {

			// Loop through each object in the dictionary, run it's tick:
			foreach(var obj in objectGroup) {
				Character character = (Character) obj.Value;

				// TODO HIGH PRIORITY: If activity is active, run tick, otherwise do not.
				character.SpriteTick();
			}
		}

		public override void Draw() {
			//this.stopwatch.Start();

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

			// Run Parallax Handler
			this.parallax.Draw();

			var tileMap = Systems.mapper.TileMap;

			// Loop through the tilemap data:
			for(int y = startY; y <= gridY; y++) {
				int tileYPos = y * (byte)TilemapEnum.TileHeight - camY;

				for(int x = startX; x <= gridX; x++) {

					// Scan the Tiles Data at this grid square:
					uint gridId = this.tilemap.GetGridID((ushort) x, (ushort) y);

					byte[] tileData = this.tilemap.GetTileDataAtGridID(gridId);

					if(tileData == null) {
						// TODO HIGH PRIOIRTY: This block should never run; tileData should never be null.
						// Remove any invalid options here.
						// Delete this block once the TileMap has been completed.
						continue;
					}

					TileGameObject tileObj = tileMap[tileData[0]];

					// Render the tile with its designated Class Object:
					if(tileObj is TileGameObject) {
						tileObj.Draw(this, tileData[1], x * (byte) TilemapEnum.TileWidth - camX, tileYPos);
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

			// Draw Particles
			this.particles.Draw();

			// Draw Frame Debugging Aid
			if(DebugConfig.DrawDebugFrames) { this.DrawDebug(camX, camY, camRight, camBottom); }

			// Debugging
			//this.stopwatch.Stop();
			//System.Console.WriteLine("Benchmark: " + this.stopwatch.ElapsedTicks + ", " + this.stopwatch.ElapsedMilliseconds);
		}

		private void DrawObjectGroup( Dictionary<uint, DynamicGameObject> objectGroup, int camX, int camY, int camRight, int camBottom ) {

			// Loop through each object in the dictionary:
			foreach( var obj in objectGroup ) {
				DynamicGameObject oVal = obj.Value;

				// Make sure the frame is visible:
				if(oVal.posX < camRight && oVal.posY < camBottom && oVal.posX + 48 > camX && oVal.posY > camY) {
					oVal.Draw( camX, camY );
				}
			}
		}

		private void DrawDebug(int camX, int camY, int camRight, int camBottom) {

			// Loop through all load orders in the draw debug list:
			foreach(LoadOrder order in DebugConfig.DrawDebugLoadOrders) {

				// Loop through all game objects and draw frame boundary.
				this.DrawDebugFrame(this.objects[(byte)order], camX, camY, camRight, camBottom);
			}
		}

		private void DrawDebugFrame(Dictionary<uint, DynamicGameObject> objectGroup, int camX, int camY, int camRight, int camBottom) {

			// Loop through each object in the dictionary:
			foreach(var obj in objectGroup) {
				DynamicGameObject oVal = obj.Value;

				// Make sure the object is visible, then draw a debug rectangle over it.
				if(oVal.posX < camRight && oVal.posY < camBottom && oVal.posX + 48 > camX && oVal.posY > camY) {
					Texture2D rect = new Texture2D(Systems.graphics.GraphicsDevice, 1, 1);
					rect.SetData(new[] { Color.DarkRed });
					Systems.spriteBatch.Draw(rect, new Rectangle(oVal.posX + oVal.bounds.Left - camX, oVal.posY + oVal.bounds.Top - camY, oVal.bounds.Width, oVal.bounds.Height), Color.White * 0.5f);
				}
			}
		}

		public void AddToScene( DynamicGameObject gameObject, bool immediately ) {
			if(immediately) {
				this.objects[(byte)gameObject.Meta.LoadOrder][gameObject.id] = gameObject;
			} else {
				this.markedForAddition.Add(gameObject);
			}
		}

		public void RemoveFromScene( DynamicGameObject gameObject ) {
			this.markedForRemoval.Add(gameObject);
		}

		// We use this method to add objects outside of the loops they're created in. This is to allow enumerators to continue working.
		private void AddObjectsMarkedForAddition() {

			// Only continue if there are items marked for removal:
			if(this.markedForAddition.Count == 0) { return; }

			// Loop through the list of objects to destroy
			foreach(DynamicGameObject obj in this.markedForAddition) {
				this.objects[(byte)obj.Meta.LoadOrder][obj.id] = obj;
			}

			// Clear the list of any objects being marked for removal.
			this.markedForAddition.Clear();
		}

		// We use this method to destroy objects outside of the loops they're called in. This is to allow enumerators to continue working.
		private void DestroyObjectsMarkedForRemoval() {

			// Only continue if there are items marked for removal:
			if(this.markedForRemoval.Count == 0) { return; }

			// Loop through the list of objects to destroy
			foreach(DynamicGameObject obj in this.markedForRemoval) {
				this.objects[(byte)obj.Meta.LoadOrder].Remove(obj.id);
			}

			// Clear the list of any objects being marked for removal.
			this.markedForRemoval.Clear();
		}

		public void RestartRoom() {

			// Toggle Resets
			this.flags.toggleBR = true;
			this.flags.toggleGY = true;

			// Regenerate Room
			// this.SpawnRoom(posX, posY, roomId);
		}
	}
}
