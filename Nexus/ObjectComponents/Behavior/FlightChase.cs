using Microsoft.Xna.Framework;
using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.Objects;
using System;
using System.Collections.Generic;

namespace Nexus.ObjectComponents {

	public class FlightChase : FlightBehavior {

		private enum ChaseAction : byte {
			Standard,
			Flee,
			Return,
			Chase,
			Wait,
		}

		private Character charBeingChased;

		// Chase Mechanics
		private byte axis;
		private FInt speed;
		private bool returns;
		private short retDelay;

		// Chase Distances
		private int chase;
		private int stall;
		private int flee;
		private int reactDist;      // The reaction distance; based on the largest of 'chase', 'stall', and 'flee'.

		// Behavior Checks
		private Vector2 startPos;
		private ChaseAction quickAct;
		public int waitEndFrame;

		protected byte clusterId;

		public FlightChase(GameObject actor, Dictionary<string, short> paramList) : base(actor, paramList) {

			// Retrieve Mechanics
			this.axis = (paramList == null || !paramList.ContainsKey("axis") ? (byte)FlightChaseAxis.Both : (byte)paramList["axis"]);
			this.speed = FInt.Create((paramList == null || !paramList.ContainsKey("speed") ? 100 : paramList["speed"]) * 0.01 * 2);

			// Chase Distance Mechanics
			this.stall = (paramList == null || !paramList.ContainsKey("stall") ? (byte) 0 : (byte)paramList["axis"]);	// In Tiles
			this.flee = (paramList == null || !paramList.ContainsKey("flee") ? (byte) 0 : (byte)paramList["flee"]);	// In Tiles
			this.chase = (paramList == null || !paramList.ContainsKey("chase") ? (byte) 0 : (byte)paramList["chase"]);   // In Tiles

			this.chase *= (byte) TilemapEnum.TileWidth;
			this.stall *= (byte) TilemapEnum.TileWidth;
			this.flee *= (byte) TilemapEnum.TileWidth;

			this.reactDist = Math.Max(this.chase, Math.Max(this.stall, this.flee)); // Get the largest reaction distance for view purposes.

			// Returns Back to Starting Position
			this.returns = (paramList == null || !paramList.ContainsKey("returns") ? true : false);
			this.retDelay = (paramList == null || !paramList.ContainsKey("retDelay") ? (byte) 120 : (byte)paramList["retDelay"]); // Frames

			// Cluster Parent Handling
			this.clusterId = (paramList == null || !paramList.ContainsKey("clusterId") ? (byte) 0 : (byte)paramList["clusterId"]);

			// Quick Actions
			this.quickAct = ChaseAction.Standard;
			this.waitEndFrame = 0;

			// Positions
			this.startPos = new Vector2(actor.posX + actor.bounds.MidX, actor.posY + actor.bounds.MidY);

			// If the object is a parent cluster, it must be tracked through the full level.
			// TODO: NOTE: All objects are probably already active, so this might be unnecessary.
			if(this.clusterId > 0) {
				actor.SetActivity(Activity.ForceActive);
			}
		}

		public void SetStallMinimum(byte minStall) {
			minStall *= (byte) TilemapEnum.TileWidth;
			if(this.stall < minStall) { this.stall = minStall; }
		}
		
		private int WatchForCharacter(int midX, int midY) {

			int objectId = CollideRect.FindOneObjectTouchingArea(
				this.actor.room.objects[(byte)LoadOrder.Character],
				Math.Max(0, midX - this.reactDist),
				Math.Max(0, midY - this.reactDist),
				(short)(this.reactDist * 2), // View Distance (Width)
				(short)(this.reactDist * 2) // View Height
			);

			return objectId;
		}

		private void TryUpdateBehavior(int midX, int midY) {

			// If there is no character in sight, check for a new one:
			if(this.charBeingChased is Character == false) {
				int objectId = this.WatchForCharacter(midX, midY);

				if(objectId > 0) {
					this.charBeingChased = (Character)this.actor.room.objects[(byte)LoadOrder.Character][objectId];
				}
			}

			// Prepare Values
			int frame = Systems.timer.Frame;

			// Get distance from Character, if applicable.
			int destX = this.charBeingChased is Character ? this.charBeingChased.posX + this.charBeingChased.bounds.MidX : midX;
			int destY = this.charBeingChased is Character ? this.charBeingChased.posY + this.charBeingChased.bounds.MidY : midY;
			int distance = FPTrigCalc.GetDistance(FVector.Create(midX, midY), FVector.Create(destX, destY)).RoundInt;

			// Assign New Chase Action (When Action Time Expires).

			// Flee
			if(this.flee > 0 && distance < this.flee) {
				this.quickAct = ChaseAction.Flee;
			}

			// Chase
			else if(this.chase > 0 && distance <= this.chase) {
				this.quickAct = ChaseAction.Chase;
			}

			// Return to Start Position
			else if(this.returns) {

				if(this.quickAct == ChaseAction.Return || (this.quickAct == ChaseAction.Wait && this.waitEndFrame < frame)) {
					this.quickAct = ChaseAction.Return;

				} else {
					this.quickAct = ChaseAction.Wait;
					this.waitEndFrame = this.waitEndFrame < frame ? frame + this.retDelay : this.waitEndFrame;
				}
			}

			// Standard: Do Nothing
			else {
				this.quickAct = ChaseAction.Standard;
			}
		}

