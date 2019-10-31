using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public enum CoinsSubType : byte {
		Coin = 0,
		Gem = 1,
	}

	class Coins : Collectable {

		public static void TileGenerate(RoomScene room, ushort gridX, ushort gridY, byte subTypeId) {

			// Check if the ClassGameObject has already been created in the room. If it hasn't, create it.
			if(!room.IsClassGameObjectRegistered((byte) TileGameObjectId.CollectableCoin)) {
				new Coins(room);
			}

			// Add to Tilemap
			room.tilemap.AddTile(gridX, gridY, (byte) TileGameObjectId.CollectableCoin, subTypeId);
		}

		public Coins(RoomScene room) : base(room, TileGameObjectId.CollectableCoin) {
			this.CreateTextures();
		}

		public override void Collect( Character character, uint gridId ) {
			Systems.sounds.coin.Play();

			byte subType = this.room.tilemap.GetSubTypeAtGridID(gridId);

			if(subType == (byte)CoinsSubType.Coin) {
				Systems.handler.levelState.AddCoins(character, 1);
			} else if(subType == (byte)CoinsSubType.Gem) {
				Systems.handler.levelState.AddCoins(character, 10);
			}

			base.Collect(character, gridId);
		}

		private void CreateTextures() {
			this.Texture = new string[2];
			this.Texture[0] = "Treasure/Coin";
			this.Texture[1] = "Treasure/Gem";
		}
	}
}
