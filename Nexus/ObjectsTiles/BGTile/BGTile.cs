using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class BGTile : TileGameObject {

		public BGTile() : base() {
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.BGTile];
		}

		public override bool RunImpact(RoomScene room, DynamicGameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {

			if(actor is Character) {
				this.ActivateBGEffect((Character)actor);
			}

			return false;
		}

		public virtual void ActivateBGEffect( Character character ) {}
	}
}
