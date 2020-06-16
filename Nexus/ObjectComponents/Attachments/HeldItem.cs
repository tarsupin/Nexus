using Nexus.Config;
using Nexus.Engine;
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

		public bool IsHeld { get { return this.objHeld != null; } }

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
						this.KickItem(this.objHeld, this.character.FaceRight ? DirCardinal.Right : DirCardinal.Left);
					} else {
						this.DropItem();
					}
				}
			}
		}

		public void ActivateItem() {
			if(!this.IsHeld) { return; }
			this.objHeld.ActivateItem(this.character);
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

				// If the shell is moving quickly, it can't be picked up (unless you have the bamboo hat).
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
			item.intangible = Systems.timer.Frame + 5;
			item.releasedMomentum = useXMomentum ? (sbyte) Math.Round(this.character.physics.velocity.X.RoundInt / 2.5) : (sbyte) 0;
			item.physics.velocity.X = FInt.Create(item.releasedMomentum);
			item.physics.velocity.Y = FInt.Create(-item.ThrowStrength);

			// Play Throw Sound
			Systems.sounds.wooshSubtle.Play();

			// No longer holding object:
			this.ResetHeldItem();
		}

		public void KickItem(Item item, DirCardinal dir ) {
			item.intangible = Systems.timer.Frame + 5;

			// If the Shell is stationary, or character is hitting it from behind, or was wearing a Bamboo Hat.
			sbyte xStrength = 0;
			sbyte yStrength = 0;

			// Affect Kick by Input
			if(this.character.input.isDown(IKey.Down)) { xStrength = -2; }
			else if(this.character.input.isDown(IKey.Up)) { yStrength = 6; }

			// If facing the wrong way, reduce kick power:
			if(dir == DirCardinal.Left && this.character.FaceRight) { xStrength = -2; yStrength = 0; }
			if(dir == DirCardinal.Right && !this.character.FaceRight) { xStrength = -2; yStrength = 0; }

			item.physics.velocity.X = FInt.Create(dir == DirCardinal.Right ? item.KickStrength + xStrength : -(item.KickStrength + xStrength));
			item.physics.velocity.Y = FInt.Create(-1.5 - yStrength);

			// Animate Shells
			if(item is Shell) {
				item.animate.SetAnimation(null, item.physics.velocity.X > 0 ? AnimCycleMap.Cycle4 : AnimCycleMap.Cycle4Reverse, 7, 1);
			}

			// Play Kick Sound
			Systems.sounds.shellBoop.Play(0.3f, 0, 0);

			this.ResetHeldItem();
		}

		public void DropItem() {
			if(this.objHeld == null) { return; }

			Item item = this.objHeld;
			item.intangible = Systems.timer.Frame + 7;

			// Check what Main Layer has present at the throw-release tile:
			TilemapLevel tilemap = this.character.room.tilemap;

			bool isBlocked = false;
			bool faceRight = this.character.FaceRight;

			isBlocked = CollideTile.IsBlockingSquare(tilemap, faceRight ? item.GridX2 : item.GridX, item.GridY, DirCardinal.Up);

			// If the drop tiles are blocked, see if we can drop it in the tile below.
			if(isBlocked) {
				int limitY = item.GridY * (byte)TilemapEnum.TileHeight + (byte)TilemapEnum.TileHeight;
				int dist = limitY - (item.posY + item.bounds.Top);

				// If we only have to drop it 24 pixels (or less), we can just lower the item slightly.
				if(dist < 24) {
					item.physics.MoveToPosY(limitY - item.bounds.Top);
					isBlocked = CollideTile.IsBlockingSquare(tilemap, faceRight ? item.GridX2 : item.GridX, item.GridY, DirCardinal.Up);
				}
			}

			// If the tile is still blocked, then a Y-reposition didn't work. We'll have to move it to character's tile.
			if(isBlocked) {
				int limitX;

				// NOTE: MUST have it like this. I don't understand the nuanced difference here, but it works.
				if(faceRight) {
					limitX = item.GridX2 * (byte)TilemapEnum.TileWidth;
				} else {
					limitX = item.GridX * (byte)TilemapEnum.TileWidth + (byte)TilemapEnum.TileWidth;
				}

				item.physics.MoveToPosX(limitX - (faceRight ? item.bounds.Right : item.bounds.Left));
			}
			
			// Assign Minimal Drop Physics (to smooth natural look)
			item.physics.velocity.X = FInt.Create(this.character.physics.velocity.X.RoundInt / 3);
			item.physics.velocity.Y = FInt.Create(-1.5);

			// Play Drop Sound
			Systems.sounds.wooshSubtle.Play(0.5f, 0, 0);

			this.ResetHeldItem();
		}

		public void MoveToHeldPosition() {
			if(this.objHeld == null) { return; }
			this.objHeld.physics.MoveToPos(this.character.posX + (this.character.FaceRight ? this.objHeld.gripRight : this.objHeld.gripLeft), this.character.posY + this.objHeld.gripLift);
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
