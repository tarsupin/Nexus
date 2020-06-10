﻿using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class Spear : PowerThrust {

		public Spear( Character character ) : base( character ) {
			this.SetActivationSettings(72, 1, 72);
			this.range = 260; // Range of weapon's attack.
			this.weaponWidth = 90;
			this.offsetY = 10;
			this.sound = Systems.sounds.sword;
		}

		public override void Launch(GameObject actor, int startX, int startY, int endX, int endY) {
			var projectile = SpearProjectile.Create(actor.room, this.subType, FVector.Create(startX, startY), FVector.Create(endX, endY));
			projectile.SetActorID(actor);
		}
	}
}