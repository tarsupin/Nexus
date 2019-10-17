using Nexus.Engine;
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
			if(!scene.IsClassGameObjectRegistered((byte)TileGameObjectId.PromptArrow)) {
				new PromptArrow(scene);
			}

			// Add to Tilemap
			scene.tilemap.AddTile(gridX, gridY, (byte)TileGameObjectId.PromptArrow, subTypeId);
		}

		public PromptArrow(LevelScene scene) : base(scene, TileGameObjectId.PromptArrow) {
			this.atlas = Systems.mapper.atlas[(byte)AtlasGroup.Tiles];
			this.BuildTextures();
		}

		public void BuildTextures() {
			this.Texture = new string[23];
			this.Texture[(byte)ArrowSubType.Arrow] = "Prompt/Arrow";
			this.Texture[(byte)ArrowSubType.Finger] = "Prompt/Finger";
		}
	}
}
