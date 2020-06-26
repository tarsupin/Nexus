using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// This power activates Levitation (like Hover, but with vertical movement).
	public class LevitateMobility : PowerMobility {

		public LevitateMobility( Character character ) : base( character ) {
			this.subType = (byte)PowerSubType.Levitate;
			this.IconTexture = "Power/Levitate";
			this.subStr = "levitate";
			this.SetActivationSettings(210, 1, 210);
		}

		public override bool Activate() {

			// Make sure the power can be activated
			if(!this.CanActivate()) { return false; }

			// Start the Levitation Action (same as Hover, but isn't restricted to horizontal movement)
			ActionMap.Hover.StartAction(this.character, true);
			Systems.sounds.wooshDeep.Play();
			return true;
		}
	}
}
