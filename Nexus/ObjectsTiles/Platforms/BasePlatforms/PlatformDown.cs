using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class PlatformDown : TileObject {

		protected string Texture;

		protected PlatformDown() : base() {
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.Platform];
		}

		public override bool RunImpact(RoomScene room, DynamicObject actor, ushort gridX, ushort gridY, DirCardinal dir) {

			// Actor must cross the UP threshold for this ledge; otherwise, it shouldn't compute any collision.
			if(!actor.physics.CrossedThresholdUp(gridY * (byte)TilemapEnum.TileHeight + (byte)TilemapEnum.TileHeight)) { return false; }

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
