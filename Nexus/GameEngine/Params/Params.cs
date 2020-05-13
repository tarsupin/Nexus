using Newtonsoft.Json.Linq;
using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;
using System.Collections;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class Params {
		public ParamGroup[] rules;
		public Params() {}
		
		public ParamGroup GetParamRule(string paramKey) {
			for(byte i = 0; i < this.rules.Length; i++) {
				if(this.rules[i].key == paramKey) { return this.rules[i]; }
			}
			return null;
		}

		public string GetParamKey(byte ruleNum) {
			if(this.rules.Length - 1 < ruleNum) { return null; }
			return this.rules[ruleNum].key;
		}

		public void CycleParam(Dictionary<string, Dictionary<string, ArrayList>> layerData, ushort gridX, ushort gridY, string paramKey, bool up = true) {
			ArrayList tileObj = LevelContent.GetTileDataWithParams(layerData, gridX, gridY);
			
			// Verify that the parameter can exist.
			if(tileObj == null || tileObj.Count <= 2 || tileObj[2] is JObject == false) { return; }

			// Retrieve Rule
			ParamGroup paramRule = this.GetParamRule(paramKey);

			// Get Tile Data
			JObject paramList = (JObject) tileObj[2];
			JToken paramVal = null;

			// Retrieve the parameter value (or the default value if it's not set)
			if(paramList.ContainsKey(paramKey)) {
				paramVal = paramList[paramKey];
			}

			// Cycle an Integer Param
			if(paramRule is IntParam) {
				IntParam intRule = (IntParam) paramRule;
				if(paramVal == null) { paramVal = intRule.defValue; }
				short newValue = this.CycleNumber((short) paramVal, intRule.min, intRule.max, intRule.increment, up);
				this.UpdateParamNum(paramList, paramKey, newValue, intRule.defValue);
			}

			// Cycle a Percent Param
			else if(paramRule is PercentParam) {
				PercentParam perRule = (PercentParam)paramRule;
				if(paramVal == null) { paramVal = perRule.defValue; }
				short newValue = this.CycleNumber((short)paramVal, perRule.min, perRule.max, perRule.increment, up);
				this.UpdateParamNum(paramList, paramKey, newValue, perRule.defValue);
			}

			// Cycle a Labeled Param
			else if(paramRule is LabeledParam) {
				LabeledParam labelRule = (LabeledParam)paramRule;
				if(paramVal == null) { paramVal = labelRule.defValue; }
				short newValue = this.CycleNumber((short)paramVal, 0, (short) (labelRule.labels.Length - 1), 1, up);
				this.UpdateParamNum(paramList, paramKey, newValue, labelRule.defValue);
			}
		}

		private short CycleNumber(short value, short min, short max, short increment, bool up = true) {
			short newValue = (short) (value + (up ? increment : 0 - increment));
			if(newValue <= min) { return min; }
			if(newValue >= max) { return max; }
			return newValue;
		}

		// Update a numeric parameter (or remove it if it's the default value).
		private void UpdateParamNum(JObject paramList, string paramKey, short value, short defVal) {
			if(value == defVal) {
				paramList.Remove(paramKey);
			} else {
				paramList[paramKey] = value;
			}
		}
	}

	// Params TODO: 
	//		- ParamsCluster
	//		- ParamsClusterChar
	//		- ParamsClusterScreen
	//		- ParamsContains (combine dictionary with system)
	//		- ParamsMoveFlight (advanced mechanics)
	//		- ParamsPlacer (requires dictionary, like Contains)

	public class ParamsAttackBolt : Params {

		public ParamsAttackBolt() {
			this.rules = new ParamGroup[5];
			this.rules[0] = new IntParam("count", "Number of Bolts", 1, 3, 1, 1);								// Number of bolts that gets shot simultaneously (1 to 3)
			this.rules[1] = new PercentParam("speed", "Bolt Speed", 20, 200, 10, 100, FInt.Create(4));			// Velocity of the bolts (Y-axis).
			this.rules[2] = new PercentParam("spread", "Bolt Spread", 50, 200, 10, 100, FInt.Create(0.3));		// The % spread between each bolt.
			this.rules[3] = new IntParam("cycle", "Attack Frequency", 60, 300, 15, 120, " frames");				// Frequency of the attack (in frames).
			this.rules[4] = new IntParam("offset", "Timer Offset", 0, 300, 15, 0, " frames");					// The offset of the frequency on the global time.
		}
	}

	public class ParamsAttackEarth : Params {

		public ParamsAttackEarth() {
			this.rules = new ParamGroup[3];
			this.rules[0] = new PercentParam("speed", "Stone Speed", 20, 200, 10, 100, FInt.Create(4));			// Velocity of the stones (Y-axis).
			this.rules[1] = new IntParam("cycle", "Attack Frequency", 60, 300, 15, 120, " frames");				// Frequency of the attack (in frames).
			this.rules[2] = new IntParam("offset", "Timer Offset", 0, 300, 15, 0, " frames");					// The offset of the frequency on the global time.
		}
	}

	public class ParamsBeats : Params {

		public ParamsBeats() {

			this.rules = new ParamGroup[4];

			// Every second has four beats. This indicates which it should trigger at, for two seconds worth of beats.
			this.rules[0] = new IntParam("beat1", "Beat #1", 0, 7, 1, 0);
			this.rules[1] = new IntParam("beat2", "Beat #2", 0, 7, 1, 0);
			this.rules[2] = new IntParam("beat3", "Beat #3", 0, 7, 1, 0);
			this.rules[3] = new IntParam("beat4", "Beat #4", 0, 7, 1, 0);
		}
	}

	public class ParamsCollectable : Params {

		public ParamsCollectable() {

			this.rules = new ParamGroup[2];

			// The Collectable Rule governs how the collectable behaves after collection; this can affect multiplayer, reuse, etc.
			this.rules[0] = new LabeledParam("beat1", "Collectable Rule", new string[4] { "One Use Only", "One Per Player", "Always Available", "Regenerates After Use" }, (byte)CollectableRule.OneUseOnly);

			// Regen only applies if the rule is set to "Regenerates After Use"
			this.rules[1] = new IntParam("regen", "Regeneration Time", 0, 60, 1, 0);
		}
	}

	public class ParamsDoor : Params {

		public ParamsDoor() {

			this.rules = new ParamGroup[2];

			// The room # that the door will take you to.
			this.rules[0] = new IntParam("room", "Room Destination", 0, 9, 1, 0, "");

			// Determines what type of door you'll enter to (or to a checkpoint)
			this.rules[1] = new LabeledParam("exit", "Exit Type", new string[3] { "To Same Door Color", "To Open Doorway", "To Checkpoint" }, (byte) DoorExitType.ToSameColor);
		}
	}

	public class ParamsEmblem : Params {

		public ParamsEmblem() {
			this.rules = new ParamGroup[2];
			this.rules[0] = new LabeledParam("color", "Emblem Color", new string[5] { "None", "Blue", "Red", "Green", "Yellow" }, (byte) 0);
			this.rules[1] = new LabeledParam("on", "Active at Start", new string[2] { "Inactive", "Active" }, (byte) 0);
		}
	}

	public class ParamsFireBurst : Params {

		// TODO: Need to account for X and Y directions of Fire Burst Params
		// if(dirFacing == DirCardinal.Left) { xSpeed = -8 * speedMult; }
		// else if(dirFacing == DirCardinal.Right) { xSpeed = 8 * speedMult; }
		// else if(dirFacing == DirCardinal.Down) { ySpeed = 4 * speedMult; }
		// else { ySpeed = (-8 * speedMult) - 4; }

		public ParamsFireBurst() {
			this.rules = new ParamGroup[6];
			this.rules[0] = new PercentParam("grav", "Gravity Influence", 0, 200, 10, 100, FInt.Create(1));			// The percent that gravity influences the first.
			this.rules[1] = new IntParam("count", "Number of Fireballs", 1, 3, 1, 1);								// Number of bolts that gets shot simultaneously (1 to 3).
			this.rules[2] = new PercentParam("speed", "Fireball Speed", 20, 200, 10, 100, FInt.Create(4));			// Velocity of the bolts (Y-axis).
			this.rules[3] = new PercentParam("spread", "Fireball Spread", 50, 250, 10, 100, FInt.Create(0.3));		// The % spread between each bolt.
			this.rules[4] = new IntParam("cycle", "Attack Frequency", 60, 300, 15, 120, " frames");					// Frequency of the attack (in frames).
			this.rules[5] = new IntParam("offset", "Timer Offset", 0, 300, 15, 0, " frames");						// The offset of the frequency on the global time.
		}
	}

	public class ParamsMoveBounce : Params {

		// This applies to the bouncing movement creature.
		public ParamsMoveBounce() {
			this.rules = new ParamGroup[2];
			this.rules[0] = new IntParam("x", "X Movement", -6, 6, 1, 2, " tiles(s)");
			this.rules[1] = new IntParam("y", "Y Movement", -6, 6, 1, 2, " tiles(s)");
		}
	}

	public class ParamsMoveChase : Params {

		// This applies to chasing creatures.
		public ParamsMoveChase() {
			this.rules = new ParamGroup[7];
			this.rules[0] = new LabeledParam("axis", "Movement Axis", new string[3] { "Both", "Vertical", "Horizontal" }, (byte) FlightChaseAxis.Both);
			this.rules[1] = new PercentParam("speed", "Movement Speed", 10, 200, 10, 100, FInt.Create(2));
			this.rules[2] = new IntParam("chase", "Chase Range", 0, 40, 1, 10, " tile(s)");
			this.rules[3] = new IntParam("flee", "Flee Range", 0, 40, 1, 0, " tile(s)");
			this.rules[4] = new IntParam("stall", "Stall Range", 0, 40, 1, 8, " tile(s)");
			this.rules[1] = new LabeledParam("returns", "Returns to Start", new string[2] { "Returns", "Doesn't Return" }, (byte)0);
			this.rules[6] = new IntParam("retDelay", "Delay for Returning", 0, 300, 15, 120, " frames");
		}
	}

	public class ParamsShell : Params {

		// This applies to the bouncing movement creature.
		public ParamsShell() {
			this.rules = new ParamGroup[2];
			this.rules[0] = new IntParam("x", "X Movement", -7, 7, 1, 0, " tiles(s)");
			this.rules[1] = new IntParam("y", "Y Movement", -12, 7, 1, 0, " tiles(s)");
		}
	}

	public class ParamsMoveTrack : Params {

		public ParamsMoveTrack() {
			this.rules = new ParamGroup[5];
			this.rules[0] = new IntParam("trackNum", "Track ID", 0, 99, 1, 0, "");
			this.rules[1] = new IntParam("to", "Goes to Track ID", 0, 99, 1, 0, "");
			this.rules[2] = new IntParam("duration", "Travel Duration", 60, 3600, 15, 180, " frames");
			this.rules[3] = new IntParam("delay", "Departure Delay", 0, 3600, 15, 0, " frames");
			this.rules[1] = new LabeledParam("beginFall", "Falls on Arrival", new string[2] { "False", "True" }, (byte)0);
		}
	}


}
