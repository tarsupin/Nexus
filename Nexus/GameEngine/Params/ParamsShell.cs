using Newtonsoft.Json.Linq;
using Nexus.Gameplay;

namespace Nexus.GameEngine {

	interface IParamsShell {
		sbyte XMovement { get; set; }		// X Velocity in grid spaces.
		sbyte YMovement { get; set; }		// Y Velocity in grid spaces.
	}

	public class IMechanicsShell {
		public sbyte XMovement { get; set; }
		public sbyte YMovement { get; set; }
	}

	public static class ParamsShellRules {
		public static WholeRangeParam XMovement = new WholeRangeParam("X Movement", -7, 7, 1, 0, " tile(s)");
		public static WholeRangeParam YMovement = new WholeRangeParam("Y Movement", -12, 7, 1, 0, " tile(s)");
	}

	public static class ParamsShell {

		public static IMechanicsShell ConvertToMechanics( JObject paramsList ) {
			return new IMechanicsShell() {
				XMovement = paramsList["x"] == null ? (sbyte) ParamsShellRules.XMovement.defValue : paramsList["x"].Value<sbyte>(),
				YMovement = paramsList["y"] == null ? (sbyte) ParamsShellRules.YMovement.defValue : paramsList["y"].Value<sbyte>(),
			};
		}
	}
}
