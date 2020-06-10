using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// TODO: Add this power's behavior. Currently does nothing.
	public class LeapMobility : PowerMobility {

		public LeapMobility( Character character ) : base( character ) {
			this.IconTexture = "Power/Leap";
			this.subStr = "leap";
			this.SetActivationSettings(15, 1, 15);
		}

		public override bool Activate() {

			// Make sure the power can be activated
			if(!this.CanActivate()) { return false; }

			System.Console.WriteLine("Activated LeapMobility Power");

			return true;
		}
	}
}
