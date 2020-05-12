using System;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class LevelConsole : Console {

		public LevelConsole() : base() {
			this.baseHelperText = "The debug console is used to access helpful diagnostic tools, cheat codes, level design options, etc.";

			this.consoleDict = new Dictionary<string, Action>() {
			
				// Debug
				{ "debug", ConsoleDebug.DebugBase },
				{ "macro", ConsoleMacro.DebugMacro },

				// Level
				{ "level", ConsoleLevel.CheatCodeLevel },

				// Character Stats
				{ "stats", ConsoleStats.CheatCodeStats },

				// Character Equipment
				{ "suit", ConsoleEquipment.CheatCodeSuit },
				{ "hat", ConsoleEquipment.CheatCodeHat },
				{ "head", ConsoleEquipment.CheatCodeHead },

				// Character Powers
				{ "power", ConsolePowers.CheatCodePowers },
			
				// Health, Wounds
				{ "heal", ConsoleWounds.CheatCodeHeal },
				{ "armor", ConsoleWounds.CheatCodeArmor },
				{ "invincible", ConsoleWounds.CheatCodeInvincible },
				{ "wound", ConsoleWounds.CheatCodeWound },
				{ "kill", ConsoleWounds.CheatCodeKill },
			};
		}
	}
}
