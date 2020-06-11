using Nexus.Config;
using Nexus.Objects;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public static class ConsoleDebug {

		public static void DebugBase() {
			string statIns = ConsoleTrack.GetArgAsString();

			ConsoleTrack.PrepareTabLookup(debugBaseList, statIns, "View or change debug settings.");

			if(debugBaseList.ContainsKey(statIns)) {
				debugBaseList[statIns].Invoke();
				return;
			}
		}

		public static readonly Dictionary<string, System.Action> debugBaseList = new Dictionary<string, System.Action>() {
			{ "speed", ConsoleDebug.DebugSpeed },
			{ "data", ConsoleDebug.DebugData },
		};

		private static void DebugSpeed() {
			string currentIns = ConsoleTrack.GetArgAsString();

			ConsoleTrack.PrepareTabLookup(debugSpeedCodes, currentIns, "Assign a debug speed / tick rate that the game runs at.");

			if(ConsoleTrack.activate) {

				if(debugSpeedCodes.ContainsKey(currentIns)) {
					DebugConfig.SetTickSpeed((DebugTickSpeed)debugSpeedCodes[currentIns]);
					return;
				}
			}
		}

		public static readonly Dictionary<string, object> debugSpeedCodes = new Dictionary<string, object>() {
			{ "normal", DebugTickSpeed.StandardSpeed },
			{ "slow", DebugTickSpeed.HalfSpeed },
			{ "slower", DebugTickSpeed.QuarterSpeed },
			{ "slowest", DebugTickSpeed.EighthSpeed },
			{ "y-pressed", DebugTickSpeed.WhenYPressed },
			{ "y-held", DebugTickSpeed.WhileYHeld },
			{ "y-slow-held", DebugTickSpeed.WhileYHeldSlow },
		};

		private static void DebugData() {
			string currentIns = ConsoleTrack.GetArgAsString();

			ConsoleTrack.PrepareTabLookup(debugDataCodes, currentIns, "Retrieve helpful debug data.");

			if(debugDataCodes.ContainsKey(currentIns)) {
				ConsoleTrack.possibleTabs = "";
				ConsoleTrack.helpText = debugDataCodes[currentIns].ToString();
			}

			Character character = ConsoleTrack.character;

			if(currentIns == "grid") {
				ConsoleTrack.helpText = "Character's Grid Coordinates are: " + character.GridX.ToString() + ", " + character.GridY.ToString();
			} else if(currentIns == "coords") {
				ConsoleTrack.helpText = "Character's Coordinates are: " + character.posX.ToString() + ", " + character.posY.ToString();
			}
		}

		public static readonly Dictionary<string, object> debugDataCodes = new Dictionary<string, object>() {
			{ "grid", "Returns the X, Y grid coordinates of the character." },
			{ "coords", "Returns the X, Y position coordinates of the character." },
		};

	}
}
