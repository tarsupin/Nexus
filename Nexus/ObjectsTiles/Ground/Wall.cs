using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Wall : Ground {

		public static void TileGenerate(RoomScene room, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the room. If it hasn't, create it.
			if(!room.IsTileGameObjectRegistered((byte)TileEnum.Wall)) {
				new Wall(room);
			}

			// Add to Tilemap
			room.tilemap.AddTile(gridX, gridY, (byte)TileEnum.Wall, subTypeId);
		}

		public Wall(RoomScene room) : base(room, TileEnum.Wall) {
			this.BuildTextures("Slab/Gray/");	// TODO: Change to Wall
		}
	}
}
