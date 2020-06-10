using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// This power activates Hovering.
	public class HoverMobility : PowerMobility {

		public HoverMobility( Character character ) : base( character ) {
			this.IconTexture = "Power/Hover";
			this.subStr = "hover";
			this.SetActivationSettings(105, 1, 105);
		}

		public override bool Activate() {

			// Make sure the power can be activated
			if(!this.CanActivate()) { return false; }

			// Start the Hover Action
			ActionMap.Hover.StartAction(character, true);

			// TODO SOUND: Trigger a "Start Hover" sound, to identify that the hover has begun.
			return true;
		}
	}
}
