using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class GroundSnow : Ground {

		public static void TileGenerate(RoomScene room, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the room. If it hasn't, create it.
			if(!room.IsClassGameObjectRegistered((byte) TileGameObjectId.GroundSnow)) {
				new GroundSnow(room);
			}

			// Add to Tilemap
			room.tilemap.AddTile(gridX, gridY, (byte) TileGameObjectId.GroundSnow, subTypeId);
		}

		public GroundSnow(RoomScene room) : base(room, TileGameObjectId.GroundSnow) {
			this.BuildTextures("Snow/");
		}
	}
}
