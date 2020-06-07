using Nexus.Gameplay;

namespace Nexus.Objects {

	public class DecorSnow : Decor {

		public enum SnowSubType : byte {
			Bank1 = 0,
			Bank2 = 1,
			Bush1 = 2,
			Log1 = 3,
		}

		public DecorSnow() : base() {
			this.BuildTextures();
			this.tileId = (byte)TileEnum.DecorSnow;
		}

		public void BuildTextures() {
			this.Texture = new string[4];
			this.Texture[(byte)SnowSubType.Bank1] = "Decor/Bank1";
			this.Texture[(byte)SnowSubType.Bank2] = "Decor/Bank2";
			this.Texture[(byte)SnowSubType.Bush1] = "Decor/Bush1";
			this.Texture[(byte)SnowSubType.Log1] = "Decor/Log1";
		}
	}
}
