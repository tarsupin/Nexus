using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class ParamsUpgrades : Params {

		public ParamsUpgrades() {
			this.rules.Add(new LabeledParam("hp", "Starting Health", ParamTrack.AssignHP, (byte)0));
			this.rules.Add(new LabeledParam("armor", "Starting Armor", ParamTrack.AssignArmor, (byte)0));
			this.rules.Add(new LabeledParam("suit", "Suit", ParamTrack.AssignSuitList, 0));
			this.rules.Add(new LabeledParam("hat", "Hat", ParamTrack.AssignHatList, 0));
			this.rules.Add(new LabeledParam("shoes", "Shoes", ParamTrack.AssignShoeList, 0));
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
			this.AddRulesToShow(new string[] { "hp", "armor", "suit", "hat", "shoes", "mob", "att" }, ref rulesToShow);

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
