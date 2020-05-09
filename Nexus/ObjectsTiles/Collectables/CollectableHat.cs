using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	class CollectableHat : Collectable {

		public CollectableHat() : base() {
			this.CreateTextures();
			this.tileId = (byte)TileEnum.CollectableHat;
			this.title = "Hat Collectable";
			this.description = "Character equips the hat collected.";
		}

		public override void Collect( RoomScene room, Character character, uint gridId ) {

			byte[] tileData = room.tilemap.GetTileDataAtGridID(gridId);
			Hat.AssignToCharacter(character, tileData[1], true);

			Systems.sounds.collectBweep.Play();
			base.Collect(room, character, gridId);
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
			this.Texture[(byte)HatSubType.RandomHat] = "HatCollect/RandomHat";
		}
	}
}
