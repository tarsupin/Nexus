using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class PowerMobility : Power {

		public PowerMobility( Character character ) : base( character ) { }

		public virtual void EndPower() {}
	}
}
