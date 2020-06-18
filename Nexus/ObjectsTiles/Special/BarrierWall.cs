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
			else if(dir == DirCardinal.Down) {
				if(actor is Character) { ((Character)actor).wounds.Death(); }
				else if(actor is Enemy) { ((Enemy)actor).Destroy(); }
			}
			return true;
		}

		public override void Draw(RoomScene room, byte subType, int posX, int posY) {}
	}
}
