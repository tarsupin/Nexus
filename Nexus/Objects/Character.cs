using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System;

// NOTES:
// Must be able to have multiple characters in the level.
// Characters must be controlled by their player.

namespace Nexus.Objects {

	public class Character : DynamicGameObject {

		// References
		public Player player;       // The player that controls this character.
		public PlayerInput input;   // The player's input class.

		// Stats & Statuses
		public readonly CharacterStats stats;
		public readonly CharacterStatus status;
		public readonly CharacterWounds wounds;

		// Equipment & Powers
		public Suit suit;
		public Hat hat;
		public PowerAttack attackPower;
		public PowerMobility mobilityPower;

		// Miscellaneous
		public bool faceRight;		// TRUE if the character is facing right.

		public Character(LevelScene scene, byte subType, FVector pos, object[] paramList) : base(scene, subType, pos, paramList) {
			this.Meta = scene.mapper.MetaList[MetaGroup.Character];
			this.SpriteName = "Moosh/Brown/Left2";

			// Physics, Collisions, etc.
			this.AssignBounds(8, 12, 28, 44);
			this.physics = new Physics(this);
			this.physics.SetGravity(FInt.FromParts(0, 350));

			// Default Stats & Statuses
			this.stats = new CharacterStats(this);
			this.status = new CharacterStatus(this);
			this.wounds = new CharacterWounds(this, scene.timer);
		}

		public void AssignPlayer( Player player ) {
			this.player = player;
			this.input = player.input;
		}

		public ActionMap ActionMap { get { return this.scene.mapper.actions; }	}

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

		public override void RunTick() {

			// Ground Movement & Actions
			if(this.physics.touch.toBottom) { this.OnFloorUpdate(); }
			
			// In Air Update
			else { this.InAirUpdate(); }

			// Process Behaviors & Actions
			if(this.status.action is ActionCharacter) {
				this.status.action.RunAction(this);
			}

			// Activate Powers
			if(this.input.isDown(IKey.R1)) {

				// If the character is holding an item, cannot activate powers.
				// However, the item may be possible to activate.

				// TODO HIGH PRIORITY: Allow activating items:
				//if(this.item is Item) {
				//	if(this.input.isPressed(IKey.R1)) { this.item.ActivateWhileHeld(this);  }
				//} else if(this.attackPower) {
					this.attackPower.Activate();
				//}
			}

			// Activate Mobility Powers
			if(this.input.isDown(IKey.BButton) && this.mobilityPower is PowerMobility) {
				this.mobilityPower.Activate();
			}

			// Update Animations
			// TODO HIGH PRIORITY: Animation
			// this.animation.run(timer);

			// Restrict to World Bounds (except below, for falling deaths)
			this.RestrictWorldSides();
			this.RestrictWorldTop();
			this.CheckFallOfWorld();
			
			// Run Physics
			base.RunTick();
		}

		private void OnFloorUpdate() {

			// Character Movement & Handling
			FInt velX = this.physics.velocity.X;
			FInt speedMult = (this.input.isDown(IKey.XButton) ? (FInt) 1 : this.stats.SlowSpeedMult);
			FInt maxSpeed = this.stats.RunMaxSpeed * speedMult;
				// TODO LOW PRIORITY: add * status.friction

			// Movement Right
			if(this.input.isDown(IKey.Right)) {
				this.faceRight = true;

				// Move Right up to maximum speed. (Must use FInt Math, not Math.Min)
				if(maxSpeed > velX) {
					this.physics.velocity.X += this.stats.RunAcceleration * speedMult;
					if(this.physics.velocity.X > maxSpeed) { this.physics.velocity.X = maxSpeed; }
				}

				// If there's too much momentum, decelerate to normal speed:
				else {
					this.DecelerateChar(this.stats.RunDeceleration, 2 - speedMult, FInt.FromParts(0, 250));
				}
			}

			// Movement Left
			else if(this.input.isDown(IKey.Left)) {
				this.faceRight = true;

				// Move Left up to maximum speed. (Must use FInt Math, not Math.Min)
				if(velX > maxSpeed.Inverse) {
					this.physics.velocity.X -= this.stats.RunAcceleration * speedMult;
					if(this.physics.velocity.X < maxSpeed.Inverse) { this.physics.velocity.X = maxSpeed.Inverse; }
				}

				// If there's too much momentum, decelerate to normal speed:
				else {
					this.DecelerateChar(this.stats.RunDeceleration, 2 - speedMult, FInt.FromParts(0, 250));
				}
			}

			// Resting Friction (No Intentional Movement)
			else {
				this.DecelerateChar(this.stats.RunDeceleration, 2 - speedMult, FInt.FromParts(0, 250));
			}

			// As long as we're not currently sliding:
			if(status.action is SlideAction == false) {

				// JUMP Button Pressed
				if(input.isPressed(IKey.AButton)) {

					// JUMP+DOWN (Slide or Platform Drop) is Activated
					if(input.isDown(IKey.Down)) {

						// TODO HIGH PRIORITY: Platform Down Jump
						// If on a Platform, perform Down-Jump
						//if(this.physics.touch.touchObj is Platform) {
						//	status.dropdown = this.scene.timer.frame + 6;
						//}

						//// Slide, if able:
						//else
						if(SlideAction.IsAbleToSlide(this, this.faceRight)) {
							this.ActionMap.Slide.StartAction(this, this.faceRight);
						}

						// Otherwise, JUMP:
						else if(status.jumpsUsed == 0) { // Prevents immediate re-jump on landing.
							this.ActionMap.Jump.StartAction(this);
						}
					}

					// JUMP
					else if(status.jumpsUsed == 0) { // Prevents immediate re-jump on landing.
						this.ActionMap.Jump.StartAction(this);
					}
				}

				// Reset Actions on Land
				else {
					if(status.action is ActionCharacter) { status.action.LandsOnGround(this);  }
					status.jumpsUsed = 0;
				}
			}
		}

