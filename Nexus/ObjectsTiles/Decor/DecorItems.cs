using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class DecorItems : Decor {

		public enum CrysSubType : byte {
			Sign = 1,
			Gem1 = 2,
			Gem2 = 3,
			Gem3 = 4,
			Gem4 = 5,
		}

		public DecorItems() : base() {
			this.BuildTextures();
			this.tileId = (byte)TileEnum.DecorItems;
		}

		public void BuildTextures() {
			this.Texture = new string[6];
			this.Texture[(byte)CrysSubType.Sign] = "Decor/Sign";
			this.Texture[(byte)CrysSubType.Gem1] = "Decor/Gem1";
			this.Texture[(byte)CrysSubType.Gem2] = "Decor/Gem2";
			this.Texture[(byte)CrysSubType.Gem3] = "Decor/Gem3";
			this.Texture[(byte)CrysSubType.Gem4] = "Decor/Gem4";
		}
	}
}
