﻿using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System.Collections.Generic;

// NOTES:
// Must be able to have multiple characters in the level.
// Characters must be controlled by their player.

namespace Nexus.Objects {

	public class Character : GameObject {

		// References
		public Player player;       // The player that controls this character.
		public PlayerInput input;   // The player's input class.

		// Stats & Statuses
		public readonly CharacterStats stats;
		public readonly CharacterStatus status;
		public readonly CharacterWounds wounds;

		// Equipment & Powers
		public Suit suit;
		public Head head;
		public Hat hat;
		public PowerAttack attackPower;
		public PowerMobility mobilityPower;

		// Attachments
		public TrailingKeys trailKeys;
		public HeldItem heldItem;
		public MagiShield magiShield;
		public Nameplate nameplate;

		// Survival
		public uint deathFrame = 0;		// The frame that the character died.

		public Character(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.ObjectMetaData[(byte)ObjectEnum.Character].meta;
			this.SetSpriteName("Stand");

			// Physics, Collisions, etc.
			this.AssignBounds(8, 12, 28, 44);
			this.physics = new Physics(this);
			this.physics.SetGravity(FInt.Create(0.7));

			// Default Stats & Statuses
			this.stats = new CharacterStats(this);
			this.status = new CharacterStatus();
			this.wounds = new CharacterWounds(this, Systems.timer);

			// Attachments
			this.trailKeys = new TrailingKeys(this);
			this.heldItem = new HeldItem(this);
			this.magiShield = new MagiShield(this);
			this.nameplate = new Nameplate(this, "Ryu", false, false);

			// Images and Animations
			this.animate = new Animate(this, "/");

			// Reset Character, Set Default Values
			this.ResetCharacter();
		}

		public void AssignPlayer( Player player ) {
			this.player = player;
			this.input = player.input;
		}

		public void ResetCharacter() {

			// TODO HIGH PRIORITY: Character Archetype
			// Character Archetype

			// Status Reset (NOT "STATS")
			this.status.ResetCharacterStatus();

			// TODO HIGH PRIORITY: Add item, attachments

			// Item Handling
			//if(this.item is Item) { this.item.DropItem(); }		// Multiplayer must drop. Single player will reset level.

			// Equipment
			if(this.hat is Hat) { this.hat.DestroyHat(this, false); };

			// Default Suit, Default Head
			Suit.AssignToCharacter(this, (byte) SuitSubType.RedBasic, false);
			HeadMap.RyuHead.ApplyHead(this, false);

			// Reset Stats (NOT "STATUS")
			this.stats.ResetCharacterStats();

			// Reset Wounds + Survival
			this.wounds.WoundsDeathReset();
			this.deathFrame = 0;

			// Attachments
			this.trailKeys.ResetTrailingKeys();
			this.heldItem.ResetHeldItem();

			// Reset Physics to ensure it doesn't maintain knowledge from previous state.
			this.physics.touch.ResetTouch();
		}

		// Disable Suit, Hat, Powers
		public bool DisableAbilities() {
			bool disable = false; // We track if anything was disabled because certain sound effects require it.

			if(this.hat is Hat && this.hat.IsPowerHat) { this.hat.DestroyHat(this, false); disable = true; }
			if(this.suit is Suit && this.suit.IsPowerSuit) { this.suit.DestroySuit(this, false); disable = true; }
			if(this.attackPower is PowerAttack) { this.attackPower.EndPower(); disable = true; }
			if(this.mobilityPower is PowerMobility) { this.mobilityPower.EndPower(); disable = true; }

			if(disable) {

				// Reset Character Abilities (since we set each individual destroy function to false)
				this.stats.ResetCharacterStats();
			}

			return disable;
		}

		public override void RunTick() {

			// Ground Movement & Actions
			if(this.physics.touch.toFloor == 0) { this.OnFloorUpdate(); }
			
			// In Air Update
			else { this.InAirUpdate(); }

			// Process Behaviors & Actions
			if(this.status.action is Action) {
				this.status.action.RunAction(this);
			}

			// Activate Powers
			if(this.input.isDown(IKey.R1)) {

				// If the character is holding an item, cannot activate powers.
				// However, the item may be possible to activate.

				// TODO HIGH PRIORITY: Allow activating items:
				//if(this.item is Item) {
				//	if(this.input.isPressed(IKey.R1)) { this.item.ActivateWhileHeld(this);  }
				//}

				if(this.attackPower is Power) {
					this.attackPower.Activate();
				}
			}

			// Activate Mobility Powers
			if(this.input.isDown(IKey.BButton) && this.mobilityPower is PowerMobility) {
				this.mobilityPower.Activate();
			}

			// Restrict to World Bounds (except below, for falling deaths)
			this.CheckFallOfWorld();

			// Run Physics
			base.RunTick();

			// Update Attachments
			this.trailKeys.RunKeyTick();
			this.heldItem.RunHeldItemTick();
		}

