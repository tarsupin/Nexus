using Newtonsoft.Json.Linq;
using Nexus.Engine;
using Nexus.GameEngine;

namespace Nexus.Objects {

	public class EnemyFlight : Enemy {

		public EnemyFlight(RoomScene room, byte subType, FVector pos, JObject paramList) : base(room, subType, pos, paramList) {

		}
	}
}
