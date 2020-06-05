using Nexus.Engine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class PromptArrow : Decor {

		public enum ArrowSubType : byte {
			ArrowUp = 1,
			ArrowUpRight = 2,
			ArrowRight = 3,
			ArrowDownRight = 4,
			ArrowDown = 5,
			ArrowDownLeft = 6,
			ArrowLeft = 7,
			ArrowUpLeft = 8,
			FingerUp = 9,
			FingerUpRight = 10,
			FingerRight = 11,
			FingerDownRight = 12,
			FingerDown = 13,
			FingerDownLeft = 14,
			FingerLeft = 15,
			FingerUpLeft = 16,
		}

		public PromptArrow() : base() {
			this.atlas = Systems.mapper.atlas[(byte)AtlasGroup.Tiles];
			this.BuildTextures();
			this.tileId = (byte)TileEnum.PromptArrow;
			this.title = "Cosmetic Arrows";
			this.description = "Helps the player with directions or subtle instructions.";
		}

		public void BuildTextures() {
			this.Texture = new string[17];
			this.Texture[0] = "Arrow/Up";
			this.Texture[(byte)ArrowSubType.ArrowUp] = "Arrow/Up";
			this.Texture[(byte)ArrowSubType.ArrowUpRight] = "Arrow/UpRight";
			this.Texture[(byte)ArrowSubType.ArrowRight] = "Arrow/Right";
			this.Texture[(byte)ArrowSubType.ArrowDownRight] = "Arrow/DownRight";
			this.Texture[(byte)ArrowSubType.ArrowDown] = "Arrow/Down";
			this.Texture[(byte)ArrowSubType.ArrowDownLeft] = "Arrow/DownLeft";
			this.Texture[(byte)ArrowSubType.ArrowLeft] = "Arrow/Left";
			this.Texture[(byte)ArrowSubType.ArrowUpLeft] = "Arrow/UpLeft";
			this.Texture[(byte)ArrowSubType.FingerUp] = "Finger/Up";
			this.Texture[(byte)ArrowSubType.FingerUpRight] = "Finger/UpRight";
			this.Texture[(byte)ArrowSubType.FingerRight] = "Finger/Right";
			this.Texture[(byte)ArrowSubType.FingerDownRight] = "Finger/DownRight";
			this.Texture[(byte)ArrowSubType.FingerDown] = "Finger/Down";
			this.Texture[(byte)ArrowSubType.FingerDownLeft] = "Finger/DownLeft";
			this.Texture[(byte)ArrowSubType.FingerLeft] = "Finger/Left";
			this.Texture[(byte)ArrowSubType.FingerUpLeft] = "Finger/UpLeft";
		}
	}
}
