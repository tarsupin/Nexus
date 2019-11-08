using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	class CollectableSuit : Collectable {

		public static void TileGenerate(RoomScene room, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the room. If it hasn't, create it.
			if(!room.IsClassGameObjectRegistered((byte) TileGameObjectId.CollectableSuit)) {
				new CollectableSuit(room);
			}

			// Add to Tilemap
			room.tilemap.AddTile(gridX, gridY, (byte) TileGameObjectId.CollectableSuit, subTypeId);
		}

		public CollectableSuit(RoomScene room) : base(room, TileGameObjectId.CollectableSuit) {
			this.CreateTextures();
		}

		public override void Collect(Character character, uint gridId) {

			byte subType = this.room.tilemap.GetSubTypeAtGridID(gridId);
			Suit.AssignToCharacter(character, subType, true);

			Systems.sounds.collectBweep.Play();
			base.Collect(character, gridId);
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
