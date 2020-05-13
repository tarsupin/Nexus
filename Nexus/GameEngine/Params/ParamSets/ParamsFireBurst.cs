using Newtonsoft.Json.Linq;
using Nexus.Engine;
using Nexus.Gameplay;

namespace Nexus.GameEngine {

	interface IParamsFireBurst {
		byte Count { get; set; }			// Number of fireballs that gets shot simultaneously (1 to 3)
		byte Gravity { get; set; }			// Strength of gravity applied to fireballs launched (0% to 250%)
		ushort Cycle { get; set; }			// Frequency of the attack (in frames)
		ushort Offset { get; set; }			// The offset of the frequency on the global timer.
		byte Speed { get; set; }			// Velocity of the fireballs (Y-axis).
		byte Spread { get; set; }			// The % spread between fireballs launched (if applicable).
	}

	public class IMechanicsFireBurst {
		public byte Count { get; set; }
		public byte Gravity { get; set; }
		public ushort Cycle { get; set; }
		public ushort Offset { get; set; }
		public FInt Spread { get; set; }
		public FInt XSpeed { get; set; }
		public FInt YSpeed { get; set; }
	}

	public static class ParamsFireBurstRules {
		public static IntParam Count = new IntParam("Number of Fireballs", 1, 3, 1, 1);
		public static IntParam Gravity = new IntParam("Gravity Influence", 0, 200, 10, 100, "%");
		public static IntParam Cycle = new IntParam("Attack Frequency", 60, 300, 15, 120, " frames");
		public static IntParam Offset = new IntParam("Timer Offset", 0, 300, 15, 0, " frames");
		public static IntParam Speed = new IntParam("Fireball Speed", 20, 200, 10, 100, "%");
		public static IntParam Spread = new IntParam("Fireball Spread", 50, 250, 10, 100, "%");
	}

	public static class ParamsFireBurst {

		public static IMechanicsFireBurst ConvertToMechanics( JObject paramsList, DirCardinal dirFacing ) {

			FInt speedMult = (paramsList["attSpeed"] == null ? FInt.Create(ParamsFireBurstRules.Speed.defValue) : FInt.Create(paramsList["attSpeed"].Value<byte>())) / 100;
			FInt spreadMult = (paramsList["attSpread"] == null ? FInt.Create(ParamsFireBurstRules.Spread.defValue) : FInt.Create(paramsList["attSpread"].Value<byte>())) / 100;

			FInt xSpeed = FInt.Create(0);
			FInt ySpeed = FInt.Create(0);

			if(dirFacing == DirCardinal.Left) {
				xSpeed = -8 * speedMult;
			} else if(dirFacing == DirCardinal.Right) {
				xSpeed = 8 * speedMult;
			} else if(dirFacing == DirCardinal.Down) {
				ySpeed = 4 * speedMult;
			} else {
				ySpeed = (-8 * speedMult) - 4;
			}

			return new IMechanicsFireBurst() {
				Count = paramsList["attCount"] == null ? (byte) ParamsFireBurstRules.Cycle.defValue : paramsList["attCount"].Value<byte>(),
				Cycle = paramsList["attCycle"] == null ? (ushort) ParamsFireBurstRules.Cycle.defValue : paramsList["attCycle"].Value<ushort>(),
				Offset = paramsList["attOffset"] == null ? (ushort) ParamsFireBurstRules.Offset.defValue : paramsList["attOffset"].Value<ushort>(),
				Spread = spreadMult * FInt.Create(0.3),
				XSpeed = xSpeed,
				YSpeed = ySpeed
			};
		}
	}
}
