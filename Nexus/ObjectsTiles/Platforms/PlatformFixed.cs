using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class PlatformFixed : TileGameObject {

		protected string[] Texture;

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte) TileGameObjectId.PlatformFixed)) {
				new PlatformFixed(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddTile(gridX, gridY, (byte) TileGameObjectId.PlatformFixed, subTypeId);
		}

		public PlatformFixed(LevelScene scene) : base(scene, TileGameObjectId.PlatformFixed, AtlasGroup.Tiles) {
			this.BuildTexture("Platform/Fixed/");
		}

		public override void Draw(byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture[subType], posX, posY);
		}

		protected void BuildTexture(string baseName) {
			this.Texture = new string[16];
			this.Texture[(byte)GroundSubTypes.S] = baseName + "S";
			this.Texture[(byte)GroundSubTypes.H1] = baseName + "H1";
			this.Texture[(byte)GroundSubTypes.H2] = baseName + "H2";
			this.Texture[(byte)GroundSubTypes.H3] = baseName + "H3";
		}
	}
}
