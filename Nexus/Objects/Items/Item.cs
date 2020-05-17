using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System.Collections.Generic;

// Items are considered objects that Characters can pick up.
// DROP is a guaranteed action.

namespace Nexus.Objects {

	public class Item : DynamicObject {

		// Item Traits
		public byte KickStrength { get; protected set; }		// The X-Axis Force that an item is kicked with. 0 means it cannot be kicked.
		public byte ThrowStrength { get; protected set; }		// The Y-Axis Force that an item is thrown with. 0 means it cannot be thrown.

		// Grip Points (where the item is held, relative to the Character holding it)
		public sbyte gripLeft;				// The negative X value offset when facing left.
		public sbyte gripRight;				// The positive X value offset when facing right.
		public sbyte gripLift;				// The Y value offset when lifting the item.

		// Status
		public byte releasedMomentum;       // The amount of momentum (X-Axis) the item has when thrown (used to determine how it lands).
		public uint intangible;				// The frame (relative to timer.frame) until it is no longer intangible.

		public Item(RoomScene room, byte subType, FVector pos, Dictionary<string, short> paramList) : base(room, subType, pos, paramList) {
			this.Meta = Systems.mapper.MetaList[MetaGroup.Item];

			// Physics
			this.physics = new Physics(this);
			this.physics.SetGravity(FInt.Create(0.5));
		}
	}
}
