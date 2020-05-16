using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Placer : TileGameObject {

		protected PlacerSubType subType;

		public enum PlacerSubType : byte {
			Up = 0,
			Right = 1,
			Down = 2,
			Left = 3,
		}

		// TODO: Integrate params into Placer.
		// TODO: Integrate behavior of Placer.

		public Placer() : base() {
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.Generator];
			this.tileId = (byte) TileEnum.Placer;
			this.title = "Placer";
			this.description = "Can place objects, items, collectables, etc.";
			this.paramSet =  Params.ParamMap["Placer"];
		}

		public override bool RunImpact(RoomScene room, DynamicObject actor, ushort gridX, ushort gridY, DirCardinal dir) {
			return true;
		}

		// TODO: Run Placer RunTick()
		public void RunTick(RoomScene room, ushort gridX, ushort gridY) {

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

		public void ActivatePlacer(RoomScene room, byte subType, ushort gridX, ushort gridY) {

			// Determine Grid Of Placement
			if(subType == (byte) PlacerSubType.Up) { gridY -= 1; }
			else if(subType == (byte) PlacerSubType.Down) { gridY += 1; }
			else if(subType == (byte) PlacerSubType.Left) { gridX -= 1; }
			else if(subType == (byte) PlacerSubType.Right) { gridX += 1; }

			// Convert Grid to Position
			uint posX = (uint) (gridX * (byte) TilemapEnum.TileWidth);
			uint posY = (uint) (gridY * (byte) TilemapEnum.TileWidth);

			// TODO: Add Placer Placement

			//// Confirm that there are no items blocking the placer.
			//let items = findObjectsTouchingArea(this.scene.activeObjects[LoadOrder.Item], posX + 16, posY + 16, 16, 16);
			//if(items.length > 0) { return; }

			//// Check if there are any creatures by the placer; and if so, either kill them (if item), or prevent creation.
			//let enemies = findObjectsTouchingArea(this.scene.activeObjects[LoadOrder.Enemy], posX + 16, posY + 16, 16, 16);
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
			//let charBlock = findObjectsTouchingArea(this.scene.activeObjects[LoadOrder.Character], posX, posY - 8, 48, 56);
			//if(charBlock.length > 0) { return; }

			//// Place New Entity
			//const entityID = this.scene.idCounter;
			//new this.place.entClass(this.scene, this.place.subType, posX, posY, { } );

			//const child = this.sceneArch[entityID + 1]; // Get the new entity that was just created.
			//this.scene.addToActiveObjects(child); // Add the child to the scene immediately.
			//this.children.push(child); // Track the child for spawn identification.

			Systems.sounds.pop.Play(0.25f, 1, 1);
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			if(subType == (byte) PlacerSubType.Up) {
				this.atlas.Draw("Placer/Vertical", posX, posY);
			} else if(subType == (byte) PlacerSubType.Down) {
				this.atlas.DrawFaceDown("Placer/Vertical", posX, posY);
			} else if(subType == (byte) PlacerSubType.Left) {
				this.atlas.DrawFaceDown("Placer/Horizontal", posX, posY);
			} else if(subType == (byte) PlacerSubType.Right) {
				this.atlas.Draw("Placer/Horizontal", posX, posY);
			}
		}
	}
}
