using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class BlockTile : TileGameObject {

		public BlockTile() : base() {
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.Block];
		}

		public override bool RunImpact(RoomScene room, DynamicObject actor, ushort gridX, ushort gridY, DirCardinal dir) {

			if(actor is Character) {
				TileSolidImpact.RunImpact(actor, gridX, gridY, dir);
				return TileCharBasicImpact.RunImpact((Character)actor, dir);
			}
			
			if(actor is Projectile) {
				return TileProjectileImpact.RunImpact((Projectile)actor, gridX, gridY, dir);
			}

			return TileSolidImpact.RunImpact(actor, gridX, gridY, dir);
		}
	}
}
