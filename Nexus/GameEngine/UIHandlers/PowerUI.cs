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
			Character character = Systems.localServer.MyCharacter;
			short posX = (short)(Systems.screen.windowHalfWidth + (byte)(TilemapEnum.HalfWidth));

			// Power Positioning
			if(character.mobilityPower != null) { posX -= (byte)(TilemapEnum.HalfWidth + 16); }
			if(character.attackPower != null) { posX -= (byte)(TilemapEnum.HalfWidth + 16); }
			if(character.magiShield.IconTexture != null) { posX -= (byte)(TilemapEnum.HalfWidth + 16); }

			// Mobility Power
			if(character.mobilityPower != null) {
				UIHandler.atlas.Draw(UIIcon.Down, posX - 4, topRow - 4);
				atlas.Draw(character.mobilityPower.IconTexture, posX, topRow);
				posX += (byte)(TilemapEnum.TileWidth + 16);
			}

			// Attack Power
			if(character.attackPower != null) {
				UIHandler.atlas.Draw(UIIcon.Down, posX - 4, topRow - 4);
				atlas.Draw(character.attackPower.IconTexture, posX, topRow);
				posX += (byte)(TilemapEnum.TileWidth + 16);
			}

			// Magi-Shield
			if(character.magiShield.IconTexture != null) {
				UIHandler.atlas.Draw(UIIcon.Down, posX - 4, topRow - 4);
				atlas.Draw(character.magiShield.IconTexture, posX, topRow);
				posX += (byte)(TilemapEnum.TileWidth + 16);
			}

			// Dashing Power / Shoes
			if(character.shoes != null) {
				UIHandler.atlas.Draw(UIIcon.Down, posX - 4, topRow - 4);
				atlas.Draw(character.shoes.IconTexture, posX, topRow);
			}
		}
	}
}
