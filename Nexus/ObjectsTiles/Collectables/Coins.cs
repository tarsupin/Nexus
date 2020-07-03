using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

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

		public override void Collect( RoomScene room, Character character, short gridX, short gridY ) {

			byte subType = room.tilemap.GetMainSubType(gridX, gridY);
			byte border = subType == (byte)CoinsSubType.Coin ? (byte)10 : (byte)2;

			// Get the Direction of an Inner Boundary
			DirCardinal newDir = TileSolidImpact.RunOverlapTest(character, gridX * (byte)TilemapEnum.TileWidth + border, gridX * (byte)TilemapEnum.TileWidth + (byte)TilemapEnum.TileWidth - border, gridY * (byte)TilemapEnum.TileHeight + border, gridY * (byte)TilemapEnum.TileHeight + (byte)TilemapEnum.TileHeight - border);

			if(newDir == DirCardinal.None) { return; }

			// Collect Coins
			room.PlaySound(Systems.sounds.coin, 0.6f, gridX * (byte)TilemapEnum.TileWidth, gridY * (byte)TilemapEnum.TileHeight);

			if(subType == (byte)CoinsSubType.Coin) {
				Systems.handler.levelState.AddCoins(character, 1);
			} else if(subType == (byte)CoinsSubType.Gem) {
				Systems.handler.levelState.AddCoins(character, 10);
			}

			base.Collect(room, character, gridX, gridY);
		}

		private void CreateTextures() {
			this.Texture = new string[2];
			this.Texture[(byte)CoinsSubType.Coin] = "Treasure/Coin";
			this.Texture[(byte)CoinsSubType.Gem] = "Treasure/Gem";
		}
	}
}
