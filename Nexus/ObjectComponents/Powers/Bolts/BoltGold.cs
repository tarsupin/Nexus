using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class BoltGold : PowerBolt {

		public BoltGold( Character character ) : base( character ) {
			this.SetActivationSettings(72, 1, 72);
			this.IconTexture = "Weapon/BoltGold";
			this.subStr = "gold";
		}

		public override void Launch(GameObject actor, int posX, int posY, FInt velX, FInt velY) {
			var projectile = ProjectileBolt.Create(actor.room, (byte)ProjectileBoltSubType.Gold, FVector.Create(posX, posY), FVector.Create(velX, velY));
			projectile.SetActorID(actor);
		}
	}
}
