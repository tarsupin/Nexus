using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ToggleBlockYellow : ToggleBlock {

		protected new bool Toggled {
			get { return this.room.flags.toggleGY; }
		}

		public static void TileGenerate(RoomScene room, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the room. If it hasn't, create it.
			if(!room.IsTileGameObjectRegistered((byte) TileEnum.ToggleBlockYellow)) {
				new ToggleBlockYellow(room);
			}

			// Add to Tilemap
			room.tilemap.AddTile(gridX, gridY, (byte) TileEnum.ToggleBlockYellow, subTypeId);
		}

		public ToggleBlockYellow(RoomScene room) : base(room, TileEnum.ToggleBlockYellow) {
			this.Texture = "/Yellow/Block";
		}
	}
}
