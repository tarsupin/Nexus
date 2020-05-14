using Newtonsoft.Json.Linq;
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
			this.paramSets = new Params[1] { Params.ParamMap["FireBurst"] };
		}

		public override void UpdateParams(JObject paramList) {
			// JToken token = paramList["attGrav"];
			// if(token != null) {
			//	int a = (int) paramList.GetValue("attGrav");
			//	System.Console.WriteLine("GRAV: " + a.ToString());
			// }
		}
	}
}
