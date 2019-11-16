using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class TogglePlatBlue : TogglePlat {

		protected new bool Toggled {
			get { return this.room.flags.toggleBR; }
		}

		public static void TileGenerate(RoomScene room, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the room. If it hasn't, create it.
			if(!room.IsTileGameObjectRegistered((byte) TileEnum.TogglePlatBlue)) {
				new TogglePlatBlue(room, subTypeId);
			}

			// Add to Tilemap
			room.tilemap.AddTile(gridX, gridY, (byte) TileEnum.TogglePlatBlue, subTypeId);
		}

		public TogglePlatBlue(RoomScene room, byte subTypeId) : base(room, TileEnum.TogglePlatBlue) {
			this.Texture = "/Blue/Plat";
		}
	}
}
