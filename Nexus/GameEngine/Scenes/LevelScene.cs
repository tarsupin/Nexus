using Nexus.Config;
using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;
using System.Collections.Generic;
using System.Diagnostics;

namespace Nexus.GameEngine {

	public class LevelFlags {
		public bool toggleBR = true;
		public bool toggleGY = true;
	}

	public class LevelScene : Scene {

		// References
		protected readonly LocalServer localServer;
		protected readonly CollideSequence collideSequence;
		public Stopwatch stopwatch;

		// Level Data
		public TilemapBool tilemap;
		public Dictionary<byte, Dictionary<uint, DynamicGameObject>> objects;		// objects[LoadOrder][ObjectID] = DynamicGameObject
		public Dictionary<byte, TileGameObject> tileObjects;

		public LevelFlags flags = new LevelFlags();

		// Level Cleanup
		private List<DynamicGameObject> markedForRemoval;		// A list of objects that will be removed.

		public LevelScene() : base() {

			// TODO CLEANUP: Debugging stopwatch should be removed.
			this.stopwatch = new Stopwatch();

			// References
			this.localServer = Systems.localServer;
			this.collideSequence = new CollideSequence(this);

			// Important Components
			this.tilemap = new TilemapBool(this, 400, 100);       // TODO: Get X,Y grid sizes from the level data.
			this.camera = new Camera(this);

			// Game Objects
			this.objects = new Dictionary<byte, Dictionary<uint, DynamicGameObject>> {
				[(byte) LoadOrder.Platform] = new Dictionary<uint, DynamicGameObject>(),
				[(byte) LoadOrder.Enemy] = new Dictionary<uint, DynamicGameObject>(),
				[(byte) LoadOrder.Item] = new Dictionary<uint, DynamicGameObject>(),
				[(byte) LoadOrder.TrailingItem] = new Dictionary<uint, DynamicGameObject>(),
				[(byte) LoadOrder.Character] = new Dictionary<uint, DynamicGameObject>(),
				[(byte) LoadOrder.Projectile] = new Dictionary<uint, DynamicGameObject>()
			};

			// Cleanup
			this.markedForRemoval = new List<DynamicGameObject>();

			// Game Class Objects
			this.tileObjects = new Dictionary<byte, TileGameObject>();

			// Generate Room 0
			Systems.handler.level.generate.GenerateRoom(this, "0");
		}

		public override int Width { get { return this.tilemap.Width; } }
		public override int Height { get { return this.tilemap.Height; } }

		public void SpawnRoom() {

		}

		// Class Game Objects
		public bool IsClassGameObjectRegistered( byte classId ) {
			return tileObjects.ContainsKey(classId);
		}

		public void RegisterClassGameObject(TileGameObjectId classId, TileGameObject cgo ) {
			tileObjects[(byte) classId] = cgo;
		}

		public override void RunTick() {

			// TODO HIGH PRIORITY: Change this to the actual frame for this tick.
			byte frame = 0;

			// Loop through every player and update inputs for this frame tick:
			foreach(var player in this.localServer.players) {
				player.Value.input.UpdateKeyStates(0);
			}

			// If we're in debug mode and want to run every tick by control:
			if(DebugConfig.TickSpeed != (byte) DebugTickSpeed.StandardSpeed) {

				switch(DebugConfig.TickSpeed) {

					case DebugTickSpeed.HalfSpeed:
						DebugConfig.trackTicks++;
						if(DebugConfig.trackTicks % 2 != 0) { return; }
						break;

					case DebugTickSpeed.QuarterSpeed:
						DebugConfig.trackTicks++;
						if(DebugConfig.trackTicks % 4 != 0) { return; }
						break;

					case DebugTickSpeed.EighthSpeed:
						DebugConfig.trackTicks++;
						if(DebugConfig.trackTicks % 8 != 0) { return; }
						break;

					case DebugTickSpeed.WhenYPressed:
						if(!this.localServer.MyPlayer.input.isPressed(IKey.YButton)) { return; }
						break;

					case DebugTickSpeed.WhileYHeld:
						if(!this.localServer.MyPlayer.input.isDown(IKey.YButton)) { return; }
						break;

					case DebugTickSpeed.WhileYHeldSlow:
						if(!this.localServer.MyPlayer.input.isDown(IKey.YButton)) { return; }
						DebugConfig.trackTicks++;
						if(DebugConfig.trackTicks % 4 != 0) { return; }
						break;
				}
			}

			// Update Timer
			Systems.timer.RunTick();

			// Update All Objects
			this.RunTickForObjectGroup(this.objects[(byte)LoadOrder.Platform]);
			this.RunTickForObjectGroup(this.objects[(byte)LoadOrder.Enemy]);
			this.RunTickForObjectGroup(this.objects[(byte)LoadOrder.Item]);
			this.RunTickForObjectGroup(this.objects[(byte)LoadOrder.TrailingItem]);
			this.RunTickForObjectGroup(this.objects[(byte)LoadOrder.Character]);
			this.RunTickForObjectGroup(this.objects[(byte)LoadOrder.Projectile]);

			// Object Cleanup
			this.DestroyObjectsMarkedForRemoval();

			// Camera Movement
			Character MyCharacter = this.localServer.MyCharacter;
			if(MyCharacter is Character) {
				this.camera.Follow(MyCharacter.posX + 80, MyCharacter.posY, 50, 50);
			}

			//this.camera.MoveWithInput(this.localServer.MyPlayer.input);

			// Run Collisions
			this.collideSequence.RunCollisionSequence();

			// Run Important Sprite Changes and Animations (like Character)
			// We do this here because collisions may have influenced how they change.
			this.RunCharacterSpriteTick(this.objects[(byte)LoadOrder.Character]);
		}

