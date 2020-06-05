using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class PlatformRight : TileObject {

		protected string Texture;

		protected PlatformRight() : base() {
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.Platform];
		}

		public override bool RunImpact(RoomScene room, DynamicObject actor, ushort gridX, ushort gridY, DirCardinal dir) {

			// Actor must cross the RIGHT threshold for this ledge; otherwise, it shouldn't compute any collision.
			if(!actor.physics.CrossedThresholdRight(gridX * (byte)TilemapEnum.TileWidth + (byte)TilemapEnum.TileWidth)) { return false; }

			bool collided = CollideTileFacing.RunImpact(actor, gridX, gridY, dir, DirCardinal.Right);

			// Additional Character Collisions (such as Wall Jumps)
			if(collided && actor is Character) {
				TileCharBasicImpact.RunImpact((Character)actor, dir);
			}

			return collided;
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			this.atlas.DrawFaceRight(this.Texture, posX, posY);
		}
	}
}
