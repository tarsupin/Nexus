using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class PromptArrow : Decor {

		public enum ArrowSubType : byte {
			Arrow = 0,
			Finger = 1,
		}

		public PromptArrow() : base() {
			this.atlas = Systems.mapper.atlas[(byte)AtlasGroup.Tiles];
			this.BuildTextures();
			this.tileId = (byte)TileEnum.PromptArrow;
			this.title = "Cosmetic Arrows";
			this.description = "Helps the player with directions or subtle instructions.";
		}

		public void BuildTextures() {
			this.Texture = new string[23];
			this.Texture[(byte)ArrowSubType.Arrow] = "Prompt/Arrow";
			this.Texture[(byte)ArrowSubType.Finger] = "Prompt/Finger";
		}
	}
}
