using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Nexus.GameEngine {

	public class ParamsCharacter : Params {

		public string[] teams = new string[5] { "No Team", "Team #1", "Team #2", "Team #3", "Team #4" };
		public string[] faceOpt = new string[3] { "Ryu", "Poo", "Carl" };

		public ParamsCharacter() {

			this.rules.Add(new LabeledParam("team", "Team", this.teams, (byte) 0));
			this.rules.Add(new IntParam("num", "Team Position", 0, 8, 1, 0, ""));
			this.rules.Add(new LabeledParam("face", "Face", this.faceOpt, (byte) 0));
			this.rules.Add(new LabeledParam("dir", "Facing Direction", new string[2] { "Right", "Left" }, (byte) 0));

			// Equipment
			//this.rules.Add(new LabeledParam("eq", "Starting Upgrades", new string[2] { "False", "True" }, (byte)0));
			//this.rules.Add(new IntParam("hp", "Starting Health", 0, 3, 1, 0, " Health"));
			//this.rules.Add(new IntParam("armor", "Starting Armor", 0, 3, 1, 0, " Armor"));
			//this.rules.Add(new DictParam("suit", "Suit", ParamDict.Suits, (byte)SuitSubType.RandomSuit));
			//this.rules.Add(new DictParam("hat", "Hat", ParamDict.Hats, (byte)HatSubType.RandomPowerHat));
			//this.rules.Add(new DictParam("mob", "Mobility Power", ParamDict.MobPowers, (byte)PowerSubType.RandomPotion));
			//this.rules.Add(new DictParam("att", "Weapon", ParamDict.Weapons, (byte)PowerSubType.RandomWeapon));
			//this.rules.Add(new DictParam("att", "Spells", ParamDict.Spells, (byte)PowerSubType.RandomBook));
			//this.rules.Add(new DictParam("att", "Thrown", ParamDict.Thrown, (byte)PowerSubType.RandomThrown));
			//this.rules.Add(new DictParam("att", "Bolts", ParamDict.Bolts, (byte)PowerSubType.RandomBolt));
		}

		//// Returning `true` means it ran a custom menu update. `false` means the menu needs to be updated manually.
		//public override bool CycleParam(string paramKey, bool up = true) {
		//	ArrayList tileObj = WandData.wandTileData;
			
		//	// Verify that the parameter can exist.
		//	if(tileObj == null) { return false; }

		//	if(tileObj.Count <= 2) {
		//		tileObj.Add(new Dictionary<string, short>());
		//	}

		//	// Get Tile Data
		//	Dictionary<string, short> paramList = (Dictionary<string, short>)tileObj[2];

		//	// Show Equipment
		//	List<byte> rulesToShow = new List<byte>();

		//	// Add Rules that are present for this menu:
		//	this.AddRulesToShow(new string[] { "team", "num", "face", "dir" }, ref rulesToShow);

		//	//// , "eq"
		//	//if(paramList.ContainsKey("eq") && paramList["eq"] == 1) {
		//	//	this.AddRulesToShow(new string[] { "hp", "armor", "suit", "hat", "mob", "att" }, ref rulesToShow);
		//	//}

		//	byte[] ruleIdsToShow = rulesToShow.ToArray();
		//	WandData.moveParamMenu.UpdateMenuOptions((byte)ruleIdsToShow.Length, ruleIdsToShow);
		//	return true;
		//}

		//public void AddRulesToShow(string[] ruleKeys, ref List<byte> rulesToShow) {
		//	byte index = 0;
		//	foreach(var rule in this.rules) {
		//		if(ruleKeys.Contains(rule.key)) {
		//			rulesToShow.Add(index);
		//		}
		//		index++;
		//	}
		//}
	}
}
