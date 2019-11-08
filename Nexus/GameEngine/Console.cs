using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nexus.Config;
using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using Nexus.Objects;
using System;
using System.Collections.Generic;

namespace Nexus.GameEngine {

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

			// Clean the instruction text.
			insText = insText.Trim().ToLower();

			// Get the instruction array.
			string[] ins = insText.Split(' ');

			// Must have at least one part for a valid instruction.
			if(ins.Length < 1) { return; }

			// Help
			if(ins[0] == "help") { /* Explain the help topics */  }

			// Run Collectable Cheat Codes
			else if(ins[0] == "suit") { Console.CheatCodeSuit(ins); }
			else if(ins[0] == "hat") { Console.CheatCodeHat(ins); }
			else if(ins[0] == "head") { Console.CheatCodeHead(ins); }

			// Health, Wounds
			else if(ins[0] == "heal") { Console.CheatCodeHeal(ins); }
			else if(ins[0] == "armor") { Console.CheatCodeArmor(ins); }
			else if(ins[0] == "invincible") { Console.CheatCodeInvincible(ins); }
			else if(ins[0] == "wound") { Console.CheatCodeWound(ins); }
			else if(ins[0] == "kill") { Console.CheatCodeKill(ins); }

			// Stats
			else if(ins[0] == "stat") { Console.CheatCodeStat(ins); }

			// Run Movement
			else if(ins[0] == "teleport") { Console.CheatCodeTeleport(ins); }

