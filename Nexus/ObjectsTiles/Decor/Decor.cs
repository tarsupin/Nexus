using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Decor : TileGameObject {

		public string[] Texture;

		public Decor() : base() {
			this.collides = false; // Since 'collides' is false, it never runs RunCollision() in base class.
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture[subType], posX, posY);
		}
	}
}
