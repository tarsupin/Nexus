using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Box : ClassGameObject {

		public string[] Texture;

		public enum BoxSubType {
			Brown = 0,
			Gray = 1,
		}

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte) ClassGameObjectId.Box)) {
				new Box(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddClassTile(gridX, gridY, (byte) ClassGameObjectId.Box, subTypeId, true, true, false, true);
		}

		public Box(LevelScene scene) : base(scene, ClassGameObjectId.Box, AtlasGroup.Blocks) {
			this.CreateTextures();
		}

		// TODO HIGH PRIORITY: See DetectObject.DamageAbove() for process of damaging above the box when broken.
		// TODO HIGH PRIORITY: See DetectObject.DamageAbove() for process of damaging above the box when broken.
		// TODO HIGH PRIORITY: Also need a damaging effect (special collision), which will remove the tile.

		private void CreateTextures() {
			this.Texture = new string[2];
			this.Texture[(byte) BoxSubType.Brown] = "Box/Brown";
			this.Texture[(byte) BoxSubType.Gray] = "Box/Gray";
		}

		public override void Draw(byte subType, ushort posX, ushort posY) {
			this.atlas.Draw(this.Texture[subType], FVector.Create(posX, posY));
		}
	}
}
