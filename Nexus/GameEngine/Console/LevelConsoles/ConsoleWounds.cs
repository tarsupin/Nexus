using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public static class ConsoleWounds {

		public static void CheatCodeHealth() {
			ConsoleTrack.PrepareTabLookup("Assign a given number of health (e.g. 'health 2'). Default is max health.");

			// Activate the Instruction
			if(ConsoleTrack.activate) {
				byte health = ConsoleTrack.instructionList.Count >= 2 ? (byte)ConsoleTrack.GetArgAsInt() : (byte)100;
				ConsoleTrack.character.wounds.AddHealth(health);
			}
		}

		public static void CheatCodeArmor() {
			ConsoleTrack.PrepareTabLookup("Assign a given number of armor (e.g. 'armor 2'). Default is max armor.");

			// Activate the Instruction
			if(ConsoleTrack.activate) {
				byte armor = ConsoleTrack.instructionList.Count >= 2 ? (byte)ConsoleTrack.GetArgAsInt() : (byte)100;
				ConsoleTrack.character.wounds.AddArmor(armor);
			}
		}

		public static void CheatCodeInvincible() {
			ConsoleTrack.PrepareTabLookup("Grant invincibility for X seconds (e.g. 'invincible 30'). Default is 60 seconds.");

			// Activate the Instruction
			if(ConsoleTrack.activate) {
				int duration = ConsoleTrack.instructionList.Count >= 2 ? (byte)ConsoleTrack.GetArgAsInt() : 60;
				ConsoleTrack.character.wounds.SetInvincible(duration * 60);
			}
		}

		public static void CheatCodeWound() {
			ConsoleTrack.PrepareTabLookup("Causes a standard wound. Deals 1 health damage.");

			// Activate the Instruction
			if(ConsoleTrack.activate) {
				ConsoleTrack.character.wounds.ReceiveWoundDamage(DamageStrength.Standard);
			}
		}

		public static void CheatCodeKill() {
			ConsoleTrack.PrepareTabLookup("Kills the character.");

			// Activate the Instruction
			if(ConsoleTrack.activate) {
				ConsoleTrack.character.wounds.ReceiveWoundDamage(DamageStrength.InstantKill);
			}
		}
	}
}
