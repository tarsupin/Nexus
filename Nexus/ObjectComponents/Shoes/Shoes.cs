using Nexus.Engine;
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
		public const byte duration = 30;	// # of frames the effect lasts. Will also end on touch (wall or ground).

		// References
		protected readonly Character character;

		public byte subType; //The subtype of the power (e.g. "Axe", "Sword", "Slam", "Phase", etc)
		public string IconTexture { get; protected set; }	// The texture path for the Power Icon (e.g. "Power/" + this.pool)
		public string subStr { get; protected set; }    // Used for console. The texture path for the Power Icon (e.g. "Power/" + this.pool)

		// Mechanics
		protected int lastTrigger;		// The last frame that you triggered / activated a dash.
		protected int lastHeld;			// The last frame that you were holding an active dash.
		protected bool dashReset;		// TRUE if you've touched the ground (or wall) since your last dash.
		protected bool isActive;

		public Shoes( Character character ) {
			this.character = character;
			this.lastTrigger = 0;
			this.lastHeld = 0;
			this.isActive = false;
			this.dashReset = true;
		}

		public static void AssignShoe(Character character, byte subType) {
			switch(subType) {
				case (byte) ShoeSubType.Dashing: character.shoes = new DashingShoes(character); break;
				case (byte) ShoeSubType.Spike: character.shoes = new SpikeShoes(character); break;
				case (byte) ShoeSubType.Wing: character.shoes = new WingShoes(character); break;
				default: character.shoes = null; break;
			}
		}

		public void RemoveShoes(Character character) {
			character.shoes = null;
		}

		public void ActivateDash() {
			if(!this.CanActivate()) { return; }
			this.lastTrigger = Systems.timer.Frame;
			this.isActive = true;
			this.dashReset = false;
		}

		public bool MaintainDash() {
			if(!this.isActive) { return false; }

			this.lastHeld = Systems.timer.Frame;

			// End the dash once the duration runs out.
			if(Systems.timer.Frame > this.lastTrigger + Shoes.duration) {
				this.ReleaseDash();
			}

			return this.isActive;
		}

		public void ReleaseDash() {
			this.isActive = false;
		}

		public bool CanActivate() {

			// Cannot activate this power while holding an item:
			if(this.character.heldItem.IsHeld) { return false; }

			// Cannot activate if it's already active, or if you haven't reset the dash with a ground or wall touch.
			if(this.isActive || !this.dashReset) { return false; }

			// Delay if the cooldown hasn't passed yet.
			if(Systems.timer.Frame < this.lastHeld + Shoes.cooldown) { return false; }

			return true;
		}

		public void TouchWall() {
			this.dashReset = true;
		}
	}
}
