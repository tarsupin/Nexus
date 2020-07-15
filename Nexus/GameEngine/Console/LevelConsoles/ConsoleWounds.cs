using Nexus.Engine;
using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public static class ConsoleWounds {

		public static void CheatCodeHealth() {
			ConsoleTrack.PrepareTabLookup("Assign a given number of health (e.g. 'health 2'). Default is max health.");

			// Activate the Instruction
			if(ConsoleTrack.activate) {
				byte health = ConsoleTrack.instructionList.Count >= 2 ? (byte)ConsoleTrack.GetArgAsInt() : (byte)100;
				UIHandler.AddNotification(UIAlertType.Success, "Health Granted", "Granted " + health + " Health to Character.", 180);
				ConsoleTrack.character.wounds.SetHealth(health);
			}
		}

		public static void CheatCodeArmor() {
			ConsoleTrack.PrepareTabLookup("Assign a given number of armor (e.g. 'armor 2'). Default is max armor.");

			// Activate the Instruction
			if(ConsoleTrack.activate) {
				byte armor = ConsoleTrack.instructionList.Count >= 2 ? (byte)ConsoleTrack.GetArgAsInt() : (byte)100;
				UIHandler.AddNotification(UIAlertType.Success, "Armor Granted", "Granted " + armor + " Armor to Character.", 180);
				ConsoleTrack.character.wounds.SetArmor(armor);
			}
		}

		public static void CheatCodeInvincible() {
			ConsoleTrack.PrepareTabLookup("Grant invincibility for X seconds (e.g. 'invincible 30'). Default is 60 seconds.");

			// Activate the Instruction
			if(ConsoleTrack.activate) {
				int duration = ConsoleTrack.instructionList.Count >= 2 ? (byte)ConsoleTrack.GetArgAsInt() : 60;
				UIHandler.AddNotification(UIAlertType.Success, "Invincibility Granted", "Granted Invincibility for " + duration + " Seconds.", 180);
				ConsoleTrack.character.wounds.SetInvincible(duration * 60);
			}
		}

		public static void CheatCodeWound() {
			ConsoleTrack.PrepareTabLookup("Causes a standard wound. Deals 1 health damage.");

			// Activate the Instruction
			if(ConsoleTrack.activate) {
				UIHandler.AddNotification(UIAlertType.Warning, "Damage Inflicted", "Recieved 1 Wound Damage.", 180);
				ConsoleTrack.character.wounds.ReceiveWoundDamage(DamageStrength.Standard);
			}
		}

		public static void CheatCodeKill() {
			ConsoleTrack.PrepareTabLookup("Kills the character.");

			// Activate the Instruction
			if(ConsoleTrack.activate) {
				UIHandler.AddNotification(UIAlertType.Warning, "Killed Self", "Character chose to respawn.", 180);
				ConsoleTrack.character.wounds.ReceiveWoundDamage(DamageStrength.InstantKill);
			}
		}
	}
}
