using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class PlatformFixed : TileGameObject {

		protected string[] Texture;
		protected DirCardinal facing;

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte) TileGameObjectId.PlatformFixed)) {
				new PlatformFixed(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddTile(gridX, gridY, (byte) TileGameObjectId.PlatformFixed, subTypeId);
		}

		public PlatformFixed(LevelScene scene) : base(scene, TileGameObjectId.PlatformFixed, AtlasGroup.Tiles) {
			this.collides = true;
			this.facing = DirCardinal.Up;
			this.BuildTexture("Platform/Fixed/");
		}

		public override bool RunCollision(DynamicGameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {
			bool collided = TileFacingImpact.RunImpact(actor, gridX, gridY, dir, this.facing);

			// Characters Can Wall Jump
			if(actor is Character) {
				if(dir == DirCardinal.Left || dir == DirCardinal.Right) {
					TileCharWallImpact.RunImpact((Character)actor, dir == DirCardinal.Right);
				}
			}

			return collided;
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
