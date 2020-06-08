using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class Cannon : BlockTile {

		// TODO: Integrate params into Cannon

		public Cannon() : base() {
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.Generator];
			this.title = "Cannon";
			this.description = "Fires cannonballs at instructed times.";
			this.paramSet = Params.ParamMap["Beats"];
		}

		// Return false if (and/or when) the event should no longer be looped in the QueueEvent class for a given beatMod.
		public override bool TriggerEvent(RoomScene room, ushort gridX, ushort gridY, short beatMod, short val2 = 0) {

			// Track the activations for this cannon.
			byte subType = room.tilemap.GetMainSubType(gridX, gridY);
			Dictionary<string, short> paramList = room.tilemap.GetParamList(gridX, gridY);

			// Reject this Cycle if the cannon isn't triggered on this beat.
			if(!paramList.ContainsKey("beat" + (beatMod + 1).ToString())) { return false; }

			byte cannonSpeed = paramList.ContainsKey("speed") ? (byte)paramList["speed"] : (byte)0;

			// Run Cannon Activation
			this.ActivateCannon(room, subType, gridX, gridY, cannonSpeed);

			return true;
		}

		public virtual void ActivateCannon(RoomScene room, byte subType, ushort gridX, ushort gridY, byte cannonSpeed) { }
	}
}
