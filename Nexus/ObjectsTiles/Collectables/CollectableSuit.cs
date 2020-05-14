using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

namespace Nexus.Objects {

	class CollectableSuit : Collectable {

		public CollectableSuit() : base() {
			this.CreateTextures();
			this.tileId = (byte)TileEnum.CollectableSuit;
			this.paramSets = new Params[1] { Params.ParamMap["Collectable"] };

			// Helper Texts
			this.titles = new string[12];
			this.titles[(byte)SuitSubType.RandomSuit] = "Random Suit";
			this.titles[(byte)SuitSubType.RandomNinja] = "Random Ninja Suit";
			this.titles[(byte)SuitSubType.RandomWizard] = "Random Wizard Suit";

			this.titles[(byte)SuitSubType.BlackNinja] = "Black Ninja Suit";
			this.titles[(byte)SuitSubType.BlueNinja] = "Blue Ninja Suit";
			this.titles[(byte)SuitSubType.GreenNinja] = "Green Ninja Suit";
			this.titles[(byte)SuitSubType.RedNinja] = "Red Ninja Suit";
			this.titles[(byte)SuitSubType.WhiteNinja] = "White Ninja Suit";

			this.titles[(byte)SuitSubType.BlueWizard] = "Blue Wizard Suit";
			this.titles[(byte)SuitSubType.GreenWizard] = "Green Wizard Suit";
			this.titles[(byte)SuitSubType.RedWizard] = "Red Wizard Suit";
			this.titles[(byte)SuitSubType.WhiteWizard] = "White Wizard Suit";

			this.descriptions = new string[12];
			this.descriptions[(byte)SuitSubType.RandomSuit] = "Grants a random suit.";
			this.descriptions[(byte)SuitSubType.RandomNinja] = "Grants one of the ninja suits at random.";
			this.descriptions[(byte)SuitSubType.RandomWizard] = "Grants one of the wizard suits at random.";

			this.descriptions[(byte)SuitSubType.BlackNinja] = "Can cling to walls and either leap or drop off of them.";
			this.descriptions[(byte)SuitSubType.BlueNinja] = "Gain wall-slide and wall-jumping. Jumps higher.";
			this.descriptions[(byte)SuitSubType.GreenNinja] = "Gain wall-slide and wall-jumping. Moves faster.";
			this.descriptions[(byte)SuitSubType.RedNinja] = "Gain wall-slide and wall-jumping. Gains an additional mid-air jump.";
			this.descriptions[(byte)SuitSubType.WhiteNinja] = "Gain wall-slide and wall-jumping.";

			this.descriptions[(byte)SuitSubType.BlueWizard] = "Can jump higher, casts projectiles faster.";
			this.descriptions[(byte)SuitSubType.GreenWizard] = "Increased movement speed, casts projectiles faster.";
			this.descriptions[(byte)SuitSubType.RedWizard] = "Gains an additional mid-air jump, casts projectiles faster.";
			this.descriptions[(byte)SuitSubType.WhiteWizard] = "Gains a special regenerating shield, casts projectiles faster.";
		}

		public override void Collect(RoomScene room, Character character, ushort gridX, ushort gridY) {

			byte subType = room.tilemap.GetMainSubType(gridX, gridY);
			Suit.AssignToCharacter(character, subType, true);

			Systems.sounds.collectBweep.Play();
			base.Collect(room, character, gridX, gridY);
		}

		private void CreateTextures() {
			this.Texture = new string[12];
			
			this.Texture[(byte)SuitSubType.RandomSuit] = "SuitCollect/RandomSuit";
			this.Texture[(byte)SuitSubType.RandomNinja] = "SuitCollect/RandomNinja";
			this.Texture[(byte)SuitSubType.RandomWizard] = "SuitCollect/RandomWizard";

			this.Texture[(byte)SuitSubType.BlackNinja] = "SuitCollect/BlackNinja";
			this.Texture[(byte)SuitSubType.BlueNinja] = "SuitCollect/BlueNinja";
			this.Texture[(byte)SuitSubType.GreenNinja] = "SuitCollect/GreenNinja";
			this.Texture[(byte)SuitSubType.RedNinja] = "SuitCollect/RedNinja";
			this.Texture[(byte)SuitSubType.WhiteNinja] = "SuitCollect/WhiteNinja";

			this.Texture[(byte)SuitSubType.BlueWizard] = "SuitCollect/BlueWizard";
			this.Texture[(byte)SuitSubType.GreenWizard] = "SuitCollect/GreenWizard";
			this.Texture[(byte)SuitSubType.RedWizard] = "SuitCollect/RedWizard";
			this.Texture[(byte)SuitSubType.WhiteWizard] = "SuitCollect/WhiteWizard";
		}
	}
}
