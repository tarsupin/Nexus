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

			var action = this.character.action;

			if(action is HoverAction) {

				// Don't begin hovering if there is already a hover action active.
				if(action.duration > 0) { return; }

				// TODO SOUND: Trigger a "Start Hover" sound, to identify that the hover has begun.
				action.EndAction();

			}

			// TODO: This Hover Is Not Done; Requires some updates given the mapping for actions I'll be doing.
			// TODO: This Hover Is Not Done; Requires some updates given the mapping for actions I'll be doing.
			// TODO: This Hover Is Not Done; Requires some updates given the mapping for actions I'll be doing.
		}
	}
}
