using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;
using System;

namespace Nexus.ObjectComponents {

	public class HeldItem {

		private readonly Character character;

		private Item objHeld = null;

		public HeldItem( Character character ) {
			this.character = character;
			this.ResetHeldItem();
		}

		public void RunHeldItemTick() {
			if(this.objHeld == null) { return; }
			this.MoveToHeldPosition();

			// If the character releases the item:
			if(!this.character.input.isDown(IKey.XButton)) {
				this.ReleaseItem();
			}
		}

		public bool WillPickupAttemptWork( Item item, DirCardinal dirToward ) {

			// Don't pick up an item if you're already carrying one.
			if(this.objHeld != null) { return false; }

			// Make sure the action key is down:
			if(!this.character.input.isDown(IKey.XButton)) { return false; }

			// Make sure you're facing the right direction (Shells don't apply to this rule).
			if(!(item is Shell)) {
				if(dirToward == DirCardinal.Left && this.character.FaceRight) { return false; }
				if(dirToward == DirCardinal.Right && !this.character.FaceRight) { return false; }
			}

			// Shells have special requirements for pickup:
			if(item is Shell) {

				// If thee shell is moving quickly, it can't be picked up (unless you have the bamboo hat).
				if(Math.Abs(item.physics.velocity.X.RoundInt) > 4) {
					if(!(this.character.hat is BambooHat)) { return false; }
				}
			}

			return true;
		}

		public void PickUpItem( Item item ) {
			item.isHeld = true;
			this.objHeld = item;
			this.MoveToHeldPosition();
		}

		public void ReleaseItem() {
			if(this.objHeld == null) { return; }
			this.objHeld.isHeld = false;
			this.objHeld = null;
		}

		public void MoveToHeldPosition() {
			if(this.objHeld == null) { return; }
			this.objHeld.physics.physPos.X = FInt.Create(this.character.posX + (this.character.FaceRight ? this.objHeld.gripRight : this.objHeld.gripLeft));
			this.objHeld.physics.physPos.Y = FInt.Create(this.character.posY + this.objHeld.gripLift);
			this.objHeld.posX = this.character.posX + (this.character.FaceRight ? this.objHeld.gripRight : this.objHeld.gripLeft);
			this.objHeld.posY = this.character.posY + this.objHeld.gripLift;
		}

		public void ResetHeldItem() {
			if(this.objHeld != null) {
				this.objHeld.isHeld = false;
				this.objHeld = null;
			}
		}

		// Draw Held Item
		public void Draw(int camX, int camY) {
			if(this.objHeld is Item) {
				this.objHeld.Draw(camX, camY);
			}
		}
	}
}
