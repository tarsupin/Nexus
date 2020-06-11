using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class PowerMobility : Power {

		public PowerMobility( Character character ) : base( character ) { }

		public virtual void EndPower() {
			//this.character.attackMobility = null;
			// TODO UI: Mobility Icon
			//this.character.scene.uiLevel.updatePowerMobilityIcon(); // Update the Mobility Power icon, if applicable.
		}
	}
}
