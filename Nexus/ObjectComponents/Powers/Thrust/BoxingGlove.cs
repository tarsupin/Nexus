using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class BoxingGlove : PowerThrust {

		public BoxingGlove( Character character ) : base( character ) {
			this.subType = (byte)PowerSubType.BoxingRed;
			this.SetActivationSettings(60, 1, 60);
			this.duration = 10;
			this.speed = 13;
			this.weaponWidth = 54;
			this.offsetY = 24;
			this.behindVal = 20;
			this.sound = Systems.sounds.sword;
			this.IconTexture = "Weapon/BoxingRed";
			this.baseStr = "weapon";
			this.subStr = "glove";
		}

		public override void Launch(GameObject actor, int startX, int startY, sbyte velX) {
			var projectile = GloveProjectile.Create(actor.room, this.projSubType, FVector.Create(startX, startY), FVector.Create(velX, 0));
			projectile.SetActorID(actor);
			projectile.SetEndLife(Systems.timer.Frame + this.duration);
		}
	}
}
