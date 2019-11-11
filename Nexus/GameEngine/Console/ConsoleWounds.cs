using Nexus.Gameplay;

namespace Nexus.GameEngine {

	public static class ConsoleWounds {

		public static void CheatCodeHeal() {
			byte health = ConsoleTrack.instructionList.Count >= 2 ? (byte)ConsoleTrack.NextInt() : (byte)100;
			ConsoleTrack.character.wounds.AddHealth(health);
		}

		public static void CheatCodeArmor() {
			byte armor = ConsoleTrack.instructionList.Count >= 2 ? (byte)ConsoleTrack.NextInt() : (byte)100;
			ConsoleTrack.character.wounds.AddArmor(armor);
		}

		public static void CheatCodeInvincible() {
			int duration = ConsoleTrack.instructionList.Count >= 2 ? (byte)ConsoleTrack.NextInt() : 50000;
			ConsoleTrack.character.wounds.SetInvincible((uint)duration * 60);
		}

		public static void CheatCodeWound() {
			ConsoleTrack.character.wounds.ReceiveWoundDamage(DamageStrength.Standard);
		}

		public static void CheatCodeKill() {
			ConsoleTrack.character.wounds.ReceiveWoundDamage(DamageStrength.Forced);
		}
	}
}
