using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class PromptArrow : Decor {

		public enum ArrowSubType : byte {
			Arrow = 0,
			Finger = 1,
		}

		public static void TileGenerate(RoomScene room, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the room. If it hasn't, create it.
			if(!room.IsTileGameObjectRegistered((byte) TileEnum.PromptArrow)) {
				new PromptArrow(room);
			}

			// Add to Tilemap
			room.tilemap.AddTile(gridX, gridY, (byte) TileEnum.PromptArrow, subTypeId);
		}

		public PromptArrow(RoomScene room) : base(room, TileEnum.PromptArrow) {
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
