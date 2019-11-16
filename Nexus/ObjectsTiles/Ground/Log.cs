using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Log : Ground {

		public static void TileGenerate(RoomScene room, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the room. If it hasn't, create it.
			if(!room.IsTileGameObjectRegistered((byte) TileEnum.Log)) {
				new Log(room);
			}

			// Add to Tilemap
			room.tilemap.AddTile(gridX, gridY, (byte) TileEnum.Log, subTypeId);
		}

		public Log(RoomScene room) : base(room, TileEnum.Log) {
			this.BuildTextures("Log/");
		}
	}
}
