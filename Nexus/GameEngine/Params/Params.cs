﻿using Newtonsoft.Json.Linq;
using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using Nexus.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static Nexus.Objects.Goodie;

namespace Nexus.GameEngine {

	public class Params {

		// Map of Parameters
		public static Dictionary<string, Params> ParamMap = new Dictionary<string, Params>() {
			{ "AttackBolt", new ParamsAttackBolt() },
			{ "AttackEarth", new ParamsAttackEarth() },
			{ "Beats", new ParamsBeats() },
			{ "Checkpoint", new ParamsCheckpoint() },
			{ "Collectable", new ParamsCollectable() },
			{ "Contents", new ParamsContents() },
			{ "Door", new ParamsDoor() },
			{ "Emblem", new ParamsEmblem() },
			{ "FireBurst", new ParamsFireBurst() },
			{ "MoveBounce", new ParamsMoveBounce() },
			{ "MoveChase", new ParamsMoveChase() },
			{ "Placer", new ParamsPlacer() },
			{ "TrackDot", new ParamsTrackDot() },
			{ "Shell", new ParamsShell() },
		};

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

		public virtual void CycleParam(string paramKey, bool up = true) {
			ArrayList tileObj = WandData.wandTileData;
			
			// Verify that the parameter can exist.
			if(tileObj == null) { return; }

			if(tileObj.Count <= 2 || tileObj[2] is JObject == false) {
				tileObj.Add(new JObject());
			}

			// Retrieve Rule
			ParamGroup paramRule = this.GetParamRule(paramKey);

			// Get Tile Data
			JObject paramList = (JObject)tileObj[2];
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

			else { return; }

			// Update the Menu Options
			this.UpdateMenu();
		}

		protected short CycleNumber(short value, short min, short max, short increment, bool up = true) {
			short newValue = (short) (value + (up ? increment : 0 - increment));
			if(newValue <= min) { return min; }
			if(newValue >= max) { return max; }
			return newValue;
		}

		// Update a numeric parameter (or remove it if it's the default value).
		protected void UpdateParamNum(JObject paramList, string paramKey, short value, short defVal) {
			if(value == defVal) {
				paramList.Remove(paramKey);
			} else {
				paramList[paramKey] = value;
			}
		}

		public virtual void UpdateMenu() {
			WandData.UpdateMenuOptions();		// The default practice is to defer to the WandData's method.
		}
	}

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

