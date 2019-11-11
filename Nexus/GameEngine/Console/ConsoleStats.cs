﻿using Nexus.Engine;
using Nexus.Objects;
using System;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public static class ConsoleStats {

		public static void CheatCodeStats() {
			string statIns = ConsoleTrack.NextArg();

			ConsoleTrack.PrepareTabLookup(statCodes, statIns, "Assign stats for the character.");

			// If the stat instruction is a full word, then we can indicate that it's time to provide additional help text:
			if(statCodes.ContainsKey(statIns)) {

				if(statCodes[statIns] is Action[]) {
					(statCodes[statIns] as Action[])[0].Invoke();
					return;
				}

				ConsoleTrack.possibleTabs = "";
				ConsoleTrack.helpText = statCodes[statIns].ToString();
			}

			if(ConsoleTrack.activate) {
				Character character = ConsoleTrack.character;

				// Gravity
				if(statIns == "gravity") { character.stats.BaseGravity = FInt.Create(ConsoleTrack.NextFloat()); }

				// Abilities
				else if(statIns == "fast-cast") { character.stats.CanFastCast = ConsoleTrack.NextBool(); }
				else if(statIns == "shell-mastery") { character.stats.ShellMastery = ConsoleTrack.NextBool(); }
				else if(statIns == "safe-above") { character.stats.SafeVsDamageAbove = ConsoleTrack.NextBool(); }
				else if(statIns == "damage-above") { character.stats.InflictDamageAbove = ConsoleTrack.NextBool(); }

				// Wound Stats
				else if(statIns == "maxhealth") { character.wounds.WoundMaximum = (byte) ConsoleTrack.NextInt(); }
				else if(statIns == "maxarmor") { character.wounds.WoundMaximum = (byte) ConsoleTrack.NextInt(); }
			}
		}

		public static readonly Dictionary<string, object> statCodes = new Dictionary<string, object>() {
			
			// Actions
			{ "run", new Action[] { ConsoleStats.CheatStatRun } },
			{ "jump", new Action[] { ConsoleStats.CheatStatJump } },
			{ "slide", new Action[] { ConsoleStats.CheatStatSlide } },
			{ "wall", new Action[] { ConsoleStats.CheatStatWall } },
			
			// Gravity
			{ "gravity", "How strong does gravity apply to the character? Default value is 0.5" },

			// Abilities
			{ "fast-cast", "Is the character able to use weapons and powers faster than usual? Set to TRUE or FALSE." },
			{ "shell-mastery", "Is the character given special protection against shell damage? Set to TRUE or FALSE." },
			{ "safe-above", "Is the character protected from damage from above? Set to TRUE or FALSE." },
			{ "damage-above", "Does the character cause extra damage to objects above? Set to TRUE or FALSE." },

			// Wound Stats
			{ "maxhealth", "What is the maximum amount of health the character can possess? Default value is 3." },
			{ "maxarmor", "What is the maximum amount of armor the character can possess? Default value is 3." },
		};

		public static void CheatStatWall() {
			string currentIns = ConsoleTrack.NextArg();

			ConsoleTrack.PrepareTabLookup(wallStatCodes, currentIns, "Assign wall-related stats for the character.");

			// If the stat instruction is a full word, then we can indicate that it's time to provide additional help text:
			if(wallStatCodes.ContainsKey(currentIns)) {
				ConsoleTrack.possibleTabs = "";
				ConsoleTrack.helpText = wallStatCodes[currentIns].ToString();
			}

			if(ConsoleTrack.activate) {
				Character character = ConsoleTrack.character;

				// Abilities
				if(currentIns == "can-wall-jump") { bool boolVal = ConsoleTrack.NextBool(); character.stats.CanWallJump = boolVal; character.stats.CanWallSlide = boolVal; }
				else if(currentIns == "can-grab") { character.stats.CanWallGrab = ConsoleTrack.NextBool(); }
				else if(currentIns == "can-slide") { character.stats.CanWallSlide = ConsoleTrack.NextBool(); }

				// Wall Jumping
				else if(currentIns == "jump-duration") { character.stats.WallJumpDuration = (byte)Math.Round(ConsoleTrack.NextFloat()); }
				else if(currentIns == "jump-strength") { character.stats.WallJumpXStrength = (byte) Math.Round(ConsoleTrack.NextFloat()); }
				else if(currentIns == "jump-height") { character.stats.WallJumpYStrength = (byte) Math.Round(ConsoleTrack.NextFloat()); }
			}
		}

		public static readonly Dictionary<string, object> wallStatCodes = new Dictionary<string, object>() {
			{ "can-wall-jump", "Character can jump off of walls? Set to TRUE or FALSE." },
			{ "can-grab", "Character can cling to walls? Set to TRUE or FALSE." },
			{ "can-slide", "Character slides down walls slowly? Set to TRUE or FALSE." },
			{ "jump-duration", "How long does a wall-jump last, in frames? Default value is 8." },
			{ "jump-strength", "How much jump force is applied to a wall jump? Default value is 8." },
			{ "jump-height", "How much upward force is applied to a wall jump? Default value is 8." },
		};

		public static void CheatStatRun() {
			string currentIns = ConsoleTrack.NextArg();

			ConsoleTrack.PrepareTabLookup(runStatCodes, currentIns, "Assign ground and running-related stats for the character.");

			// If the stat instruction is a full word, then we can indicate that it's time to provide additional help text:
			if(runStatCodes.ContainsKey(currentIns)) {
				ConsoleTrack.possibleTabs = "";
				ConsoleTrack.helpText = runStatCodes[currentIns].ToString();
			}

			if(ConsoleTrack.activate) {
				Character character = ConsoleTrack.character;
				
				// Ground Speed
				if(currentIns == "accel") { character.stats.RunAcceleration = FInt.Create(ConsoleTrack.NextFloat()); }
				else if(currentIns == "decel") { character.stats.RunDeceleration = FInt.Create(ConsoleTrack.NextFloat()); }
				else if(currentIns == "speed") { character.stats.RunMaxSpeed = (byte) Math.Round(ConsoleTrack.NextFloat()); }
				else if(currentIns == "walkmult") { character.stats.SlowSpeedMult = FInt.Create(ConsoleTrack.NextFloat()); }
			}
		}

		public static readonly Dictionary<string, object> runStatCodes = new Dictionary<string, object>() {
			{ "accel", "How fast does the character accelerate? Default value is 0.3" },
			{ "decel", "How fast does the character decelerate? Default value is 0.2" },
			{ "max-speed", "What is the maximum speed the character can run? Default value is 7." },
			{ "walk", "What is the walk multiplier, or the percent run speed that walk moves at? Default is 0.65" },
		};

		public static void CheatStatJump() {
			string currentIns = ConsoleTrack.NextArg();

			ConsoleTrack.PrepareTabLookup(jumpStatCodes, currentIns, "Assign jumping-related stats for the character.");

			// If the stat instruction is a full word, then we can indicate that it's time to provide additional help text:
			if(jumpStatCodes.ContainsKey(currentIns)) {
				ConsoleTrack.possibleTabs = "";
				ConsoleTrack.helpText = jumpStatCodes[currentIns].ToString();
			}

			if(ConsoleTrack.activate) {
				Character character = ConsoleTrack.character;
				
				// Air Speed
				if(currentIns == "accel") { character.stats.AirAcceleration = FInt.Create(ConsoleTrack.NextFloat()); }
				else if(currentIns == "decel") { character.stats.AirDeceleration = FInt.Create(ConsoleTrack.NextFloat()); }

				// Jumping
				else if(currentIns == "max-jumps") { character.stats.JumpMaxTimes = (byte) Math.Round(ConsoleTrack.NextFloat()); }
				else if(currentIns == "duration") { character.stats.JumpDuration = (byte) Math.Round(ConsoleTrack.NextFloat()); }
				else if(currentIns == "height") { character.stats.JumpStrength = (byte) Math.Round(ConsoleTrack.NextFloat()); }
			}
		}

		public static readonly Dictionary<string, object> jumpStatCodes = new Dictionary<string, object>() {
			
			// Air Speed
			{ "accel", "How fast does the character accelerate while in the air? Default value is 0.45" },
			{ "decel", "How fast does the character decelerate while in the air? Default value is 0.2" },

			// Jumping
			{ "max-jumps", "How many jumps do you receive before landing? Default value is 1." },
			{ "duration", "How long (in frames) does the jump last? Default value is 10." },
			{ "height", "How much force does the jump have? Default value is 11." },
		};

		public static void CheatStatSlide() {
			string currentIns = ConsoleTrack.NextArg();

			ConsoleTrack.PrepareTabLookup(slideStatCodes, currentIns, "Assign sliding-related stats for the character.");

			// If the stat instruction is a full word, then we can indicate that it's time to provide additional help text:
			if(slideStatCodes.ContainsKey(currentIns)) {
				ConsoleTrack.possibleTabs = "";
				ConsoleTrack.helpText = slideStatCodes[currentIns].ToString();
			}

			if(ConsoleTrack.activate) {
				Character character = ConsoleTrack.character;
				
				// Slide
				if(currentIns == "cooldown") { character.stats.SlideWaitDuration = (byte) Math.Round(ConsoleTrack.NextFloat()); }
				else if(currentIns == "duration") { character.stats.SlideDuration = (byte) Math.Round(ConsoleTrack.NextFloat()); }
				else if(currentIns == "strength") { character.stats.SlideStrength = FInt.Create(ConsoleTrack.NextFloat()); }
			}
		}

		public static readonly Dictionary<string, object> slideStatCodes = new Dictionary<string, object>() {
			{ "cooldown", "How long (in frames) before the slide ability can be reused? Default value is 36." },
			{ "duration", "How long (in frames) does the slide last? Default value is 12." },
			{ "strength", "How much force is applied to a slide? Default value is 12." },
		};
	}
}
