using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class PromptArrow : Decor {

		public enum ArrowSubType {
			Arrow = 0,
			Finger = 1,
		}

		public static void TileGenerate(LevelScene scene, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the scene. If it hasn't, create it.
			if(!scene.IsClassGameObjectRegistered((byte)ClassGameObjectId.PromptArrow)) {
				new PromptArrow(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddClassTile(gridX, gridY, (byte)ClassGameObjectId.PromptArrow, subTypeId, true, true, false);
		}

		public PromptArrow(LevelScene scene) : base(scene, ClassGameObjectId.PromptArrow) {
			this.atlas = scene.mapper.atlas[(byte)AtlasGroup.Other];
			this.BuildTextures();
		}

		public void BuildTextures() {
			this.Texture = new string[23];
			this.Texture[(byte)ArrowSubType.Arrow] = "Prompt/Arrow";
			this.Texture[(byte)ArrowSubType.Finger] = "Prompt/Finger";
		}
	}
}
