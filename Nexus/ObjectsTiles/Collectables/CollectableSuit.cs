using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System;

namespace Nexus.Objects {

	class CollectableSuit : Collectable {

		public enum SuitSubType : byte {
			RandomSuit = 0,
			RandomNinja = 1,
			RandomWizard = 2,

			BlackNinja = 3,
			BlueNinja = 4,
			GreenNinja = 5,
			RedNinja = 6,
			WhiteNinja = 7,

			BlueWizard = 8,
			GreenWizard = 9,
			RedWizard = 10,
			WhiteWizard = 11,
		}

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

			// Random Suit
			if(subType == (byte)SuitSubType.RandomSuit) {
				Random rand = new Random((int)Systems.timer.Frame);
				subType = (byte)rand.Next(3, 11);
			}

			else if(subType == (byte)SuitSubType.RandomNinja) {
				Random rand = new Random((int)Systems.timer.Frame);
				subType = (byte)rand.Next(3, 7);
			}

			else if(subType == (byte)SuitSubType.RandomWizard) {
				Random rand = new Random((int)Systems.timer.Frame);
				subType = (byte)rand.Next(8, 11);
			}

			switch(subType) {

				// Ninjas
				case (byte)SuitSubType.BlackNinja: SuitMap.BlackNinja.ApplySuit(character, true); break;
				case (byte)SuitSubType.BlueNinja: SuitMap.BlueNinja.ApplySuit(character, true); break;
				case (byte)SuitSubType.GreenNinja: SuitMap.GreenNinja.ApplySuit(character, true); break;
				case (byte)SuitSubType.RedNinja: SuitMap.RedNinja.ApplySuit(character, true); break;
				case (byte)SuitSubType.WhiteNinja: SuitMap.WhiteNinja.ApplySuit(character, true); break;

				// Wizards
				case (byte)SuitSubType.BlueWizard: SuitMap.BlueWizard.ApplySuit(character, true); break;
				case (byte)SuitSubType.GreenWizard: SuitMap.GreenWizard.ApplySuit(character, true); break;
				case (byte)SuitSubType.RedWizard: SuitMap.RedWizard.ApplySuit(character, true); break;
				case (byte)SuitSubType.WhiteWizard: SuitMap.WhiteWizard.ApplySuit(character, true); break;
			}

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
