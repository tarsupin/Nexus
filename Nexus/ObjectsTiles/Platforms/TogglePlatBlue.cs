﻿using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class TogglePlatBlue : TogglePlat {

		protected new bool Toggled {
			get { return this.scene.flags.toggleBR; }
		}

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte) TileGameObjectId.TogglePlatBlue)) {
				new TogglePlatBlue(scene, subTypeId);
			}

			// Add to Tilemap
			scene.tilemap.AddTile(gridX, gridY, (byte) TileGameObjectId.TogglePlatBlue, subTypeId);
		}

		public TogglePlatBlue(LevelScene scene, byte subTypeId) : base(scene, subTypeId, TileGameObjectId.TogglePlatBlue) {
			this.Texture = "/Blue/Plat";
		}
	}
}