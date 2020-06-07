using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;

namespace Nexus.Objects {

	public class Cannon : TileObject {

		public string Texture;
		
		// TODO: Integrate params into Cannon

		public Cannon() : base() {
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.Generator];
			this.title = "Cannon";
			this.description = "Fires cannonballs at instructed times.";
			this.paramSet = Params.ParamMap["Beats"];
		}

		public override bool RunImpact(RoomScene room, DynamicObject actor, ushort gridX, ushort gridY, DirCardinal dir) {
			return true;
		}

		// TODO: Run Cannon RunTick()
		public void RunTick(RoomScene room, ushort gridX, ushort gridY) {

			// Can only activate cannon when there's a beat frame.
			if(Systems.timer.IsBeatFrame) {


				// TODO: Track the activations for this cannon.
				byte subType = room.tilemap.GetMainSubType(gridX, gridY);

				// TODO: Use the gridId as part of a dictionary or KVP that activates the cannonballs.

				// Run Cannon Activation
				this.ActivateCannon(room, subType, gridX, gridY);
			}
		}

		public virtual void ActivateCannon(RoomScene room, byte subType, ushort gridX, ushort gridY) {
			
		}
	}
}
