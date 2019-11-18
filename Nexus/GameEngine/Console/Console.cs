using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
				ConsoleTrack.tabLookup = tab != null ? Regex.Replace(tab, "^" + currentIns, "") : string.Empty;
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

	public static class Console {

		public static bool isEnabled = true;	// If console is not enabled, no commands can be triggered (even through macros).
		public static bool isVisible = false;   // Whether or not console is currently visible.

		public static LinkedList<string> consoleLines = new LinkedList<string>(); // Tracks the last twenty lines that have been typed.
		public static sbyte lineNum = 0;
		public static uint backspaceFrame = 0;

		public static void SetEnabled(bool enable) { Console.isEnabled = enable; if(!enable) { Console.isVisible = false; } }
		public static void SetVisible(bool visible) { if(Console.isEnabled) { Console.PrepareConsole(visible); } else { Console.isVisible = false; } }

		public static void PrepareConsole(bool visible) {
			Console.isVisible = visible;
			Console.lineNum = 0;
		}

		public static void RunTick() {
			InputClient input = Systems.input;

			// Determine if the console is being set to visible, or hidden (with the tilde key)
			if(input.LocalKeyPressed(Keys.OemTilde)) {
				Console.SetVisible(!Console.isVisible);

				if(Console.isVisible) {
					ConsoleTrack.ResetAfterActivation();
					ConsoleTrack.PrepareTabLookup(consoleDict, "", "The debug console is used to access helpful diagnostic tools, cheat codes, level design options, etc.");
				}
			}

			if(!Console.isVisible) { return; }

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
					Console.backspaceFrame = Systems.timer.Frame + 10;
				}

				else if(Console.backspaceFrame < Systems.timer.Frame) {
					ConsoleTrack.instructionText = ConsoleTrack.instructionText.Substring(0, ConsoleTrack.instructionText.Length - 1);

					// Delete a character every 3 frames.
					Console.backspaceFrame = Systems.timer.Frame + 3;
				}
			}

			// Press Up
			else if(input.LocalKeyPressed(Keys.Up)) {
				Console.ScrollConsoleText(-1);
			}

			// Press Down
			else if(input.LocalKeyPressed(Keys.Down)) {
				Console.ScrollConsoleText(1);
			}

			// Tab
			else if(input.LocalKeyDown(Keys.Tab)) {

				// Appends the tab lookup to the current instruction text.
				ConsoleTrack.instructionText += ConsoleTrack.tabLookup;
			}
			
			// Enter
			else if(input.LocalKeyPressed(Keys.Enter)) {
				ConsoleTrack.activate = true; // The instruction is meant to run (rather than just reveal new text hints).
			}

			// If there was no input provided, end here.
			else { return; }

			// Process the Instruction
			Console.SendCommand(ConsoleTrack.instructionText, false);
		}

		public static void SendCommand(string insText, bool setActivate = true) {
			if(!Console.isEnabled) { return; }

			if(setActivate) { ConsoleTrack.activate = true; } // The instruction is meant to run (rather than just reveal new text hints).
			ConsoleTrack.instructionText = insText;

			// Process the Instruction
			Console.ProcessInstruction(ConsoleTrack.instructionText);

			if(ConsoleTrack.activate) {

				// Track Written Lines
				Console.consoleLines.AddFirst(ConsoleTrack.instructionText);

				// If we're in the console save slots, save the instruction to the appropriate slot:
				if(Console.lineNum > 0 && Console.lineNum <= 5) {
					Systems.settings.input.consoleDown[Console.lineNum - 1] = ConsoleTrack.instructionText;
					Systems.settings.input.SaveSettings();
				}

				// If there are over 20 lines, remove the last one.
				if(Console.consoleLines.Count > 20) {
					Console.consoleLines.RemoveLast();
				}

				// Cleanup
				Console.lineNum = 0;
				ConsoleTrack.ResetAfterActivation();
				ConsoleTrack.PrepareTabLookup(consoleDict, "", "The debug console is used to access helpful diagnostic tools, cheat codes, level design options, etc.");
			}
		}

		public static void ProcessInstruction(string insText) {

			// Clean the instruction text.
			string cleanText = insText.Trim().ToLower();

			// If there is no instruction text, no need to continue.
			if(cleanText.Length == 0) {
				ConsoleTrack.tabLookup = string.Empty;
				return;
			}

			// If there was a pipe, split into multiple instructions UNLESS the instruction starts with "macro"
			if(insText.Contains('|') && !cleanText.StartsWith("macro")) {
				string[] multiInstructions = insText.Split('|');

				// If we're activating the instructions, run all of them.
				if(ConsoleTrack.activate) {
					foreach(string ins in multiInstructions) {
						Console.ProcessInstruction(ins);
					}
				}

				// If we're not activating the instructions, only run the last pipe (since we only need to identify that one).
				else {
					Console.ProcessInstruction(multiInstructions.Last());
				}

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

			//// Run Movement
			//else if(currentIns == "teleport") { Console.CheatCodeTeleport(); }
		}

		public static readonly Dictionary<string, Action> consoleDict = new Dictionary<string, Action>() {
			
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

		public static void ScrollConsoleText(sbyte scrollAmount) {
			Console.lineNum += scrollAmount;

			// If the scroll is less than 0, we're in the Console UP set:
			if(Console.lineNum < 0) {
				byte num = (byte) Math.Abs(Console.lineNum);

				if(num > Console.consoleLines.Count) { num = (byte) Console.consoleLines.Count; }
				ConsoleTrack.instructionText = num > 0 ? Console.consoleLines.ElementAt(num - 1) : "";
				Console.lineNum = (sbyte) -num;
			}

			// If the scroll value is greater than 0, we're in the Console DOWN set:
			else if(Console.lineNum > 0) {
				Console.lineNum = (sbyte) Math.Min((byte) 5, Console.lineNum);
				ConsoleTrack.instructionText = Systems.settings.input.consoleDown[Console.lineNum - 1];
				if(ConsoleTrack.instructionText == null) { ConsoleTrack.instructionText = ""; }
				ConsoleTrack.PrepareTabLookup(consoleDict, ConsoleTrack.instructionText, "Console Save Slot #" + Console.lineNum);
			}

			// If the scroll value is 0, we've returned to the default / entry point for the Console line:
			else {
				ConsoleTrack.instructionText = "";
				ConsoleTrack.PrepareTabLookup(consoleDict, "", "The debug console is used to access helpful diagnostic tools, cheat codes, level design options, etc.");
			}
		}

		public static void Draw() {
			if(!Console.isVisible) { return; }

			FontClass consoleFont = Systems.fonts.console;

			// Draw Console Background
			Systems.spriteBatch.Draw(Systems.tex2dBlack, new Rectangle(0, Systems.screen.windowHeight - 100, Systems.screen.windowWidth, Systems.screen.windowHeight), Color.Black * 0.85f);

			// Draw Console Text
			string consoleString = "> " + ConsoleTrack.instructionText;
			consoleFont.Draw(consoleString + (Systems.timer.tick20Modulus < 10 ? "|" : ""), 10, Systems.screen.windowHeight - 90, Color.White);

			// Draw Console Tab Highlight, if applicable
			if(ConsoleTrack.tabLookup.Length > 0) {

				// Determine length of current instruction line:
				Vector2 fontLen = consoleFont.font.MeasureString(consoleString);

				consoleFont.Draw(ConsoleTrack.tabLookup, 10 + (int) Math.Round(fontLen.X), Systems.screen.windowHeight - 90, Color.DarkSlateGray);
			}

			// Draw Console Help Text, if applicable.
			if(ConsoleTrack.helpText.Length > 0) {
				consoleFont.Draw(ConsoleTrack.helpText, 10, Systems.screen.windowHeight - 75, Color.Gray);
			}

			// Draw Console Possible Tab Options, if applicable.
			if(ConsoleTrack.possibleTabs.Length > 0) {
				consoleFont.Draw(ConsoleTrack.possibleTabs, 10, Systems.screen.windowHeight - 60, Color.DarkTurquoise);
			}

			// Draw Chat Console, if applicable.
			ChatConsole.Draw();
		}
	}
}