		public void RunTickForObjectGroup(Dictionary<uint, DynamicGameObject> objectGroup) {

			// Loop through each object in the dictionary, run it's tick:
			foreach(var obj in objectGroup) {

				// TODO HIGH PRIORITY: If activity is active, run tick, otherwise do not.
				obj.Value.RunTick();

				// Run Tile Detection for the Object
				// TODO: Eventually check 1x1 size, and replace with a tile detection that can account for larger sets if it's not 1x1 tile sized.
				CollideTile.RunQuadrantDetection(obj.Value);
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

			ushort startX = this.camera.GridX;
			ushort startY = this.camera.GridY;

			ushort gridX = (ushort) (startX + 29 + 1);
			ushort gridY = (ushort) (startY + 16 + 1);

			// Camera Position
			bool isShaking = camera.IsShaking();
			int camX = this.camera.posX + (isShaking ? this.camera.GetCameraShakeOffsetX() : 0);
			int camY = this.camera.posY + (isShaking ? this.camera.GetCameraShakeOffsetY() : 0); ;
			int camRight = camX + this.camera.width;
			int camBottom = camY + this.camera.height;

			// Run Parallax Handler
			//if(this.parallax) { this.parallax.render(); }		// TODO HIGH PRIORITY: PARALLAX

			// Loop through the tilemap data:
			for(ushort y = startY; y <= gridY; y++) {
				int tileYPos = y * (byte)TilemapEnum.TileHeight - camY;

				for(ushort x = startX; x <= gridX; x++) {

					// Scan the Tiles Data at this grid square:
					uint gridId = this.tilemap.GetGridID(x, y);

					TileGameObject tileObj = this.tilemap.GetTileAtGridID(gridId);

					// Render the tile with its designated Class Object:
					if(tileObj is TileGameObject) {
						byte subType = this.tilemap.GetSubTypeAtGridID(gridId);
						tileObj.Draw(subType, x * (byte) TilemapEnum.TileWidth - camX, tileYPos);
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

			// Loop through each object in the dictionary:
			foreach( var obj in objectGroup ) {
				DynamicGameObject oVal = obj.Value;

				// Make sure the frame is visible:
				if(oVal.posX < camRight && oVal.posY < camBottom && oVal.posX + 48 > camX && oVal.posY > camY) {

					// Custom Rendering Rules
					// TODO HIGH PRIOIRTY: if CUSTOM RENDER RULES, DO CUSTOM RENDER RULES

					// Render Standard Objects
					oVal.Draw( camX, camY );
				}
			}
		}

		public void AddToObjects( DynamicGameObject gameObject ) {
			this.objects[(byte)gameObject.Meta.LoadOrder][gameObject.id] = gameObject;
		}

		public void DestroyObject( DynamicGameObject gameObject ) {
			this.markedForRemoval.Add(gameObject);
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

		public void RunCharacterDeath( Character character ) {
			// TODO UI - Reset coin counter (if character was self)
			// this.coinIcon.text.setText("0"); // Reset Coin Counter
			// TODO HIGH PRIORITY:
			// this.RestartLevel();		// true if all players are just self. for multiplayer, this changes... maybe a new scene for multiplayer?
		}

		public void RestartLevel() {
			// Also had params: posX: number = null, posY: number = null, roomId: number = null

			// Toggle Resets
			// TODO HIGH PRIORITY:
			// this.toggleRedBlue = true;
			// this.toggleGreenYellow = true;
			// this.toggleConveyor = true;

			// Regenerate Room
			// this.SpawnRoom(posX, posY, roomId);


			// OLD CODE
			//// Timer Reset
			//this.time.unpause(); // Make sure timer is unpaused.
			//this.time.reset();

			//// Reset Character Status
			//// The character may preserve suits or abilities that track timestamps. Need to reset these.
			//this.character.status.reset();
			//this.character.stats.reset();
			//this.character.action = new StallAction(this.character, 250);

			//// UI Resets
			//let status = this.character.status;
			//// TODO UI
			//// if(this.healthIcons) { this.healthIcons.updateIcons( status.health, status.armor ); }
			//// if(this.powerAttIcon) { this.powerAttIcon.setText(""); }
		}

	}
}