		public override void RunTick() {

			// Update the rotation to move toward.
			int midX = this.actor.posX + this.actor.bounds.MidX;
			int midY = this.actor.posY + this.actor.bounds.MidY;

			// Only change behaviors every 16 frames.
			if(Systems.timer.frame16Modulus == 7) {
				this.TryUpdateBehavior(midX, midY);
			}

			// Get distance from Character, if applicable.
			int destX = this.charBeingChased is Character ? this.charBeingChased.posX + this.charBeingChased.bounds.MidX : midX;
			int destY = this.charBeingChased is Character ? this.charBeingChased.posY + this.charBeingChased.bounds.MidY : midY;
			int distance = FPTrigCalc.GetDistance(FVector.Create(midX, midY), FVector.Create(destX, destY)).RoundInt;

			// Stall
			if(this.stall > 0 && distance < this.stall) {
				if(this.physics.velocity.X != 0) { this.physics.velocity.X *= FInt.Create(0.85); }
				if(this.physics.velocity.Y != 0) { this.physics.velocity.Y *= FInt.Create(0.85); }
				return;
			}

			// Stop Chasing
			if(this.quickAct == ChaseAction.Standard || this.quickAct == ChaseAction.Wait) {
				if(this.physics.velocity.X != 0) { this.physics.velocity.X *= FInt.Create(0.9); }
				if(this.physics.velocity.Y != 0) { this.physics.velocity.Y *= FInt.Create(0.9); }
				return;
			}

			// Return to Starting Position
			if(this.quickAct == ChaseAction.Return) {
				destX = (int) this.startPos.X;
				destY = (int) this.startPos.Y;
			}

			FInt newVelX = FInt.Create(0);
			FInt newVelY = FInt.Create(0);

			// Chase or Flee
			if(this.axis == (byte) FlightChaseAxis.Both) {

				if(Math.Abs(midX - destX) < 2 && Math.Abs(midY - destY) <= 2) {
					this.physics.velocity.X *= FInt.Create(0.90);
					this.physics.velocity.Y *= FInt.Create(0.90);
					return;
				}

				FInt rotRadian = FPRadians.GetRadiansBetweenCoords(midX, midY, destX, destY);
				newVelX = FPRadians.GetXFromRotation(rotRadian, this.speed);
				newVelY = FPRadians.GetYFromRotation(rotRadian, this.speed);
			}
			
			// Vertical Movement Only
			else if(this.axis == (byte) FlightChaseAxis.Vertical) {
				if(Math.Abs(midY - destY) <= 2) { this.physics.velocity.Y *= FInt.Create(0.90); return; }
				FInt rotRadian = FPRadians.GetRadiansBetweenCoords(0, midY, 0, destY);
				newVelY = FPRadians.GetYFromRotation(rotRadian, this.speed);

			}
			
			// Horizontal Movement Only
			else {
				if(Math.Abs(midX - destX) <= 2) { this.physics.velocity.X *= FInt.Create(0.90); return; }
				FInt rotRadian = FPRadians.GetRadiansBetweenCoords(midX, 0, destX, 0);
				newVelX = FPRadians.GetXFromRotation(rotRadian, this.speed);
			}

			// Flee
			if(this.quickAct == ChaseAction.Flee) {
				newVelX = newVelX.Inverse;
				newVelY = newVelY.Inverse;
			}

			FInt accel = this.speed * FInt.Create(0.02);

			if(this.physics.velocity.X > newVelX) {
				this.physics.velocity.X -= accel;
			} else if(this.physics.velocity.X < newVelX) {
				this.physics.velocity.X += accel;
			}

			if(this.physics.velocity.Y > newVelY) {
				this.physics.velocity.Y -= accel;
			} else if(this.physics.velocity.Y < newVelY) {
				this.physics.velocity.Y += accel;
			}
		}
	}
}
