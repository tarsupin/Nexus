using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Leaf : BlockTile {

		public string[] Texture;

		public enum LeafSubType : byte {
			Basic = 0,
			Reform = 1,

			// Invisible SubTypes at +10 the original value. When set, the Draw() method won't draw them; but this maintains their original nature.
			InvisibleBasic = 10,
			InvisibleReform = 11,

			// Untouchable SubTypes at +20 the original value. When set, the tile has no collision.
			UntouchableReform = 21,
		}

		private enum LeafTriggerEvent : byte {
			BreakApart = 0,
			Reform = 1,
		}

		public Leaf() : base() {
			this.CreateTextures();
			this.tileId = (byte)TileEnum.Leaf;

			// Helper Texts
			this.titles = new string[2];
			this.titles[(byte)LeafSubType.Basic] = "Leaf Block";
			this.titles[(byte)LeafSubType.Reform] = "Reforming Leaf Block";

			this.descriptions = new string[2];
			this.descriptions[(byte)LeafSubType.Basic] = "Shatters a short duration after standing on it.";
			this.descriptions[(byte)LeafSubType.Reform] = "Shatters a short duration after standing on it, but reforms seconds later.";
		}

		public override bool RunImpact(RoomScene room, DynamicObject actor, ushort gridX, ushort gridY, DirCardinal dir) {

			byte subType = room.tilemap.GetMainSubType(gridX, gridY);

			// If the SubType is over 20 (Untouchable), don't run any collisions. It's in an invisible and untouchable state.
			if(subType > 20) { return false; }

			if(subType < 10) {

				// Destroy Leaf
				if(dir == DirCardinal.Up) {
					BlockTile.BreakApart(room, gridX, gridY);
					ExplodeEmitter.BoxExplosion(room, "Particles/Leaf", gridX * (byte)TilemapEnum.TileWidth + (byte)TilemapEnum.HalfWidth, gridY * (byte)TilemapEnum.TileHeight + (byte)TilemapEnum.HalfHeight);
					Systems.sounds.thudTap.Play();
				}

				// Begin Shaking. Add a Queue for 1 second that will break the leaf block.
				else if(dir == DirCardinal.Down) {
					room.tilemap.SetTileSubType(gridX, gridY, (byte)(subType + 10));
					room.queueEvents.AddEvent(Systems.timer.Frame + 60, this.tileId, (short)gridX, (short)gridY, (byte)LeafTriggerEvent.BreakApart);
				}
			}

			return base.RunImpact(room, actor, gridX, gridY, dir);
		}

		// Trigger Event: BeginShake, Break Apart, Reform
		public override bool TriggerEvent(RoomScene room, ushort gridX, ushort gridY, short triggerType = 0, short val2 = 0) {

			// Break Apart Event
			if(triggerType == (byte) LeafTriggerEvent.BreakApart) {
				BlockTile.BreakApart(room, gridX, gridY);
				ExplodeEmitter.BoxExplosion(room, "Particles/Leaf", gridX * (byte)TilemapEnum.TileWidth + (byte)TilemapEnum.HalfWidth, gridY * (byte)TilemapEnum.TileHeight + (byte)TilemapEnum.HalfHeight);
				Systems.sounds.thudTap.Play();
			}

			//// Shake Event
			//else if(triggerType == (byte) LeafTriggerEvent.BeginShake) {

			//}

			//// If Button is Blue-Red Switch
			//if(this.toggleBR) {
			//	room.tilemap.SetMainTile(gridX, gridY, (byte)TileEnum.ButtonTimedBRUp, 0);
			//} else {
			//	room.tilemap.SetMainTile(gridX, gridY, (byte)TileEnum.ButtonTimedGYUp, 0);
			//}

			//// Toggle the color-toggle that matches this tap type.
			//room.colors.ToggleColor(this.toggleBR);

			return true;
		}

		private void CreateTextures() {
			this.Texture = new string[2];
			this.Texture[(byte) LeafSubType.Basic] = "Leaf/Basic";
			this.Texture[(byte) LeafSubType.Reform] = "Leaf/Reform";
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			
			// Don't Render any subtypes of 10+ (Invisible)
			if(subType >= 10) { return; }

			this.atlas.Draw(this.Texture[subType], posX, posY);
		}
	}
}
