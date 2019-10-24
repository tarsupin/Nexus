using Newtonsoft.Json.Linq;
using Nexus.Engine;
using Nexus.GameEngine;

namespace Nexus.Objects {

	public class EnemyFlight : Enemy {

		public EnemyFlight(LevelScene scene, byte subType, FVector pos, JObject paramList) : base(scene, subType, pos, paramList) {

		}
	}
}
