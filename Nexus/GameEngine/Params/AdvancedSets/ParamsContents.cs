using Nexus.ObjectComponents;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static Nexus.Objects.Goodie;

namespace Nexus.GameEngine {

	public class ParamsContents : Params {

		public string[] contentGroup = new string[10] { "Goodie", "Suit", "Hat", "Timer Mod", "Mobility Power", "Weapon", "Potion", "Thrown", "Bolt", "Stack" };

		public ParamsContents() {
			this.rules.Add(new LabeledParam("content", "Content Type", this.contentGroup, (byte)0));
			this.rules.Add(new DictParam("id", "Goodie", ParamDict.Goodies, (byte)GoodieSubType.Apple));
			this.rules.Add(new DictParam("id", "Suit", ParamDict.Suits, (byte)SuitSubType.RandomSuit));
			this.rules.Add(new DictParam("id", "Hat", ParamDict.Hats, (byte)HatSubType.RandomPowerHat));
			this.rules.Add(new DictParam("id", "Timers", ParamDict.Timers, (byte)GoodieSubType.Plus5));
			this.rules.Add(new DictParam("id", "Mobility Power", ParamDict.MobPowers, (byte)PowerSubType.RandomPotion));
			this.rules.Add(new DictParam("id", "Weapon", ParamDict.Weapons, (byte)PowerSubType.RandomWeapon));
			this.rules.Add(new DictParam("id", "Spells", ParamDict.Spells, (byte)PowerSubType.RandomBook));
			this.rules.Add(new DictParam("id", "Thrown", ParamDict.Thrown, (byte)PowerSubType.RandomThrown));
			this.rules.Add(new DictParam("id", "Bolts", ParamDict.Bolts, (byte)PowerSubType.RandomBolt));
			this.rules.Add(new DictParam("id", "Stacks", ParamDict.Stacks, (byte)PowerSubType.Chakram));
		}

		// Returning `true` means it ran a custom menu update. `false` means the menu needs to be updated manually.
		public override bool CycleParam(string paramKey, bool up = true) {
			ArrayList tileObj = WandData.wandTileData;

			// Verify that the parameter can exist.
			if(tileObj == null) { return false; }

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

				DictParam rule = (DictParam)WandData.actParamSet.rules[(byte)(groupVal + 1)];
				Dictionary<byte, string> dict = rule.dict;
				byte[] contentKeys = dict.Keys.ToArray<byte>();

				byte cId = byte.Parse(paramList["id"].ToString());
				short newId = this.CycleNumber((short)cId, 0, (short)(contentKeys.Length - 1), 1, up);
				this.UpdateParamNum(paramList, paramKey, newId, contentKeys[0]);
			}

			// Update the Menu Options
			return this.RunCustomMenuUpdate();
		}

		// This override will check the "content" group param, and show the appropriate rule accordingly.
		public override bool RunCustomMenuUpdate() {
			short contentVal = WandData.GetParamVal(WandData.actParamSet, "content");
			byte ruleIdToShow = (byte)(contentVal + 1);
			WandData.actParamMenu.UpdateMenuOptions(2, new byte[2] { 0, ruleIdToShow });
			return true;
		}
	}

}
