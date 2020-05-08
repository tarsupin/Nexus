using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Cannon : TileGameObject {

		public string Texture;
		
		// TODO: Integrate params into Cannon

		public Cannon() : base() {
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.Generator];
		}

		public override bool RunImpact(RoomScene room, DynamicGameObject actor, ushort gridX, ushort gridY, DirCardinal dir) {
			return true;
		}

		// TODO: Run Cannon RunTick()
		public void RunTick(RoomScene room, ushort gridX, ushort gridY) {

			// Can only activate cannon when there's a beat frame.
			if(Systems.timer.IsBeatFrame) {

				// TODO: Track the activations for this cannon.
				uint gridId = room.tilemap.GetGridID(gridX, gridY);
				byte[] tileData = room.tilemap.GetTileDataAtGridID(gridId);
				byte subType = tileData[1];

				// TODO: Use the gridId as part of a dictionary or KVP that activates the cannonballs.

				// Run Cannon Activation
				this.ActivateCannon(room, subType, gridX, gridY);
			}
		}

		public virtual void ActivateCannon(RoomScene room, byte subType, ushort gridX, ushort gridY) {
			
		}
	}
}
