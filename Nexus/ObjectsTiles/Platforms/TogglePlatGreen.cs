using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class TogglePlatGreen : TogglePlat {

		protected new bool Toggled {
			get { return this.room.flags.toggleGY; }
		}

		public static void TileGenerate(RoomScene room, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the room. If it hasn't, create it.
			if(!room.IsTileGameObjectRegistered((byte) TileEnum.TogglePlatGreen)) {
				new TogglePlatGreen(room, subTypeId);
			}

			// Add to Tilemap
			room.tilemap.AddTile(gridX, gridY, (byte) TileEnum.TogglePlatGreen, subTypeId);
		}

		public TogglePlatGreen(RoomScene room, byte subTypeId) : base(room, TileEnum.TogglePlatGreen) {
			this.Texture = "/Green/Plat";
		}
	}
}
