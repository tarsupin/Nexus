using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class PlatformFixed : TileObject {

		protected string[] Texture;
		protected DirCardinal facing;

		public PlatformFixed() : base() {
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.Platform];
			this.facing = DirCardinal.Up;
			this.BuildTexture("Platform/Fixed/");
			this.tileId = (byte)TileEnum.PlatformFixed;
			this.title = "Fixed Platform";
			this.description = "A platform that never moves.";
		}

		public override bool RunImpact(RoomScene room, DynamicObject actor, ushort gridX, ushort gridY, DirCardinal dir) {
			bool collided = TileFacingImpact.RunImpact(actor, gridX, gridY, dir, this.facing);

			if(collided && actor is Character) {

				// Standard Character Tile Collisions
				TileCharBasicImpact.RunImpact((Character)actor, dir);
			}

			return collided;
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture[subType], posX, posY);
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
