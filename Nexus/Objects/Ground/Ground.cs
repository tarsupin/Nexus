using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Ground : ClassGameObject {

		protected string[] GroundTexture;

		public Ground(LevelScene scene, ClassGameObjectId classId) : base(scene, classId, AtlasGroup.Blocks) {

		}

		public virtual void Draw(byte subType, ushort posX, ushort posY) {
			this.atlas.Draw(this.GroundTexture[subType], FVector.Create(posX, posY));
		}

		protected void BuildGroundTextures(string baseName) {
			this.GroundTexture = new string[16];
			this.GroundTexture[(byte)GroundSubTypes.S] = baseName + "S";
			this.GroundTexture[(byte)GroundSubTypes.FUL] = baseName + "FUL";
			this.GroundTexture[(byte)GroundSubTypes.FU] = baseName + "FU";
			this.GroundTexture[(byte)GroundSubTypes.FUR] = baseName + "FUR";
			this.GroundTexture[(byte)GroundSubTypes.FL] = baseName + "FL";
			this.GroundTexture[(byte)GroundSubTypes.FC] = baseName + "FC";
			this.GroundTexture[(byte)GroundSubTypes.FR] = baseName + "FR";
			this.GroundTexture[(byte)GroundSubTypes.FBL] = baseName + "FBL";
			this.GroundTexture[(byte)GroundSubTypes.FB] = baseName + "FB";
			this.GroundTexture[(byte)GroundSubTypes.FBR] = baseName + "FBR";
			this.GroundTexture[(byte)GroundSubTypes.H1] = baseName + "H1";
			this.GroundTexture[(byte)GroundSubTypes.H2] = baseName + "H2";
			this.GroundTexture[(byte)GroundSubTypes.H3] = baseName + "H3";
			this.GroundTexture[(byte)GroundSubTypes.V1] = baseName + "V1";
			this.GroundTexture[(byte)GroundSubTypes.V2] = baseName + "V2";
			this.GroundTexture[(byte)GroundSubTypes.V3] = baseName + "V3";
		}
	}
}
