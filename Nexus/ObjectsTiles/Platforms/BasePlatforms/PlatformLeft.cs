using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class PlatformLeft : TileGameObject {

		protected string Texture;

		protected PlatformLeft(RoomScene room, TileGameObjectId classId) : base(room, classId, AtlasGroup.Tiles) {
			this.collides = true;
		}

		public override bool RunImpact(DynamicGameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {
			bool collided = TileFacingImpact.RunImpact(actor, gridX, gridY, dir, DirCardinal.Left);

			if(collided && actor is Character) {

				// Standard Character Tile Collisions
				TileCharBasicImpact.RunImpact((Character)actor, dir);
			}

			return collided;
		}

		public override void Draw(byte subType, int posX, int posY) {
			this.atlas.DrawFaceLeft(this.Texture, posX, posY);
		}
	}
}
