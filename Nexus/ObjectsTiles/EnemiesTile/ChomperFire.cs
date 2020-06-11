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

			var a = 1;
		}

	}
}
