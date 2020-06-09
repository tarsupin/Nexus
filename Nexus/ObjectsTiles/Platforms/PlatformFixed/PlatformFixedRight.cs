using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class PlatformFixedRight : TileObject {

		protected string[] Texture;

		public PlatformFixedRight() : base() {
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.Platform];
			this.BuildTexture("Platform/Fixed/");
			this.tileId = (byte)TileEnum.PlatformFixedRight;
			this.title = "Fixed Platform";
			this.description = "A platform that never moves.";
		}

		public override bool RunImpact(RoomScene room, GameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {

			// Actor must cross the LEFT threshold for this ledge; otherwise, it shouldn't compute any collision.
			if(!actor.physics.CrossedThresholdLeft(gridX * (byte)TilemapEnum.TileWidth + (byte)TilemapEnum.TileWidth)) { return false; }

			if(actor is Projectile) {
				if(!CollideTileFacing.RunImpactTest(dir, DirCardinal.Right)) { return false; }
				return TileProjectileImpact.RunImpact((Projectile)actor, gridX, gridY, dir);
			}

			bool collided = CollideTileFacing.RunImpact(actor, gridX, gridY, dir, DirCardinal.Right);

			// Additional Character Collisions (such as Wall Jumps)
			if(collided && actor is Character) {
				TileCharBasicImpact.RunImpact((Character)actor, dir);
			}

			return collided;
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			this.atlas.DrawFaceRight(this.Texture[subType], posX, posY);
		}

		protected void BuildTexture(string baseName) {
			this.Texture = new string[4];
			this.Texture[(byte) HorizontalSubTypes.S] = baseName + "S";
			this.Texture[(byte) HorizontalSubTypes.H1] = baseName + "H1";
			this.Texture[(byte) HorizontalSubTypes.H2] = baseName + "H2";
			this.Texture[(byte) HorizontalSubTypes.H3] = baseName + "H3";
		}
	}
}
