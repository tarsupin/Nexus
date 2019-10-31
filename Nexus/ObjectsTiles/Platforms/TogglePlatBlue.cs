using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class TogglePlatBlue : TogglePlat {

		protected new bool Toggled {
			get { return this.room.flags.toggleBR; }
		}

		public static void TileGenerate(RoomScene room, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the room. If it hasn't, create it.
			if(!room.IsClassGameObjectRegistered((byte) TileGameObjectId.TogglePlatBlue)) {
				new TogglePlatBlue(room, subTypeId);
			}

			// Add to Tilemap
			room.tilemap.AddTile(gridX, gridY, (byte) TileGameObjectId.TogglePlatBlue, subTypeId);
		}

		public TogglePlatBlue(RoomScene room, byte subTypeId) : base(room, TileGameObjectId.TogglePlatBlue) {
			this.Texture = "/Blue/Plat";
		}
	}
}
