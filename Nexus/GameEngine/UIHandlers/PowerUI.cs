using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.GameEngine {

	public class PowerUI {

		private readonly LevelUI levelUI;

		public PowerUI( LevelUI levelUI ) {
			this.levelUI = levelUI;
		}

		public void Draw() {

			Atlas atlas = this.levelUI.atlas;
			short bottomRow = (short)(this.levelUI.bottomRow - 8);
			Character character = this.levelUI.myPlayer.character;
			short posX = (short)(Systems.screen.windowWidth - (byte)TilemapEnum.TileWidth - 8);

			// Mobility Power
			if(character.mobilityPower != null) {
				atlas.Draw(character.mobilityPower.IconTexture, posX, bottomRow);
				posX -= (byte)TilemapEnum.TileWidth + 8;
			}

			// Attack Power
			if(character.attackPower != null) {
				atlas.Draw(character.attackPower.IconTexture, posX, bottomRow);
				posX -= (byte)TilemapEnum.TileWidth + 8;
			}

			// Magi-Shield
			if(character.magiShield.IconTexture != null) {
				atlas.Draw(character.magiShield.IconTexture, posX, bottomRow);
				//posX -= (byte)TilemapEnum.TileWidth + 8;
			}

			// Passive Power
		}
	}
}
