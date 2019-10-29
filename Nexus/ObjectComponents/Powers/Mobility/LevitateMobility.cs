using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// This power activates Levitation (like Hover, but with vertical movement).
	public class LevitateMobility : PowerMobility {

		public LevitateMobility( Character character, string pool ) : base( character, pool ) {
			this.IconTexture = "Power/Levitate";
			this.SetActivationSettings(105, 1, 105);
		}

		public override bool Activate() {

			// Make sure the power can be activated
			if(!this.CanActivate()) { return false; }

			// Start the Levitation Action (same as Hover, but isn't restricted to horizontal movement)
			ActionMap.Hover.StartAction(character);

			// TODO SOUND: Trigger a "Start Hover" sound, to identify that the levitation has begun. (same as hover, flight, etc)
			return true;
		}
	}
}
