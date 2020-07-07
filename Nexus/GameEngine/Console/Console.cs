using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nexus.Engine;
using Nexus.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nexus.GameEngine {

	public class Console : IMenu {

		public LinkedList<string> consoleLines = new LinkedList<string>(); // Tracks the last twenty lines that have been typed.
		public sbyte lineNum = 0;
		public int backspaceFrame = 0;
		public bool enabled = true;			// Set to false when you're in a live game, or where you shouldn't be allowed to use console.
		public bool beenOpened = false;		// Set to true on the first open.

		// Console-Specific Values
		public Dictionary<string, Action> consoleDict;     // A dictionary of base commands for the console.
		public string baseHelperText = "";

		public Console() {}

		public virtual void OnFirstOpen() { this.beenOpened = true; }
		public virtual void OnOpen() {}

		public void Open() {
			UIHandler.SetMenu(this, true);
			ConsoleTrack.ResetValues();
			ConsoleTrack.PrepareTabLookup(this.consoleDict, "", this.baseHelperText);
			if(!this.beenOpened) { this.OnFirstOpen(); }
			this.OnOpen();
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
					this.backspaceFrame = Systems.timer.UniFrame + 10;

					// If held shift or control, remove the full line.
					if(input.LocalKeyDown(Keys.LeftShift) || input.LocalKeyDown(Keys.RightShift) || input.LocalKeyDown(Keys.LeftControl) || input.LocalKeyDown(Keys.RightControl)) {
						ConsoleTrack.instructionText = "";
					}
				}

				else if(this.backspaceFrame < Systems.timer.UniFrame) {
					ConsoleTrack.instructionText = ConsoleTrack.instructionText.Substring(0, ConsoleTrack.instructionText.Length - 1);

					// Delete a character every 3 frames.
					this.backspaceFrame = Systems.timer.UniFrame + 3;
				}
			}

			// Up + Down - Scroll through Console Text
			else if(input.LocalKeyPressed(Keys.Up)) { this.ScrollConsoleText(-1); }
			else if(input.LocalKeyPressed(Keys.Down)) { this.ScrollConsoleText(1); }

			// Tab - Appends the tab lookup to the current instruction text.
			else if(input.LocalKeyDown(Keys.Tab)) { ConsoleTrack.instructionText += ConsoleTrack.tabLookup; }
			
			// Enter - Process the instruction.
			else if(input.LocalKeyPressed(Keys.Enter)) {
				ConsoleTrack.activate = true; // The instruction is meant to run (rather than just reveal new text hints).
			}
			
			// Close Console (Tilde, Escape)
			else if(input.LocalKeyPressed(Keys.OemTilde) || input.LocalKeyPressed(Keys.Escape)) {
				UIHandler.SetMenu(null, false);
			}

			// If there was no input provided, end here.
			else { return; }

			// Process the Instruction
			this.SendCommand(ConsoleTrack.instructionText, false);
		}

		public void SendCommand(string insText, bool setActivate = true) {
			if(setActivate) { ConsoleTrack.activate = true; } // The instruction is meant to run (rather than just reveal new text hints).
			ConsoleTrack.instructionText = insText;

			// Process the Instruction
			this.ProcessInstruction(ConsoleTrack.instructionText);

			if(ConsoleTrack.activate) {

				// Track Written Lines
				this.consoleLines.AddFirst(ConsoleTrack.instructionText);

				// If we're in the console save slots, save the instruction to the appropriate slot:
				if(this.lineNum > 0 && this.lineNum <= 5) {
					Systems.settings.input.consoleDown[this.lineNum - 1] = ConsoleTrack.instructionText;
					Systems.settings.input.SaveSettings();
				}

				// If there are over 20 lines, remove the last one.
				if(this.consoleLines.Count > 20) {
					this.consoleLines.RemoveLast();
				}

				// Cleanup
				this.lineNum = 0;
				ConsoleTrack.ResetValues();
				ConsoleTrack.PrepareTabLookup(this.baseHelperText);
				ConsoleTrack.possibleTabs = "Options: " + String.Join(", ", this.consoleDict.Keys.ToArray());
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

			// If there was a pipe, split into multiple instructions UNLESS the instruction starts with "macro"
			if(insText.Contains('|') && !cleanText.StartsWith("macro")) {
				string[] multiInstructions = insText.Split('|');

				// If we're activating the instructions, run all of them.
				if(ConsoleTrack.activate) {
					foreach(string ins in multiInstructions) {
						this.ProcessInstruction(ins);
					}
				}

				// If we're not activating the instructions, only run the last pipe (since we only need to identify that one).
				else {
					this.ProcessInstruction(multiInstructions.Last());
				}

				return;
			}

			// Load Console Track Information
			ConsoleTrack.LoadInstructionText(cleanText);

			// Must have at least one part for a valid instruction.
			if(ConsoleTrack.instructionList.Count < 1) { return; }

			string currentIns = ConsoleTrack.GetArg();

			// Assign Default Character and Player
			ConsoleTrack.player = Systems.localServer.MyPlayer;
			ConsoleTrack.character = Systems.localServer.MyCharacter;

			// Update the tab lookup.
			ConsoleTrack.PrepareTabLookup(this.consoleDict, currentIns, this.baseHelperText);

			// Invoke the Relevant Next Function
			if(this.consoleDict.ContainsKey(currentIns)) {
				this.consoleDict[currentIns].Invoke();
			}
		}

		public void ScrollConsoleText(sbyte scrollAmount) {
			this.lineNum += scrollAmount;

			// If the scroll is less than 0, we're in the Console UP set:
			if(this.lineNum < 0) {
				byte num = (byte) Math.Abs(this.lineNum);

				if(num > this.consoleLines.Count) { num = (byte) this.consoleLines.Count; }
				ConsoleTrack.instructionText = num > 0 ? this.consoleLines.ElementAt(num - 1) : "";
				this.lineNum = (sbyte) -num;
			}

			// If the scroll value is greater than 0, we're in the Console DOWN set:
			else if(this.lineNum > 0) {
				this.lineNum = (sbyte) Math.Min((byte) 5, this.lineNum);
				ConsoleTrack.instructionText = Systems.settings.input.consoleDown[this.lineNum - 1];
				if(ConsoleTrack.instructionText == null) { ConsoleTrack.instructionText = ""; }
				ConsoleTrack.PrepareTabLookup(this.consoleDict, ConsoleTrack.instructionText, "Console Save Slot #" + this.lineNum);
			}

			// If the scroll value is 0, we've returned to the default / entry point for the Console line:
			else {
				ConsoleTrack.instructionText = "";
				ConsoleTrack.PrepareTabLookup(this.consoleDict, "", this.baseHelperText);
			}
		}

		public void Draw() {
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
