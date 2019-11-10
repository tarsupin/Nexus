using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nexus.Config;
using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;
using System;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public static class ConsoleTrack {
		public static Player player = null;
		public static Character character = null;
		public static string instructionText = String.Empty;
		public static List<string> instructionList = new List<string>() {};
		public static byte instructionNum = 0;
		public static string tabLookup = String.Empty;
		public static List<string> possibleTabs = new List<string>() {};

		public static void LoadInstructionText( string insText ) {

			// Clean the instruction text.
			ConsoleTrack.instructionText = insText.Trim().ToLower();

			// Get the instruction array.
			string[] insArray = ConsoleTrack.instructionText.Split(' ');

			ConsoleTrack.instructionList = new List<string>(insArray);

			// Reset Console Track Information
			ConsoleTrack.instructionNum = 0;
			ConsoleTrack.tabLookup = String.Empty;
			ConsoleTrack.possibleTabs.Clear();
			ConsoleTrack.player = null;
			ConsoleTrack.character = null;
		}

		public static string NextString() {
			byte num = ConsoleTrack.instructionNum;
			ConsoleTrack.instructionNum++;

			if(ConsoleTrack.instructionList.Count > num) {
				return ConsoleTrack.instructionList[num];
			}

			return String.Empty;
		}

		public static bool NextBool() {
			byte num = ConsoleTrack.instructionNum;
			ConsoleTrack.instructionNum++;

			if(ConsoleTrack.instructionList.Count > num) {
				string arg = ConsoleTrack.instructionList[num];
				if(arg == "true" || arg == "1" || arg == "on") { return true; }
			}

			return false;
		}

		public static int NextInt() {
			byte num = ConsoleTrack.instructionNum;
			ConsoleTrack.instructionNum++;

			if(ConsoleTrack.instructionList.Count > num) {
				string arg = ConsoleTrack.instructionList[num];
				int intVal;
				if(!Int32.TryParse(arg, out intVal)) { intVal = 0; }
				return intVal;
			}

			return 0;
		}

		public static float NextFloat() {
			byte num = ConsoleTrack.instructionNum;
			ConsoleTrack.instructionNum++;

			if(ConsoleTrack.instructionList.Count > num) {
				string arg = ConsoleTrack.instructionList[num];
				float floatVal;
				if(!float.TryParse(arg, out floatVal)) { floatVal = 0; }
				return floatVal;
			}

			return 0;
		}
	}

	public class Console {

		private readonly LevelScene scene;
		public Atlas atlas;

		// Console Text
		public List<string> consoleLines;		// Tracks each line that's been written
		public string consoleText;

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
				this.consoleText += charsPressed;
			}

			// Backspace
			if(input.LocalKeyDown(Keys.Back) && this.consoleText.Length > 0) {

				// After a hard delete (just pressed), wait 10 frames rather than 3.
				if(input.LocalKeyPressed(Keys.Back)) {
					this.consoleText = this.consoleText.Substring(0, this.consoleText.Length - 1);
					this.backspaceFrame = Systems.timer.Frame + 10;
				}

				else if(this.backspaceFrame < Systems.timer.Frame) {
					this.consoleText = this.consoleText.Substring(0, this.consoleText.Length - 1);

					// Delete a character every 3 frames.
					this.backspaceFrame = Systems.timer.Frame + 3;
				}
			}
			
			// Enter
			if(input.LocalKeyPressed(Keys.Enter)) {

				// Confirm the Instruction
				this.ProcessInstruction(this.consoleText);

				this.consoleText = string.Empty;
			}
		}

		public void ProcessInstruction( string insText ) {

			// Load Console Track Information
			ConsoleTrack.LoadInstructionText(insText);

			// Must have at least one part for a valid instruction.
			if(ConsoleTrack.instructionList.Count < 1) { return; }

			string currentIns = ConsoleTrack.NextString();

			// Assign Default Character and Player
			ConsoleTrack.player = Systems.localServer.MyPlayer;
			ConsoleTrack.character = Systems.localServer.MyCharacter;

			// Help
			if(currentIns == "help") { /* Explain the help topics */  }

			// Run Character Cheat Codes
			else if(currentIns == "suit") { ConsoleCharUpgrades.CheatCodeSuit(); }
			else if(currentIns == "hat") { ConsoleCharUpgrades.CheatCodeHat(); }
			else if(currentIns == "head") { ConsoleCharUpgrades.CheatCodeHead(); }
			else if(currentIns == "power") { ConsoleCharUpgrades.CheatCodePower(); }

			// Health, Wounds
			else if(currentIns == "heal") { Console.CheatCodeHeal(); }
			else if(currentIns == "armor") { Console.CheatCodeArmor(); }
			else if(currentIns == "invincible") { Console.CheatCodeInvincible(); }
			else if(currentIns == "wound") { Console.CheatCodeWound(); }
			else if(currentIns == "kill") { Console.CheatCodeKill(); }

			// Stats
			else if(currentIns == "stat") { Console.CheatCodeStat(); }

			// Run Movement
			else if(currentIns == "teleport") { Console.CheatCodeTeleport(); }

			// Debug
			else if(currentIns == "speed") { Console.DebugSpeed(); }
		}

		private static void CheatCodeStat() {
			string currentIns = ConsoleTrack.NextString();
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

		private static void CheatCodeKill() {
			ConsoleTrack.character.wounds.ReceiveWoundDamage(DamageStrength.Forced);
		}

		private static void CheatCodeWound() {
			ConsoleTrack.character.wounds.ReceiveWoundDamage(DamageStrength.Standard);
		}

		private static void CheatCodeHeal() {
			byte health = ConsoleTrack.instructionList.Count >= 2 ? (byte) ConsoleTrack.NextInt() : (byte) 100;
			ConsoleTrack.character.wounds.AddHealth(health);
		}

		private static void CheatCodeArmor() {
			byte armor = ConsoleTrack.instructionList.Count >= 2 ? (byte) ConsoleTrack.NextInt() : (byte) 100;
			ConsoleTrack.character.wounds.AddArmor(armor);
		}

		private static void CheatCodeInvincible() {
			int duration = ConsoleTrack.instructionList.Count >= 2 ? (byte) ConsoleTrack.NextInt() : 50000;
			ConsoleTrack.character.wounds.SetInvincible((uint) duration * 60);
		}

		private static void DebugSpeed() {
			string currentIns = ConsoleTrack.NextString();
			if(currentIns == string.Empty) { return; }

			if(currentIns == "normal" || currentIns == "fast") { DebugConfig.SetTickSpeed(DebugTickSpeed.StandardSpeed); }
			if(currentIns == "slow") { DebugConfig.SetTickSpeed(DebugTickSpeed.HalfSpeed); }
			if(currentIns == "slower") { DebugConfig.SetTickSpeed(DebugTickSpeed.QuarterSpeed); }
			if(currentIns == "slowest") { DebugConfig.SetTickSpeed(DebugTickSpeed.EighthSpeed); }
		}

		private static void CheatCodeTeleport() {
			string currentIns = ConsoleTrack.NextString();
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

			Systems.fonts.console.Draw("> " + consoleText + (Systems.timer.tick20Modulus < 10 ? "|" : ""), 10, Systems.screen.windowHeight - 90, Color.White);
		}
	}
}
