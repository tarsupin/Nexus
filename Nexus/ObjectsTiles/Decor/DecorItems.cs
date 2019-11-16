using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class DecorItems : Decor {

		public enum CrysSubType : byte {
			Pot = 0,
			Tomb = 1,
			Gem1 = 2,
			Gem2 = 3,
			Gem3 = 4,
			Gem4 = 5,
		}

		public static void TileGenerate(RoomScene room, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the room. If it hasn't, create it.
			if(!room.IsTileGameObjectRegistered((byte) TileEnum.DecorItems)) {
				new DecorItems(room);
			}

			// Add to Tilemap
			room.tilemap.AddTile(gridX, gridY, (byte) TileEnum.DecorItems, subTypeId);
		}

		public DecorItems(RoomScene room) : base(room, TileEnum.DecorItems) {
			this.BuildTextures();
		}

		public void BuildTextures() {
			this.Texture = new string[6];
			this.Texture[(byte)CrysSubType.Pot] = "Decor/Pot";
			this.Texture[(byte)CrysSubType.Tomb] = "Decor/Tomb";
			this.Texture[(byte)CrysSubType.Gem1] = "Decor/Gem1";
			this.Texture[(byte)CrysSubType.Gem2] = "Decor/Gem2";
			this.Texture[(byte)CrysSubType.Gem3] = "Decor/Gem3";
			this.Texture[(byte)CrysSubType.Gem4] = "Decor/Gem4";
		}
	}
}
