using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// TODO: Add this power's behavior. Currently does nothing.
	public class AthleteMobility : PowerMobility {

		public AthleteMobility( Character character, string pool ) : base( character, pool ) {
			this.IconTexture = "Power/Athlete";
			this.SetActivationSettings(15, 1, 15);
		}

		public override void Activate() {

			// Make sure the power can be activated
			if(!this.CanActivate()) { return; }

			System.Console.WriteLine("Activated AthleteMobility Power");
		}
	}
}
