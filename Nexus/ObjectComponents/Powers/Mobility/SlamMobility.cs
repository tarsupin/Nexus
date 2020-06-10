using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// TODO: Add this power's behavior. Currently does nothing.
	public class SlamMobility : PowerMobility {

		public SlamMobility( Character character ) : base( character ) {
			this.IconTexture = "Power/Slam";
			this.subStr = "slam";
			this.SetActivationSettings(15, 1, 15);
		}

		public override bool Activate() {

			// Make sure the power can be activated
			if(!this.CanActivate()) { return false; }

			System.Console.WriteLine("Activated Slam Mobility Power");

			return true;
		}
	}
}
