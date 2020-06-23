using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class Spear : PowerThrust {

		public Spear( Character character ) : base( character ) {
			this.SetActivationSettings(120, 1, 120);
			this.duration = 18;
			this.speed = 17;
			this.weaponWidth = 90;
			this.offsetY = 10;
			this.behindVal = 50;
			this.sound = Systems.sounds.sword;
			this.IconTexture = "Weapon/Spear";
			this.baseStr = "weapon";
			this.subStr = "spear";
		}

		public override void Launch(GameObject actor, int startX, int startY, sbyte velX) {
			var projectile = SpearProjectile.Create(actor.room, this.subType, FVector.Create(startX, startY), FVector.Create(velX, 0));
			projectile.SetActorID(actor);
			projectile.SetEndLife(Systems.timer.Frame + this.duration);
		}
	}
}
