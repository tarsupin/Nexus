using Microsoft.Xna.Framework.Audio;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class PowerAttack : Power {

		protected byte subType;
		protected SoundEffect sound;

		public PowerAttack( Character character ) : base( character ) {

		}

		public virtual void EndPower() {
			//this.character.attackPower = null;
		}
	}
}
