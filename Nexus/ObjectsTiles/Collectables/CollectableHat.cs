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

		public static void TileGenerate(RoomScene room, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the room. If it hasn't, create it.
			if(!room.IsClassGameObjectRegistered((byte) TileGameObjectId.CollectableHat)) {
				new CollectableHat(room);
			}

			// Add to Tilemap
			room.tilemap.AddTile(gridX, gridY, (byte) TileGameObjectId.CollectableHat, subTypeId);
		}

		public CollectableHat(RoomScene room) : base(room, TileGameObjectId.CollectableHat) {
			this.CreateTextures();
		}

		public override void Collect( Character character, uint gridId ) {
			byte subType = this.room.tilemap.GetSubTypeAtGridID(gridId);

			// Random Hat
			if(subType == (byte)HatSubType.RandomHat) {
				Random rand = new Random((int) Systems.timer.Frame);
				subType = (byte) rand.Next(1, 9);
			}

			switch(subType) {
				case (byte)HatSubType.AngelHat: HatMap.AngelHat.ApplyHat(character, true); break;
				case (byte)HatSubType.BambooHat: HatMap.BambooHat.ApplyHat(character, true); break;
				case (byte)HatSubType.CowboyHat: HatMap.CowboyHat.ApplyHat(character, true); break;
				case (byte)HatSubType.FeatheredHat: HatMap.FeatheredHat.ApplyHat(character, true); break;
				case (byte)HatSubType.FedoraHat: HatMap.FedoraHat.ApplyHat(character, true); break;
				case (byte)HatSubType.HardHat: HatMap.HardHat.ApplyHat(character, true); break;
				case (byte)HatSubType.RangerHat: HatMap.RangerHat.ApplyHat(character, true); break;
				case (byte)HatSubType.SpikeyHat: HatMap.SpikeyHat.ApplyHat(character, true); break;
				case (byte)HatSubType.TopHat: HatMap.TopHat.ApplyHat(character, true); break;
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
