using Newtonsoft.Json.Linq;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	interface IParamsMoveFlight {
		FlightMovement FlyType { get; set; }		// Indicates what type of flight pattern will be used.
		sbyte XMovement { get; set; }               // X Movement (in grid squares) - Axis, Quadratic
		sbyte YMovement { get; set; }               // Y Movement (in grid squares) - Axis, Quadratic
		sbyte MidX { get; set; }                    // Midpoint of X Movement (in grid squares) - Quadratic
		sbyte MidY { get; set; }                    // Midpoint of Y Movement (in grid squares) - Quadratic
		sbyte Diameter { get; set; }				// Diameter of movement - Circle
		bool Reverse { get; set; }                  // Whether to move in the reverse direction or not. - Circle, Axis, Quadratic
		byte Countdown { get; set; }                // An optional timer that counts down, then causes a drop. - To
		ushort Duration { get; set; }               // How long the full cycle / duration of the movement lasts.
		ushort Offset { get; set; }					// The duration offset of the global timer.
		byte ToTrack { get; set; }                  // The ID of the track to move to - Track
		byte ClusterID { get; set; }                // If set, this object acts as a cluster parent.
		byte ParentClusterID { get; set; }          // If set, this object is bound to a parent cluster.
	}

	public class IMechanicsMoveFlight {
		public byte FlyType { get; set; }
		public sbyte XMovement { get; set; }
		public sbyte YMovement { get; set; }
		public sbyte MidX { get; set; }
		public sbyte MidY { get; set; }
		public sbyte Diameter { get; set; }
		public bool Reverse { get; set; }
		public byte Countdown { get; set; }
		public ushort Duration { get; set; }
		public ushort Offset { get; set; }
		public byte ToTrack { get; set; }
		public byte ClusterID { get; set; }
		public byte ParentClusterID { get; set; }
	}

	public static class ParamsMoveFlightRules {

		public static DictionaryParam FlyType = new DictionaryParam("Movement Type", new Dictionary<byte, string>() {
			{ (byte) FlightMovement.Axis, "Axis" },
			{ (byte) FlightMovement.Quadratic, "Quadratic" },
			{ (byte) FlightMovement.Circle, "Circle" },
			{ (byte) FlightMovement.Track, "Track" },
			{ (byte) FlightMovement.To, "Toward Destination" },
		}, (byte) FlightMovement.Axis );

		public static WholeRangeParam Duration = new WholeRangeParam("Move Duration", 60, 3600, 15, 180, " frame(s)");
		public static WholeRangeParam Offset = new WholeRangeParam("Timer Offset", 60, 3600, 15, 180, " frame(s)");

		public static WholeRangeParam XMovement = new WholeRangeParam("X Movement", -20, 20, 1, 0, " tiles(s)");
		public static WholeRangeParam YMovement = new WholeRangeParam("Y Movement", -20, 20, 1, 0, " tiles(s)");

		public static WholeRangeParam MidX = new WholeRangeParam("X Midpoint", -20, 20, 1, 0, " tiles(s)");
		public static WholeRangeParam MidY = new WholeRangeParam("Y Midpoint", -20, 20, 1, 0, " tiles(s)");

		public static WholeRangeParam Diameter = new WholeRangeParam("Diameter", 0, 20, 1, 0, " tiles(s)");

		public static BoolParam Reverse = new BoolParam("Reverses Direction", false);
		public static WholeRangeParam Countdown = new WholeRangeParam("Countdown", 0, 60, 1, 0, " second(s)");

		public static WholeRangeParam ToTrack = new WholeRangeParam("To Track #", 0, 100, 1, 1);
		public static WholeRangeParam ClusterID = new WholeRangeParam("Act As Cluster ID", 0, 9, 1, 0);
		public static WholeRangeParam ParentClusterID = new WholeRangeParam("Link To Cluster ID", 0, 9, 1, 0);
	}

	public static class ParamsMoveFlight {

		public static IMechanicsMoveFlight ConvertToMechanics( JObject paramsList ) {
			IMechanicsMoveFlight mechanics = new IMechanicsMoveFlight();

			mechanics.FlyType = paramsList["fly"] == null ? ParamsMoveFlightRules.FlyType.defValue : paramsList["fly"].Value<byte>();

			switch(mechanics.FlyType) {

				case (byte) FlightMovement.Axis:
					mechanics = ParamsMoveFlight.GetMechanicsAxis(paramsList, mechanics);
					break;

				case (byte) FlightMovement.Quadratic:
					mechanics = ParamsMoveFlight.GetMechanicsQuadratic(paramsList, mechanics);
					break;

				case (byte) FlightMovement.Circle:
					mechanics = ParamsMoveFlight.GetMechanicsCircle(paramsList, mechanics);
					break;

				case (byte) FlightMovement.Track:
					mechanics = ParamsMoveFlight.GetMechanicsTrack(paramsList, mechanics);
					break;

				case (byte) FlightMovement.To:
					mechanics = ParamsMoveFlight.GetMechanicsTo(paramsList, mechanics);
					break;
			}

			// Add Durations and Clusters
			mechanics.Duration = paramsList["duration"] == null ? (ushort) ParamsMoveFlightRules.Duration.defValue : paramsList["duration"].Value<ushort>();
			mechanics.Offset = paramsList["durationOffset"] == null ? (ushort) ParamsMoveFlightRules.Offset.defValue : paramsList["durationOffset"].Value<ushort>();
			mechanics.ClusterID = paramsList["clusterId"] == null ? (byte) ParamsMoveFlightRules.ClusterID.defValue : paramsList["clusterId"].Value<byte>();
			mechanics.ParentClusterID = paramsList["toCluster"] == null ? (byte) ParamsMoveFlightRules.ParentClusterID.defValue : paramsList["toCluster"].Value<byte>();

			return mechanics;
		}

		public static IMechanicsMoveFlight GetMechanicsAxis( JObject paramsList, IMechanicsMoveFlight mechanics ) {
			mechanics.XMovement = paramsList["x"] == null ? (sbyte) ParamsMoveFlightRules.XMovement.defValue : paramsList["x"].Value<sbyte>();
			mechanics.YMovement = paramsList["y"] == null ? (sbyte) ParamsMoveFlightRules.YMovement.defValue : paramsList["y"].Value<sbyte>();
			mechanics.Reverse = paramsList["reverse"] == null ? (bool) ParamsMoveFlightRules.Reverse.defValue : paramsList["reverse"].Value<bool>();
			return mechanics;
		}

		public static IMechanicsMoveFlight GetMechanicsQuadratic( JObject paramsList, IMechanicsMoveFlight mechanics ) {
			mechanics.XMovement = paramsList["x"] == null ? (sbyte) ParamsMoveFlightRules.XMovement.defValue : paramsList["x"].Value<sbyte>();
			mechanics.YMovement = paramsList["y"] == null ? (sbyte) ParamsMoveFlightRules.YMovement.defValue : paramsList["y"].Value<sbyte>();
			mechanics.MidX = paramsList["midX"] == null ? (sbyte) ParamsMoveFlightRules.MidX.defValue : paramsList["midX"].Value<sbyte>();
			mechanics.MidY = paramsList["midY"] == null ? (sbyte) ParamsMoveFlightRules.MidY.defValue : paramsList["midY"].Value<sbyte>();
			mechanics.Reverse = paramsList["reverse"] == null ? ParamsMoveFlightRules.Reverse.defValue : paramsList["reverse"].Value<bool>();
			return mechanics;
		}

		public static IMechanicsMoveFlight GetMechanicsCircle( JObject paramsList, IMechanicsMoveFlight mechanics ) {
			mechanics.Diameter = paramsList["diameter"] == null ? (sbyte) ParamsMoveFlightRules.Diameter.defValue : paramsList["diameter"].Value<sbyte>();
			mechanics.Reverse = paramsList["reverse"] == null ? ParamsMoveFlightRules.Reverse.defValue : paramsList["reverse"].Value<bool>();
			return mechanics;
		}

		public static IMechanicsMoveFlight GetMechanicsTrack( JObject paramsList, IMechanicsMoveFlight mechanics ) {
			mechanics.ToTrack = paramsList["toTrack"] == null ? (byte)ParamsMoveFlightRules.ToTrack.defValue : paramsList["toTrack"].Value<byte>();
			return mechanics;
		}

		public static IMechanicsMoveFlight GetMechanicsTo( JObject paramsList, IMechanicsMoveFlight mechanics ) {
			mechanics.XMovement = paramsList["x"] == null ? (sbyte) ParamsMoveFlightRules.XMovement.defValue : paramsList["x"].Value<sbyte>();
			mechanics.YMovement = paramsList["y"] == null ? (sbyte) ParamsMoveFlightRules.YMovement.defValue : paramsList["y"].Value<sbyte>();
			mechanics.Countdown = paramsList["countdown"] == null ? (byte) ParamsMoveFlightRules.Countdown.defValue : paramsList["countdown"].Value<byte>();
			return mechanics;
		}
	}
}
