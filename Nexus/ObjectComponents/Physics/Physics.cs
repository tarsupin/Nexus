using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System;

namespace Nexus.ObjectComponents {

	public class Physics {

		// Object Reference
		protected DynamicObject actor;

		// Physics Values
		public FVector physPos;			// The "physics" tracks "true" positions with Fixed-Point math, but is separate from "position" on the Game Object, which uses ints.

		// Detection
		public int lastPosX;
		public int lastPosY;
		public Touch touch;

		protected bool hasExtraMovement;

		// Movements
		public FVector moved;           // The X, Y actually moved during its last frame, after its resistances were handled. Once set for the frame, don't change it.
		public FVector intend;          // The X, Y the actor intends to move during its frame. A combination of gravity + velocity + extraMovement. Once set for the frame, don't change it.
		public FVector result;			// The X, Y the actor will actually travel. This can change through the frame, if blocked by tile or object.
		public FVector velocity;
		public FVector extraMovement;	// Added values, such as for conveyor belts, platforms, etc.
		public FInt gravity;

		// Grid Crossing
		public sbyte passHorTile;		// 0 = no horizontal grid squares were crossed. -1 means crossed LEFT, 1 means crossed RIGHT
		public sbyte passVertTile;      // 0 = no vertical grid squares were crossed. -1 means crossed UP, 1 means crossed DOWN
		public int gridCrossHor;		// The integer value of the horizontal grid line being crossed.
		public int gridCrossVert;		// The integer value of the vertical grid line being crossed.

		public Physics( DynamicObject actor ) {
			this.actor = actor;

			this.physPos = FVector.Create(this.actor.posX, this.actor.posY);

			this.lastPosX = this.actor.posX;
			this.lastPosY = this.actor.posY;

			this.velocity = new FVector();
			this.extraMovement = new FVector();
			this.gravity = new FInt();
			this.hasExtraMovement = false;
			this.touch = new Touch();
		}

		// Get Amount Moved
		public int AmountMovedX { get { return this.actor.posX - this.lastPosX; } }
		public int AmountMovedY { get { return this.actor.posY - this.lastPosY; } }

		public void SetGravity( FInt gravity ) {
			this.gravity = gravity;
		}

		// Run this method BEFORE any collisions take place. The .result value will change from collisions.
		public void InitializePhysicsTick() {

			// Track what was moved last frame:
			this.moved = this.result;

			// Apply Gravity to Velocity
			this.velocity.Y += this.gravity;

			// Determine what the intended movement is for this frame.
			this.intend = this.velocity;

			if(hasExtraMovement) {
				this.intend = FVector.VectorAdd(this.intend, this.extraMovement);
			}

			// Reset the result, matching the intention - it may change throughout the rest of the physics update.
			this.result = this.intend;

			// --- Determine if actor crosses a grid square to potentially collide with a tile. --- //
			this.passHorTile = 0;
			this.passVertTile = 0;

			// Crosses X Grid Left?
			if(this.result.X < 0) {
				int posX = this.actor.posX + this.actor.bounds.Left;
				int gridRight = (int) (Math.Ceiling((double)(posX / (byte) TilemapEnum.TileWidth)) * (byte) TilemapEnum.TileWidth);
				
				if(gridRight >= posX - this.result.X) {
					this.passHorTile = -1;
					this.gridCrossHor = gridRight;
				}
			}
			
			// Crosses X Grid Right?
			else if(this.result.X > 0) {
				int posX = this.actor.posX + this.actor.bounds.Right;
				int gridLeft = (int)(Math.Floor((double)(posX / (byte)TilemapEnum.TileWidth)) * (byte)TilemapEnum.TileWidth);

				if(gridLeft <= posX + this.result.X) {
					this.passHorTile = 1;
					this.gridCrossHor = gridLeft;
				}
			}

			// Crosses Y Grid Up?
			if(this.result.Y < 0) {
				int posY = this.actor.posY + this.actor.bounds.Left;
				int gridBottom = (int) (Math.Ceiling((double)(posY / (byte) TilemapEnum.TileWidth)) * (byte) TilemapEnum.TileWidth);
				
				if(gridBottom >= posY - this.result.Y) {
					this.passVertTile = -1;
					this.gridCrossVert = gridBottom;
				}
			}
			
			// Crosses Y Grid Down?
			else if(this.result.Y > 0) {
				int posY = this.actor.posY + this.actor.bounds.Right;
				int gridTop = (int)(Math.Floor((double)(posY / (byte)TilemapEnum.TileWidth)) * (byte)TilemapEnum.TileWidth);

				if(gridTop <= posY + this.result.Y) {
					this.passVertTile = 1;
					this.gridCrossVert = gridTop;
				}
			}
		}

		// Run this method AFTER collisions take place.
		// At this point, this.result may have changed from collisions. Update certain values accordingly.
		public void RunPhysicsTick() {

			// Extra Movement (such as caused by Platforms or Conveyors)
			if(hasExtraMovement) {
				this.physPos = FVector.VectorAdd(this.physPos, this.extraMovement);
				this.extraMovement = new FVector();
			}

			this.velocity.Y += this.gravity;
			this.touch.ResetTouch();
			this.TrackPhysicsTick();
		}

		public void TrackPhysicsTick() {

			// Update Positions
			this.physPos = FVector.VectorAdd(this.physPos, this.velocity);

			// Extra Movement (such as caused by Platforms or Conveyors)
			if(hasExtraMovement) {
				this.physPos = FVector.VectorAdd(this.physPos, this.extraMovement);
				this.extraMovement = new FVector();
			}

			this.UpdatePosX();
			this.UpdatePosY();
		}

		public void UpdatePosX() {
			this.lastPosX = this.actor.posX;
			this.actor.posX = this.physPos.X.IntValue;
		}

		public void UpdatePosY() {
			this.lastPosY = this.actor.posY;
			this.actor.posY = this.physPos.Y.IntValue;
		}

		public void MoveToPos( FVector pos ) {
			this.physPos = pos;
			this.UpdatePosX();
			this.UpdatePosY();
		}

		public void MoveToPosX( int posX ) {
			this.physPos.X = FInt.Create(posX);
			this.UpdatePosX();
		}

		public void MoveToPosY( int posY ) {
			this.physPos.Y = FInt.Create(posY);
			this.UpdatePosY();
		}

		public void StopX() {
			this.velocity.X = FInt.Create(0);
		}

		public void StopY() {
			this.velocity.Y = FInt.Create(0);
		}
	}
}
