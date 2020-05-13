using Newtonsoft.Json.Linq;

namespace Nexus.GameEngine {

	interface IParamsEmblem {
		byte Color { get; set; }		// The color of the emblem (ToggleColor enum)
		bool On { get; set; }			// Whether the emblem starts "on" or not.
	}

	public class IMechanicsEmblem {
		public byte Color { get; set; }
		public bool On { get; set; }
	}

	public static class ParamsEmblemRules {
		public static LabeledParam Color = new LabeledParam("Emblem Color", new string[5] { "None", "Blue", "Red", "Green", "Yellow" }, (byte) 0);
		public static BoolParam On = new BoolParam("Active at Start", false);
	}

	public static class ParamsEmblem {

		public static IMechanicsEmblem ConvertToMechanics( JObject paramsList ) {

			return new IMechanicsEmblem() {
				Color = paramsList["color"] == null ? (byte) 0 : paramsList["color"].Value<byte>(),
				On = paramsList["on"] == null ? (bool) ParamsEmblemRules.On.defValue : paramsList["on"].Value<bool>()
			};
		}
	}
}
