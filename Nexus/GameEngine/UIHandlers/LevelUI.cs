using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using Nexus.Objects;

namespace Nexus.GameEngine {

	public class LevelUI {

		private readonly LevelScene scene;
		public Atlas atlas;
		public Player myPlayer;
		private readonly ushort bottomRow;

		public LevelUI( LevelScene scene ) {
			this.scene = scene;
			this.atlas = Systems.mapper.atlas[(byte) AtlasGroup.Tiles];
			this.myPlayer = Systems.localServer.MyPlayer;
			this.bottomRow = (ushort) (Systems.screen.windowHeight - (byte) TilemapEnum.TileHeight);
		}

		public void Draw() {
			this.atlas.Draw("Treasure/Gem", 10, 10);

			// Draw health, if applicable.
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
