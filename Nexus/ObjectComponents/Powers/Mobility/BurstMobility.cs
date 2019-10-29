﻿using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// This power bursts the character into a particular direction, often stacking with existing momentum.
	public class BurstMobility : PowerMobility {

		public BurstMobility( Character character, string pool ) : base( character, pool ) {
			this.IconTexture = "Power/Burst";
			this.SetActivationSettings(90, 2, 30);
		}

		public override bool Activate() {

			// Make sure the power can be activated
			if(!this.CanActivate()) { return false; }

			// Prepare Direction
			sbyte directionHor = 0;
			sbyte directionVert = 0;

			// Character Input determines which way the air burst effect occurs.
			PlayerInput input = this.character.input;

			if(input.isDown(IKey.Up)) {
				directionVert = -1;
			} else if(input.isDown(IKey.Down)) {
				directionVert = 1;
			}

			if(input.isDown(IKey.Left)) {
				directionHor = -1;
			} else if(input.isDown(IKey.Right)) {
				directionHor = 1;
			}

			// Default to Upward Direction
			if(directionHor == 0 && directionVert == 0) {
				directionVert = -1;
			}

			Physics physics = this.character.physics;

			// Affect Character's Velocity
			if(directionHor != 0) {
				physics.velocity.X += 6 * directionHor;

				if(physics.velocity.X.IntValue > 16) { physics.velocity.X = (FInt) 16; }
				else if(physics.velocity.X.IntValue < -16) { physics.velocity.X = (FInt) 0 - 16; }
			}

			if(directionVert == -1) {

				// If you're already moving upward, increase speed by -10 (up to -16). Otherwise, automatically increase to -12.
				if(physics.velocity.Y < 0) {
					physics.velocity.Y -= 10;
					if(physics.velocity.Y < -16) { physics.velocity.Y = (FInt)0 - 16; }
				}
				else { physics.velocity.Y = (FInt) 0-12; }

			} else if(directionVert == 1) {
				physics.velocity.Y += 4;
			}

			// Display the "Air" particle event and play the "Air" sound.
			// TODO UI: Display air particle. PEventAir.activate( char, char.scene.game.particles, dirHor, dirVert );
			// TODO SOUND: Play sound for air. char.scene.soundList.air.play(0.5);

			return true;
		}
	}
}
