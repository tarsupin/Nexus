using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class EditorUI {

		private readonly EditorScene scene;
		public Atlas atlas;
		private readonly ushort bottomRow;
		private readonly LevelState levelState;

		public EditorUI( EditorScene scene ) {
			this.scene = scene;
			this.atlas = Systems.mapper.atlas[(byte) AtlasGroup.Tiles];
			this.bottomRow = (ushort) (Systems.screen.windowHeight - (byte) TilemapEnum.TileHeight);
			this.levelState = Systems.handler.levelState;
		}

		public void Draw() {

			// Coins / Gems
			this.atlas.Draw("Treasure/Gem", 10, 10);
			Systems.fonts.counter.Draw(this.levelState.coins.ToString(), 65, 10, Color.White);

			// Timer
			Systems.fonts.counter.Draw(this.levelState.TimeRemaining.ToString(), Systems.screen.windowWidth - 100, 10, Color.White);

		}
	}
}
