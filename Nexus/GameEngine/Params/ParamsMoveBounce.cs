using Newtonsoft.Json.Linq;
using Nexus.Gameplay;

namespace Nexus.GameEngine {

	interface IParamsMoveBounce {
		sbyte XMovement { get; set; }		// X Velocity in grid spaces.
		sbyte YMovement { get; set; }		// Y Velocity in grid spaces.
	}

	public class IMechanicsMoveBounce {
		public sbyte XMovement { get; set; }
		public sbyte YMovement { get; set; }
	}

	public static class ParamsMoveBounceRules {
		public static WholeRangeParam XMovement = new WholeRangeParam("X Movement", -6, 6, 1, 2, " tile(s)");
		public static WholeRangeParam YMovement = new WholeRangeParam("Y Movement", -6, 6, 1, 2, " tile(s)");
	}

	public static class ParamsMoveBounce {

		public static IMechanicsMoveBounce ConvertToMechanics( JObject paramsList ) {
			return new IMechanicsMoveBounce() {
				XMovement = paramsList["x"] == null ? (sbyte) ParamsMoveBounceRules.XMovement.defValue : paramsList["x"].Value<sbyte>(),
				YMovement = paramsList["y"] == null ? (sbyte) ParamsMoveBounceRules.YMovement.defValue : paramsList["y"].Value<sbyte>(),
			};
		}
	}
}
