using Nexus.Engine;
using Nexus.GameEngine;
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
			this.Texture[0] = "Prompt/ArrowUp";
			this.Texture[(byte)ArrowSubType.ArrowUp] = "Prompt/Arrow";
			this.Texture[(byte)ArrowSubType.ArrowUpRight] = "Prompt/Arrow";
			this.Texture[(byte)ArrowSubType.ArrowRight] = "Prompt/Arrow";
			this.Texture[(byte)ArrowSubType.ArrowDownRight] = "Prompt/Arrow";
			this.Texture[(byte)ArrowSubType.ArrowDown] = "Prompt/Arrow";
			this.Texture[(byte)ArrowSubType.ArrowDownLeft] = "Prompt/Arrow";
			this.Texture[(byte)ArrowSubType.ArrowLeft] = "Prompt/Arrow";
			this.Texture[(byte)ArrowSubType.ArrowUpLeft] = "Prompt/Arrow";
			this.Texture[(byte)ArrowSubType.FingerUp] = "Prompt/FingerUp";
			this.Texture[(byte)ArrowSubType.FingerUpRight] = "Prompt/Finger";
			this.Texture[(byte)ArrowSubType.FingerRight] = "Prompt/Finger";
			this.Texture[(byte)ArrowSubType.FingerDownRight] = "Prompt/Finger";
			this.Texture[(byte)ArrowSubType.FingerDown] = "Prompt/Finger";
			this.Texture[(byte)ArrowSubType.FingerDownLeft] = "Prompt/Finger";
			this.Texture[(byte)ArrowSubType.FingerLeft] = "Prompt/Finger";
			this.Texture[(byte)ArrowSubType.FingerUpLeft] = "Prompt/Finger";

			//this.Texture[(byte)ArrowSubType.ArrowUp] = "Prompt/ArrowUp";
			//this.Texture[(byte)ArrowSubType.ArrowUpRight] = "Prompt/ArrowUpRight";
			//this.Texture[(byte)ArrowSubType.ArrowRight] = "Prompt/ArrowRight";
			//this.Texture[(byte)ArrowSubType.ArrowDownRight] = "Prompt/ArrowDownRight";
			//this.Texture[(byte)ArrowSubType.ArrowDown] = "Prompt/ArrowDown";
			//this.Texture[(byte)ArrowSubType.ArrowDownLeft] = "Prompt/ArrowDownLeft";
			//this.Texture[(byte)ArrowSubType.ArrowLeft] = "Prompt/ArrowLeft";
			//this.Texture[(byte)ArrowSubType.ArrowUpLeft] = "Prompt/ArrowUpLeft";
			//this.Texture[(byte)ArrowSubType.FingerUp] = "Prompt/FingerUp";
			//this.Texture[(byte)ArrowSubType.FingerUpRight] = "Prompt/FingerUpRight";
			//this.Texture[(byte)ArrowSubType.FingerRight] = "Prompt/FingerRight";
			//this.Texture[(byte)ArrowSubType.FingerDownRight] = "Prompt/FingerDownRight";
			//this.Texture[(byte)ArrowSubType.FingerDown] = "Prompt/FingerDown";
			//this.Texture[(byte)ArrowSubType.FingerDownLeft] = "Prompt/FingerDownLeft";
			//this.Texture[(byte)ArrowSubType.FingerLeft] = "Prompt/FingerLeft";
			//this.Texture[(byte)ArrowSubType.FingerUpLeft] = "Prompt/FingerUpLeft";
		}
	}
}
