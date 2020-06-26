using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class Dagger : PowerThrust {

		public Dagger( Character character ) : base( character ) {
			this.subType = (byte)PowerSubType.Dagger;
			this.SetActivationSettings(30, 1, 30);
			this.duration = 4;
			this.speed = 15;
			this.weaponWidth = 54;
			this.offsetY = 11;
			this.behindVal = 10;
			this.sound = Systems.sounds.dagger;
			this.IconTexture = "Weapon/Dagger";
			this.baseStr = "weapon";
			this.subStr = "dagger";
		}

		public override void Launch(GameObject actor, int startX, int startY, sbyte velX) {
			var projectile = DaggerProjectile.Create(actor.room, this.projSubType, FVector.Create(startX, startY), FVector.Create(velX, 0));
			projectile.SetActorID(actor);
			projectile.SetEndLife(Systems.timer.Frame + this.duration);
		}
	}
}
