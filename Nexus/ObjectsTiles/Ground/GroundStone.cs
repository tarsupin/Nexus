using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class GroundStone : Ground {

		public static void TileGenerate(RoomScene room, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the room. If it hasn't, create it.
			if(!room.IsTileGameObjectRegistered((byte) TileEnum.GroundStone)) {
				new GroundStone(room);
			}

			// Add to Tilemap
			room.tilemap.AddTile(gridX, gridY, (byte) TileEnum.GroundStone, subTypeId);
		}

		public GroundStone(RoomScene room) : base(room, TileEnum.GroundStone) {
			this.BuildTextures("Stone/");
		}
	}
}
