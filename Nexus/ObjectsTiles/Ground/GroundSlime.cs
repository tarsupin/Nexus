﻿using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class GroundSlime : Ground {

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte) TileGameObjectId.GroundSlime)) {
				new GroundSlime(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddTile(gridX, gridY, (byte) TileGameObjectId.GroundSlime, subTypeId);
		}

		public GroundSlime(LevelScene scene) : base(scene, TileGameObjectId.GroundSlime) {
			this.BuildTextures("Slime/");
		}
	}
}
