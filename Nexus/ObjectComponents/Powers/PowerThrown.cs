using Nexus.Engine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class PowerThrown : PowerAttack {

		public PowerThrown( Character character ) : base( character ) {
			this.sound = Systems.sounds.axe;
		}
	}
}
