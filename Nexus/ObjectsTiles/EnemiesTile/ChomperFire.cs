using Nexus.Gameplay;
using Nexus.GameEngine;

namespace Nexus.Objects {

	public class ChomperFire : Chomper {

		public ChomperFire() : base() {
			this.SpriteName = "Chomper/Fire/Chomp";
			this.KnockoutName = "Particles/Chomp/Fire";
			this.tileId = (byte)TileEnum.ChomperFire;
			this.title = "Fire Chomper";
			this.description = "Stationary enemy. Can shoot fireballs.";
			this.paramSet =  Params.ParamMap["FireBurst"];
		}
	}
}
