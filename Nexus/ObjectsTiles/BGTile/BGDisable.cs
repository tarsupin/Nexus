using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class BGDisable : BGTile {

		public string Texture;

		public BGDisable() : base() {
			this.tileId = (byte)TileEnum.BGDisable;
			this.Texture = "BGObject/Disable";
		}

		public override void ActivateBGEffect(Character character) {
			if(character.DisableAbilities()) {
				Systems.sounds.disableCollectable.Play();
			}
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture, posX, posY);
		}
	}
}
