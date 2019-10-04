using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Spike : ClassGameObject {

		public string[] Texture;

		public enum  SpikeSubType {
			Basic = 0,
			Lethal = 1,
		}

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte)ClassGameObjectId.Spike)) {
				new Spike(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddClassTile(gridX, gridY, (byte)ClassGameObjectId.Spike, subTypeId, true, true, false, true);
		}

		public Spike(LevelScene scene) : base(scene, ClassGameObjectId.Spike, AtlasGroup.Blocks) {
			this.CreateTextures();
		}

		private void CreateTextures() {
			this.Texture = new string[2];
			this.Texture[(byte) SpikeSubType.Basic] = "Spike/Basic";
			this.Texture[(byte) SpikeSubType.Lethal] = "Spike/Lethal";
		}

		public override void Draw(byte subType, ushort posX, ushort posY) {
			this.atlas.Draw(this.Texture[subType], FVector.Create(posX, posY));
		}
	}
}
