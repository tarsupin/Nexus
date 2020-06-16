using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.ObjectComponents;
using System.Collections.Generic;

// Items are considered objects that Characters can pick up.
// DROP is a guaranteed action.

namespace Nexus.Objects {

	public class Item : GameObject {

		// Item Traits
		public byte KickStrength { get; protected set; }		// The X-Axis Force that an item is kicked with. 0 means it cannot be kicked.
		public byte ThrowStrength { get; protected set; }		// The Y-Axis Force that an item is thrown with. 0 means it cannot be thrown.

		// Grip Points (where the item is held, relative to the Character holding it)
		public sbyte gripLeft;				// The negative X value offset when facing left.
		public sbyte gripRight;				// The positive X value offset when facing right.
		public sbyte gripLift;              // The Y value offset when lifting the item.

		// Status
		public bool isHeld;					// TRUE if the object is currently being held.
		public sbyte releasedMomentum;		// The amount of momentum (X-Axis) the item has when thrown (used to determine how it lands).
		public uint intangible;				// The frame (relative to timer.frame) until it is no longer intangible.

		public Item(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {

			// Physics
			this.physics = new Physics(this);
			this.physics.SetGravity(FInt.Create(0.5));
		}

		public override void RunTick() {

			// Skip All Behaviors if the item is being held.
			if(this.isHeld) {
				Physics phys = this.physics;
				phys.touch.ResetTouch();

				// Update Last Positions
				phys.lastPosX = this.posX;
				phys.lastPosY = this.posY;

				phys.TrackPhysicsTick();
				return;
			}
			
			// If Item is not being held, run Standard Physics.
			else {
				this.physics.RunPhysicsTick();
			}

			// Animations, if applicable.
			if(this.animate is Animate) {
				this.animate.RunAnimationTick(Systems.timer);
			}
		}

		public virtual void ActivateItem(Character character) {}

		public override void CollidePosDown(int posY) {
			base.CollidePosDown(posY);
			this.physics.StopX();
		}

		public override bool CollideObjDown(GameObject obj) {
			if(base.CollideObjDown(obj)) {
				this.physics.StopX();
				return true;
			}
			return false;
		}
	}
}
