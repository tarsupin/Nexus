using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class PowerAttack : Power {

		public PowerAttack( Character character ) : base( character ) {

		}

		public virtual void EndPower() {
			//this.character.attackPower = null;
		}
	}
}
