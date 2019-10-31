using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ToggleBlockRed : ToggleBlock {

		protected new bool Toggled {
			get { return this.room.flags.toggleBR; }
		}

		public static void TileGenerate(RoomScene room, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the room. If it hasn't, create it.
			if(!room.IsClassGameObjectRegistered((byte) TileGameObjectId.ToggleBlockRed)) {
				new ToggleBlockRed(room);
			}

			// Add to Tilemap
			room.tilemap.AddTile(gridX, gridY, (byte) TileGameObjectId.ToggleBlockRed, subTypeId);
		}

		public ToggleBlockRed(RoomScene room) : base(room, TileGameObjectId.ToggleBlockRed) {
			this.Texture = "/Red/Block";
		}
	}
}
