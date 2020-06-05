using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class PlatformUp : TileObject {

		protected string Texture;

		protected PlatformUp() : base() {
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.Platform];
		}

		public override bool RunImpact(RoomScene room, DynamicObject actor, ushort gridX, ushort gridY, DirCardinal dir) {

			// Actor must cross the DOWN threshold for this ledge; otherwise, it shouldn't compute any collision.
			if(!actor.physics.CrossedThresholdDown(gridY * (byte)TilemapEnum.TileHeight)) { return false; }

			bool collided = CollideTileFacing.RunImpact(actor, gridX, gridY, dir, DirCardinal.Up);

			// Additional Character Collisions (such as Wall Jumps)
			if(collided && actor is Character) {
				TileCharBasicImpact.RunImpact((Character)actor, dir);
			}

			return collided;
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture, posX, posY);
		}
	}
}
