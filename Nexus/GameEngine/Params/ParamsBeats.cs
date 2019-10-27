using Newtonsoft.Json.Linq;

namespace Nexus.GameEngine {

	// TODO: Must change param names like "attSpeed" to "Speed" (see LevelConvert to handle this)
	interface IParamsBeats {
		byte Beat1 { get; set; }
		byte Beat2 { get; set; }
		byte Beat3 { get; set; }
		byte Beat4 { get; set; }
	}

	public class IMechanicsBeats {
		public byte[] Beats { get; set; }		// Triggers every beat contained within the array.
	}

	public static class ParamsBeatsRules {
		public static WholeRangeParam Beat1 = new WholeRangeParam("Beat #1", 0, 20, 1, 0);
		public static WholeRangeParam Beat2 = new WholeRangeParam("Beat #2", 0, 20, 1, 0);
		public static WholeRangeParam Beat3 = new WholeRangeParam("Beat #3", 0, 20, 1, 0);
		public static WholeRangeParam Beat4 = new WholeRangeParam("Beat #4", 0, 20, 1, 0);
	}

	public static class ParamsBeats {

		public static IMechanicsBeats ConvertToMechanics( JObject paramsList ) {
			byte count = 0;

			if(paramsList["beat1"] != null) { count++; }
			if(paramsList["beat2"] != null) { count++; }
			if(paramsList["beat3"] != null) { count++; }
			if(paramsList["beat4"] != null) { count++; }

			byte[] beats = new byte[count];
			count = 0;

			if(paramsList["beat1"] != null) { beats[count] = paramsList["beat1"].Value<byte>(); count++; }
			if(paramsList["beat2"] != null) { beats[count] = paramsList["beat2"].Value<byte>(); count++; }
			if(paramsList["beat3"] != null) { beats[count] = paramsList["beat3"].Value<byte>(); count++; }
			if(paramsList["beat4"] != null) { beats[count] = paramsList["beat4"].Value<byte>(); count++; }

			return new IMechanicsBeats() {
				Beats = beats
			};
		}
	}
}
