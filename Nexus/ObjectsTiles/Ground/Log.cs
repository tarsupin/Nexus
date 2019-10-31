using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Log : Ground {

		public static void TileGenerate(RoomScene room, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the room. If it hasn't, create it.
			if(!room.IsClassGameObjectRegistered((byte) TileGameObjectId.Log)) {
				new Log(room);
			}

			// Add to Tilemap
			room.tilemap.AddTile(gridX, gridY, (byte) TileGameObjectId.Log, subTypeId);
		}

		public Log(RoomScene room) : base(room, TileGameObjectId.Log) {
			this.BuildTextures("Log/");
		}
	}
}
