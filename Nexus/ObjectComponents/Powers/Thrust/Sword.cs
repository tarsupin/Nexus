using Nexus.Engine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class Sword : PowerThrust {

		public Sword( Character character ) : base( character ) {
			this.sound = Systems.sounds.sword;
		}

		public override bool Activate() {
			if(!base.Activate()) { return false; }

			//this.Launch(this.startX, this.startY);

			return true;
		}

		//public WeaponSword Launch(int startX, int startY) {
		//	//this.sound.Play();
		//	//WeaponSword projectile = WeaponSword.Create(this.character.scene, this.subType, FVector.Create(posX, posY), FVector.Create(velX, velY));
		//	//projectile.power = this;
		//	//projectile.rotation = this.rotation;
		//}
	}
}
