using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using Nexus.Objects;
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

			// Run Cheat Codes
			if(ins[0] == "suit") { this.CheatCodeSuit(ins); }
			else if(ins[0] == "hat") { this.CheatCodeHat(ins); }
		}

		private void CheatCodeSuit( string[] ins ) {

			// If "suit" is the only instruction, give a random suit to the character.
			if(ins.Length <= 1) {
				Suit.AssignToCharacter(Systems.localServer.MyCharacter, (byte) SuitSubType.RandomSuit, true);
				return;
			}

			// Get the Suit Type by instruction:
			if(suitCodes.ContainsKey(ins[1])) {
				SuitSubType suitType = suitCodes[ins[1]];
				Suit.AssignToCharacter(Systems.localServer.MyCharacter, (byte) suitType, true);
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
		
		private void CheatCodeHat( string[] ins ) {

			// If "hat" is the only instruction, give a random hat to the character.
			if(ins.Length <= 1) {
				Hat.AssignToCharacter(Systems.localServer.MyCharacter, (byte) HatSubType.RandomHat, true);
				return;
			}

			// Get the Hat Type by instruction:
			if(hatCodes.ContainsKey(ins[1])) {
				HatSubType suitType = hatCodes[ins[1]];
				Hat.AssignToCharacter(Systems.localServer.MyCharacter, (byte) suitType, true);
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

		public void Draw() {

			// Draw Console Background
			Texture2D rect = new Texture2D(Systems.graphics.GraphicsDevice, 1, 1);
			rect.SetData(new[] { Color.Black });
			Systems.spriteBatch.Draw(rect, new Rectangle(0, Systems.screen.windowHeight - 100, Systems.screen.windowWidth, Systems.screen.windowHeight), Color.Black * 0.85f);

			Systems.fonts.console.Draw("> " + consoleText + (Systems.timer.tick20Modulus < 10 ? "|" : ""), 10, Systems.screen.windowHeight - 90, Color.White);
		}
	}
}
