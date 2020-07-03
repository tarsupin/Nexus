﻿using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Placer : BlockTile {

		public string[] Texture;

		public enum PlacerSubType : byte {
			Up = 0,
			Right = 1,
			Down = 2,
			Left = 3,
		}

		// TODO: Integrate params into Placer.
		// TODO: Integrate behavior of Placer.

		public Placer() : base() {
			this.setupRules = SetupRules.SetupTile;
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.Generator];
			this.tileId = (byte) TileEnum.Placer;
			this.title = "Placer";
			this.description = "Can place objects, items, collectables, etc.";
			this.moveParamSet =  Params.ParamMap["Placer"];
			this.CreateTextures();
		}

		public bool SetupTile(RoomScene room, short gridX, short gridY) {

			//// Track the activations for this cannon.
			//Dictionary<string, short> paramList = room.tilemap.GetParamList(gridX, gridY);

			//// Reject this Cycle if the cannon isn't triggered on at least one beat (considers first beat required)
			//short beat1 = (short)((paramList.ContainsKey("beat1") ? (byte)paramList["beat1"] : 0) - 1);
			//if(beat1 == -1) { return false; }

			//short beat2 = (short)((paramList.ContainsKey("beat2") ? (byte)paramList["beat2"] : 0) - 1);
			//short beat3 = (short)((paramList.ContainsKey("beat3") ? (byte)paramList["beat3"] : 0) - 1);
			//short beat4 = (short)((paramList.ContainsKey("beat4") ? (byte)paramList["beat4"] : 0) - 1);

			//bool[] addToBeat = new bool[4] { false, false, false, false };

			//// Since Cannon Beats check against tempo 8 or 16, we have to run a modulus 4 check to accomodate the QueueEvent.beatEvents.
			//if(beat1 > -1) { addToBeat[beat1 % 4] = true; }
			//if(beat2 > -1) { addToBeat[beat2 % 4] = true; }
			//if(beat3 > -1) { addToBeat[beat3 % 4] = true; }
			//if(beat4 > -1) { addToBeat[beat4 % 4] = true; }

			//// Add All Relevant Beat Events
			//if(addToBeat[0]) { room.queueEvents.AddBeatEvent(this.tileId, (short)gridX, (short)gridY, 0); }
			//if(addToBeat[1]) { room.queueEvents.AddBeatEvent(this.tileId, (short)gridX, (short)gridY, 1); }
			//if(addToBeat[2]) { room.queueEvents.AddBeatEvent(this.tileId, (short)gridX, (short)gridY, 2); }
			//if(addToBeat[3]) { room.queueEvents.AddBeatEvent(this.tileId, (short)gridX, (short)gridY, 3); }

			return true;
		}

		// TODO: Run Placer RunTick()
		public void RunTick(RoomScene room, short gridX, short gridY) {

			// Can only activate Placer when there's a beat frame.
			if(Systems.timer.IsBeatFrame) {

				// TODO: Track the activations for this Placer.
				byte subType = room.tilemap.GetMainSubType(gridX, gridY);

				// TODO: Use the gridId as part of a dictionary or KVP that activates the Placer Objects.

				// Run Placer Activation
				this.ActivatePlacer(room, subType, gridX, gridY);
			}
		}

		//update( time: Timer ): void {

		//	// On every tick, attempt to activate.
		//	if(time.tick <= this.trackTicks) { return; }

		//	this.trackTicks = time.tick;

		//	// Tick must match with the activation cycle, or end here.
		//	if((this.trackTicks * 125) % this.place.cycle !== 0) { return; }

		//	// Verify that children are still in the scene. If not, stop tracking them.
		//	let children = this.children;

		//	for(let i = 0; i < children.length; i++ ) {
		//		let child = children[i];

		//		// If the spawned object no longer exists (was deleted), then remove it from the tracker.
		//		if(!child || !this.sceneArch[child.id]) {
		//			children.splice(i, 1);
		//			i--;

		//		// If the spawned object is no longer on the screen, delete it, and stop tracking.
		//		} else if(!this.scene.activeObjects[this.place.loadOrder][child.id]) {
		//			children[i].destroy();
		//			children.splice(i, 1);
		//			i--;
		//		}
		//	}

		//	// Cannot activate while the placer itself is inactive.
		//	if(!this.emblem.on) { return; }

		//	// Make sure the maximum number of children spawned has not been reached.
		//	if(this.children.length >= this.place.maxChildren) { return ;}

		//	// Run the activation.
		//	this.activate();
		//}

		public void ActivatePlacer(RoomScene room, byte subType, short gridX, short gridY) {

			// Determine Grid Of Placement
			if(subType == (byte) PlacerSubType.Up) { gridY -= 1; }
			else if(subType == (byte) PlacerSubType.Down) { gridY += 1; }
			else if(subType == (byte) PlacerSubType.Left) { gridX -= 1; }
			else if(subType == (byte) PlacerSubType.Right) { gridX += 1; }

			// Convert Grid to Position
			int posX = gridX * (byte) TilemapEnum.TileWidth;
			int posY = gridY * (byte) TilemapEnum.TileHeight;

			// TODO: Add Placer Placement

			//// Confirm that there are no items blocking the placer.
			//let items = FindObjectTouchingArea(this.scene.activeObjects[LoadOrder.Item], posX + 16, posY + 16, 16, 16);
			//if(items.length > 0) { return; }

			//// Check if there are any creatures by the placer; and if so, either kill them (if item), or prevent creation.
			//let enemies = FindObjectTouchingArea(this.scene.activeObjects[LoadOrder.Enemy], posX + 16, posY + 16, 16, 16);
			//if(enemies.length > 0) {
			//for(let i in enemies) {
			//		if(this.place.archetype === Arch.Item) {
			//			(enemies[i] as Enemy).dieKnockout();
			//			this.scene.game.audio.soundList.thudThump.play(0.5);
			//		} else {
			//			return;
			//		}
			//	}
			//}

			//// Confirm that the character isn't blocking the placer.
			//let charBlock = FindObjectTouchingArea(this.scene.activeObjects[LoadOrder.Character], posX, posY - 8, 48, 56);
			//if(charBlock.length > 0) { return; }

			//// Place New Entity
			//const entityID = this.scene.idCounter;
			//new this.place.entClass(this.scene, this.place.subType, posX, posY, { } );

			//const child = this.sceneArch[entityID + 1]; // Get the new entity that was just created.
			//this.scene.addToActiveObjects(child); // Add the child to the scene immediately.
			//this.children.push(child); // Track the child for spawn identification.

			room.PlaySound(Systems.sounds.pop, 0.25f, gridX * (byte)TilemapEnum.TileWidth, gridY * (byte)TilemapEnum.TileHeight);
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture[subType], posX, posY);
		}

		private void CreateTextures() {
			this.Texture = new string[4];
			this.Texture[(byte)PlacerSubType.Up] = "Placer/Up";
			this.Texture[(byte)PlacerSubType.Down] = "Placer/Down";
			this.Texture[(byte)PlacerSubType.Left] = "Placer/Left";
			this.Texture[(byte)PlacerSubType.Right] = "Placer/Right";
		}
	}
}
