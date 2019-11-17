using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	class CollectableSuit : Collectable {

		public CollectableSuit() : base() {
			this.CreateTextures();
			this.tileId = (byte)TileEnum.CollectableSuit;
		}

		public override void Collect(RoomScene room, Character character, uint gridId) {

			byte[] tileData = room.tilemap.GetTileDataAtGridID(gridId);
			Suit.AssignToCharacter(character, tileData[1], true);

			Systems.sounds.collectBweep.Play();
			base.Collect(room, character, gridId);
		}

		private void CreateTextures() {
			this.Texture = new string[12];
			
			this.Texture[(byte)SuitSubType.RandomSuit] = "SuitCollect/RandomSuit";
			this.Texture[(byte)SuitSubType.RandomNinja] = "SuitCollect/RandomNinja";
			this.Texture[(byte)SuitSubType.RandomWizard] = "SuitCollect/RandomWizard";

			this.Texture[(byte)SuitSubType.BlackNinja] = "SuitCollect/BlackNinja";
			this.Texture[(byte)SuitSubType.BlueNinja] = "SuitCollect/BlueNinja";
			this.Texture[(byte)SuitSubType.GreenNinja] = "SuitCollect/GreenNinja";
			this.Texture[(byte)SuitSubType.RedNinja] = "SuitCollect/RedNinja";
			this.Texture[(byte)SuitSubType.WhiteNinja] = "SuitCollect/WhiteNinja";

			this.Texture[(byte)SuitSubType.BlueWizard] = "SuitCollect/BlueWizard";
			this.Texture[(byte)SuitSubType.GreenWizard] = "SuitCollect/GreenWizard";
			this.Texture[(byte)SuitSubType.RedWizard] = "SuitCollect/RedWizard";
			this.Texture[(byte)SuitSubType.WhiteWizard] = "SuitCollect/WhiteWizard";
		}
	}
}
