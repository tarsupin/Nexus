using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// TODO: Add this power's behavior. Currently does nothing. Maybe speed? Jump? Etc?
	public class AthleteMobility : PowerMobility {

		public AthleteMobility( Character character ) : base( character ) {
			this.subType = (byte)PowerSubType.Athlete;
			this.IconTexture = "Power/Athlete";
			this.subStr = "athlete";
			this.SetActivationSettings(15, 1, 15);
		}

		public override bool Activate() {

			// Make sure the power can be activated
			if(!this.CanActivate()) { return false; }

			System.Console.WriteLine("Activated AthleteMobility Power");

			return true;
		}
	}
}
