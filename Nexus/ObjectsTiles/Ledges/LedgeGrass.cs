using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class LedgeGrass : Ledge {

		public static void TileGenerate(RoomScene room, ushort gridX, ushort gridY, byte subTypeId) {

			// Add the Top Layers of Grass Ledges as "LedgeGrass" tiles.
			// Only some SubTypes apply.
			switch(subTypeId) {
				case (byte) GroundSubTypes.S:
				case (byte) GroundSubTypes.FUL:
				case (byte) GroundSubTypes.FU:
				case (byte) GroundSubTypes.FUR:
				case (byte) GroundSubTypes.H1:
				case (byte) GroundSubTypes.H2:
				case (byte) GroundSubTypes.H3:
				case (byte) GroundSubTypes.V1:

					// Check if the ClassGameObject has already been created in the room. If it hasn't, create it.
					if(!room.IsTileGameObjectRegistered((byte)TileEnum.LedgeGrass)) {
						new LedgeGrass(room);
					}

					room.tilemap.AddTile(gridX, gridY, (byte)TileEnum.LedgeGrass, subTypeId);
					return;
			}

			// Check if the ClassGameObject has already been created in the room. If it hasn't, create it.
			if(!room.IsTileGameObjectRegistered((byte)TileEnum.LedgeGrassDecor)) {
				new LedgeGrassDecor(room);
			}

			// Add to Ledge Grass Decor (no collisions, no facing).
			room.tilemap.AddTile(gridX, gridY, (byte)TileEnum.LedgeGrassDecor, subTypeId);
		}

		public LedgeGrass(RoomScene room) : base(room, TileEnum.LedgeGrass) {
			this.BuildTextures("GrassLedge/");
		}
	}
}
