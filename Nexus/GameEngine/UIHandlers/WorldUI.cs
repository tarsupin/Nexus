using Nexus.Engine;
using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public class WorldUI {

		private readonly WorldScene scene;
		public Atlas atlas;
		private readonly ushort bottomRow;
		private readonly WorldContent worldContent;

		public WorldUI( WorldScene scene ) {
			this.scene = scene;
			this.atlas = Systems.mapper.atlas[(byte) AtlasGroup.Tiles];
			this.bottomRow = (ushort) (Systems.screen.windowHeight - (byte) WorldmapEnum.TileHeight);
			this.worldContent = this.scene.worldContent;
		}

		public void RunTick() {

		}

		public void Draw() {

			// Coins / Gems
			this.atlas.Draw("Treasure/Gem", 10, 10);
			//Systems.fonts.counter.Draw(this.levelState.coins.ToString(), 65, 10, Color.White);
		}
	}
}
