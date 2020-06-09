using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class SpringTile : TileObject {

		public string[] Texture;

		public SpringTile() : base() {
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.Block];
		}

		public override bool RunImpact(RoomScene room, GameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {

			if(actor is Character) {
				TileSolidImpact.RunImpact(actor, gridX, gridY, dir);
				return TileCharBasicImpact.RunImpact((Character)actor, dir);
			}
			
			if(actor is Projectile) {
				TileProjectileImpact.RunImpact((Projectile)actor, gridX, gridY, dir);
				return false;
			}

			// Objects that shouldn't interact with Spring.
			if(actor is EnemyFlight || actor is Platform) {
				return false;
			}

			return TileSolidImpact.RunImpact(actor, gridX, gridY, dir);
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {
			this.atlas.Draw(this.Texture[subType], posX, posY);
		}
	}
}
