﻿using Nexus.Engine;
using Nexus.GameEngine;
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

				// If Looking Up
				if(this.character.input.isDown(IKey.Up)) {
					this.ThrowItem();
				}

				// If Looking Down
				else if(this.character.input.isDown(IKey.Down)) {
					this.ThrowItem(true);
				}

				// If Looking Left or Right
				else {

					// If it can be kicked:
					if(this.objHeld.KickStrength > 0) {
						this.KickItem();
					} else {
						this.DropItem();
					}
				}
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

		public void ThrowItem(bool useXMomentum = false) {
			if(this.objHeld == null) { return; }
			Item item = this.objHeld;

			// Item will release in the direction the character does.
			item.SetDirection(this.character.FaceRight);

			// Check what Main Layer has present at the throw-release tile:
			TilemapLevel tilemap = this.character.room.tilemap;

			bool isBlocked = CollideTile.IsBlockingSquare(tilemap, item.GridX, item.GridY, DirCardinal.Up);

			if(!isBlocked && item.GridX != item.GridX2) {
				isBlocked = CollideTile.IsBlockingSquare(tilemap, item.GridX2, item.GridY, DirCardinal.Up);
			}

			// Prevent Throw
			if(isBlocked) {
				this.DropItem();
				return;
			}

			// Assign Item Physics + Thrown Properties
			item.intangible = Systems.timer.Frame + 7;
			item.releasedMomentum = useXMomentum ? (sbyte) Math.Round(this.character.physics.velocity.X.RoundInt / 2.5) : (sbyte) 0;
			item.physics.velocity.X = FInt.Create(item.releasedMomentum);
			item.physics.velocity.Y = FInt.Create(0 - item.ThrowStrength);

			// No longer holding object:
			this.ResetHeldItem();
		}

		public void KickItem() {
			if(this.objHeld == null) { return; }
			this.ResetHeldItem();
		}

		public void DropItem() {
			if(this.objHeld == null) { return; }
			this.ResetHeldItem();
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
