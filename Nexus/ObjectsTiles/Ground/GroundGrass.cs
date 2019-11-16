using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class GroundGrass : Ground {

		public static void TileGenerate(RoomScene room, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the room. If it hasn't, create it.
			if(!room.IsTileGameObjectRegistered((byte) TileEnum.GroundGrass)) {
				new GroundGrass(room);
			}

			// Add to Tilemap
			room.tilemap.AddTile(gridX, gridY, (byte) TileEnum.GroundGrass, subTypeId);
		}

		public GroundGrass(RoomScene room) : base(room, TileEnum.GroundGrass) {
			this.BuildTextures("Grass/");
		}
	}
}
