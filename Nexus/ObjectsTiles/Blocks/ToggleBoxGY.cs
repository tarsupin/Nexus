using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ToggleBoxGY : ToggleBlock {
		 
		protected new bool Toggled {
			get { return this.room.flags.toggleGY; }
		}

		public static void TileGenerate(RoomScene room, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the room. If it hasn't, create it.
			if(!room.IsClassGameObjectRegistered((byte) TileGameObjectId.ToggleBoxGY)) {
				new ToggleBoxGY(room);
			}

			// Add to Tilemap
			room.tilemap.AddTile(gridX, gridY, (byte) TileGameObjectId.ToggleBoxGY, subTypeId);
		}

		public ToggleBoxGY(RoomScene room) : base(room, TileGameObjectId.ToggleBoxGY) {
			this.Texture = "/BoxGY";
		}
	}
}
