using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	class Coins : Collectable {

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte) ClassGameObjectId.CollectableCoin)) {
				new Coins(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddClassTile(gridX, gridY, (byte) ClassGameObjectId.CollectableCoin, subTypeId, true, false, true, true);
		}

		public Coins(LevelScene scene) : base(scene, ClassGameObjectId.CollectableCoin) {
			this.CreateTextures();
		}

		private void CreateTextures() {
			this.Texture = new string[2];
			this.Texture[0] = "Treasure/Coin";
			this.Texture[1] = "Treasure/Gem";
		}
	}
}
