using Nexus.Gameplay;
using Nexus.GameEngine;
using System.Collections.Generic;

namespace Nexus.Objects {

	public class ChomperFire : Chomper {

		public ChomperFire() : base() {
			this.hasSetup = true;
			this.SpriteName = "Chomper/Fire/Chomp";
			this.KnockoutName = "Particles/Chomp/Fire";
			this.DamageSurvive = DamageStrength.Standard;
			this.tileId = (byte)TileEnum.ChomperFire;
			this.title = "Fire Chomper";
			this.description = "Stationary enemy. Can shoot fireballs.";
			this.paramSet =  Params.ParamMap["FireBurst"];
		}

		public void SetupTile(RoomScene room, ushort gridX, ushort gridY) {

			// Track the activations for this tile.
			Dictionary<string, short> paramList = room.tilemap.GetParamList(gridX, gridY);

			// Reject this Cycle if the tile isn't triggered on at least one beat (considers first beat required)
			short beat1 = (short)((paramList.ContainsKey("beat1") ? (byte)paramList["beat1"] : 0) - 1);
				if(beat1 == -1) { return; }

				short beat2 = (short)((paramList.ContainsKey("beat2") ? (byte)paramList["beat2"] : 0) - 1);
			short beat3 = (short)((paramList.ContainsKey("beat3") ? (byte)paramList["beat3"] : 0) - 1);
			short beat4 = (short)((paramList.ContainsKey("beat4") ? (byte)paramList["beat4"] : 0) - 1);

			bool[] addToBeat = new bool[4] { false, false, false, false };

			// Since Beats check against tempo 16, we have to run a modulus 4 check to accomodate the QueueEvent.beatEvents.
			if(beat1 > -1) { addToBeat[beat1 % 4] = true; }
			if(beat2 > -1) { addToBeat[beat2 % 4] = true; }
			if(beat3 > -1) { addToBeat[beat3 % 4] = true; }
			if(beat4 > -1) { addToBeat[beat4 % 4] = true; }

			// Add All Relevant Beat Events
			if(addToBeat[0]) { room.queueEvents.AddBeatEvent(this.tileId, (short) gridX, (short) gridY, 0); }
			if(addToBeat[1]) { room.queueEvents.AddBeatEvent(this.tileId, (short) gridX, (short) gridY, 1); }
			if(addToBeat[2]) { room.queueEvents.AddBeatEvent(this.tileId, (short) gridX, (short) gridY, 2); }
			if(addToBeat[3]) { room.queueEvents.AddBeatEvent(this.tileId, (short) gridX, (short) gridY, 3); }
		}

	}
}
