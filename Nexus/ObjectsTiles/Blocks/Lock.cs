using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Lock : BlockTile {

		public string Texture;

		public Lock() : base() {
			this.Texture = "Lock/Lock";
			this.tileId = (byte)TileEnum.Lock;
			this.title = "Lock";
			this.description = "A key will remove it, along with all neighboring locks.";
		}

		public override bool RunImpact(RoomScene room, DynamicObject actor, ushort gridX, ushort gridY, DirCardinal dir) {

			if(actor is Character) {
				Character character = (Character)actor;

				// Check if the character possesses a key:
				bool hadKey = character.trailKeys.RemoveKey();

				// Unlock if Character had a key.
				if(hadKey) {
					Systems.sounds.unlock.Play();
					this.DestroyLockGroup(room, gridX, gridY);
					return false;
				}
			}

			return base.RunImpact(room, actor, gridX, gridY, dir);
		}

		private void DestroyLockGroup(RoomScene room, ushort gridX, ushort gridY) {

			// Check if the tile exists:
			byte[] td = room.tilemap.GetTileDataAtGrid(gridX, gridY);
			if(td == null) { return; }

			// If the tile is a Lock, destroy it, and then run DestroyLockGroup on it.
			if(td[0] != this.tileId) { return; }

			// Destroy This Lock
			room.tilemap.ClearMainLayer(gridX, gridY);

			// Destroy all Neighbor BGTaps of the same subtype:
			this.DestroyLockGroup(room, (ushort)(gridX - 1), gridY);
			this.DestroyLockGroup(room, (ushort)(gridX + 1), gridY);
			this.DestroyLockGroup(room, gridX, (ushort)(gridY - 1));
			this.DestroyLockGroup(room, gridX, (ushort)(gridY + 1));
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture, posX, posY);
		}
	}
}
