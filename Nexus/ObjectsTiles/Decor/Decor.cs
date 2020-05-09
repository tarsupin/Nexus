using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Decor : TileGameObject {

		public string[] Texture;

		public Decor() : base() {
			this.collides = false; // Since 'collides' is false, it never runs RunCollision() in base class.
			this.Meta = Systems.mapper.MetaList[MetaGroup.Decor];
			this.title = "Decorations";
			this.description = "Cosmetic only. Doesn't affect gameplay.";
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture[subType], posX, posY);
		}
	}
}
