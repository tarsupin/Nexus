﻿using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class Sword : PowerThrust {

		public Sword( Character character ) : base( character ) {
			this.SetActivationSettings(30, 1, 30);
			this.cycleDuration = 24; // The duration of the weapon's attack.
			this.range = 130; // Range of weapon's attack.
			this.weaponWidth = 81;
			this.offsetY = 15;
			this.sound = Systems.sounds.sword;
		}

		public override void Launch(GameObject actor, int startX, int startY, int endX, int endY, uint startFrame, uint endFrame) {
			var projectile = SwordProjectile.Create(actor.room, this.subType, FVector.Create(startX, startY), FVector.Create(endX, endY), startFrame, endFrame);
			projectile.SetActorID(actor);
		}
	}
}
