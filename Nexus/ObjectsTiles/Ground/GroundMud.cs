using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class GroundMud : Ground {

		public static void TileGenerate(RoomScene room, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the room. If it hasn't, create it.
			if(!room.IsTileGameObjectRegistered((byte) TileEnum.GroundMud)) {
				new GroundMud(room);
			}

			// Add to Tilemap
			room.tilemap.AddTile(gridX, gridY, (byte) TileEnum.GroundMud, subTypeId);
		}

		public GroundMud(RoomScene room) : base(room, TileEnum.GroundMud) {
			this.BuildTextures("Mud/");
		}
	}
}
