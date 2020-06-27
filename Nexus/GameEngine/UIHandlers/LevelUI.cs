using Microsoft.Xna.Framework;
using Nexus.Config;
using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using Nexus.Objects;

namespace Nexus.GameEngine {

	public class LevelUI {

		private readonly LevelScene scene;
		private readonly MenuUI menuUI;
		private readonly PowerUI powerUI;
		public Atlas atlas;
		public Player myPlayer;
		public readonly short bottomRow;
		private readonly LevelState levelState;

		public LevelUI( LevelScene scene ) {
			this.scene = scene;
			this.menuUI = new MenuUI(this);
			this.powerUI = new PowerUI(this);
			this.atlas = Systems.mapper.atlas[(byte) AtlasGroup.Tiles];
			this.bottomRow = (short) (Systems.screen.windowHeight - (byte) TilemapEnum.TileHeight);
			this.levelState = Systems.handler.levelState;
		}

		public void RunTick() {
			this.menuUI.RunTick();
		}

		public void Draw() {

			// Menu
			this.menuUI.Draw();

			// Coins / Gems
			this.atlas.Draw("Treasure/Gem", 10, 10);
			Systems.fonts.counter.Draw(this.levelState.coins.ToString(), 65, 10, Color.White);

			// Timer
			Systems.fonts.counter.Draw(this.levelState.TimeRemaining.ToString(), Systems.screen.windowWidth - 90, 10, Color.White);

			// Health & Armor
			if(Systems.localServer.MyPlayer.character is Character) {
				CharacterWounds wounds = Systems.localServer.MyCharacter.wounds;
				byte i = 0;

				while(i < wounds.Health) {
					this.atlas.Draw("Icon/HP", 10 + 48 * i, this.bottomRow);
					i++;
				}

				while(i < wounds.Health + wounds.Armor) {
					this.atlas.Draw("Icon/Shield", 10 + 48 * i, this.bottomRow);
					i++;
				}

				// Power Icons
				this.powerUI.Draw();
			}

			// Debug Render
			if(DebugConfig.Debug) {
				DebugConfig.DrawDebugNotes();
			}
		}
	}
}
