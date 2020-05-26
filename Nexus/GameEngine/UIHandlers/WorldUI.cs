﻿using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using Nexus.Objects;

namespace Nexus.GameEngine {

	public class WorldUI {

		private readonly WorldScene scene;
		public Atlas atlas;
		public Player myPlayer;
		private readonly ushort bottomRow;
		private readonly LevelState levelState;

		public WorldUI( WorldScene scene ) {
			this.scene = scene;
			this.atlas = Systems.mapper.atlas[(byte) AtlasGroup.Tiles];
			this.myPlayer = Systems.localServer.MyPlayer;
			this.bottomRow = (ushort) (Systems.screen.windowHeight - (byte) TilemapEnum.TileHeight);
			this.levelState = Systems.handler.levelState;
		}

		public void RunTick() {

		}

		public void Draw() {

			// Coins / Gems
			this.atlas.Draw("Treasure/Gem", 10, 10);
			Systems.fonts.counter.Draw(this.levelState.coins.ToString(), 65, 10, Color.White);

			// Timer
			Systems.fonts.counter.Draw(this.levelState.TimeRemaining.ToString(), Systems.screen.windowWidth - 100, 10, Color.White);

			// Health & Armor
			if(this.myPlayer.character is Character) {
				CharacterWounds wounds = this.myPlayer.character.wounds;
				byte i = 0;

				while(i < wounds.Health) {
					this.atlas.Draw("Icon/Heart", 10 + 48 * i, this.bottomRow);
					i++;
				}

				while(i < wounds.Health + wounds.Armor) {
					this.atlas.Draw("Icon/Armor", 10 + 48 * i, this.bottomRow);
					i++;
				}
			}
		}
	}
}
