using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class PlatformDown : TileGameObject {

		protected string Texture;

		protected PlatformDown() : base() {
			this.collides = true;
		}

		public override bool RunImpact(RoomScene room, DynamicGameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {
			bool collided = TileFacingImpact.RunImpact(actor, gridX, gridY, dir, DirCardinal.Down);

			if(collided && actor is Character) {

				// Standard Character Tile Collisions
				TileCharBasicImpact.RunImpact((Character)actor, dir);
			}

			return collided;
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			this.atlas.DrawFaceDown(this.Texture, posX, posY);
		}
	}
}
