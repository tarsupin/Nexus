using Newtonsoft.Json.Linq;

namespace Nexus.GameEngine {

	interface IParamsTrack {
		byte TrackNum { get; set; }				// The ID of the track.
		byte ToTrack { get; set; }				// The ID that the track will move to.
		ushort Duration { get; set; }			// The duration of how long this track will take to move to the next one.
		ushort Delay { get; set; }				// The duration of delay before the track leaves to the next
		bool WillFall { get; set; }				// If TRUE, the track begins to fall instead of going to another track.
	}

	public class IMechanicsTrack {
		public byte TrackNum { get; set; }
		public byte ToTrack { get; set; }
		public ushort Duration { get; set; }
		public ushort Delay { get; set; }
		public bool WillFall { get; set; }
	}

	public static class ParamsTrackRules {
		public static WholeRangeParam TrackNum = new WholeRangeParam("Track ID", 0, 99, 1, 0);
		public static WholeRangeParam ToTrack = new WholeRangeParam("Goes to Track ID", 0, 99, 1, 0);
		public static WholeRangeParam Duration = new WholeRangeParam("Travel Duration", 60, 3600, 15, 180, " frame(s)");
		public static WholeRangeParam Delay = new WholeRangeParam("Departure Delay", 0, 3600, 15, 0, " frame(s)");
		public static BoolParam WillFall = new BoolParam("Falls On Arrival", false);
	}

	public static class ParamsTrack {

		/// TODO: ALL BELOW
		/// TODO: ALL BELOW
		/// TODO: ALL BELOW
		/// TODO: ALL BELOW
		/// TODO: ALL BELOW
		/// TODO: ALL BELOW
		/// TODO: ALL BELOW
		/// TODO: ALL BELOW
		/// TODO: ALL BELOW

		public static IMechanicsTrack ConvertToMechanics( JObject paramsList ) {
			IMechanicsTrack mechanics = new IMechanicsTrack();

			mechanics.TrackNum = paramsList["trackNum"] == null ? (byte) ParamsTrackRules.TrackNum.defValue : paramsList["trackNum"].Value<byte>();
			mechanics.ToTrack = paramsList["to"] == null ? (byte) ParamsTrackRules.ToTrack.defValue : paramsList["to"].Value<byte>();
			mechanics.Duration = paramsList["duration"] == null ? (ushort) ParamsTrackRules.Duration.defValue : paramsList["duration"].Value<ushort>();
			mechanics.Delay = paramsList["delay"] == null ? (ushort) ParamsTrackRules.Delay.defValue : paramsList["delay"].Value<ushort>();
			mechanics.WillFall = paramsList["beginFall"] == null ? ParamsTrackRules.WillFall.defValue : paramsList["beginFall"].Value<bool>();

			return mechanics;
		}
	}
}
