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
using static Nexus.Objects.CollectableHat;
using static Nexus.Objects.CollectableSuit;

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

			// Run Movement
			else if(ins[0] == "teleport") { Console.CheatCodeTeleport(ins); }

			// Debug
			else if(ins[0] == "speed") { Console.DebugSpeed(ins); }
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
				x = Int32.Parse(ins[2]) * (byte) TilemapEnum.TileWidth;
				y = Int32.Parse(ins[3]) * (byte) TilemapEnum.TileHeight;
			} else {
				x = Int32.Parse(ins[1]) * (byte) TilemapEnum.TileWidth;
				y = Int32.Parse(ins[2]) * (byte) TilemapEnum.TileHeight;
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

		public void Draw() {

			// Draw Console Background
			Texture2D rect = new Texture2D(Systems.graphics.GraphicsDevice, 1, 1);
			rect.SetData(new[] { Color.Black });
			Systems.spriteBatch.Draw(rect, new Rectangle(0, Systems.screen.windowHeight - 100, Systems.screen.windowWidth, Systems.screen.windowHeight), Color.Black * 0.85f);

			Systems.fonts.console.Draw("> " + consoleText + (Systems.timer.tick20Modulus < 10 ? "|" : ""), 10, Systems.screen.windowHeight - 90, Color.White);
		}
	}
}
