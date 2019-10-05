using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Lock : ClassGameObject {

		public string Texture;

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte) ClassGameObjectId.Lock)) {
				new Lock(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddClassTile(gridX, gridY, (byte) ClassGameObjectId.Lock, subTypeId, true, true, false, true);
		}

		public Lock(LevelScene scene) : base(scene, ClassGameObjectId.Lock, AtlasGroup.Tiles) {
			this.Texture = "Lock/Lock";
		}

		public override void Draw(byte subType, ushort posX, ushort posY) {
			this.atlas.Draw(this.Texture, FVector.Create(posX, posY));
		}
	}
}
