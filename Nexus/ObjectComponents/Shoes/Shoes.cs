using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public enum ShoeSubType : byte {
		None = 0,
		Dashing = 1,
		Spike = 2,
		Wing = 3,
	}

	// Shoes replace the X Button (Run) with a Dashing Power. It may also grant additional powers, like wall climb.
	// The Dashing Power takes 30+ frames to regenerate, and you must touch ground before you can use it again.
	// Dash can be affected by the direction you're facing, your location (wall, ground, air), etc.
	// This may complicate the ability to hold items since item holding uses the X Button. It shouldn't prevent it, though.
	public class Shoes {

		// Constants
		public const byte cooldown = 20;	// # of frames AFTER the duration of the action has expired.

		// References
		protected readonly Character character;

		public byte subType; //The subtype of the power (e.g. "Axe", "Sword", "Slam", "Phase", etc)
		public string IconTexture { get; protected set; }	// The texture path for the Power Icon (e.g. "Power/" + this.pool)
		public string subStr { get; protected set; }    // Used for console. The texture path for the Power Icon (e.g. "Power/" + this.pool)

		// Mechanics
		public byte duration;			// # of frames the effect lasts. Will also end on touch (wall or ground).
		protected int lastHeld;			// The last frame that you triggered / activated a dash.
		protected bool dashReset;		// TRUE if you've touched the ground (or wall) since your last dash.

		public Shoes( Character character ) {
			this.duration = 8;
			this.character = character;
			this.lastHeld = 0;
			this.dashReset = true;
		}

		public static void AssignShoe(Character character, byte subType) {

			// Prepare Shoe
			switch(subType) {
				case (byte) ShoeSubType.Dashing: character.shoes = new DashingShoes(character); break;
				case (byte) ShoeSubType.Spike: character.shoes = new SpikeShoes(character); break;
				case (byte) ShoeSubType.Wing: character.shoes = new WingShoes(character); break;
				default: character.shoes = null; break;
			}

			// Reset Character Stats
			character.stats.ResetCharacterStats();
		}

		public void RemoveShoes(Character character) {
			character.shoes = null;
		}

		public void SetLastHeld(int frame) {
			this.lastHeld = frame;
		}

		public virtual void UpdateCharacterStats(Character character) { }

		public void ActivateDash() {

			// Verify Dash can be activated.
			if(!this.CanActivate()) { return; }

			// Set Trackers for Re-Activation
			this.lastHeld = Systems.timer.Frame;
			this.dashReset = false;

			// Prepare Direction
			sbyte directionHor = 0;
			sbyte directionVert = 0;

			// Character Input determines which way the air burst effect occurs.
			PlayerInput input = this.character.input;

			if(input.isDown(IKey.Up)) {
				directionVert = -1;
			} else if(input.isDown(IKey.Down)) {
				directionVert = 1;
			}

			if(input.isDown(IKey.Left)) {
				directionHor = -1;
			} else if(input.isDown(IKey.Right)) {
				directionHor = 1;
			}

			// Default to Upward Direction
			if(directionHor == 0 && directionVert == 0) {
				directionVert = -1;
			}

			// If the character is standing on ground, it interferes with actions; fix that.
			if(this.character.physics.touch.toBottom) {
				this.character.physics.touch.ResetTouch();
				this.character.physics.MoveToPosY(this.character.posY - 1);
			}

			// Trigger the Air Burst Action
			ActionMap.Dash.StartAction(this.character, directionHor, directionVert);

			// Play the "Air" sound.
			this.character.room.PlaySound(Systems.sounds.air, 0.2f, this.character.posX + 16, this.character.posY + 16);
		}

		public bool CanActivate() {

			// Cannot activate this power while holding an item:
			if(this.character.heldItem.IsHeld) { return false; }

			// Cannot activate if it's already active.
			if(this.character.status.action is DashAction) { return false; }

			// Cannot activate if you haven't reset the dash with a ground or wall touch.
			if(!this.dashReset) { return false; }

			// Delay if the cooldown hasn't passed yet.
			if(Systems.timer.Frame < this.lastHeld + Shoes.cooldown) { return false; }

			return true;
		}

		public void TouchWall() {
			this.dashReset = true;
		}
	}
}
