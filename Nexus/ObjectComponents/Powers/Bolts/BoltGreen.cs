using Nexus.Config;
using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Objects;
using System;

namespace Nexus.ObjectComponents {

	public class BoltGreen : PowerBolt {

		public BoltGreen( Character character ) : base( character ) {
			this.IconTexture = "Weapon/BoltGreen";
			this.subStr = "green";
			this.SetActivationSettings(96, 3, 17);

			this.yVelUp = -4;
			this.yVelDown = 4;
		}

		public override void Launch(GameObject actor, int posX, int posY, FInt velX, FInt velY) {

			// Green Bolts have random direction casting:
			Random rand = new Random((int)Systems.timer.Frame);
			FInt modY = FInt.Create(rand.Next(-20, 20) * 0.1);

			var projectile = ProjectileBolt.Create(actor.room, (byte)ProjectileBoltSubType.Green, FVector.Create(posX, posY), FVector.Create(velX, velY + modY));
			projectile.SetActorID(actor);
		}
	}
}
