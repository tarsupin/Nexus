using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using Nexus.Objects;

namespace Nexus.GameEngine {

	public class ConsoleUI {

		private readonly LevelScene scene;
		public Atlas atlas;
		public Player myPlayer;
		private readonly ushort bottomRow;

		public ConsoleUI( LevelScene scene ) {
			this.scene = scene;
			this.atlas = Systems.mapper.atlas[(byte) AtlasGroup.Tiles];
			this.myPlayer = Systems.localServer.MyPlayer;
			this.bottomRow = (ushort) (Systems.screen.windowHeight - (byte) TilemapEnum.TileHeight);
		}

		public void Draw() {
			Systems.fonts.console.Draw("This is a test", 65, 10, Color.White);
		}
	}
}
