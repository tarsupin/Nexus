using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// This power activates Hovering.
	public class HoverMobility : PowerMobility {

		public HoverMobility( Character character, string pool ) : base( character, pool ) {
			this.IconTexture = "Power/Hover";
			this.SetActivationSettings(105, 1, 105);
		}

		public override void Activate() {

			// Make sure the power can be activated
			if(!this.CanActivate()) { return; }

			// Start the Hover Action
			character.ActionMap.Hover.StartAction(character, true);

			// TODO SOUND: Trigger a "Start Hover" sound, to identify that the hover has begun.
		}
	}
}
