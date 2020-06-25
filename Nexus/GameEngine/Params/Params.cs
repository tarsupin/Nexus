using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Nexus.GameEngine {

	public class Params {

		// Map of Parameters
		public static Dictionary<string, Params> ParamMap = new Dictionary<string, Params>() {
			{ "AttackBolt", new ParamsAttackBolt() },
			{ "Beats", new ParamsBeats() },
			{ "Character", new ParamsCharacter() },
			{ "Cluster", new ParamsCluster() },
			{ "Collectable", new ParamsCollectable() },
			{ "Contents", new ParamsContents() },
			{ "Door", new ParamsDoor() },
			{ "Elemental", new ParamsElemental() },
			{ "Emblem", new ParamsEmblem() },
			{ "FireSpit", new ParamsAttackFireSpit() },
			{ "Flight", new ParamsFlight() },
			{ "MoveBounce", new ParamsMoveBounce() },
			{ "MoveChase", new ParamsMoveChase() },
			{ "Placer", new ParamsPlacer() },
			{ "Shell", new ParamsShell() },
			{ "TrackDot", new ParamsTrackDot() },
			{ "Upgrades", new ParamsUpgrades() },
		};

		public List<ParamGroup> rules;

		public Params() {
			this.rules = new List<ParamGroup>();
		}
		
		public ParamGroup GetParamRule(string paramKey) {
			for(byte i = 0; i < this.rules.Count; i++) {
				if(this.rules[i].key == paramKey) { return this.rules[i]; }
			}
			return null;
		}

		public string GetParamKey(byte ruleNum) {
			if(this.rules.Count - 1 < ruleNum) { return null; }
			return this.rules[ruleNum].key;
		}

		// Returning `true` means it ran a custom menu update. `false` means the menu needs to be updated manually.
		public virtual bool CycleParam(string paramKey, bool up = true) {
			ArrayList tileObj = WandData.wandTileData;
			
			// Verify that the parameter can exist.
			if(tileObj == null) { return false; }

			if(tileObj.Count <= 2) {
				tileObj.Add(new Dictionary<string, short>());
			}

			// Retrieve Rule
			ParamGroup paramRule = this.GetParamRule(paramKey);

			// Get Tile Data
			Dictionary<string, short> paramList = (Dictionary<string, short>) tileObj[2];

			// Retrieve the parameter value (or the default value if it's not set)
			short paramVal = paramList.ContainsKey(paramKey) ? paramList[paramKey] : paramRule.defValue;

			// Cycle an Integer Param
			if(paramRule is IntParam) {
				IntParam intRule = (IntParam) paramRule;
				short newValue = this.CycleNumber((short) paramVal, intRule.min, intRule.max, intRule.increment, up);
				this.UpdateParamNum(paramList, paramKey, newValue, intRule.defValue);
			}

			// Cycle a Percent Param
			else if(paramRule is PercentParam) {
				PercentParam perRule = (PercentParam)paramRule;
				short newValue = this.CycleNumber((short)paramVal, perRule.min, perRule.max, perRule.increment, up);
				this.UpdateParamNum(paramList, paramKey, newValue, perRule.defValue);
			}

			// Cycle a Labeled Param
			else if(paramRule is LabeledParam) {
				LabeledParam labelRule = (LabeledParam)paramRule;
				short newValue = this.CycleNumber((short)paramVal, 0, (short) (labelRule.labels.Length - 1), 1, up);
				this.UpdateParamNum(paramList, paramKey, newValue, labelRule.defValue);
			}

			else { return false; }

			// Run Custom Cycle Updates for Advanced Params
			return this.RunCustomMenuUpdate();
		}

		protected short CycleNumber(short value, short min, short max, short increment, bool up = true) {
			short newValue = (short) (value + (up ? increment : 0 - increment));
			if(newValue <= min) { return min; }
			if(newValue >= max) { return max; }
			return newValue;
		}

		// Update a numeric parameter (or remove it if it's the default value).
		protected void UpdateParamNum(Dictionary<string, short> paramList, string paramKey, short value, short defVal) {
			if(value == defVal) {
				paramList.Remove(paramKey);
			} else {
				paramList[paramKey] = value;
			}
		}

		public virtual bool RunCustomMenuUpdate() { return false; }

		public void AddRulesToShow(string[] ruleKeys, ref List<byte> rulesToShow) {
			byte index = 0;
			foreach(var rule in this.rules) {
				if(ruleKeys.Contains(rule.key)) {
					rulesToShow.Add(index);
				}
				index++;
			}
		}
	}

	public class ParamsAttack : Params {

		public const short DefaultCycle = 120;

		public ParamsAttack() {
			this.rules.Add(new FrameParam("cycle", "Attack Frequency", 60, 300, 15, DefaultCycle, " frames"));	// Frequency of the attack (in frames).
			this.rules.Add(new FrameParam("offset", "Timer Offset", 0, 300, 15, 0, " frames"));					// The offset of the frequency on the global time.
			this.rules.Add(new PercentParam("grav", "Gravity Influence", 0, 200, 10, 0, FInt.Create(0.5)));		// The percent that gravity influences the projectile.
			this.rules.Add(new PercentParam("speed", "Attack Speed", 20, 200, 10, 100, FInt.Create(4)));		// Velocity of the bolts (Y-axis).
		}
	}

	public class ParamsAttackBolt : ParamsAttack {
		public ParamsAttackBolt() : base() {
			this.rules.Remove(this.rules[2]); // Remove Gravity Influence
			this.rules.Add(new IntParam("count", "Number of Bolts", 1, 3, 1, 1));								// Number of bolts that gets shot simultaneously (1 to 3)
			this.rules.Add(new PercentParam("spread", "Bolt Spread", 50, 200, 10, 100, FInt.Create(0.3)));		// The % spread between each bolt.
		}
	}

	public class ParamsAttackFireSpit : ParamsAttack {
		public ParamsAttackFireSpit() : base() {
			this.rules.Add(new IntParam("count", "Number of Fireballs", 1, 2, 1, 1));                               // Number of bolts that gets shot simultaneously (1 to 3).
			this.rules.Add(new PercentParam("spread", "Fireball Spread", 50, 250, 10, 100, FInt.Create(0.3)));		// The % spread between each bolt.
		}
	}

	public class ParamsElemental : ParamsAttack {
		public ParamsElemental() : base() { }
	}

	public class ParamsBeats : Params {

		public const byte DefaultSpeed = 4;

		// Every second has four beats. This indicates which it should trigger at, for two seconds worth of beats.
		public ParamsBeats() {
			this.rules.Add(new IntParam("beat1", "Beat #1", 0, 16, 1, 0));
			this.rules.Add(new IntParam("beat2", "Beat #2", 0, 16, 1, 0));
			this.rules.Add(new IntParam("beat3", "Beat #3", 0, 16, 1, 0));
			this.rules.Add(new IntParam("beat4", "Beat #4", 0, 16, 1, 0));
			this.rules.Add(new IntParam("speed", "Action Speed", 2, 12, 1, ParamsBeats.DefaultSpeed));
		}
	}

	public class ParamsCluster : Params {
		public ParamsCluster() {
			this.rules.Add(new IntParam("clusterId", "Act As Cluster ID", 0, TrackSystem.MaxClusters, 1, 0, " (0 to ignore)"));
		}
	}
	public class ParamsCollectable : Params {

		public ParamsCollectable() {

			// The Collectable Rule governs how the collectable behaves after collection; this can affect multiplayer, reuse, etc.
			this.rules.Add(new LabeledParam("collect", "Collectable Rule", new string[4] { "One Use Only", "One Per Player", "Always Available", "Regenerates After Use" }, (byte)CollectableRule.OneUseOnly));

			// Regen only applies if the rule is set to "Regenerates After Use"
			this.rules.Add(new IntParam("regen", "Regeneration Time", 0, 60, 1, 0, " seconds"));
		}
	}

	public class ParamsDoor : Params {
		public ParamsDoor() {

			// The room # that the door will take you to.
			this.rules.Add(new LabeledParam("room", "Room Destination", new string[6] { "Room #1", "Room #2", "Room #3", "Room #4", "Room #5", "Room #6" }, 0));

			// Determines what type of door you'll enter to (or to a checkpoint)
			this.rules.Add(new LabeledParam("exit", "Exit Type", new string[4] { "To Same Door Color", "To Open Doorway", "To Red Checkpoint", "To White Checkpoint" }, (byte) DoorExitType.ToSameColor));
		}
	}

	public class ParamsEmblem : Params {
		public ParamsEmblem() {
			this.rules.Add(new LabeledParam("color", "Emblem Color", new string[5] { "None", "Blue", "Red", "Green", "Yellow" }, (byte) 0));
			this.rules.Add(new LabeledParam("on", "Active at Start", new string[2] { "Inactive", "Active" }, (byte) 0));
		}
	}

	public class ParamsMoveBounce : Params {

		// This applies to the bouncing movement creature.
		public ParamsMoveBounce() {
			this.rules.Add(new IntParam("x", "X Movement", -6, 6, 1, 2, " tiles(s)"));
			this.rules.Add(new IntParam("y", "Y Movement", -6, 6, 1, 2, " tiles(s)"));
		}
	}

	public class ParamsMoveChase : Params {

		// This applies to chasing creatures.
		public ParamsMoveChase() {
			this.rules.Add(new LabeledParam("axis", "Movement Axis", new string[3] { "Both", "Vertical", "Horizontal" }, (byte) FlightChaseAxis.Both));
			this.rules.Add(new PercentParam("speed", "Movement Speed", 10, 200, 10, 100, FInt.Create(2)));
			this.rules.Add(new IntParam("chase", "Chase Range", 0, 40, 1, 0, " tile(s)"));
			this.rules.Add(new IntParam("flee", "Flee Range", 0, 40, 1, 0, " tile(s)"));
			this.rules.Add(new IntParam("stall", "Stall Range", 0, 40, 1, 0, " tile(s)"));
			this.rules.Add(new LabeledParam("returns", "Returns to Start", new string[2] { "Returns", "Doesn't Return" }, (byte)0));
			this.rules.Add(new FrameParam("retDelay", "Delay for Returning", 0, 300, 15, 120, " frames"));
			this.rules.Add(new IntParam("clusterId", "Act As Cluster ID", 0, 10, 1, 0, " (0 to ignore)"));
		}
	}
	
	public class ParamsPlacer : Params {

		// This applies to chasing creatures.
		public ParamsPlacer() {

			// TODO: ADD Placer PARAMS
			// TODO: ADD Placer PARAMS
			this.rules.Add(new IntParam("STUFF", "CHANGE THIS", 0, 7, 1, 0));
		}
	}

	public class ParamsShell : Params {

		// This applies to the bouncing movement creature.
		public ParamsShell() {
			this.rules.Add(new IntParam("x", "X Movement", -7, 7, 1, 0, " tiles(s)"));
			this.rules.Add(new IntParam("y", "Y Movement", -12, 7, 1, 0, " tiles(s)"));
		}
	}

	public class ParamsTrackDot : Params {

		public ParamsTrackDot() {
			this.rules.Add(new IntParam("trackNum", "Track ID", 0, 99, 1, 0, " (must set > 0)"));
			this.rules.Add(new IntParam("to", "Goes to Track ID", 0, 99, 1, 0, ""));
			this.rules.Add(new FrameParam("duration", "Travel Duration", 60, 3600, 15, 180, " frames"));
			this.rules.Add(new FrameParam("delay", "Departure Delay", 0, 3600, 15, 0, " frames"));
			this.rules.Add(new LabeledParam("beginFall", "Falls on Arrival", new string[2] { "False", "True" }, (byte)0));
		}
	}
}
