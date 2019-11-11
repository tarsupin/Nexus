using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nexus.Config;
using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Nexus.GameEngine {

	public static class ConsoleTrack {
		public static Player player = null; // Tracks which player to apply the effect to, if applicable.
		public static Character character = null; // Tracks which character to apply the effect to, if applicable.
		public static string instructionText = String.Empty; // The original command, as written into the console.
		public static List<string> instructionList = new List<string>() {}; // Stores the split() instructions.
		public static byte instructionIndex = 0; // The index of the instructionList that is next in line.
		public static string tabLookup = String.Empty; // The tab to highlight (whichever is most likely).
		public static string helpText = String.Empty; // Helpful text that applies to the current instruction set you're in.
		public static string possibleTabs = String.Empty; // A list of tab options to show.
		public static bool activate = false; // TRUE on the RunTick() that 'enter' gets pressed.

		public static void LoadInstructionText( string insText ) {

			// Get the instruction array.
			string[] insArray = insText.Split(' ');

			ConsoleTrack.instructionList = new List<string>(insArray);

			// Reset Console Track Information
			ConsoleTrack.instructionIndex = 0;
			ConsoleTrack.tabLookup = String.Empty;
			ConsoleTrack.helpText = String.Empty;
			ConsoleTrack.possibleTabs = String.Empty;
			ConsoleTrack.player = null;
			ConsoleTrack.character = null;
		}

		public static void PrepareTabLookup( Dictionary<string, Action> dict, string currentIns, string helpText = null ) {

			// Make sure you're on the last instruction, otherwise no tab lookup applies.
			if(!ConsoleTrack.OnLastInstruction()) { return; }

			if(currentIns.Length > 0) {

				// Scan for instructions that start with the arg provided:
				string tab = dict.Where(pv => pv.Key.StartsWith(currentIns)).FirstOrDefault().Key;

				// Update the tab lookup.
				ConsoleTrack.tabLookup = tab != null ? tab.Replace(currentIns, "") : string.Empty;
			}

			else {
				ConsoleTrack.tabLookup = string.Empty;
			}

			// Update possible tabs.
			ConsoleTrack.possibleTabs = "Options: " + String.Join(", ", dict.Keys.ToArray());

			// Update help text.
			if(helpText != null) { ConsoleTrack.helpText = helpText; }
		}

		public static void PrepareTabLookup( Dictionary<string, object> dict, string currentIns, string helpText = null ) {

			// Make sure you're on the last instruction, otherwise no tab lookup applies.
			if(!ConsoleTrack.OnLastInstruction()) { return; }

			if(currentIns.Length > 0) {

				// Scan for instructions that start with the arg provided:
				string tab = dict.Where(pv => pv.Key.StartsWith(currentIns)).FirstOrDefault().Key;

				// Update the tab lookup.
				ConsoleTrack.tabLookup = tab != null ? Regex.Replace(tab, "^" + currentIns, "") : string.Empty;

			} else {
				ConsoleTrack.tabLookup = string.Empty;
			}

			// Update possible tabs.
			ConsoleTrack.possibleTabs = "Options: " + String.Join(", ", dict.Keys.ToArray());

			// Update help text.
			if(helpText != null) { ConsoleTrack.helpText = helpText; }
		}

		// Returns `true` if we're on the last instruction arg.
		public static bool OnLastInstruction() {
			return ConsoleTrack.instructionIndex >= ConsoleTrack.instructionList.Count;
		}

		// Updates the instruction index and returns true if there's another instruction arg available.
		public static string NextArg() {
			byte num = ConsoleTrack.instructionIndex;
			ConsoleTrack.instructionIndex++;

			if(ConsoleTrack.instructionList.Count > num) {
				return ConsoleTrack.instructionList[num];
			}

			return string.Empty;
		}

		// Returns the next instruction arg as a bool.
		public static bool NextBool() {
			string arg = ConsoleTrack.NextArg();
			return (arg == "true" || arg == "1" || arg == "on");
		}

		// Returns the next instruction arg as an int.
		public static int NextInt() {
			string arg = ConsoleTrack.NextArg();

			if(arg != string.Empty) {
				int intVal;
				if(!Int32.TryParse(arg, out intVal)) { intVal = 0; }
				return intVal;
			}

			return 0;
		}

		// Returns the next instruction arg as a float.
		public static float NextFloat() {
			string arg = ConsoleTrack.NextArg();

			if(arg != string.Empty) {
				float floatVal;
				if(!float.TryParse(arg, out floatVal)) { floatVal = 0; }
				return floatVal;
			}

			return 0;
		}

		// Reset the Console Track Activation
		public static void ResetAfterActivation() {
			ConsoleTrack.activate = false; // Not resetting this would cause the instructions to activate every cycle.
			ConsoleTrack.instructionText = string.Empty;
			ConsoleTrack.helpText = String.Empty;
			ConsoleTrack.possibleTabs = String.Empty;
		}
	}

	public class Console {

		private readonly LevelScene scene;
		public Atlas atlas;

		// Console Text
		public List<string> consoleLines;		// Tracks each line that's been written

		private uint backspaceFrame = 0;

		public Console( LevelScene scene ) {
			this.scene = scene;
			this.atlas = Systems.mapper.atlas[(byte) AtlasGroup.Tiles];
		}

		public void RunTick() {
			InputClient input = Systems.input;

			// Get Characters Pressed (doesn't assist with order)
			string charsPressed = input.GetCharactersPressed();

			if(charsPressed.Length > 0) {
				ConsoleTrack.instructionText += charsPressed;
			}

			// Backspace
			else if(input.LocalKeyDown(Keys.Back) && ConsoleTrack.instructionText.Length > 0) {

				// After a hard delete (just pressed), wait 10 frames rather than 3.
				if(input.LocalKeyPressed(Keys.Back)) {
					ConsoleTrack.instructionText = ConsoleTrack.instructionText.Substring(0, ConsoleTrack.instructionText.Length - 1);
					this.backspaceFrame = Systems.timer.Frame + 10;
				}

				else if(this.backspaceFrame < Systems.timer.Frame) {
					ConsoleTrack.instructionText = ConsoleTrack.instructionText.Substring(0, ConsoleTrack.instructionText.Length - 1);

					// Delete a character every 3 frames.
					this.backspaceFrame = Systems.timer.Frame + 3;
				}
			}

			// Tab
			else if(input.LocalKeyDown(Keys.Tab)) {

				// Appends the tab lookup to the current instruction text.
				ConsoleTrack.instructionText += ConsoleTrack.tabLookup;
			}
			
			// Enter
			else if(input.LocalKeyPressed(Keys.Enter)) {
				ConsoleTrack.activate = true; // The instruction is meant to process completely.
			}

			// If there was no input provided, end here.
			else { return; }

			// Process the Instruction
			this.ProcessInstruction(ConsoleTrack.instructionText);

			// Cleanup
			if(ConsoleTrack.activate) {
				ConsoleTrack.ResetAfterActivation();
			}
		}

		public void ProcessInstruction(string insText) {

			// Clean the instruction text.
			string cleanText = insText.Trim().ToLower();

			// If there is no instruction text, no need to continue.
			if(cleanText.Length == 0) {
				ConsoleTrack.tabLookup = string.Empty;
				return;
			}

			// Load Console Track Information
			ConsoleTrack.LoadInstructionText(cleanText);

			// Must have at least one part for a valid instruction.
			if(ConsoleTrack.instructionList.Count < 1) { return; }

			string currentIns = ConsoleTrack.NextArg();

			// Assign Default Character and Player
			ConsoleTrack.player = Systems.localServer.MyPlayer;
			ConsoleTrack.character = Systems.localServer.MyCharacter;

			// Update the tab lookup.
			ConsoleTrack.PrepareTabLookup(consoleDict, currentIns, "The debug console is used to access helpful diagnostic tools, cheat codes, level design options, etc.");

			// Invoke the Relevant Next Function
			if(consoleDict.ContainsKey(currentIns)) {
				consoleDict[currentIns].Invoke();
			}

			//// Help
			//if(currentIns == "help") { /* Explain the help topics */  }

			//// Stats
			//else if(currentIns == "stat") { Console.CheatCodeStat(); }

			//// Run Movement
			//else if(currentIns == "teleport") { Console.CheatCodeTeleport(); }

			//// Debug
			//else if(currentIns == "speed") { Console.DebugSpeed(); }
		}

		public static readonly Dictionary<string, Action> consoleDict = new Dictionary<string, Action>() {
			
			// Character Equipment
			{ "suit", ConsoleEquipment.CheatCodeSuit },
			{ "hat", ConsoleEquipment.CheatCodeHat },
			{ "head", ConsoleEquipment.CheatCodeHead },

			// Character Powers
			{ "power", ConsolePowers.CheatCodePower },
			{ "magic", ConsolePowers.CheatCodeMagic },
			{ "ranged", ConsolePowers.CheatCodeRanged },
			{ "weapon", ConsolePowers.CheatCodeWeapon },
			{ "wand", ConsolePowers.CheatCodeWand },
			
			// Health, Wounds
			{ "heal", ConsoleWounds.CheatCodeHeal },
			{ "armor", ConsoleWounds.CheatCodeArmor },
			{ "invincible", ConsoleWounds.CheatCodeInvincible },
			{ "wound", ConsoleWounds.CheatCodeWound },
			{ "kill", ConsoleWounds.CheatCodeKill },
		};

		private static void CheatCodeStat() {
			string currentIns = ConsoleTrack.NextArg();
			if(currentIns == string.Empty) { return; }

			Character character = Systems.localServer.MyCharacter;

			// Abilities
			if(currentIns == "walljump") { bool boolVal = ConsoleTrack.NextBool(); character.stats.CanWallJump = boolVal; character.stats.CanWallSlide = boolVal; }
			else if(currentIns == "wallgrab") { character.stats.CanWallGrab = ConsoleTrack.NextBool(); }
			else if(currentIns == "wallslide") { character.stats.CanWallSlide = ConsoleTrack.NextBool(); }
			else if(currentIns == "fastcast") { character.stats.CanFastCast = ConsoleTrack.NextBool(); }
			else if(currentIns == "shell" || currentIns == "shellmastery") { character.stats.ShellMastery = ConsoleTrack.NextBool(); }
			else if(currentIns == "safeabove") { character.stats.SafeVsDamageAbove = ConsoleTrack.NextBool(); }
			else if(currentIns == "damageabove") { character.stats.InflictDamageAbove = ConsoleTrack.NextBool(); }

			// Gravity
			else if(currentIns == "gravity") { character.stats.BaseGravity = FInt.Create(ConsoleTrack.NextFloat()); }

			// Ground Speed
			else if(currentIns == "accel") { character.stats.RunAcceleration = FInt.Create(ConsoleTrack.NextFloat()); }
			else if(currentIns == "decel") { character.stats.RunDeceleration = FInt.Create(ConsoleTrack.NextFloat()); }
			else if(currentIns == "speed") { character.stats.RunMaxSpeed = (byte) Math.Round(ConsoleTrack.NextFloat()); }
			else if(currentIns == "walkmult") { character.stats.SlowSpeedMult = FInt.Create(ConsoleTrack.NextFloat()); }

			// Air Speed
			else if(currentIns == "airaccel") { character.stats.AirAcceleration = FInt.Create(ConsoleTrack.NextFloat()); }
			else if(currentIns == "airdecel") { character.stats.AirDeceleration = FInt.Create(ConsoleTrack.NextFloat()); }

			// Jumping
			else if(currentIns == "jumps" || currentIns == "maxjumps") { character.stats.JumpMaxTimes = (byte) Math.Round(ConsoleTrack.NextFloat()); }
			else if(currentIns == "jumpduration") { character.stats.JumpDuration = (byte) Math.Round(ConsoleTrack.NextFloat()); }
			else if(currentIns == "jumpstrength") { character.stats.JumpStrength = (byte) Math.Round(ConsoleTrack.NextFloat()); }

			// Wall Jumping
			else if(currentIns == "walljumpduration") { character.stats.WallJumpDuration = (byte)Math.Round(ConsoleTrack.NextFloat()); }
			else if(currentIns == "wallxstrength") { character.stats.WallJumpXStrength = (byte) Math.Round(ConsoleTrack.NextFloat()); }
			else if(currentIns == "wallystrength") { character.stats.WallJumpYStrength = (byte) Math.Round(ConsoleTrack.NextFloat()); }

			// Slide
			else if(currentIns == "slidedelay") { character.stats.SlideWaitDuration = (byte) Math.Round(ConsoleTrack.NextFloat()); }
			else if(currentIns == "slideduration") { character.stats.SlideDuration = (byte) Math.Round(ConsoleTrack.NextFloat()); }
			else if(currentIns == "slidestrength") { character.stats.SlideStrength = FInt.Create(ConsoleTrack.NextFloat()); }

			// Wound Stats
			else if(currentIns == "maxhealth") { character.wounds.WoundMaximum = (byte) ConsoleTrack.NextInt(); }
			else if(currentIns == "maxarmor") { character.wounds.WoundMaximum = (byte) ConsoleTrack.NextInt(); }
		}

		private static void DebugSpeed() {
			string currentIns = ConsoleTrack.NextArg();
			if(currentIns == string.Empty) { return; }

			if(currentIns == "normal" || currentIns == "fast") { DebugConfig.SetTickSpeed(DebugTickSpeed.StandardSpeed); }
			if(currentIns == "slow") { DebugConfig.SetTickSpeed(DebugTickSpeed.HalfSpeed); }
			if(currentIns == "slower") { DebugConfig.SetTickSpeed(DebugTickSpeed.QuarterSpeed); }
			if(currentIns == "slowest") { DebugConfig.SetTickSpeed(DebugTickSpeed.EighthSpeed); }
		}

		private static void CheatCodeTeleport() {
			string currentIns = ConsoleTrack.NextArg();
			if(currentIns == string.Empty) { return; }

			int x, y;

			if(currentIns == "coords") {
				if(ConsoleTrack.instructionList.Count < 4) { return; }
				x = ConsoleTrack.NextInt() * (byte) TilemapEnum.TileWidth;
				y = ConsoleTrack.NextInt() * (byte) TilemapEnum.TileHeight;
			} else {
				x = ConsoleTrack.NextInt() * (byte) TilemapEnum.TileWidth;
				y = ConsoleTrack.NextInt() * (byte) TilemapEnum.TileHeight;
			}

			// Run Teleport
			Character.Teleport(ConsoleTrack.character, x, y);
		}

		public void Draw() {

			// Draw Console Background
			Texture2D rect = new Texture2D(Systems.graphics.GraphicsDevice, 1, 1);
			rect.SetData(new[] { Color.Black });
			Systems.spriteBatch.Draw(rect, new Rectangle(0, Systems.screen.windowHeight - 100, Systems.screen.windowWidth, Systems.screen.windowHeight), Color.Black * 0.85f);

			// Draw Console Text
			string consoleString = "> " + ConsoleTrack.instructionText;
			Systems.fonts.console.Draw(consoleString + (Systems.timer.tick20Modulus < 10 ? "|" : ""), 10, Systems.screen.windowHeight - 90, Color.White);

			// Draw Console Tab Highlight, if applicable
			if(ConsoleTrack.tabLookup.Length > 0) {

				// Determine length of current instruction line:
				Vector2 fontLen = Systems.fonts.console.font.MeasureString(consoleString);

				Systems.fonts.console.Draw(ConsoleTrack.tabLookup, 10 + (int) Math.Round(fontLen.X), Systems.screen.windowHeight - 90, Color.DarkSlateGray);
			}

			// Draw Console Help Text, if applicable.
			if(ConsoleTrack.helpText.Length > 0) {
				Systems.fonts.console.Draw(ConsoleTrack.helpText, 10, Systems.screen.windowHeight - 75, Color.Gray);
			}

			// Draw Console Possible Tab Options, if applicable.
			if(ConsoleTrack.possibleTabs.Length > 0) {
				Systems.fonts.console.Draw(ConsoleTrack.possibleTabs, 10, Systems.screen.windowHeight - 60, Color.DarkTurquoise);
			}
		}
	}
}
