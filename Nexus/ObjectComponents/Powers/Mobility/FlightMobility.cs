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
				this.character.room.PlaySound(Systems.sounds.wooshSubtle, 0.5f, this.character.posX + 16, this.character.posY + 16);

			} else {
				ActionMap.Flight.StartAction(character);
				this.character.room.PlaySound(Systems.sounds.wooshDeep, 1f, this.character.posX + 16, this.character.posY + 16);
			}

			return true;
		}
	}
}
