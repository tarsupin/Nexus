using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class LedgeGrassDecor : Decor {

		public LedgeGrassDecor() : base() {
			this.BuildTextures("GrassLedge/");
			this.tileId = (byte)TileEnum.LedgeGrassDecor;
		}

		protected void BuildTextures(string baseName) {
			this.Texture = new string[16];
			this.Texture[(byte)GroundSubTypes.FL] = baseName + "FL";
			this.Texture[(byte)GroundSubTypes.FC] = baseName + "FC";
			this.Texture[(byte)GroundSubTypes.FR] = baseName + "FR";
			this.Texture[(byte)GroundSubTypes.FBL] = baseName + "FBL";
			this.Texture[(byte)GroundSubTypes.FB] = baseName + "FB";
			this.Texture[(byte)GroundSubTypes.FBR] = baseName + "FBR";
			this.Texture[(byte)GroundSubTypes.V2] = baseName + "V2";
			this.Texture[(byte)GroundSubTypes.V3] = baseName + "V3";
		}
	}
}
