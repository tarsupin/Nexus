using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class PromptIcon : Decor {

		public enum IconSubType : byte {
			Aim = 0,
			Burst = 1,
			Cast = 2,
			Chat = 3,
			Fist = 4,
			Hand = 5,
			Jump = 6,
			Run = 7,
			A = 8,
			B = 9,
			X = 10,
			Y = 11,
			L1 = 12,
			R1 = 13,
			N1 = 14,
			N2 = 15,
			N3 = 16,
			N4 = 17,
			N5 = 18,
			Left = 19,
			Right = 20,
			Up = 21,
			Down = 22,
		}

		public static void TileGenerate(RoomScene room, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the room. If it hasn't, create it.
			if(!room.IsTileGameObjectRegistered((byte) TileEnum.PromptIcon)) {
				new PromptIcon(room);
			}

			// Add to Tilemap
			room.tilemap.AddTile(gridX, gridY, (byte) TileEnum.PromptIcon, subTypeId);
		}

		public PromptIcon(RoomScene room) : base(room, TileEnum.PromptIcon) {
			this.atlas = Systems.mapper.atlas[(byte)AtlasGroup.Tiles];
			this.BuildTextures();
		}

		public void BuildTextures() {
			this.Texture = new string[23];
			this.Texture[(byte)IconSubType.Aim] = "Prompt/Aim";
			this.Texture[(byte)IconSubType.Burst] = "Prompt/Burst";
			this.Texture[(byte)IconSubType.Cast] = "Prompt/Cast";
			this.Texture[(byte)IconSubType.Chat] = "Prompt/Chat";
			this.Texture[(byte)IconSubType.Fist] = "Prompt/Fist";
			this.Texture[(byte)IconSubType.Hand] = "Prompt/Hand";
			this.Texture[(byte)IconSubType.Jump] = "Prompt/Jump";
			this.Texture[(byte)IconSubType.Run] = "Prompt/Run";
			this.Texture[(byte)IconSubType.A] = "Prompt/Button/A";
			this.Texture[(byte)IconSubType.B] = "Prompt/Button/B";
			this.Texture[(byte)IconSubType.X] = "Prompt/Button/X";
			this.Texture[(byte)IconSubType.Y] = "Prompt/Button/Y";
			this.Texture[(byte)IconSubType.L1] = "Prompt/Button/L1";
			this.Texture[(byte)IconSubType.R1] = "Prompt/Button/R1";
			this.Texture[(byte)IconSubType.N1] = "Prompt/Number/N1";
			this.Texture[(byte)IconSubType.N2] = "Prompt/Number/N2";
			this.Texture[(byte)IconSubType.N3] = "Prompt/Number/N3";
			this.Texture[(byte)IconSubType.N4] = "Prompt/Number/N4";
			this.Texture[(byte)IconSubType.N5] = "Prompt/Number/N5";
			this.Texture[(byte)IconSubType.Left] = "Prompt/DPad/Left";
			this.Texture[(byte)IconSubType.Right] = "Prompt/DPad/Right";
			this.Texture[(byte)IconSubType.Up] = "Prompt/DPad/Up";
			this.Texture[(byte)IconSubType.Down] = "Prompt/DPad/Down";
		}
	}
}
