using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	class Coins : Collectable {

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte) TileGameObjectId.CollectableCoin)) {
				new Coins(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddTile(gridX, gridY, (byte) TileGameObjectId.CollectableCoin, subTypeId);
		}

		public Coins(LevelScene scene) : base(scene, TileGameObjectId.CollectableCoin) {
			this.CreateTextures();
		}

		public override void Collect(uint gridId) {
			// TODO SOUND: Collect Coins
			base.Collect(gridId);
		}

		private void CreateTextures() {
			this.Texture = new string[2];
			this.Texture[0] = "Treasure/Coin";
			this.Texture[1] = "Treasure/Gem";
		}
	}
}
