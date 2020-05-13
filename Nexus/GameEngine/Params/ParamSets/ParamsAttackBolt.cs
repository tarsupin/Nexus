using Newtonsoft.Json.Linq;
using Nexus.Engine;
using System.Collections;

namespace Nexus.GameEngine {

	// TODO: Must change param names like "attSpeed" to "Speed" (see LevelConvert to handle this)
	interface IParamsAttackBolt {
		byte Count { get; set; }			// Number of bolts that gets shot simultaneously (1 to 3)
		ushort Cycle { get; set; }			// Frequency of the attack (in frames)
		ushort Offset { get; set; }			// The offset of the frequency on the global timer.
		byte Speed { get; set; }			// Velocity of the bolts (Y-axis).
		byte Spread { get; set; }			// The % spread between each bolt.
	}

	public class IMechanicsAttackBolt {
		public byte Count { get; set; }
		public ushort Cycle { get; set; }
		public ushort Offset { get; set; }
		public FInt Speed { get; set; }
		public FInt Spread { get; set; }
	}

	public static class ParamsAttackBoltRules {
		public static IntParam Count = new IntParam("Number of Bolts", 1, 3, 1, 1);
		public static IntParam Cycle = new IntParam("Attack Frequency", 60, 300, 15, 120, " frames");
		public static IntParam Offset = new IntParam("Timer Offset", 0, 300, 15, 0, " frames");
		public static IntParam Speed = new IntParam("Bolt Speed", 20, 200, 10, 100, "%");
		public static IntParam Spread = new IntParam("Bolt Spread", 50, 200, 10, 100, "%");
	}

	public static class ParamsAttackBolt {

		public static IMechanicsAttackBolt ConvertToMechanics( JObject paramsList ) {
			FInt speedMult = (paramsList["attSpeed"] == null ? FInt.Create(ParamsAttackBoltRules.Speed.defValue) : FInt.Create(paramsList["attSpeed"].Value<byte>())) / 100;
			FInt spreadMult = (paramsList["attSpread"] == null ? FInt.Create(ParamsAttackBoltRules.Spread.defValue) : FInt.Create(paramsList["attSpread"].Value<byte>())) / 100;

			return new IMechanicsAttackBolt() {
				Count = paramsList["attCount"] == null ? (byte) ParamsAttackBoltRules.Cycle.defValue : paramsList["attCount"].Value<byte>(),
				Cycle = paramsList["attCycle"] == null ? (ushort) ParamsAttackBoltRules.Cycle.defValue : paramsList["attCycle"].Value<ushort>(),
				Offset = paramsList["attOffset"] == null ? (ushort) ParamsAttackBoltRules.Offset.defValue : paramsList["attOffset"].Value<ushort>(),
				Speed = speedMult * 4,
				Spread = spreadMult * FInt.Create(0.3)
			};
		}
	}
}
