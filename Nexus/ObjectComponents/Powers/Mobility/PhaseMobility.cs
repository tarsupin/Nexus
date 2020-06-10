using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// TODO: Add this power's behavior. Currently does nothing.
	public class PhaseMobility : PowerMobility {

		public PhaseMobility( Character character ) : base( character ) {
			this.IconTexture = "Power/Phase";
			this.subStr = "phase";
			this.SetActivationSettings(15, 1, 15);
		}

		public override bool Activate() {

			// Make sure the power can be activated
			if(!this.CanActivate()) { return false; }

			System.Console.WriteLine("Activated PhaseMobility Power");

			return true;
		}
	}
}