			// Debug
			else if(ins[0] == "speed") { Console.DebugSpeed(ins); }
		}

		private static void CheatCodeStat(string[] ins) {
			if(ins.Length < 3) { return; }

			Character character = Systems.localServer.MyCharacter;
			bool boolVal = Console.GetBoolArg(ins[2]);
			float floatVal = Console.GetFloatArg(ins[2]);

			// Abilities
			if(ins[1] == "walljump") { character.stats.CanWallJump = boolVal; character.stats.CanWallSlide = boolVal; }
			else if(ins[1] == "wallgrab") { character.stats.CanWallGrab = boolVal; }
			else if(ins[1] == "wallslide") { character.stats.CanWallSlide = boolVal; }
			else if(ins[1] == "fastcast") { character.stats.CanFastCast = boolVal; }
			else if(ins[1] == "shell" || ins[1] == "shellmastery") { character.stats.ShellMastery = boolVal; }
			else if(ins[1] == "safeabove") { character.stats.SafeVsDamageAbove = boolVal; }
			else if(ins[1] == "damageabove") { character.stats.InflictDamageAbove = boolVal; }

			// Gravity
			else if(ins[1] == "gravity") { character.stats.BaseGravity = FInt.Create(floatVal); }

			// Ground Speed
			else if(ins[1] == "accel") { character.stats.RunAcceleration = FInt.Create(floatVal); }
			else if(ins[1] == "decel") { character.stats.RunDeceleration = FInt.Create(floatVal); }
			else if(ins[1] == "speed") { character.stats.RunMaxSpeed = (byte) Math.Round(floatVal); }
			else if(ins[1] == "walkmult") { character.stats.SlowSpeedMult = FInt.Create(floatVal); }

			// Air Speed
			else if(ins[1] == "airaccel") { character.stats.AirAcceleration = FInt.Create(floatVal); }
			else if(ins[1] == "airdecel") { character.stats.AirDeceleration = FInt.Create(floatVal); }

			// Jumping
			else if(ins[1] == "jumps" || ins[1] == "maxjumps") { character.stats.JumpMaxTimes = (byte) Math.Round(floatVal); }
			else if(ins[1] == "jumpduration") { character.stats.JumpDuration = (byte) Math.Round(floatVal); }
			else if(ins[1] == "jumpstrength") { character.stats.JumpStrength = (byte) Math.Round(floatVal); }

			// Wall Jumping
			else if(ins[1] == "walljumpduration") { character.stats.WallJumpDuration = (byte)Math.Round(floatVal); }
			else if(ins[1] == "wallxstrength") { character.stats.WallJumpXStrength = (byte) Math.Round(floatVal); }
			else if(ins[1] == "wallystrength") { character.stats.WallJumpYStrength = (byte) Math.Round(floatVal); }

			// Slide
			else if(ins[1] == "slidedelay") { character.stats.SlideWaitDuration = (byte) Math.Round(floatVal); }
			else if(ins[1] == "slideduration") { character.stats.SlideDuration = (byte) Math.Round(floatVal); }
			else if(ins[1] == "slidestrength") { character.stats.SlideStrength = FInt.Create(floatVal); }

			// Wound Stats
			else if(ins[1] == "maxhealth") { character.wounds.WoundMaximum = (byte) Console.GetIntArg(ins[2]); }
			else if(ins[1] == "maxarmor") { character.wounds.WoundMaximum = (byte) Console.GetIntArg(ins[2]); }
		}

		private static void CheatCodeKill( string[] ins ) {
			Systems.localServer.MyCharacter.wounds.ReceiveWoundDamage(DamageStrength.Forced);
		}

		private static void CheatCodeWound( string[] ins ) {
			Systems.localServer.MyCharacter.wounds.ReceiveWoundDamage(DamageStrength.Standard);
		}

		private static void CheatCodeHeal( string[] ins ) {
			byte health = ins.Length == 2 ? (byte) Console.GetIntArg(ins[1]) : (byte) 100;
			Systems.localServer.MyCharacter.wounds.AddHealth(health);
		}

		private static void CheatCodeArmor( string[] ins ) {
			byte armor = ins.Length == 2 ? (byte) Console.GetIntArg(ins[1]) : (byte) 100;
			Systems.localServer.MyCharacter.wounds.AddArmor(armor);
		}

		private static void CheatCodeInvincible( string[] ins ) {
			int duration = ins.Length == 2 ? (byte) Console.GetIntArg(ins[1]) : 50000;
			Systems.localServer.MyCharacter.wounds.SetInvincible((uint) duration * 60);
		}

		private static void DebugSpeed( string[] ins ) {
			if(ins.Length < 2) { return; }

			if(ins[1] == "normal" || ins[1] == "fast") { DebugConfig.SetTickSpeed(DebugTickSpeed.StandardSpeed); }
			if(ins[1] == "slow") { DebugConfig.SetTickSpeed(DebugTickSpeed.HalfSpeed); }
			if(ins[1] == "slower") { DebugConfig.SetTickSpeed(DebugTickSpeed.QuarterSpeed); }
			if(ins[1] == "slowest") { DebugConfig.SetTickSpeed(DebugTickSpeed.EighthSpeed); }
		}

		private static void CheatCodeTeleport( string[] ins ) {
			if(ins.Length < 3) { return; }

			int x, y;

			if(ins[1] == "coords") {
				if(ins.Length < 4) { return; }
				x = Console.GetIntArg(ins[2]) * (byte) TilemapEnum.TileWidth;
				y = Console.GetIntArg(ins[3]) * (byte) TilemapEnum.TileHeight;
			} else {
				x = Console.GetIntArg(ins[1]) * (byte) TilemapEnum.TileWidth;
				y = Console.GetIntArg(ins[2]) * (byte) TilemapEnum.TileHeight;
			}

			// Run Teleport
			Character.Teleport(Systems.localServer.MyCharacter, x, y);
		}

		private static void CheatCodeSuit( string[] ins ) {

			// If "suit" is the only instruction, give a random suit to the character.
			if(ins.Length <= 1) {
				Suit.AssignToCharacter(Systems.localServer.MyCharacter, (byte) SuitSubType.RandomSuit, true);
				return;
			}

			// Get the Suit Type by instruction:
			if(suitCodes.ContainsKey(ins[1])) {
				SuitSubType subType = suitCodes[ins[1]];
				Suit.AssignToCharacter(Systems.localServer.MyCharacter, (byte) subType, true);
			}
		}

		private static readonly Dictionary<string, SuitSubType> suitCodes = new Dictionary<string, SuitSubType>() {

			// Random Options
			{ "any", SuitSubType.RandomSuit },
			{ "rand", SuitSubType.RandomSuit },
			{ "random", SuitSubType.RandomSuit },
			{ "randomsuit", SuitSubType.RandomSuit },

			// Ninja Suits
			{ "ninja", SuitSubType.RandomNinja },
			{ "blackninja", SuitSubType.BlackNinja },
			{ "blueninja", SuitSubType.BlueNinja },
			{ "greenninja", SuitSubType.GreenNinja },
			{ "redninja", SuitSubType.RedNinja },
			{ "whiteninja", SuitSubType.WhiteNinja },

			// Wizard Suits
			{ "wizard", SuitSubType.RandomWizard },
			{ "bluewizard", SuitSubType.BlueWizard },
			{ "greenwizard", SuitSubType.GreenWizard },
			{ "redwizard", SuitSubType.RedWizard },
			{ "whitewizard", SuitSubType.WhiteWizard },

			// Basic Suits
			{ "basic", SuitSubType.RandomBasic },
			{ "red", SuitSubType.RedBasic },
			{ "redbasic", SuitSubType.RedBasic },
		};
		
		private static void CheatCodeHat( string[] ins ) {

			// If "hat" is the only instruction, give a random hat to the character.
			if(ins.Length <= 1) {
				Hat.AssignToCharacter(Systems.localServer.MyCharacter, (byte) HatSubType.RandomHat, true);
				return;
			}

			// Get the Hat Type by instruction:
			if(hatCodes.ContainsKey(ins[1])) {
				HatSubType subType = hatCodes[ins[1]];
				Hat.AssignToCharacter(Systems.localServer.MyCharacter, (byte) subType, true);
			}
		}

		private static readonly Dictionary<string, HatSubType> hatCodes = new Dictionary<string, HatSubType>() {

			// Random Options
			{ "any", HatSubType.RandomHat },
			{ "rand", HatSubType.RandomHat },
			{ "random", HatSubType.RandomHat },
			{ "randomhat", HatSubType.RandomHat },

			// Power Hats
			{ "angel", HatSubType.AngelHat },
			{ "angelhat", HatSubType.AngelHat },
			{ "bamboo", HatSubType.BambooHat },
			{ "bamboohat", HatSubType.BambooHat },
			{ "cowboy", HatSubType.CowboyHat },
			{ "cowboyhat", HatSubType.CowboyHat },
			{ "feather", HatSubType.FeatheredHat },
			{ "feathered", HatSubType.FeatheredHat },
			{ "featheredhat", HatSubType.FeatheredHat },
			{ "fedora", HatSubType.FedoraHat },
			{ "fedorahat", HatSubType.FedoraHat },
			{ "hard", HatSubType.HardHat },
			{ "hardhat", HatSubType.HardHat },
			{ "ranger", HatSubType.RangerHat },
			{ "rangerhat", HatSubType.RangerHat },
			{ "spikey", HatSubType.SpikeyHat },
			{ "spikeyhat", HatSubType.SpikeyHat },
			{ "top", HatSubType.TopHat },
			{ "tophat", HatSubType.TopHat },
		};

		private static void CheatCodeHead( string[] ins ) {

			// If "head" is the only instruction, give a random head to the character.
			if(ins.Length <= 1) {
				Head.AssignToCharacter(Systems.localServer.MyCharacter, (byte) HeadSubType.RandomStandard, true);
				return;
			}

			// Get the Head Type by instruction:
			if(headCodes.ContainsKey(ins[1])) {
				HeadSubType subType = headCodes[ins[1]];
				Head.AssignToCharacter(Systems.localServer.MyCharacter, (byte) subType, true);
			}
		}

		private static readonly Dictionary<string, HeadSubType> headCodes = new Dictionary<string, HeadSubType>() {

			// Random Options
			{ "any", HeadSubType.RandomStandard },
			{ "rand", HeadSubType.RandomStandard },
			{ "random", HeadSubType.RandomStandard },
			{ "randomhead", HeadSubType.RandomStandard },

			// Standard Heads
			{ "ryu", HeadSubType.RyuHead },
		};

		private static bool GetBoolArg(string arg) {
			if(arg == "true" || arg == "1" || arg == "on") { return true; }
			return false;
		}

		private static int GetIntArg(string arg) {
			int intVal;
			if(!Int32.TryParse(arg, out intVal)) { intVal = 0; }
			return intVal;
		}

		private static float GetFloatArg(string arg) {
			float floatVal;
			if(!float.TryParse(arg, out floatVal)) { floatVal = 0; }
			return floatVal;
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
