using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	public class BoxingGlove : PowerThrown {

		public BoxingGlove( Character character ) : base( character ) {
			this.SetActivationSettings(60, 1, 60);
			this.sound = Systems.sounds.sword;
			this.IconTexture = "Weapon/BoxingRed";
			this.baseStr = "weapon";
			this.subStr = "glove";
		}

		public override bool Activate() {

			// Make sure the power can be activated
			if(!this.CanActivate()) { return false; }

			// References
			Character character = this.character;

			// Determine Starting Position of Projectile relative to Character
			int startX = character.posX + character.bounds.MidX + (this.character.FaceRight ? -20 : -42); // 62 is BoxGlove Width
			int startY = character.posY + character.bounds.MidY - 24; // Y Offset

			sbyte velX = (this.character.FaceRight ? (sbyte)13 : (sbyte)-13);

			this.sound.Play();

			// Launch Projectile
			this.Launch(character, startX, startY, velX);

			return true;
		}

		public virtual void Launch(GameObject actor, int startX, int startY, sbyte velX) {
			var projectile = GloveProjectile.Create(actor.room, this.subType, FVector.Create(startX, startY), FVector.Create(velX, 0));
			projectile.SetActorID(actor);
			projectile.SetEndLife(Systems.timer.Frame + 10);
		}
	}
}
