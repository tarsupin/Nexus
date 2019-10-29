using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class PowerMobility : Power {

		public PowerMobility( Character character ) : base( character ) {

		}

		public void UpdateIcon() {
			// TODO UI: Update Mobility Icon, if applicable.
			//scene.powerMobIcon.setFrame(this.IconTexture);
		}

		public virtual void EndPower() {
			//this.character.attackMobility = null;
			// TODO UI: Mobility Icon
			//this.character.scene.uiLevel.updatePowerMobilityIcon(); // Update the Mobility Power icon, if applicable.
		}
	}
}
