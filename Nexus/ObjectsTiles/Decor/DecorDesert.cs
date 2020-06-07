using Nexus.Gameplay;

namespace Nexus.Objects {

	public class DecorDesert : Decor {

		public enum DesertSubType : byte {
			Desert1 = 0,
			Desert2 = 1,
			Desert3 = 2,
			Desert4 = 3,
			Bones1 = 9,
			Bones2 = 10,
			Bones3 = 11,
			Cacti1 = 9,
			Cacti2 = 10,
			Cacti3 = 11,
		}

		public DecorDesert() : base() {
			this.BuildTextures();
			this.tileId = (byte)TileEnum.DecorDesert;
		}

		public void BuildTextures() {
			this.Texture = new string[12];
			this.Texture[(byte)DesertSubType.Desert1] = "Decor/Desert1";
			this.Texture[(byte)DesertSubType.Desert2] = "Decor/Desert2";
			this.Texture[(byte)DesertSubType.Desert3] = "Decor/Desert3";
			this.Texture[(byte)DesertSubType.Desert4] = "Decor/Desert4";
			this.Texture[(byte)DesertSubType.Bones1] = "Decor/Bones1";
			this.Texture[(byte)DesertSubType.Bones2] = "Decor/Bones2";
			this.Texture[(byte)DesertSubType.Bones3] = "Decor/Bones3";
			this.Texture[(byte)DesertSubType.Cacti1] = "Decor/Cacti1";
			this.Texture[(byte)DesertSubType.Cacti2] = "Decor/Cacti2";
			this.Texture[(byte)DesertSubType.Cacti3] = "Decor/Cacti3";
		}
	}
}
