using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;

// NOTES:
// Must be able to have multiple characters in the level.
// Characters must be controlled by their player.

namespace Nexus.Objects {

	public class Character : DynamicGameObject {

		// References
		public Player player;       // The player that controls this character.
		public PlayerInput input;   // The player's input class.

		private readonly CharacterStats stats;
		private readonly CharacterStatus status;

		public Character(LevelScene scene, byte subType, FVector pos, object[] paramList) : base(scene, subType, pos, paramList) {
			this.Meta = scene.mapper.MetaList[MetaGroup.EnemyLand];
			this.SpriteName = "Moosh/Brown/Left2";

			// Physics, Collisions, etc.
			this.AssignBounds(8, 12, 28, 44);
			this.physics = new Physics(this);
			this.physics.SetGravity(FInt.FromParts(0, 350));

			// Default Stats & Statuses
			this.stats = new CharacterStats(this);
			this.status = new CharacterStatus(this);
		}

		public void AssignPlayer( Player player ) {
			this.player = player;
			this.input = player.input;
		}

		public void ResetCharacter() {

			// TODO HIGH PRIORITY: Character Archetype
			// Character Archetype
			

			// Status Reset
			this.status.ResetCharacterStatus();

			// TODO HIGH PRIORITY: Add item, suit, hat, attachments
			
			// Item Handling
			//if(this.item is Item) { this.item.DropItem(); }		// Multiplayer must drop. Single player will reset level.
			
			// Equipment
			//this.Suit.ResetSuit();
			//if(this.Hat is Hat) { this.Hat.ResetHat(); };

			this.stats.ResetCharacterStats();

			// Attachments
			// this.attachments.Reset();		// ???? TODO

			// Reset Physics to ensure it doesn't maintain knowledge from previous state.
			this.physics.touch.ResetTouch();
		}

		// Disable Suit, Hat, Powers
		public bool DisableAbilities() {
			bool disable = false; // We track if anything was disabled because certain sound effects require it.

			// TODO HIGH PRIORITY: Reveal Disable Abilities
			//if(this.Hat && this.Hat.IsProtective) { this.Hat.DestroyHat(); disable = true; }
			//if(this.Suit && this.Suit.IsProtective) { this.Suit.DestroySuit(); disable = true; }
			//if(this.PowerAttack) { this.PowerAttack.EndPower(); disable = true; }
			//if(this.PowerMobility) { this.PowerMobility.EndPower(); disable = true; }

			return disable;
		}

		public new void RunTick() {
			base.RunTick();
		}

	}
}
