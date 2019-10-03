using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Ledge : ClassGameObject {

		protected string[] Texture;

		public Ledge(LevelScene scene, ClassGameObjectId classId) : base(scene, classId, AtlasGroup.Blocks) {

		}

		public override void Draw(byte subType, ushort posX, ushort posY) {
			this.atlas.Draw(this.Texture[subType], FVector.Create(posX, posY));
		}

		protected void BuildTextures(string baseName) {
			this.Texture = new string[16];
			this.Texture[(byte)GroundSubTypes.S] = baseName + "S";
			this.Texture[(byte)GroundSubTypes.FUL] = baseName + "FUL";
			this.Texture[(byte)GroundSubTypes.FU] = baseName + "FU";
			this.Texture[(byte)GroundSubTypes.FUR] = baseName + "FUR";
			this.Texture[(byte)GroundSubTypes.FL] = baseName + "FL";
			this.Texture[(byte)GroundSubTypes.FC] = baseName + "FC";
			this.Texture[(byte)GroundSubTypes.FR] = baseName + "FR";
			this.Texture[(byte)GroundSubTypes.FBL] = baseName + "FBL";
			this.Texture[(byte)GroundSubTypes.FB] = baseName + "FB";
			this.Texture[(byte)GroundSubTypes.FBR] = baseName + "FBR";
			this.Texture[(byte)GroundSubTypes.H1] = baseName + "H1";
			this.Texture[(byte)GroundSubTypes.H2] = baseName + "H2";
			this.Texture[(byte)GroundSubTypes.H3] = baseName + "H3";
			this.Texture[(byte)GroundSubTypes.V1] = baseName + "V1";
			this.Texture[(byte)GroundSubTypes.V2] = baseName + "V2";
			this.Texture[(byte)GroundSubTypes.V3] = baseName + "V3";
		}
	}
}
