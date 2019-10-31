using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class GroundMud : Ground {

		public static void TileGenerate(RoomScene room, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the room. If it hasn't, create it.
			if(!room.IsClassGameObjectRegistered((byte) TileGameObjectId.GroundMud)) {
				new GroundMud(room);
			}

			// Add to Tilemap
			room.tilemap.AddTile(gridX, gridY, (byte) TileGameObjectId.GroundMud, subTypeId);
		}

		public GroundMud(RoomScene room) : base(room, TileGameObjectId.GroundMud) {
			this.BuildTextures("Mud/");
		}
	}
}
