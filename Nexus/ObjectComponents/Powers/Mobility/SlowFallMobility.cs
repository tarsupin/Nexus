using Nexus.Engine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// This power activates Slow Fall; just reduces the speed at which you fall.
	public class SlowFallMobility : PowerMobility {

		public SlowFallMobility( Character character, string pool ) : base( character, pool ) {
			this.IconTexture = "Power/SlowFall";
		}

		public override void Activate() {
			Physics physics = this.character.physics;

			// Slows the Vertical Descent to Speed of 1.
			if(physics.velocity.Y > 1) { physics.velocity.Y = (FInt) 1; }

			// TODO SOUND: A "Slow Falling" sound? Like a repeating woosh or something?
		}
	}
}
