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
			short topRow = (short)10;
			//short bottomRow = (short)(this.levelUI.bottomRow - 8);
			Character character = this.levelUI.myPlayer.character;
			short posX = (short)(Systems.screen.windowHalfWidth + (byte)(TilemapEnum.HalfWidth));

			// Power Positioning
			if(character.mobilityPower != null) { posX -= (byte)(TilemapEnum.HalfWidth + 8); }
			if(character.attackPower != null) { posX -= (byte)(TilemapEnum.HalfWidth + 8); }
			if(character.magiShield.IconTexture != null) { posX -= (byte)(TilemapEnum.HalfWidth + 8); }

			// Passive Power


			// Mobility Power
			if(character.mobilityPower != null) {
				atlas.Draw(character.mobilityPower.IconTexture, posX, topRow);
				posX += (byte)(TilemapEnum.TileWidth + 8);
			}

			// Attack Power
			if(character.attackPower != null) {
				atlas.Draw(character.attackPower.IconTexture, posX, topRow);
				posX += (byte)(TilemapEnum.TileWidth + 8);
			}

			// Magi-Shield
			if(character.magiShield.IconTexture != null) {
				atlas.Draw(character.magiShield.IconTexture, posX, topRow);
				//posX += (byte)(TilemapEnum.TileWidth + 8);
			}

			// Passive Power
		}
	}
}
