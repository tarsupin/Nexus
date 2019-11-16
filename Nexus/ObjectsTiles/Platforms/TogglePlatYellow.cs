using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class TogglePlatYellow : TogglePlat {

		protected new bool Toggled {
			get { return this.room.flags.toggleGY; }
		}

		public static void TileGenerate(RoomScene room, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the room. If it hasn't, create it.
			if(!room.IsTileGameObjectRegistered((byte) TileEnum.TogglePlatYellow)) {
				new TogglePlatYellow(room, subTypeId);
			}

			// Add to Tilemap
			room.tilemap.AddTile(gridX, gridY, (byte) TileEnum.TogglePlatYellow, subTypeId);
		}

		public TogglePlatYellow(RoomScene room, byte subTypeId) : base(room, TileEnum.TogglePlatYellow) {
			this.Texture = "/Yellow/Plat";
		}
	}
}
