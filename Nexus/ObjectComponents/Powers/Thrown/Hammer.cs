using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Objects;
using System;

namespace Nexus.ObjectComponents {

	public class Hammer : PowerThrown {

		public Hammer( Character character, byte subType ) : base( character ) {
			this.subType = (byte) subType;
			this.sound = Systems.sounds.axe;
			this.IconTexture = "Power/Hammer";
			this.SetActivationSettings(210, 7, 9);

			// Power Settings
			this.multMomentum = FInt.Create(0.45);
		}

		public override bool Activate() {

			// Make sure the power can be activated
			if(!this.CanActivate()) { return false; }

			// References
			Character character = this.character;

			// Determine Starting Position of Projectile relative to Character
			int posX = character.posX + character.bounds.MidX + (character.FaceRight ? 10 : -10);
			int posY = character.posY + character.bounds.Top + 5;

			// Apply a randomly designated velocity:
			Random rand = new Random((int) Systems.timer.Frame);

			FInt velX = rand.Next(4, 10) * FInt.Create(character.FaceRight ? 1 : -1);
			FInt velY = FInt.Create(rand.Next(-17, -10));

			// Affect the Y-Velocity of the projectile if holding UP or DOWN
			this.AffectByInput(ref velX, ref velY);

			// Launch Projectile
			this.Launch(posX, posY, velX, velY);

			return true;
		}

		public override void AffectByInput(ref FInt velX, ref FInt velY) {
			PlayerInput input = this.character.input;

			if(input.isDown(IKey.Up)) {
				velX -= this.character.FaceRight ? FInt.Create(2) : FInt.Create(-2);
				velY -= 6;
			}

			else if(input.isDown(IKey.Down)) {
				velY = FInt.Create(1);
			}
		}
	}
}
