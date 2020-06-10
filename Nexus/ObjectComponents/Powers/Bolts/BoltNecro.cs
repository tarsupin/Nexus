using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class BoltNecro : PowerBolt {

		public BoltNecro(Character character) : base(character) {
			this.SetActivationSettings(72, 2, 21);
			this.IconTexture = "Weapon/BoltNecro";
			this.subStr = "necro";
		}

		public override void Launch(GameObject actor, int posX, int posY, FInt velX, FInt velY) {
			var projectile = ProjectileBolt.Create(actor.room, (byte)ProjectileBoltSubType.Necro, FVector.Create(posX, posY), FVector.Create(velX, velY));
			projectile.SetActorID(actor);
		}
	}
}
