using Nexus.ObjectComponents;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static Nexus.Objects.Goodie;

namespace Nexus.GameEngine {

	public class ParamsContents : Params {

		public string[] contentGroup = new string[10] { "Goodie", "Suit", "Hat", "Timer Mod", "Mobility Power", "Weapon", "Potion", "Thrown", "Bolt", "Stack" };

		public ParamsContents() {
			this.rules = new ParamGroup[11];
			this.rules[0] = new LabeledParam("content", "Content Type", this.contentGroup, (byte)0);
			this.rules[1] = new DictParam("id", "Goodie", ParamDict.Goodies, (byte)GoodieSubType.Apple);
			this.rules[2] = new DictParam("id", "Suit", ParamDict.Suits, (byte)SuitSubType.RandomSuit);
			this.rules[3] = new DictParam("id", "Hat", ParamDict.Hats, (byte)HatSubType.RandomHat);
			this.rules[4] = new DictParam("id", "Timers", ParamDict.Timers, (byte)GoodieSubType.Plus5);
			this.rules[5] = new DictParam("id", "Mobility Power", ParamDict.MobPowers, (byte)PowerSubType.RandomPotion);
			this.rules[6] = new DictParam("id", "Weapon", ParamDict.Weapons, (byte)PowerSubType.RandomWeapon);
			this.rules[7] = new DictParam("id", "Spells", ParamDict.Spells, (byte)PowerSubType.RandomBook);
			this.rules[8] = new DictParam("id", "Thrown", ParamDict.Thrown, (byte)PowerSubType.RandomThrown);
			this.rules[9] = new DictParam("id", "Bolts", ParamDict.Bolts, (byte)PowerSubType.RandomBolt);
			this.rules[10] = new DictParam("id", "Stacks", ParamDict.Stacks, (byte)PowerSubType.Chakram);
		}

		public override void CycleParam(string paramKey, bool up = true) {
			ArrayList tileObj = WandData.wandTileData;

			// Verify that the parameter can exist.
			if(tileObj == null) { return; }

			if(tileObj.Count <= 2) {
				tileObj.Add(new Dictionary<string, short>());
			}

			// Get Tile Data
			Dictionary<string, short> paramList = (Dictionary<string, short>)tileObj[2];

			// A Content Group must be listed:
			if(!paramList.ContainsKey("content")) { paramList["content"] = 0; }
			if(!paramList.ContainsKey("id")) { paramList["id"] = 0; }

			short groupVal = paramList["content"];

			// Cycle "content" Param
			if(paramKey == "content") {
				short newValue = this.CycleNumber(groupVal, 0, (short)(this.contentGroup.Length - 1), 1, up);
				this.UpdateParamNum(paramList, paramKey, newValue, 0);
				this.UpdateParamNum(paramList, "id", 0, 0);
			}

			// Cycle "id" Supply Param (dependent on "content" type)
			else {
				if(groupVal < 0 || groupVal > 9) { groupVal = 0; }

				DictParam rule = (DictParam)WandData.paramRules[(byte)(groupVal + 1)];
				Dictionary<byte, string> dict = rule.dict;
				byte[] contentKeys = dict.Keys.ToArray<byte>();

				byte cId = byte.Parse(paramList["id"].ToString());
				short newId = this.CycleNumber((short)cId, 0, (short)(contentKeys.Length - 1), 1, up);
				this.UpdateParamNum(paramList, paramKey, newId, contentKeys[0]);
			}

			// Update the Menu Options
			this.UpdateMenu();
		}

		// This override will check the "content" group param, and show the appropriate rule accordingly.
		public override void UpdateMenu() {
			short contentVal = WandData.GetParamVal("content");
			byte ruleIdToShow = (byte)(contentVal + 1);
			WandData.UpdateMenuOptions(2, new byte[2] { 0, ruleIdToShow });
		}
	}

}
