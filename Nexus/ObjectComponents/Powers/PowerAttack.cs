using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class PowerAttack : Power {

		public PowerAttack( Character character, string pool ) : base( character, pool ) {

		}

		public void UpdateIcon() {
			// TODO UI: Update Power Icon, if applicable.
			//scene.powerIcon.setFrame(this.IconTexture);
		}

		public virtual void EndPower() {
			//this.character.attackPower = null;
			// TODO UI: Power Icon
			//this.character.scene.uiLevel.updatePowerIcon(); // Update the power icon, if applicable.
		}
	}
}
