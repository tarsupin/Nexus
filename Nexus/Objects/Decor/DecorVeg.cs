using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class DecorVeg : Decor {

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte)ClassGameObjectId.DecorVeg)) {
				new DecorVeg(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddClassTile(gridX, gridY, (byte)ClassGameObjectId.DecorVeg, subTypeId, true, true, false);
		}

		public DecorVeg(LevelScene scene) : base(scene, ClassGameObjectId.DecorVeg) {
			this.BuildDecorTextures("Decor/");
		}

		public void BuildDecorTextures(string baseName) {
			this.DecorTexture = new string[16];
			this.DecorTexture[(byte)GroundSubTypes.S] = baseName + "S";
			this.DecorTexture[(byte)GroundSubTypes.FUL] = baseName + "FUL";
			this.DecorTexture[(byte)GroundSubTypes.FU] = baseName + "FU";
			this.DecorTexture[(byte)GroundSubTypes.FUR] = baseName + "FUR";
			this.DecorTexture[(byte)GroundSubTypes.FL] = baseName + "FL";
			this.DecorTexture[(byte)GroundSubTypes.FC] = baseName + "FC";
			this.DecorTexture[(byte)GroundSubTypes.FR] = baseName + "FR";
			this.DecorTexture[(byte)GroundSubTypes.FBL] = baseName + "FBL";
			this.DecorTexture[(byte)GroundSubTypes.FB] = baseName + "FB";
			this.DecorTexture[(byte)GroundSubTypes.FBR] = baseName + "FBR";
			this.DecorTexture[(byte)GroundSubTypes.H1] = baseName + "H1";
			this.DecorTexture[(byte)GroundSubTypes.H2] = baseName + "H2";
			this.DecorTexture[(byte)GroundSubTypes.H3] = baseName + "H3";
			this.DecorTexture[(byte)GroundSubTypes.V1] = baseName + "V1";
			this.DecorTexture[(byte)GroundSubTypes.V2] = baseName + "V2";
			this.DecorTexture[(byte)GroundSubTypes.V3] = baseName + "V3";
		}

		public virtual void Draw(byte subType, ushort posX, ushort posY) {
			this.atlas.Draw(this.DecorTexture[subType], FVector.Create(posX, posY));
		}
	}
}
