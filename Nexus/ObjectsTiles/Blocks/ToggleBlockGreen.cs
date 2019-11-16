using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class ToggleBlockGreen : ToggleBlock {

		protected new bool Toggled {
			get { return this.room.flags.toggleGY; }
		}

		public static void TileGenerate(RoomScene room, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the room. If it hasn't, create it.
			if(!room.IsTileGameObjectRegistered((byte) TileEnum.ToggleBlockGreen)) {
				new ToggleBlockGreen(room);
			}

			// Add to Tilemap
			room.tilemap.AddTile(gridX, gridY, (byte) TileEnum.ToggleBlockGreen, subTypeId);
		}

		public ToggleBlockGreen(RoomScene room) : base(room, TileEnum.ToggleBlockGreen) {
			this.Texture = "/Green/Block";
		}
	}
}
