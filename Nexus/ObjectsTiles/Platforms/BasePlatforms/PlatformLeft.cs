using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class PlatformLeft : TileGameObject {

		protected string Texture;

		protected PlatformLeft() : base() {
			this.collides = true;
		}

		public override bool RunImpact(RoomScene room, DynamicGameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {
			bool collided = TileFacingImpact.RunImpact(actor, gridX, gridY, dir, DirCardinal.Left);

			if(collided && actor is Character) {

				// Standard Character Tile Collisions
				TileCharBasicImpact.RunImpact((Character)actor, dir);
			}

			return collided;
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			this.atlas.DrawFaceLeft(this.Texture, posX, posY);
		}
	}
}
