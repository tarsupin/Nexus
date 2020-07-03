using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Nexus.Config;
using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;
using System;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class RoomScene : Scene {

		// References
		public readonly LevelScene scene;
		public readonly byte roomID;
		protected readonly CollideSequence collideSequence;

		// Components
		public RoomExits roomExits;
		public TrackSystem trackSys;
		public ColorToggles colors;
		public QueueEvent queueEvents;
		private ParallaxHandler parallax;
		public ParticleHandler particleHandler;

		// Level Data
		public TilemapLevel tilemap;
		public Dictionary<byte, Dictionary<int, GameObject>> objects;       // objects[LoadOrder][ObjectID] = DynamicObject

		// Object Coordination and Cleanup
		private List<GameObject> markedForAddition;		// A list of objects to be added after the frame's loops have ended.
		private List<GameObject> markedForRemoval;      // A list of objects that will be removed after the frame's loops have ended.

		public RoomScene(LevelScene scene, byte roomID) : base() {

			// References
			this.scene = scene;
			this.roomID = roomID;
			this.collideSequence = new CollideSequence(this);

			// Components that don't need to be rebuilt on room reset.
			this.parallax = ParallaxOcean.CreateOceanParallax(this);
			this.particleHandler = new ParticleHandler(this);
		}

		public void BuildRoom() {

			// Game Objects
			this.objects = new Dictionary<byte, Dictionary<int, GameObject>> {
				[(byte)LoadOrder.Platform] = new Dictionary<int, GameObject>(),
				[(byte)LoadOrder.Enemy] = new Dictionary<int, GameObject>(),
				[(byte)LoadOrder.Item] = new Dictionary<int, GameObject>(),
				[(byte)LoadOrder.TrailingItem] = new Dictionary<int, GameObject>(),
				[(byte)LoadOrder.Character] = new Dictionary<int, GameObject>(),
				[(byte)LoadOrder.Projectile] = new Dictionary<int, GameObject>()
			};

			// Object Coordination and Cleanup
			this.markedForAddition = new List<GameObject>();
			this.markedForRemoval = new List<GameObject>();

			// Build Tilemap with Correct Dimensions
			short xCount, yCount;
			RoomGenerate.DetectRoomSize(Systems.handler.levelContent.data.rooms[roomID.ToString()], out xCount, out yCount);

			this.tilemap = new TilemapLevel(xCount, yCount);

			// Additional Components
			this.colors = new ColorToggles();
			this.trackSys = new TrackSystem();
			this.roomExits = new RoomExits();
			this.queueEvents = new QueueEvent(this);

			// Generate Room Content (Tiles, Objects)
			RoomGenerate.GenerateRoom(this, Systems.handler.levelContent, roomID.ToString());

			// Prepare the Full Track System
			this.trackSys.SetupTrackSystem();
		}

		public override int Width { get { return this.tilemap.InnerWidth; } }
		public override int Height { get { return this.tilemap.InnerHeight; } }

		public override void RunTick() {

			// Update All Objects
			this.RunTickForObjectGroup(this.objects[(byte)LoadOrder.Platform]);
			this.RunTickForObjectGroup(this.objects[(byte)LoadOrder.Enemy]);
			this.RunTickForObjectGroup(this.objects[(byte)LoadOrder.Item]);
			this.RunTickForObjectGroup(this.objects[(byte)LoadOrder.TrailingItem]);
			this.RunTickForCharacterGroup(this.objects[(byte)LoadOrder.Character]);
			this.RunTickForObjectGroup(this.objects[(byte)LoadOrder.Projectile]);

			// Update Components
			this.queueEvents.RunEventSequence();
			this.colors.RunColorTimers();
			this.parallax.RunParallaxTick();
			this.particleHandler.RunParticleTick();

			// Object Coordination & Cleanup
			this.AddObjectsMarkedForAddition();
			this.DestroyObjectsMarkedForRemoval();

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

		private void RunTickForObjectGroup(Dictionary<int, GameObject> objectGroup) {

			// Loop through each object in the dictionary, run it's tick:
			foreach(var obj in objectGroup) {

				obj.Value.RunTick();

				// Run Tile Detection for the Object
				CollideTile.RunTileCollision(obj.Value);
			}
		}

		// Identical to "RunTickForObjectGroup", but with an extra overlap test for Characters.
		// RunTileCollision only detects what you're moving INTO, not what you're on top of.
		// Characters need the additional check to know what they're on top of, such as for Chests, Doors, NPCs, etc.
		private void RunTickForCharacterGroup(Dictionary<int, GameObject> objectGroup) {
			foreach(var obj in objectGroup) {
				Character character = (Character)obj.Value;

				character.RunTick();

				CollideTile.RunTileCollision(character);

				// Determine Tiles Potentially Touched
				short gridX = character.GridX;
				short gridY = character.GridY;
				short gridX2 = character.GridX2;
				short gridY2 = character.GridY2;

				// Run Collision Tests on any tiles beneath the Character (to identify Chests, Doors, NPCs, etc).
				CollideTile.RunGridTest(character, gridX, gridY, DirCardinal.Center);
				
				if(gridX != gridX2) {
					CollideTile.RunGridTest(character, gridX2, gridY, DirCardinal.Center);

					if(gridY != gridY2) {
						CollideTile.RunGridTest(character, gridX, gridY2, DirCardinal.Center);
						CollideTile.RunGridTest(character, gridX2, gridY2, DirCardinal.Center);
					}
				}
				else if(gridY != gridY2) {
					CollideTile.RunGridTest(character, gridX, gridY2, DirCardinal.Center);
				}
			}
		}
		
		private void RunCharacterSpriteTick(Dictionary<int, GameObject> objectGroup) {

			// Loop through each object in the dictionary, run it's tick:
			foreach(var obj in objectGroup) {
				Character character = (Character) obj.Value;

				// TODO HIGH PRIORITY: If activity is active, run tick, otherwise do not.
				character.SpriteTick();
			}
		}

		public override void Draw() {
			//Systems.timer.stopwatch.Start();

			Camera cam = Systems.camera;
			cam.StayBounded((byte)TilemapEnum.GapLeftPixel, this.Width + (byte)TilemapEnum.GapLeftPixel, (byte)TilemapEnum.GapUpPixel, this.Height + (byte)TilemapEnum.GapUpPixel);

			short startX = Math.Max((short)0, cam.GridX);
			short startY = Math.Max((short)0, cam.GridY);

			// Must adjust for the World Gaps (Resistance Barrier)
			if(startX < (byte)TilemapEnum.GapLeft) { startX = (byte)TilemapEnum.GapLeft; }
			if(startY < (byte)TilemapEnum.GapUp) { startY = (byte)TilemapEnum.GapUp; }

			short gridX = (short)(startX + (byte)TilemapEnum.MinWidth + 1); // 30 is view size. +1 is to render the edge.
			short gridY = (short)(startY + (byte)TilemapEnum.MinHeight + 1); // 18 is view size. +1 is to render the edge.

			if(gridX > this.tilemap.XCount) { gridX = (short)(this.tilemap.XCount + (byte)TilemapEnum.GapLeft); } // Must limit to room size.
			if(gridY > this.tilemap.YCount) { gridY = (short)(this.tilemap.YCount + (byte)TilemapEnum.GapUp); } // Must limit to room size.

			// Camera Position
			bool isShaking = cam.IsShaking();
			int camX = cam.posX + (isShaking ? cam.GetCameraShakeOffsetX() : 0);
			int camY = cam.posY + (isShaking ? cam.GetCameraShakeOffsetY() : 0);
			int camRight = camX + cam.width;
			int camBottom = camY + cam.height + (byte)TilemapEnum.TileHeight;

			// Run Parallax Handler
			this.parallax.Draw();

			var tileMap = Systems.mapper.TileDict;

			// Loop through the tilemap data:
			for(short y = gridY; y --> startY; ) {
				short tileYPos = (short) (y * (byte)TilemapEnum.TileHeight - camY);

				for(short x = gridX; x --> startX; ) {

					// Scan the Tiles Data at this grid square:
					byte[] tileData = tilemap.GetTileDataAtGrid(x, y);

					// This occurs when there is no data on the tile (removed, etc).
					if(tileData == null) { continue; }

					// Draw Background Layer
					if(tileData[2] != 0) {
						TileObject tileObj = tileMap[tileData[2]];

						// Render the tile with its designated Class Object:
						if(tileObj is TileObject) {
							tileObj.Draw(this, tileData[3], x * (byte)TilemapEnum.TileWidth - camX, tileYPos);
						};
					}

					// Draw Main Layer
					if(tileData[0] != 0) {
						TileObject tileObj = tileMap[tileData[0]];

						// Render the tile with its designated Class Object:
						if(tileObj is TileObject) {
							tileObj.Draw(this, tileData[1], x * (byte)TilemapEnum.TileWidth - camX, tileYPos);
						};
					}

					// Draw Foreground Layer
					if(tileData[4] != 0) {
						TileObject tileObj = tileMap[tileData[4]];

						// Render the tile with its designated Class Object:
						if(tileObj is TileObject) {
							tileObj.Draw(this, tileData[5], x * (byte)TilemapEnum.TileWidth - camX, tileYPos);
						};
					}
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
			this.particleHandler.Draw();

			// Draw Frame Debugging Aid
			if(DebugConfig.DrawDebugFrames) { this.DrawDebug(camX, camY, camRight, camBottom); }

			// Debugging
			//Systems.timer.stopwatch.Stop();
			//System.Console.WriteLine("Benchmark: " + Systems.timer.stopwatch.ElapsedTicks + ", " + Systems.timer.stopwatch.ElapsedMilliseconds);
		}

		private void DrawObjectGroup( Dictionary<int, GameObject> objectGroup, int camX, int camY, int camRight, int camBottom ) {

			// Loop through each object in the dictionary:
			foreach( var obj in objectGroup ) {
				GameObject oVal = obj.Value;
				
				// Make sure the frame is visible:
				if(oVal.posX < camRight && oVal.posY < camBottom && oVal.posX + oVal.bounds.Right > camX && oVal.posY + oVal.bounds.Bottom > camY) {
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

		private void DrawDebugFrame(Dictionary<int, GameObject> objectGroup, int camX, int camY, int camRight, int camBottom) {

			// Loop through each object in the dictionary:
			foreach(var obj in objectGroup) {
				GameObject oVal = obj.Value;

				// Make sure the object is visible, then draw a debug rectangle over it.
				if(oVal.posX < camRight && oVal.posY < camBottom && oVal.posX + (byte)TilemapEnum.TileWidth > camX && oVal.posY + (byte)TilemapEnum.TileHeight > camY) {
					Systems.spriteBatch.Draw(Systems.tex2dDarkRed, new Rectangle(oVal.posX + oVal.bounds.Left - camX, oVal.posY + oVal.bounds.Top - camY, oVal.bounds.Width, oVal.bounds.Height), Color.White * 0.5f);
				}
			}
		}

		public void AddToScene( GameObject gameObject, bool immediately ) {
			if(immediately) {
				this.objects[(byte)gameObject.Meta.LoadOrder][gameObject.id] = gameObject;
			} else {
				this.markedForAddition.Add(gameObject);
			}
		}

		public void RemoveFromScene( GameObject gameObject, bool immediately = false ) {
			if(immediately) {
				this.objects[(byte)gameObject.Meta.LoadOrder].Remove(gameObject.id);
			} else {
				this.markedForRemoval.Add(gameObject);
			}
		}

		// We use this method to add objects outside of the loops they're created in. This is to allow enumerators to continue working.
		private void AddObjectsMarkedForAddition() {

			// Only continue if there are items marked for removal:
			if(this.markedForAddition.Count == 0) { return; }

			// Loop through the list of objects to destroy
			foreach(GameObject obj in this.markedForAddition) {
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
			foreach(GameObject obj in this.markedForRemoval) {
				this.objects[(byte)obj.Meta.LoadOrder].Remove(obj.id);
			}

			// Clear the list of any objects being marked for removal.
			this.markedForRemoval.Clear();
		}

		// Play a sound, but only if it's local to the character, and the distance relative to them.
		public void PlaySound(SoundEffect sound, float volume, int posX, int posY) {

			// Do not play this sound if they're not in the same room.
			if(Systems.localServer.MyCharacter.room.roomID != this.roomID) { return; }

			// Determine the approximate distance from the character / screen.
			int midX = Systems.camera.posX + Systems.camera.halfWidth;
			float pan = 0f;

			if(midX > posX) {
				int diff = midX - posX;
				if(diff > 1400) { return; }
				if(diff > 250) { pan = 0 - Spectrum.GetPercentFromValue(diff, 250, 1400); }
			}

			else if(midX < posX) {
				int diff = posX - midX;
				if(diff > 1400) { return; }
				if(diff > 250) { pan = Spectrum.GetPercentFromValue(diff, 250, 1400); }
			}

			if(Math.Abs(pan) > 0.1) {
				volume = Math.Min(volume, volume * (1f - Math.Abs(pan)));
			}

			// Play the sound:
			sound.Play(volume, 0f, pan);
		}
	}
}