		private void OnFloorUpdate() {

			// Character Movement & Handling
			FInt speedMult = (this.input.isDown(IKey.XButton) ? (FInt) 1 : this.stats.SlowSpeedMult);
			FInt maxSpeed = this.stats.RunMaxSpeed * speedMult;
				// TODO LOW PRIORITY: add * status.friction

			// Movement Right
			if(this.input.isDown(IKey.Right)) {
				this.FaceRight = true;

				// Move Right up to maximum speed. (Must use FInt Math, not Math.Min)
				if(maxSpeed > this.physics.velocity.X) {
					this.physics.velocity.X += this.stats.RunAcceleration * speedMult;
					if(this.physics.velocity.X > maxSpeed) { this.physics.velocity.X = maxSpeed; }
				}

				// If there's too much momentum, decelerate to normal speed:
				else {
					this.DecelerateChar(this.stats.RunDeceleration, 2 - speedMult, FInt.Create(0.25));
				}
			}

			// Movement Left
			else if(this.input.isDown(IKey.Left)) {
				this.FaceRight = false;

				// Move Left up to maximum speed. (Must use FInt Math, not Math.Min)
				if(this.physics.velocity.X > maxSpeed.Inverse) {
					this.physics.velocity.X -= this.stats.RunAcceleration * speedMult;
					if(this.physics.velocity.X < maxSpeed.Inverse) { this.physics.velocity.X = maxSpeed.Inverse; }
				}

				// If there's too much momentum, decelerate to normal speed:
				else {
					this.DecelerateChar(this.stats.RunDeceleration, 2 - speedMult, FInt.Create(0.25));
				}
			}

			// Resting Friction (No Intentional Movement)
			else {
				this.DecelerateChar(this.stats.RunDeceleration, 2 - speedMult, FInt.Create(2));
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
						if(SlideAction.IsAbleToSlide(this, this.FaceRight)) {
							ActionMap.Slide.StartAction(this, this.FaceRight);
						}

						// Otherwise, JUMP:
						else if(status.jumpsUsed == 0) { // Prevents immediate re-jump on landing.
							ActionMap.Jump.StartAction(this);
						}
					}

					// JUMP
					else if(status.jumpsUsed == 0) { // Prevents immediate re-jump on landing.
						ActionMap.Jump.StartAction(this);
					}
				}

