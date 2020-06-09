using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class Dagger : PowerThrust {

		public Dagger( Character character ) : base( character ) {
			this.SetActivationSettings(30, 1, 30);
			this.cycleDuration = 12; // The duration of the weapon's attack.
			this.range = 60; // Range of weapon's attack.
			this.weaponWidth = 54;
			this.offsetY = 11;
			this.sound = Systems.sounds.dagger;
		}

		public override void Launch(GameObject actor, int startX, int startY, int endX, int endY, uint startFrame, uint endFrame) {
			var projectile = DaggerProjectile.Create(actor.room, this.subType, FVector.Create(startX, startY), FVector.Create(endX, endY), startFrame, endFrame);
			projectile.SetActorID(actor);
		}
	}
}
