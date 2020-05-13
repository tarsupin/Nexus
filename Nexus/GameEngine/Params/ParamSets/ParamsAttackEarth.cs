using Newtonsoft.Json.Linq;
using Nexus.Engine;

namespace Nexus.GameEngine {

	// TODO: Must change param names like "attSpeed" to "Speed" (see LevelConvert to handle this)
	interface IParamsAttackEarth {
		ushort Cycle { get; set; }			// Frequency of the attack (in frames)
		ushort Offset { get; set; }			// The offset of the frequency on the global timer.
		byte Speed { get; set; }			// Velocity of the stones (Y-axis).
	}

	public class IMechanicsAttackEarth {
		public ushort Cycle { get; set; }
		public ushort Offset { get; set; }
		public FInt Speed { get; set; }
	}

	public static class ParamsAttackEarthRules {
		public static IntParam Cycle = new IntParam("Attack Frequency", 60, 300, 15, 120, " frames");
		public static IntParam Offset = new IntParam("Timer Offset", 0, 300, 15, 0, " frames");
		public static IntParam Speed = new IntParam("Stone Speed", 20, 200, 10, 100, "%");
	}

	public static class ParamsAttackEarth {

		public static IMechanicsAttackEarth ConvertToMechanics( JObject paramsList ) {
			FInt speedMult = (paramsList["attSpeed"] == null ? FInt.Create(ParamsAttackEarthRules.Speed.defValue) : FInt.Create(paramsList["attSpeed"].Value<byte>())) / 100;

			return new IMechanicsAttackEarth() {
				Cycle = paramsList["attCycle"] == null ? (ushort) ParamsAttackEarthRules.Cycle.defValue : paramsList["attCycle"].Value<ushort>(),
				Offset = paramsList["attOffset"] == null ? (ushort) ParamsAttackEarthRules.Offset.defValue : paramsList["attOffset"].Value<ushort>(),
				Speed = speedMult * 4
			};
		}
	}
}
