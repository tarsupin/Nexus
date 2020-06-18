using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System.Collections.Generic;

namespace Nexus.ObjectComponents {

	public class SlammerBehavior : Behavior {

		private enum SlammerState : byte {
			Passive,
			Slamming,
			ResetDelay,
			Resetting,
		}

		Physics physics;        // Reference to the actor's physics.

		// Positions
		private int startY;
		private int endY;
		private int viewY;
		private short viewHeight;

		// Movement
		private FInt fallAccel = FInt.Create(0);

		// Slamming Values
		private SlammerState state = SlammerState.Passive;
		private short resetDelay = 15;
		private int resetFrame = 0;

		public SlammerBehavior(GameObject actor, Dictionary<string, short> paramList) : base(actor) {
			this.physics = actor.physics;
			this.startY = this.actor.posY;
			this.DetermineSlamDistance();
		}

		private void DetermineSlamDistance() {
			TilemapLevel tilemap = this.actor.room.tilemap;

			// We know the slammer's starting position.
			short gridX = this.actor.GridX;
			short gridY = this.actor.GridY;

			this.viewY = this.actor.posY + ((byte)TilemapEnum.TileHeight * 2);
			this.viewHeight = 0;

			// Scan for solid tiles beneath the slammer, up to 17 below (for a full screen)
			for(short testY = (short)(gridY + 2); testY < gridY + 19; testY++) {
				
				if(testY > tilemap.YCount + (byte)TilemapEnum.GapUp) { return; }

				// If there is a blocking tile at this height below the slammer, we can determine it's final Y-position after a slam:
				if(CollideTile.IsBlockingSquare(tilemap, gridX, testY, DirCardinal.Up) || CollideTile.IsBlockingSquare(tilemap, (short)(gridX + 1), testY, DirCardinal.Up)) {
					this.endY = testY * (byte)TilemapEnum.TileHeight;
					this.viewHeight = (short)(this.endY - this.viewY - 10);
					break;
				}
			}
		}

		public override void RunTick() {

			// While Slammer is Passive
			if(this.state == SlammerState.Passive) {
				if(this.viewHeight == 0) { return; }

				// Check for Characters within Slammer's View. Being Slamming if one is found.
				int objectId = CollideRect.FindOneObjectTouchingArea(
					this.actor.room.objects[(byte)LoadOrder.Character],
					this.actor.posX + 8,
					this.viewY,
					(byte)TilemapEnum.TileWidth * 2 - 20,
					this.viewHeight
				);

				if(objectId > 0) {
					this.state = SlammerState.Slamming;
				}
			}

			// While Slammer is Slamming
			else if(this.state == SlammerState.Slamming) {
				this.fallAccel += FInt.Create(0.12);
				if(this.fallAccel > 2) { this.fallAccel = FInt.Create(2); }
				this.physics.velocity.Y += this.fallAccel;

				// If Slammer has completed its maximum journey.
				if(this.actor.posY >= this.endY) {
					this.EndSlam();
					return;
				}
			}

			// While Slammer is Reset Delayed
			else if(this.state == SlammerState.ResetDelay) {
				if(this.resetFrame <= Systems.timer.Frame) {
					this.state = SlammerState.Resetting;
				}
			}

			// While Slammer is Resetting
			else if(this.state == SlammerState.Resetting) {
				this.physics.velocity.Y -= FInt.Create(0.3);
				if(this.actor.posY <= this.startY) {
					this.SlammerHasReset();
				}
			}
		}

		public void EndSlam() {
			this.state = SlammerState.ResetDelay;
			this.physics.velocity.Y = FInt.Create(0);
			this.fallAccel = FInt.Create(0);
			this.resetFrame = Systems.timer.Frame + this.resetDelay;

			// Sound the Thud.
			Systems.sounds.thudWhomp.Play();
		}

		private void SlammerHasReset() {
			this.state = SlammerState.Passive;
			this.physics.velocity.Y = FInt.Create(0);
			this.fallAccel = FInt.Create(0);
			this.physics.MoveToPosY(this.startY);
		}
	}
}
