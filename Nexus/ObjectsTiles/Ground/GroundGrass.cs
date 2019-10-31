using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class GroundGrass : Ground {

		public static void TileGenerate(RoomScene room, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the room. If it hasn't, create it.
			if(!room.IsClassGameObjectRegistered((byte) TileGameObjectId.GroundGrass)) {
				new GroundGrass(room);
			}

			// Add to Tilemap
			room.tilemap.AddTile(gridX, gridY, (byte) TileGameObjectId.GroundGrass, subTypeId);
		}

		public GroundGrass(RoomScene room) : base(room, TileGameObjectId.GroundGrass) {
			this.BuildTextures("Grass/");
		}
	}
}
