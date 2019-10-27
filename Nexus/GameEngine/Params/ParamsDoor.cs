using Newtonsoft.Json.Linq;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	interface IParamsDoor {
		byte DestRoom { get; set; }             // The room number that this door leads to.
		DoorExitType ExitType { get; set; }		// Describes the type of exit for the door.
	}

	public class IMechanicsDoor {
		public byte DestRoom { get; set; }
		public byte ExitType { get; set; }
	}

	public static class ParamsDoorRules {
		public static WholeRangeParam DestRoom = new WholeRangeParam("Room Destination", 0, 4, 1, 0, " frame(s)");
		public static DictionaryParam ExitType = new DictionaryParam("Exit Type", new Dictionary<byte, string>() {
			{ (byte) DoorExitType.ToSameColor, "To Same Door Color" },
			{ (byte) DoorExitType.ToOpenDoor, "To Open Doorway" },
			{ (byte) DoorExitType.ToCheckpoint, "To Checkpoint" },
		}, (byte) DoorExitType.ToSameColor);
	}

	public static class ParamsDoor {

		public static IMechanicsDoor ConvertToMechanics( JObject paramsList ) {
			IMechanicsDoor mechanics = new IMechanicsDoor();
			mechanics.DestRoom = paramsList["room"] == null ? (byte) ParamsDoorRules.DestRoom.defValue : paramsList["room"].Value<byte>();
			mechanics.ExitType = paramsList["exit"] == null ? ParamsDoorRules.ExitType.defValue : paramsList["exit"].Value<byte>();
			return mechanics;
		}
	}
}
