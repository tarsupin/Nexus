using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class HorizontalWall : TileGameObject {

		protected string[] Texture;

		public HorizontalWall() : base() {
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.Ground];
		}

		public override bool RunImpact(RoomScene room, DynamicObject actor, ushort gridX, ushort gridY, DirCardinal dir) {
			bool collided = base.RunImpact(room, actor, gridX, gridY, dir);

			if(actor is Character) {
				TileCharBasicImpact.RunImpact((Character)actor, dir); // Standard Character Tile Collisions
			}

			else if(actor is Projectile) {
				TileProjectileImpact.RunImpact((Projectile)actor, gridX, gridY, dir);
			}

			return collided;
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture[subType], posX, posY);
		}

		protected void BuildTextures(string baseName) {
			this.Texture = new string[4];
			this.Texture[(byte) HorizontalSubTypes.S] = baseName + "S";
			this.Texture[(byte) HorizontalSubTypes.H1] = baseName + "H1";
			this.Texture[(byte) HorizontalSubTypes.H2] = baseName + "H2";
			this.Texture[(byte) HorizontalSubTypes.H3] = baseName + "H3";
		}
	}
}
