using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	class Goodie : Collectable {

		public enum GoodieSubType {
			Apple = 0,
			Pear = 1,
			Heart = 2,
			Shield = 3,
			ShieldPlus = 4,
			
			Guard = 5,
			GuardPlus = 6,
			
			Shiny = 7,
			Stars = 8,
			GodMode = 9,
			
			Plus5 = 10,
			Plus10 = 11,
			Plus20 = 12,
			Set5 = 13,
			Set10 = 14,
			Set20 = 15,
			
			Disrupt = 16,
			Explosive = 17,
			Key = 18,
			Blood = 19,
		}
		
		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte) ClassGameObjectId.CollectableGoodie)) {
				new Goodie(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddClassTile(gridX, gridY, (byte) ClassGameObjectId.CollectableGoodie, subTypeId, true, true, false);
		}

		public Goodie(LevelScene scene) : base(scene, ClassGameObjectId.CollectableGoodie) {
			this.CreateTextures();
		}

		private void CreateTextures() {
			this.Texture = new string[20];
			
			this.Texture[(byte)GoodieSubType.Apple] = "Goodie/Apple";
			this.Texture[(byte)GoodieSubType.Pear] = "Goodie/Pear";
			this.Texture[(byte)GoodieSubType.Heart] = "Goodie/Heart";
			this.Texture[(byte)GoodieSubType.Shield] = "Goodie/Shield";
			this.Texture[(byte)GoodieSubType.ShieldPlus] = "Goodie/ShieldPlus";

			this.Texture[(byte)GoodieSubType.Guard] = "Goodie/Guard";
			this.Texture[(byte)GoodieSubType.GuardPlus] = "Goodie/GuardPlus";

			this.Texture[(byte)GoodieSubType.Shiny] = "Goodie/Shiny";
			this.Texture[(byte)GoodieSubType.Stars] = "Goodie/Stars";
			this.Texture[(byte)GoodieSubType.GodMode] = "Goodie/GodMode";

			this.Texture[(byte)GoodieSubType.Plus5] = "Goodie/Plus5";
			this.Texture[(byte)GoodieSubType.Plus10] = "Goodie/Plus10";
			this.Texture[(byte)GoodieSubType.Plus20] = "Goodie/Plus20";
			this.Texture[(byte)GoodieSubType.Set5] = "Goodie/Set5";
			this.Texture[(byte)GoodieSubType.Set10] = "Goodie/Set10";
			this.Texture[(byte)GoodieSubType.Set20] = "Goodie/Set20";

			this.Texture[(byte)GoodieSubType.Disrupt] = "Goodie/Disrupt";
			this.Texture[(byte)GoodieSubType.Explosive] = "Goodie/Explosive";
			this.Texture[(byte)GoodieSubType.Key] = "Goodie/Key";
			this.Texture[(byte)GoodieSubType.Blood] = "Goodie/Blood";
		}
	}
}
