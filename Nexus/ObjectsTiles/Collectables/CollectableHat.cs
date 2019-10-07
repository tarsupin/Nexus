﻿using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	class CollectableHat : Collectable {

		public enum HatSubType {
			RandomHat = 0,
			AngelHat = 1,
			BambooHat = 2,
			CowboyHat = 3,
			FeatheredHat = 4,
			FedoraHat = 5,
			HardHat = 6,
			RangerHat = 7,
			SpikeyHat = 8,
			TopHat = 9,
		}

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte) ClassGameObjectId.CollectableHat)) {
				new CollectableHat(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddClassTile(gridX, gridY, (byte) ClassGameObjectId.CollectableHat, subTypeId, true, false, true, false, true);
		}

		public CollectableHat(LevelScene scene) : base(scene, ClassGameObjectId.CollectableHat) {
			this.CreateTextures();
		}

		private void CreateTextures() {
			this.Texture = new string[10];
			
			this.Texture[(byte)HatSubType.AngelHat] = "HatCollect/AngelHat";
			this.Texture[(byte)HatSubType.BambooHat] = "HatCollect/BambooHat";
			this.Texture[(byte)HatSubType.CowboyHat] = "HatCollect/CowboyHat";
			this.Texture[(byte)HatSubType.FeatheredHat] = "HatCollect/FeatheredHat";
			this.Texture[(byte)HatSubType.FedoraHat] = "HatCollect/FedoraHat";
			this.Texture[(byte)HatSubType.HardHat] = "HatCollect/HardHat";
			this.Texture[(byte)HatSubType.RangerHat] = "HatCollect/RangerHat";
			this.Texture[(byte)HatSubType.SpikeyHat] = "HatCollect/SpikeyHat";
			this.Texture[(byte)HatSubType.TopHat] = "HatCollect/TopHat";
		}
	}
}
