using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class Cannon : BlockTile {

		public Cannon() : base() {
			this.hasSetup = true;
			this.collides = true;
			this.Meta = Systems.mapper.MetaList[MetaGroup.Generator];
			this.title = "Cannon";
			this.description = "Fires cannonballs at instructed times.";
			this.paramSet = Params.ParamMap["Beats"];
		}

		public void SetupTile(RoomScene room, ushort gridX, ushort gridY) {

			// Track the activations for this cannon.
			Dictionary<string, short> paramList = room.tilemap.GetParamList(gridX, gridY);

			// Reject this Cycle if the cannon isn't triggered on at least one beat (considers first beat required)
			short beat1 = (short)((paramList.ContainsKey("beat1") ? (byte)paramList["beat1"] : 0) - 1);
			if(beat1 == -1) { return; }

			short beat2 = (short)((paramList.ContainsKey("beat2") ? (byte)paramList["beat2"] : 0) - 1);
			short beat3 = (short)((paramList.ContainsKey("beat3") ? (byte)paramList["beat3"] : 0) - 1);
			short beat4 = (short)((paramList.ContainsKey("beat4") ? (byte)paramList["beat4"] : 0) - 1);

			bool[] addToBeat = new bool[4] { false, false, false, false };

			// Since Cannon Beats check against tempo 8 or 16, we have to run a modulus 4 check to accomodate the QueueEvent.beatEvents.
			if(beat1 > -1) { addToBeat[beat1 % 4] = true; }
			if(beat2 > -1) { addToBeat[beat2 % 4] = true; }
			if(beat3 > -1) { addToBeat[beat3 % 4] = true; }
			if(beat4 > -1) { addToBeat[beat4 % 4] = true; }
			
			// Add All Relevant Beat Events
			if(addToBeat[0]) { room.queueEvents.AddBeatEvent(this.tileId, (short)gridX, (short)gridY, 0); }
			if(addToBeat[1]) { room.queueEvents.AddBeatEvent(this.tileId, (short)gridX, (short)gridY, 1); }
			if(addToBeat[2]) { room.queueEvents.AddBeatEvent(this.tileId, (short)gridX, (short)gridY, 2); }
			if(addToBeat[3]) { room.queueEvents.AddBeatEvent(this.tileId, (short)gridX, (short)gridY, 3); }
		}

		// Only return false if (and/or when) the event should no longer be looped in the QueueEvent class. For Cannons, this shouldn't occur.
		public override bool TriggerEvent(RoomScene room, ushort gridX, ushort gridY, short val1 = 0, short val2 = 0) {

			// Track the activations for this cannon.
			byte subType = room.tilemap.GetMainSubType(gridX, gridY);
			Dictionary<string, short> paramList = room.tilemap.GetParamList(gridX, gridY);

			// Reject this Cycle if the cannon isn't triggered on this beat.
			short beat1 = (short)((paramList.ContainsKey("beat1") ? (byte)paramList["beat1"] : 0) - 1);
			short beat2 = (short)((paramList.ContainsKey("beat2") ? (byte)paramList["beat2"] : 0) - 1);
			short beat3 = (short)((paramList.ContainsKey("beat3") ? (byte)paramList["beat3"] : 0) - 1);
			short beat4 = (short)((paramList.ContainsKey("beat4") ? (byte)paramList["beat4"] : 0) - 1);

			// Make sure this beat is one of the targets assigned.
			byte beatMod16 = Systems.timer.beat16Modulus;
			if(beat1 != beatMod16 && beat2 != beatMod16 && beat3 != beatMod16 && beat4 != beatMod16) {
				return true;
			}

			byte cannonSpeed = paramList.ContainsKey("speed") ? (byte) paramList["speed"] : ParamsBeats.DefaultSpeed;

			// Run Cannon Activation
			this.ActivateCannon(room, subType, gridX, gridY, cannonSpeed);

			return true;
		}

		public virtual void ActivateCannon(RoomScene room, byte subType, ushort gridX, ushort gridY, byte cannonSpeed) { }
	}
}
