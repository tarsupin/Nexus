using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	class CollectableSuit : Collectable {

		public enum SuitSubType {
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

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte) ClassGameObjectId.CollectableSuit)) {
				new CollectableSuit(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddClassTile(gridX, gridY, (byte) ClassGameObjectId.CollectableSuit, subTypeId, true, false, true, true);
		}

		public CollectableSuit(LevelScene scene) : base(scene, ClassGameObjectId.CollectableSuit) {
			this.CreateTextures();
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
