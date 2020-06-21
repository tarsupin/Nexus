using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class ParamsCheckpoint : Params {

		public ParamsCheckpoint() {
			this.rules.Add(new LabeledParam("suit", "Suit", ParamTrack.AssignSuitList, 0));
			this.rules.Add(new LabeledParam("hat", "Hat", ParamTrack.AssignHatList, 0));
			this.rules.Add(new LabeledParam("mob", "Mobility Power", ParamTrack.AssignMobilityList, 0));
			this.rules.Add(new LabeledParam("att", "Attack Power", ParamTrack.AssignAttackList, 0));
			this.rules.Add(new LabeledParam("attSet", "Weapon", ParamTrack.AssignWeaponList, 0));
			this.rules.Add(new LabeledParam("attSet", "Spells", ParamTrack.AssignSpellsList, 0));
			this.rules.Add(new LabeledParam("attSet", "Thrown", ParamTrack.AssignThrownList, 0));
			this.rules.Add(new LabeledParam("attSet", "Bolts", ParamTrack.AssignBoltsList, 0));
		}

		// Returning `true` means it ran a custom menu update. `false` means the menu needs to be updated manually.
		public override bool RunCustomMenuUpdate() {
			List<byte> rulesToShow = new List<byte>();

			// Add Rules that are present for this menu:
			this.AddRulesToShow(new string[] { "suit", "hat", "mob", "att" }, ref rulesToShow);

			short attType = WandData.GetParamVal(WandData.actParamSet, "att");

			if(attType >= 2) {
				rulesToShow.Add((byte)(attType + 2));
			}

			byte[] ruleIdsToShow = rulesToShow.ToArray();
			WandData.actParamMenu.UpdateMenuOptions((byte)ruleIdsToShow.Length, ruleIdsToShow);
			return true;
		}
	}
}
