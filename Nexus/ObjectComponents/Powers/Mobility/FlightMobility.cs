using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// This power toggles on the character's ability to fly.
	public class FlightMobility : PowerMobility {

		public FlightMobility( Character character, string pool ) : base( character, pool ) {
			this.IconTexture = "Power/Flight";
			this.SetActivationSettings(15, 1, 15);
		}

		public override void Activate() {

			// Make sure the power can be activated
			if(!this.CanActivate()) { return; }

			if(this.character.action is FlightAction) {
				this.character.action.EndAction(this.character);

				// TODO SOUND: Create an "End Flight" sound, to identify that the flight has been toggled off.

			} else {
				this.character.action = new FlightAction(this.character);

				// TODO SOUND: Create a "Flight Takeoff" sound, to identify the flight has been toggled on.
			}
		}
	}
}
