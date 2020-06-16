using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	public class BarrierWall : TileObject {

		public BarrierWall() : base() {
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.Block];
			this.tileId = (byte)TileEnum.BarrierWall;
		}

		public override bool RunImpact(RoomScene room, GameObject actor, short gridX, short gridY, DirCardinal dir) {
			TileSolidImpact.RunImpact(actor, gridX, gridY, dir);
			if(actor is Projectile) { actor.Destroy(); }
			return true;
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {}
	}
}
