using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public enum CoinsSubType : byte {
		Coin = 0,
		Gem = 1,
	}

	class Coins : Collectable {

		public Coins() : base() {
			this.CreateTextures();
			this.tileId = (byte)TileEnum.Coins;

			// Helper Texts
			this.titles = new string[2];
			this.titles[(byte)CoinsSubType.Coin] = "Small Gem";
			this.titles[(byte)CoinsSubType.Gem] = "Large Gem";

			this.descriptions = new string[2];
			this.descriptions[(byte)CoinsSubType.Coin] = "Adds 1 Gem to the character's collection.";
			this.descriptions[(byte)CoinsSubType.Gem] = "Adds 10 Gems to the character's collection.";
		}

		public override void Collect( RoomScene room, Character character, uint gridId ) {
			Systems.sounds.coin.Play();

			byte[] tileData = room.tilemap.GetTileDataAtGridID(gridId);

			if(tileData[1] == (byte)CoinsSubType.Coin) {
				Systems.handler.levelState.AddCoins(character, 1);
			} else if(tileData[1] == (byte)CoinsSubType.Gem) {
				Systems.handler.levelState.AddCoins(character, 10);
			}

			base.Collect(room, character, gridId);
		}

		private void CreateTextures() {
			this.Texture = new string[2];
			this.Texture[(byte)CoinsSubType.Coin] = "Treasure/Coin";
			this.Texture[(byte)CoinsSubType.Gem] = "Treasure/Gem";
		}
	}
}
