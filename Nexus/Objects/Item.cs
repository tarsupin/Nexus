using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.ObjectComponents;
using Newtonsoft.Json.Linq;

// Items are considered objects that Characters can pick up.
// DROP is a guaranteed action.

namespace Nexus.Objects {

	public class Item : DynamicGameObject {

		// Item Traits
		public byte KickStrength { get; private set; }		// The X-Axis Force that an item is kicked with. 0 means it cannot be kicked.
		public byte ThrowStrength { get; private set; }     // The Y-Axis Force that an item is thrown with. 0 means it cannot be thrown.

		// Grip Points (where the item is held, relative to the Character holding it)
		public sbyte gripLeft;				// The negative X value offset when facing left.
		public sbyte gripRight;				// The positive X value offset when facing right.
		public sbyte gripLift;				// The Y value offset when lifting the item.

		// Status
		public byte releasedMomentum;       // The amount of momentum (X-Axis) the item has when thrown (used to determine how it lands).
		public uint intangible;				// The frame (relative to timer.frame) until it is no longer intangible.

		public Item(LevelScene scene, byte subType, FVector pos, JObject paramList) : base(scene, subType, pos, paramList) {

			// Physics
			this.physics = new Physics(this);
			this.physics.SetGravity(FInt.Create(0.5));
		}
	}
}
