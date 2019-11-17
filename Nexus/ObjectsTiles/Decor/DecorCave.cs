using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class DecorCave : Decor {

		public enum CaveSubType : byte {
			Bulge1 = 0,
			Bulge2 = 1,
			Bulge3 = 2,
			Bulge4 = 3,
			Bulge5 = 4,
			Bulge6 = 5,
			Rock1 = 6,
			Rock2 = 7,
			Slime = 8,
			Top1 = 9,
			Top2 = 10,
			Top3 = 11,
			Top4 = 12,
		}

		public DecorCave() : base() {
			this.BuildTextures();
			this.tileId = (byte)TileEnum.DecorCave;
		}

		public void BuildTextures() {
			this.Texture = new string[13];
			this.Texture[(byte)CaveSubType.Bulge1] = "Decor/Bulge1";
			this.Texture[(byte)CaveSubType.Bulge2] = "Decor/Bulge2";
			this.Texture[(byte)CaveSubType.Bulge3] = "Decor/Bulge3";
			this.Texture[(byte)CaveSubType.Bulge4] = "Decor/Bulge4";
			this.Texture[(byte)CaveSubType.Bulge5] = "Decor/Bulge5";
			this.Texture[(byte)CaveSubType.Bulge6] = "Decor/Bulge6";
			this.Texture[(byte)CaveSubType.Rock1] = "Decor/Rock1";
			this.Texture[(byte)CaveSubType.Rock2] = "Decor/Rock2";
			this.Texture[(byte)CaveSubType.Slime] = "Decor/Slime";
			this.Texture[(byte)CaveSubType.Top1] = "Decor/Top1";
			this.Texture[(byte)CaveSubType.Top2] = "Decor/Top2";
			this.Texture[(byte)CaveSubType.Top3] = "Decor/Top3";
			this.Texture[(byte)CaveSubType.Top4] = "Decor/Top4";
		}
	}
}
