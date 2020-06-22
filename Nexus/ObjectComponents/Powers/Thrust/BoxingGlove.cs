using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class BoxingGlove : PowerThrust {

		public BoxingGlove( Character character ) : base( character ) {
			this.SetActivationSettings(60, 1, 60);
			this.range = 100; // Range of weapon's attack.
			this.weaponWidth = 62;
			this.offsetY = 22;
			this.sound = Systems.sounds.sword;
			this.IconTexture = "Weapon/BoxingRed";
			this.baseStr = "weapon";
			this.subStr = "glove";
		}

		public override void Launch(GameObject actor, int startX, int startY, int endX, int endY) {
			var projectile = GloveProjectile.Create(actor.room, this.subType, FVector.Create(startX, startY), FVector.Create(endX, endY));
			projectile.SetActorID(actor);
		}
	}
}
