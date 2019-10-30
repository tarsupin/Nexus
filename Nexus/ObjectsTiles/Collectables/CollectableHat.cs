using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System;

namespace Nexus.Objects {

	class CollectableHat : Collectable {

		public enum HatSubType : byte {
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
			if(!scene.IsClassGameObjectRegistered((byte) TileGameObjectId.CollectableHat)) {
				new CollectableHat(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddTile(gridX, gridY, (byte) TileGameObjectId.CollectableHat, subTypeId);
		}

		public CollectableHat(LevelScene scene) : base(scene, TileGameObjectId.CollectableHat) {
			this.CreateTextures();
		}

		public override void Collect( Character character, uint gridId ) {
			byte subType = this.scene.tilemap.GetSubTypeAtGridID(gridId);

			// Random Hat
			if(subType == (byte)HatSubType.RandomHat) {
				Random rand = new Random((int) Systems.timer.Frame);
				subType = (byte) rand.Next(1, 9);
			}

			switch(subType) {
				case (byte)HatSubType.AngelHat: character.hat = new AngelHat(character); break;
				case (byte)HatSubType.BambooHat: character.hat = new BambooHat(character); break;
				case (byte)HatSubType.CowboyHat: character.hat = new CowboyHat(character); break;
				case (byte)HatSubType.FeatheredHat: character.hat = new FeatheredHat(character); break;
				case (byte)HatSubType.FedoraHat: character.hat = new FedoraHat(character); break;
				case (byte)HatSubType.HardHat: character.hat = new HardHat(character); break;
				case (byte)HatSubType.RangerHat: character.hat = new RangerHat(character); break;
				case (byte)HatSubType.SpikeyHat: character.hat = new SpikeyHat(character); break;
				case (byte)HatSubType.TopHat: character.hat = new TopHat(character); break;
			}

			Systems.sounds.collectBweep.Play();
			base.Collect(character, gridId);
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
