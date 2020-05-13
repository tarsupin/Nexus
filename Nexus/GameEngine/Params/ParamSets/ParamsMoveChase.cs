using Newtonsoft.Json.Linq;
using Nexus.Gameplay;

namespace Nexus.GameEngine {

	interface IParamsMoveChase {
		FlightChaseAxis Axis { get; set; }  // The axis that the chase is allowed to happen on (can be both).
		byte Speed { get; set; }            // Velocity of chasing.
		byte Chase { get; set; }			// Distance a character must be within before chasing with initiate.
		byte Flee { get; set; }				// Distance from character that the actor will initiate flee.
		byte Stall { get; set; }			// Distance from character that the actor will initiate stall (hold still)
		bool Returns { get; set; }			// TRUE if actor returns to their original position when not chasing.
		byte ReturnDelay { get; set; }		// # of frames (delay) before returning to original position (after chase, flee, etc)
	}

	public class IMechanicsMoveChase {
		public FlightChaseAxis Axis { get; set; }
		public byte Speed { get; set; }
		public byte Chase { get; set; }
		public byte Flee { get; set; }
		public byte Stall { get; set; }
		public bool Returns { get; set; }
		public byte ReturnDelay { get; set; }
	}

	public static class ParamsMoveChaseRules {
		public static LabeledParam Axis = new LabeledParam("Movement Axis", new string[3] { "Both", "Vertical", "Horizontal" }, 0);
		public static IntParam Speed = new IntParam("Movement Speed", 10, 200, 10, 100, "%");
		public static IntParam Chase = new IntParam("Chase Range", 60, 300, 15, 120, " tile(s)");
		public static IntParam Flee = new IntParam("Flee Range", 0, 300, 15, 0, " tile(s)");
		public static IntParam Stall = new IntParam("Stall Range", 20, 200, 10, 100, " tile(s)");
		public static BoolParam Returns = new BoolParam("Returns to Start", true);
		public static IntParam ReturnDelay = new IntParam("Delay for Returning", 0, 300, 15, 120, " frames");
	}

	public static class ParamsMoveChase {

		public static IMechanicsMoveChase ConvertToMechanics( JObject paramsList ) {
			IMechanicsMoveChase mechanics = new IMechanicsMoveChase();

			// Movement Axis
			if(paramsList["axis"] == null) {
				mechanics.Axis = (byte) FlightChaseAxis.Both;
			} else {
				mechanics.Axis = paramsList["axis"].Value<FlightChaseAxis>();
			}

			// Movement Speed
			mechanics.Speed = (byte) ((paramsList["speed"] == null ? (byte)ParamsMoveChaseRules.Speed.defValue : paramsList["speed"].Value<byte>()) / 100 * 2);

			// Chase, Flee, Stall Ranges
			mechanics.Stall = paramsList["stall"] == null ? (byte) ParamsMoveChaseRules.Stall.defValue : paramsList["stall"].Value<byte>();
			mechanics.Flee = paramsList["flee"] == null ? (byte) ParamsMoveChaseRules.Flee.defValue : paramsList["flee"].Value<byte>();

			if(mechanics.Flee < mechanics.Stall) {
				mechanics.Flee = 0;
			}

			mechanics.Chase = paramsList["chase"] == null ? (byte) ParamsMoveChaseRules.Chase.defValue : paramsList["chase"].Value<byte>();

			if(mechanics.Chase < mechanics.Flee) {
				mechanics.Chase = mechanics.Flee;
			}

			// Returning to Starting Position
			mechanics.Returns = paramsList["returns"] == null ? ParamsMoveChaseRules.Returns.defValue : paramsList["returns"].Value<bool>();
			mechanics.ReturnDelay = paramsList["retDelay"] == null ? (byte) ParamsMoveChaseRules.ReturnDelay.defValue : paramsList["retDelay"].Value<byte>();

			return mechanics;
		}
	}
}
