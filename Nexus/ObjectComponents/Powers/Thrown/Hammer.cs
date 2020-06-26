using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Objects;
using System;

namespace Nexus.ObjectComponents {

	public class Hammer : PowerThrown {

		public Hammer( Character character, byte subType ) : base( character ) {
			this.subType = (byte)PowerSubType.Hammer;
			this.projSubType = (byte) subType;
			this.sound = Systems.sounds.axe;
			this.IconTexture = "Weapon/Hammer";
			this.baseStr = "ranged";
			this.subStr = "hammer";
			this.SetActivationSettings(210, 7, 9);

			// Power Settings
			this.multMomentum = FInt.Create(0.45);
		}

		public override void AffectByInput(ref FInt velX, ref FInt velY, ref int posX, ref int posY) {
			PlayerInput input = this.character.input;

			// Apply a randomly designated velocity:
			Random rand = new Random((int)Systems.timer.Frame);

			velX = rand.Next(4, 10) * FInt.Create(character.FaceRight ? 1 : -1);
			velY = FInt.Create(rand.Next(-17, -10));

			posX += 5 + (velX > 0 ? 4 : -4);

			if(input.isDown(IKey.Up)) {
				velX -= this.character.FaceRight ? FInt.Create(2) : FInt.Create(-2);
				velY -= 6;
			}

			else if(input.isDown(IKey.Down)) {
				velY = FInt.Create(1);
			}
		}

		public override void Launch(int posX, int posY, FInt velX, FInt velY) {
			var projectile = HammerProjectile.Create(this.character.room, this.projSubType, FVector.Create(posX, posY), FVector.Create(velX, velY));
			projectile.SetActorID(this.character);
		}
	}
}
