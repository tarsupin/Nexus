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

		// Stats & Statuses
		public readonly CharacterStats stats;
		public readonly CharacterStatus status;
		public readonly CharacterWounds wounds;

		// Equipment & Powers
		public Suit suit;
		public Hat hat;
		public PowerAttack attackPower;
		public PowerMobility mobilityPower;

		public Character(LevelScene scene, byte subType, FVector pos, object[] paramList) : base(scene, subType, pos, paramList) {
			this.Meta = Systems.mapper.MetaList[MetaGroup.Character];
			this.SpriteName = "Moosh/Brown/Left2";

			// Physics, Collisions, etc.
			this.AssignBounds(8, 12, 28, 44);
			this.physics = new Physics(this);
			this.physics.SetGravity(FInt.Create(0.7));
			this.impact = new Impact(this);

			// Default Stats & Statuses
			this.stats = new CharacterStats(this);
			this.status = new CharacterStatus();
			this.wounds = new CharacterWounds(this, Systems.timer);

			// Images and Animations
			this.animate = new Animate(this, "Moosh/Brown/");

			// TODO CLEANUP: Remove
			this.stats.CanWallSlide = true;
			this.stats.CanWallJump = true;
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

		public override void RunTick() {

			// Ground Movement & Actions
			if(this.physics.touch.toFloor) { this.OnFloorUpdate(); }
			
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
			FInt speedMult = (this.input.isDown(IKey.XButton) ? (FInt) 1 : this.stats.SlowSpeedMult);
			FInt maxSpeed = this.stats.RunMaxSpeed * speedMult;
				// TODO LOW PRIORITY: add * status.friction

			// Movement Right
			if(this.input.isDown(IKey.Right)) {
				this.faceRight = true;

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
				this.faceRight = false;

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
						if(SlideAction.IsAbleToSlide(this, this.faceRight)) {
							ActionMap.Slide.StartAction(this, this.faceRight);
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
					if(status.action is ActionCharacter) { status.action.LandsOnGround(this);  }
					status.jumpsUsed = 0;
				}
			}
		}

		private void InAirUpdate() {

			// NOTE: Character momentum is allowed to exceed run speed, but not by character's own force.

			// Character Movement & Handling
			FInt speedMult = (this.input.isDown(IKey.XButton) ? (FInt)1 : this.stats.SlowSpeedMult);
			FInt maxSpeed = this.stats.RunMaxSpeed * speedMult;

			// Movement Right
			if(this.input.isDown(IKey.Right)) {
				this.faceRight = true;

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
				this.faceRight = false;

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
				if(this.status.leaveWall >= Systems.timer.frame) {
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
			int velX = this.physics.velocity.X.IntValue;
			bool heldItem = false;
			string suitType = "WhiteNinja/";        // this.suit.type + "/"

			// Ground Movement & Actions
			if(this.physics.touch.toFloor) {

				// If Moving Right
				if(velX > 0) {

					// If Facing Left
					if(!this.faceRight) {
						this.SetSpriteName(suitType + "TurnLeft");
						//if(heldItem) { xShift = 74; }
					}

					// If Facing Right
					else {
						if(heldItem) { this.SpriteName = suitType + "RunHold"; } else if(velX > 5) { this.animate.SetAnimation(suitType, AnimCycleMap.CharacterRunRight, 8, 1); } else { this.animate.SetAnimation(suitType, AnimCycleMap.CharacterWalkRight, 11); }
					}
				}

				// If Moving Left
				else if(velX < 0) {

					// If Facing Right
					if(this.faceRight) {
						this.SetSpriteName(suitType + "Turn");
						//if(heldItem) { xShift = -14; }
					}

					// If Facing Left
					else {
						if(heldItem) { this.SpriteName = suitType + "RunHoldLeft"; } else if(velX < -5) { this.animate.SetAnimation(suitType, AnimCycleMap.CharacterRunLeft, 8, 1); } else { this.animate.SetAnimation(suitType, AnimCycleMap.CharacterWalkLeft, 11); }
					}
				}

				// If Not Moving
				else {
					this.SetSpriteName(suitType + "Stand" + (heldItem ? "Hold" : "") + (this.faceRight ? "" : "Left"));
				}
			}

			// Air Movement & Updates
			else {

				// If Holding Item
				if(heldItem) {
					this.SetSpriteName(suitType + "RunHold" + (this.faceRight ? "" : "Left"));
				}

				// Falling
				else if(this.physics.velocity.Y.IntValue > 3) {
					this.SetSpriteName(suitType + "Fall" + (this.faceRight ? "" : "Left"));
				}

				// Jumping (Moving Up)
				else {
					this.SetSpriteName(suitType + "Jump" + (this.faceRight ? "" : "Left"));
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

		private void RestrictWorldSides() {

			// Left Bounds
			if(this.posX < 0) {
				this.physics.MoveToPosX(0);
				this.physics.StopX();
			}

			// Right Bounds
			else if(this.posX + this.bounds.Right >= this.scene.tilemap.Width) {
				this.physics.MoveToPosX(this.scene.tilemap.Width - this.bounds.Right);
				this.physics.StopX();
			}
		}

		private void RestrictWorldTop() {
			if(this.posY < 0) {
				this.physics.MoveToPosY(0);
				this.physics.StopY();
			}
		}

		private void CheckFallOfWorld() {

			// If the Character falls off the world edge, die.
			if(this.posY > this.scene.tilemap.Height) {
				this.wounds.Death();
			}
		}
	}
}