		private void InAirUpdate() {

			// NOTE: Character momentum is allowed to exceed run speed, but not by character's own force.

			// Character Movement & Handling
			FInt velX = this.physics.velocity.X;
			FInt speedMult = (this.input.isDown(IKey.XButton) ? (FInt)1 : this.stats.SlowSpeedMult);
			FInt maxSpeed = this.stats.RunMaxSpeed * speedMult;

			// Movement Right
			if(this.input.isDown(IKey.Right)) {
				this.faceRight = true;

				// Move Right up to maximum speed. (Must use FInt Math, not Math.Min)
				if(maxSpeed > velX) {
					this.physics.velocity.X += this.stats.AirAcceleration * speedMult;
					if(this.physics.velocity.X > maxSpeed) { this.physics.velocity.X = maxSpeed; }
				}

				// If there's too much momentum, decelerate to normal speed:
				else {
					this.DecelerateChar(this.stats.AirDeceleration, 2 - speedMult, FInt.FromParts(0, 65));	// Roughly 1/16
				}
			}

			// Movement Left
			else if(this.input.isDown(IKey.Left)) {
				this.faceRight = true;

				// Move Left up to maximum speed.(Must use FInt Math, not Math.Min)
				if(maxSpeed.Inverse < velX) {
					this.physics.velocity.X -= this.stats.AirAcceleration * speedMult;
					if(this.physics.velocity.X < maxSpeed.Inverse) { this.physics.velocity.X = maxSpeed.Inverse; }
				}

				// If there's too much momentum, decelerate to normal speed:
				else {
					this.DecelerateChar(this.stats.AirDeceleration, 2 - speedMult, FInt.FromParts(0, 65)); // Roughly 1/16
				}
			}

			// Resting Friction (No Intentional Movement)
			else {
				this.DecelerateChar(this.stats.AirDeceleration, 2 - speedMult, (FInt) 1);
			}

			// TODO HIGH PRIORITY: AIR JUMP
			// TODO HIGH PRIORITY: AIR JUMP
			// TODO HIGH PRIORITY: AIR JUMP
			// TODO HIGH PRIORITY: AIR JUMP
		}

		private void DecelerateChar( FInt decelStat, FInt speedMult, FInt deceleration ) {
			FInt delta = decelStat * speedMult * deceleration;

			if(this.physics.velocity.X < 0) {
				this.physics.velocity.X -= delta;
				if(this.physics.velocity.X > 0) { this.physics.velocity.X = (FInt) 0; }
			} else {
				this.physics.velocity.X += delta;
				if(this.physics.velocity.X < 0) { this.physics.velocity.X = (FInt)0; }
			}
		}

		private void RestrictWorldSides() {

			// Left Bounds
			if(this.pos.X < 0) {
				this.physics.MoveToPosX((FInt)0);
				this.physics.StopX();
			}

			// Right Bounds
			else if(this.pos.X + this.bounds.Right >= this.scene.tilemap.width) {
				this.physics.MoveToPosX((FInt) this.scene.tilemap.width - this.bounds.Right);
				this.physics.StopX();
			}
		}

		private void RestrictWorldTop() {
			if(this.pos.Y < 0) {
				this.physics.MoveToPosY((FInt) 0);
				this.physics.StopY();
			}
		}

		private void CheckFallOfWorld() {

			// If the Character falls off the world edge, die.
			if(this.pos.Y > this.scene.tilemap.height) {
				this.wounds.Death();
			}
		}
	}
}
