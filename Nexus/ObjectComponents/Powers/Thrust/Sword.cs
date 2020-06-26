using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class Sword : PowerThrust {

		public Sword( Character character ) : base( character ) {
			this.subType = (byte)PowerSubType.Sword;
			this.SetActivationSettings(60, 1, 60);
			this.duration = 10;
			this.speed = 13;
			this.weaponWidth = 81;
			this.offsetY = 15;
			this.behindVal = 35;
			this.sound = Systems.sounds.sword;
			this.IconTexture = "Weapon/Sword";
			this.baseStr = "weapon";
			this.subStr = "sword";
		}

		public override void Launch(GameObject actor, int startX, int startY, sbyte velX) {
			var projectile = SwordProjectile.Create(actor.room, this.projSubType, FVector.Create(startX, startY), FVector.Create(velX, 0));
			projectile.SetActorID(actor);
			projectile.SetEndLife(Systems.timer.Frame + this.duration);
		}
	}
}
