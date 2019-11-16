﻿using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class GroundSlime : Ground {

		public static void TileGenerate(RoomScene room, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the room. If it hasn't, create it.
			if(!room.IsTileGameObjectRegistered((byte) TileEnum.GroundSlime)) {
				new GroundSlime(room);
			}

			// Add to Tilemap
			room.tilemap.AddTile(gridX, gridY, (byte) TileEnum.GroundSlime, subTypeId);
		}

		public GroundSlime(RoomScene room) : base(room, TileEnum.GroundSlime) {
			this.BuildTextures("Slime/");
		}
	}
}
