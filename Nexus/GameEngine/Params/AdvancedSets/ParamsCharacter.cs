using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class ParamsCharacter : Params {

		public string[] teams = new string[5] { "No Team", "Team #1", "Team #2", "Team #3", "Team #4" };
		public string[] faceOpt = new string[3] { "Ryu", "Poo", "Carl" };
		public string[] hp = new string[4] { "Default Health", "+1 Health", "+2 Health", "+3 Health" };
		public string[] armor = new string[4] { "Default Armor", "+1 Armor", "+2 Armor", "+3 Armor" };

		public ParamsCharacter() {

			this.rules.Add(new LabeledParam("team", "Team", this.teams, (byte) 0));
			this.rules.Add(new IntParam("num", "Team Position", 0, 8, 1, 0, ""));
			this.rules.Add(new LabeledParam("face", "Face", this.faceOpt, (byte) 0));
			this.rules.Add(new LabeledParam("dir", "Facing Direction", new string[2] { "Right", "Left" }, (byte) 0));

			// Starting Upgrades
			this.rules.Add(new LabeledParam("hp", "Starting Health", this.hp, (byte)0));
			this.rules.Add(new LabeledParam("armor", "Starting Armor", this.armor, (byte)0));
			this.rules.Add(new LabeledParam("suit", "Suit", ParamTrack.AssignSuitList, 0));
			this.rules.Add(new LabeledParam("hat", "Hat", ParamTrack.AssignHatList, 0));
			this.rules.Add(new LabeledParam("mob", "Mobility Power", ParamTrack.AssignMobilityList, 0));
			this.rules.Add(new LabeledParam("att", "Attack Power", ParamTrack.AssignAttackList, 0));
			this.rules.Add(new LabeledParam("weapon", "Weapon", ParamTrack.AssignWeaponList, 0));
			this.rules.Add(new LabeledParam("spell", "Spells", ParamTrack.AssignSpellsList, 0));
			this.rules.Add(new LabeledParam("thrown", "Thrown", ParamTrack.AssignThrownList, 0));
			this.rules.Add(new LabeledParam("bolt", "Bolts", ParamTrack.AssignBoltsList, 0));
		}

		// Returning `true` means it ran a custom menu update. `false` means the menu needs to be updated manually.
		public override bool RunCustomMenuUpdate() {
			List<byte> rulesToShow = new List<byte>();

			// Add Rules that are present for this menu:
			this.AddRulesToShow(new string[] { "team", "num", "face", "dir", "hp", "armor", "suit", "hat", "mob", "att" }, ref rulesToShow);

			short attType = WandData.GetParamVal(WandData.actParamSet, "att");

			if(attType == 2) { this.AddRulesToShow(new string[] { "weapon" }, ref rulesToShow); }
			else if(attType == 3) { this.AddRulesToShow(new string[] { "spell" }, ref rulesToShow); }
			else if(attType == 4) { this.AddRulesToShow(new string[] { "thrown" }, ref rulesToShow); }
			else if(attType == 5) { this.AddRulesToShow(new string[] { "bolt" }, ref rulesToShow); }

			byte[] ruleIdsToShow = rulesToShow.ToArray();
			WandData.actParamMenu.UpdateMenuOptions((byte)ruleIdsToShow.Length, ruleIdsToShow);
			return true;
		}
	}
}
