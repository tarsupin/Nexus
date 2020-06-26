using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// This power toggles on the character's ability to fly.
	public class FlightMobility : PowerMobility {

		public FlightMobility( Character character ) : base( character ) {
			this.subType = (byte)PowerSubType.Flight;
			this.IconTexture = "Power/Flight";
			this.subStr = "flight";
			this.SetActivationSettings(30, 1, 30);
		}

		public override bool Activate() {

			// Make sure the power can be activated
			if(!this.CanActivate()) { return false; }

			CharacterStatus status = this.character.status;

			if(status.action is FlightAction) {
				status.action.EndAction(this.character);
				Systems.sounds.wooshSubtle.Play(0.5f, 0, 0);

			} else {
				ActionMap.Flight.StartAction(character);
				Systems.sounds.wooshDeep.Play();
			}

			return true;
		}
	}
}
