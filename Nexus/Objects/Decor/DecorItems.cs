using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class DecorItems : Decor {

		public enum CrysSubType {
			Pot = 0,
			Tomb = 1,
			Gem1 = 2,
			Gem2 = 3,
			Gem3 = 4,
			Gem4 = 5,
		}

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte) ClassGameObjectId.DecorItems)) {
				new DecorItems(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddClassTile(gridX, gridY, (byte) ClassGameObjectId.DecorItems, subTypeId, true, true, false);
		}

		public DecorItems(LevelScene scene) : base(scene, ClassGameObjectId.DecorItems) {
			this.BuildTextures();
		}

		public void BuildTextures() {
			this.Texture = new string[6];
			this.Texture[(byte)CrysSubType.Pot] = "Decor/Pot";
			this.Texture[(byte)CrysSubType.Tomb] = "Decor/Tomb";
			this.Texture[(byte)CrysSubType.Gem1] = "Decor/Gem1";
			this.Texture[(byte)CrysSubType.Gem2] = "Decor/Gem2";
			this.Texture[(byte)CrysSubType.Gem3] = "Decor/Gem3";
			this.Texture[(byte)CrysSubType.Gem4] = "Decor/Gem4";
		}
	}
}
