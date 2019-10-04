using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class DecorVeg : Decor {

		public enum VegSubType {
			Grass1 = 0,
			Grass2 = 1,
			Plant1 = 2,
			Plant2 = 3,
			Plant3 = 4,
			Plant4 = 5,
			Vine1 = 6,
			Vine2 = 7,
			Vine3 = 8,
			Fung1 = 9,
			Fung2 = 10,
			Fung3 = 11,
			Fung4 = 12,
			Fung5 = 13,
			Tree1 = 14,
			Tree2 = 15,
		}

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte)ClassGameObjectId.DecorVeg)) {
				new DecorVeg(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddClassTile(gridX, gridY, (byte)ClassGameObjectId.DecorVeg, subTypeId, false, false);
		}

		public DecorVeg(LevelScene scene) : base(scene, ClassGameObjectId.DecorVeg) {
			this.BuildTextures();
		}

		public void BuildTextures() {
			this.Texture = new string[16];
			this.Texture[(byte)VegSubType.Grass1] = "Decor/Grass1";
			this.Texture[(byte)VegSubType.Grass2] = "Decor/Grass2";
			this.Texture[(byte)VegSubType.Plant1] = "Decor/Plant1";
			this.Texture[(byte)VegSubType.Plant2] = "Decor/Plant2";
			this.Texture[(byte)VegSubType.Plant3] = "Decor/Plant3";
			this.Texture[(byte)VegSubType.Plant4] = "Decor/Plant4";
			this.Texture[(byte)VegSubType.Vine1] = "Decor/Vine1";
			this.Texture[(byte)VegSubType.Vine2] = "Decor/Vine2";
			this.Texture[(byte)VegSubType.Vine3] = "Decor/Vine3";
			this.Texture[(byte)VegSubType.Fung1] = "Decor/Fung1";
			this.Texture[(byte)VegSubType.Fung2] = "Decor/Fung2";
			this.Texture[(byte)VegSubType.Fung3] = "Decor/Fung3";
			this.Texture[(byte)VegSubType.Fung4] = "Decor/Fung4";
			this.Texture[(byte)VegSubType.Fung5] = "Decor/Fung5";
			this.Texture[(byte)VegSubType.Tree1] = "Decor/Tree1";
			this.Texture[(byte)VegSubType.Tree2] = "Decor/Tree2";
		}
	}
}