				// Reset Actions on Land
				else if(this.physics.touch.toBottom) {

					// Only apply landing if the minimum jump time has not passed.
					if(status.action is JumpAction == false || JumpAction.MinimumTimePassed(status)) {
						if(status.action is Action) { status.action.LandsOnGround(this); }
						status.jumpsUsed = 0;
					}

				}
			}
		}

		private void InAirUpdate() {

			// NOTE: Character momentum is allowed to exceed run speed, but not by character's own force.

			// Character Movement & Handling
			FInt speedMult = (this.input.isDown(IKey.XButton) ? (FInt)1 : this.stats.SlowSpeedMult);
			FInt maxSpeed = this.stats.RunMaxSpeed * speedMult;

			// Maximum Fall Speed
			if(this.physics.velocity.Y > 14) { this.physics.velocity.Y = FInt.Create(14); }

			// Movement Right
			if(this.input.isDown(IKey.Right)) {
				this.FaceRight = true;

				// Move Right up to maximum speed. (Must use FInt Math, not Math.Min)
				if(maxSpeed > this.physics.velocity.X) {
					this.physics.velocity.X += this.stats.AirAcceleration * speedMult;
					if(this.physics.velocity.X > maxSpeed) { this.physics.velocity.X = maxSpeed; }
				}

				// If there's too much momentum, decelerate to normal speed:
				else {
					this.DecelerateChar(this.stats.AirDeceleration, 2 - speedMult, FInt.Create(0.065));	// Roughly 1/16
				}
			}

			// Movement Left
			else if(this.input.isDown(IKey.Left)) {
				this.FaceRight = false;

				// Move Left up to maximum speed.(Must use FInt Math, not Math.Min)
				if(maxSpeed.Inverse < this.physics.velocity.X) {
					this.physics.velocity.X -= this.stats.AirAcceleration * speedMult;
					if(this.physics.velocity.X < maxSpeed.Inverse) { this.physics.velocity.X = maxSpeed.Inverse; }
				}

				// If there's too much momentum, decelerate to normal speed:
				else {
					this.DecelerateChar(this.stats.AirDeceleration, 2 - speedMult, FInt.Create(0.065)); // Roughly 1/16
				}
			}

			// Resting Friction (No Intentional Movement)
			else {
				this.DecelerateChar(this.stats.AirDeceleration, 2 - speedMult, (FInt) 1);
			}

			// Attempted Air Jump
			if(this.input.isPressed(IKey.AButton)) {

				// Delayed Wall Jump
				// Creates a smoother wall jump experience by giving a little leeway after leaving the wall.
				if(this.status.leaveWall >= Systems.timer.Frame) {
					ActionMap.WallJump.StartAction(this, this.status.grabDir);

					// Double Jump
				} else if(this.stats.JumpMaxTimes > 1 && this.status.jumpsUsed < this.stats.JumpMaxTimes) {
					ActionMap.Jump.StartAction(this);
				}
			}
		}

		// Update Animations and Sprite Changes for Characters.
		// This is done after collisions, since collisions have critical impacts on how Characters are drawn.
		public void SpriteTick() {

			// Animations & Drawing
			int velX = this.physics.velocity.X.RoundInt;
			bool heldItem = this.heldItem.IsHeld;

			// Ground Movement & Actions
			if(this.physics.touch.toFloor == 0) {

				// If Moving Right
				if(velX > 0) {

					// If Facing Left
					if(!this.FaceRight) {
						this.SetSpriteName("TurnLeft");
						//if(heldItem) { xShift = 74; }
					}

					// If Facing Right
					else {
						if(heldItem) {
							if(velX > 5) { this.animate.SetAnimation("", AnimCycleMap.CharacterMoveHoldRight, 8, 1); }
							else { this.animate.SetAnimation("", AnimCycleMap.CharacterMoveHoldRight, 11); }
						}
						else if(velX > 5) { this.animate.SetAnimation("", AnimCycleMap.CharacterRunRight, 8, 1); }
						else { this.animate.SetAnimation("", AnimCycleMap.CharacterWalkRight, 11); }
					}
				}

				// If Moving Left
				else if(velX < 0) {

					// If Facing Right
					if(this.FaceRight) {
						this.SetSpriteName("Turn");
						//if(heldItem) { xShift = -14; }
					}

					// If Facing Left
					else {
						if(heldItem) {
							if(velX > 5) { this.animate.SetAnimation("", AnimCycleMap.CharacterMoveHoldLeft, 8, 1); }
							else { this.animate.SetAnimation("", AnimCycleMap.CharacterMoveHoldLeft, 11); }
						}
						else if(velX < -5) { this.animate.SetAnimation("", AnimCycleMap.CharacterRunLeft, 8, 1); }
						else { this.animate.SetAnimation("", AnimCycleMap.CharacterWalkLeft, 11); }
					}
				}

				// If Not Moving
				else {
					this.SetSpriteName("Stand" + (heldItem ? "Hold" : "") + (this.FaceRight ? "" : "Left"));
				}
			}

			// Air Movement & Updates
			else {

				// If Holding Item
				if(heldItem) {
					this.SetSpriteName("RunHold" + (this.FaceRight ? "" : "Left"));
				}

				// Falling
				else if(this.physics.velocity.Y.RoundInt > 3) {
					this.SetSpriteName("Fall" + (this.FaceRight ? "" : "Left"));
				}

				// Jumping (Moving Up)
				else {
					this.SetSpriteName("Jump" + (this.FaceRight ? "" : "Left"));
				}
			}
		}

		private void DecelerateChar( FInt decelStat, FInt speedMult, FInt deceleration ) {
			FInt delta = decelStat * speedMult * deceleration;

			if(this.physics.velocity.X < 0) {
				this.physics.velocity.X -= delta;
				if(this.physics.velocity.X > 0) { this.physics.velocity.X = (FInt) 0; }
			} else {
				this.physics.velocity.X += delta;
				if(this.physics.velocity.X < 0) { this.physics.velocity.X = (FInt) 0; }
			}
		}

		public override void Draw(int camX, int camY) {

			// Render Attachments (Behind Character)
			this.nameplate.Draw(camX, camY);
			this.trailKeys.Draw(camX, camY);

			// Handle Invincibility Coloration, if applicable:
			// TODO: Invincibility Coloration

			// Draw Character's Body
			this.suit.Draw(this.SpriteName, posX, posY, camX, camY);

			// Draw Character's Head and Hat
			this.head.Draw(this.FaceRight, posX, posY, camX, camY);
			if(this.hat is Hat) { this.hat.Draw(this.FaceRight, posX, posY, camX, camY); }
		}

		private void CheckFallOfWorld() {

			// If the Character falls off the world edge, die.
			if(this.posY - this.bounds.Top > this.room.tilemap.Height) {
				this.wounds.Death();
			}
		}

		public static void Teleport( Character character, int posX, int posY ) {
			character.physics.MoveToPos(posX, posY);
			character.physics.MoveToPos(posX, posY); // Second one is to reset the 'lastPos' value, to avoid 'moving' this frame.
		}
	}
}