		// Every second has four beats. This indicates which it should trigger at, for two seconds worth of beats.
		public ParamsBeats() {
			this.rules = new ParamGroup[4];
			this.rules[0] = new IntParam("beat1", "Beat #1", 0, 7, 1, 0);
			this.rules[1] = new IntParam("beat2", "Beat #2", 0, 7, 1, 0);
			this.rules[2] = new IntParam("beat3", "Beat #3", 0, 7, 1, 0);
			this.rules[3] = new IntParam("beat4", "Beat #4", 0, 7, 1, 0);
		}
	}

	public class ParamsCheckpoint : Params {

		public ParamsCheckpoint() {

			this.rules = new ParamGroup[1];

			// TODO: ADD CHECKPIONT PARAMS
			// TODO: ADD CHECKPIONT PARAMS
			this.rules[0] = new IntParam("STUFF", "CHANGE THIS", 0, 7, 1, 0);
		}
	}

	public class ParamsCollectable : Params {

		public ParamsCollectable() {

			this.rules = new ParamGroup[2];

			// The Collectable Rule governs how the collectable behaves after collection; this can affect multiplayer, reuse, etc.
			this.rules[0] = new LabeledParam("beat1", "Collectable Rule", new string[4] { "One Use Only", "One Per Player", "Always Available", "Regenerates After Use" }, (byte)CollectableRule.OneUseOnly);

			// Regen only applies if the rule is set to "Regenerates After Use"
			this.rules[1] = new IntParam("regen", "Regeneration Time", 0, 60, 1, 0, " seconds");
		}
	}

	public class ParamsContents : Params {

		public string[] contentGroup = new string[10] { "Goodie", "Suit", "Hat", "Timer Mod", "Mobility Power", "Weapon", "Potion", "Thrown", "Bolt", "Stack" };

		public ParamsContents() {
			this.rules = new ParamGroup[11];
			this.rules[0] = new LabeledParam("content", "Content Type", this.contentGroup, (byte) 0);
			this.rules[1] = new DictParam("id", "Goodie", ParamDict.Goodies, (byte) GoodieSubType.Apple);
			this.rules[2] = new DictParam("id", "Suit", ParamDict.Suits, (byte) SuitSubType.RandomSuit);
			this.rules[3] = new DictParam("id", "Hat", ParamDict.Hats, (byte) HatSubType.RandomHat);
			this.rules[4] = new DictParam("id", "Timers", ParamDict.Timers, (byte) GoodieSubType.Plus5);
			this.rules[5] = new DictParam("id", "Mobility Power", ParamDict.MobPowers, (byte) PowerSubType.RandomPotion);
			this.rules[6] = new DictParam("id", "Weapon", ParamDict.Weapons, (byte) PowerSubType.RandomWeapon);
			this.rules[7] = new DictParam("id", "Spells", ParamDict.Spells, (byte) PowerSubType.RandomBook);
			this.rules[8] = new DictParam("id", "Thrown", ParamDict.Thrown, (byte) PowerSubType.RandomThrown);
			this.rules[9] = new DictParam("id", "Bolts", ParamDict.Bolts, (byte) PowerSubType.RandomBolt);
			this.rules[10] = new DictParam("id", "Stacks", ParamDict.Stacks, (byte) PowerSubType.Chakram);
		}

		public override void CycleParam(string paramKey, bool up = true) {
			ArrayList tileObj = WandData.wandTileData;

			// Verify that the parameter can exist.
			if(tileObj == null) { return; }

			if(tileObj.Count <= 2) {
				tileObj.Add(new JObject());
			}

			// Get Tile Data
			JObject paramList = (JObject)tileObj[2];

			// A Content Group must be listed:
			if(!paramList.ContainsKey("content")) { paramList["content"] = 0; }
			if(!paramList.ContainsKey("id")) { paramList["id"] = 0; }

			JToken groupVal = paramList["content"];

			// Cycle "content" Param
			if(paramKey == "content") {
				short newValue = this.CycleNumber((short) groupVal, 0, (short)(this.contentGroup.Length - 1), 1, up);
				this.UpdateParamNum(paramList, paramKey, newValue, 0);
				this.UpdateParamNum(paramList, "id", 0, 0);
			}

			// Cycle "id" Supply Param (dependent on "content" type)
			else {
				byte gVal = byte.Parse(groupVal.ToString());
				if(gVal < 0 || gVal > 9) { gVal = 0; }

				DictParam rule = (DictParam) WandData.paramRules[(byte)(gVal + 1)];
				Dictionary<byte, string> dict = rule.dict;
				byte[] contentKeys = dict.Keys.ToArray<byte>();

				byte cId = byte.Parse(paramList["id"].ToString());
				short newId = this.CycleNumber((short) cId, 0, (short)(contentKeys.Length - 1), 1, up);
				this.UpdateParamNum(paramList, paramKey, newId, contentKeys[0]);
			}

			// Update the Menu Options
			this.UpdateMenu();
		}

		// This override will check the "content" group param, and show the appropriate rule accordingly.
		public override void UpdateMenu() {
			short contentVal = WandData.GetParamVal("content");
			byte ruleIdToShow = (byte) (contentVal + 1);
			WandData.UpdateMenuOptions(2, new byte[2] { 0, ruleIdToShow });
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
	
	public class ParamsPlacer : Params {

		// This applies to chasing creatures.
		public ParamsPlacer() {

			this.rules = new ParamGroup[1];

			// TODO: ADD Placer PARAMS
			// TODO: ADD Placer PARAMS
			this.rules[0] = new IntParam("STUFF", "CHANGE THIS", 0, 7, 1, 0);
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

	public class ParamsTrackDot : Params {

		public ParamsTrackDot() {
			this.rules = new ParamGroup[5];
			this.rules[0] = new IntParam("trackNum", "Track ID", 0, 99, 1, 0, "");
			this.rules[1] = new IntParam("to", "Goes to Track ID", 0, 99, 1, 0, "");
			this.rules[2] = new IntParam("duration", "Travel Duration", 60, 3600, 15, 180, " frames");
			this.rules[3] = new IntParam("delay", "Departure Delay", 0, 3600, 15, 0, " frames");
			this.rules[1] = new LabeledParam("beginFall", "Falls on Arrival", new string[2] { "False", "True" }, (byte)0);
		}
	}


}
