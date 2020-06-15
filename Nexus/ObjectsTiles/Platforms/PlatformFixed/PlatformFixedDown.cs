using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class PlatformFixedDown : TileObject {

		protected string[] Texture;

		public PlatformFixedDown() : base() {
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.Platform];
			this.BuildTexture("Platform/Fixed/");
			this.tileId = (byte)TileEnum.PlatformFixedDown;
			this.title = "Fixed Platform";
			this.description = "A platform that never moves.";
		}

		public override bool RunImpact(RoomScene room, GameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {

			// Actor must cross the UP threshold for this ledge; otherwise, it shouldn't compute any collision.
			if(!actor.physics.CrossedThresholdUp(gridY * (byte)TilemapEnum.TileHeight + (byte)TilemapEnum.TileHeight)) { return false; }

			if(actor is Projectile) {
				if(!CollideTileFacing.RunImpactTest(dir, DirCardinal.Down)) { return false; }
				return TileProjectileImpact.RunImpact((Projectile)actor, gridX, gridY, dir);
			}

			// Allow Dropdown Mechanic
			if(actor is Character) {
				if(((Character)actor).status.action is DropdownAction) {
					return false;
				}
			}

			return CollideTileFacing.RunImpact(actor, gridX, gridY, dir, DirCardinal.Down);
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			this.atlas.DrawFaceDown(this.Texture[subType], posX, posY);
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
