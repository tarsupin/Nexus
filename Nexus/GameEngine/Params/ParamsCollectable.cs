using Newtonsoft.Json.Linq;
using Nexus.Objects;

namespace Nexus.GameEngine {

	interface IParamsCollectable {
		CollectableRule Rule { get; set; }	// The rule applied to the collectable (permanent, regens, one per player, etc)
		byte RegenTime { get; set; }		// # of seconds it takes to regenerate (if applicable). 0 is doesn't regenerate.
	}

	public class IMechanicsCollectable {
		public byte Rule { get; set; }
		public byte RegenTime { get; set; }
	}

	public static class ParamsCollectableRules {
		public static LabeledParam Rule = new LabeledParam("Collectable Rule", new string[4] { "One Use Only", "One Per Player", "Always Available", "Regenerates After Use" }, (byte) CollectableRule.OneUseOnly);
		public static WholeRangeParam RegenTime = new WholeRangeParam("Regen Time", 0, 60, 1, 0);
	}

	public static class ParamsCollectable {

		public static IMechanicsCollectable ConvertToMechanics( JObject paramsList ) {

			return new IMechanicsCollectable() {
				Rule = paramsList["rule"] == null ? (byte) CollectableRule.OneUseOnly : paramsList["rule"].Value<byte>(),
				RegenTime = paramsList["regen"] == null ? (byte) ParamsCollectableRules.RegenTime.defValue : paramsList["rule"].Value<byte>()
			};
		}
	}
}
