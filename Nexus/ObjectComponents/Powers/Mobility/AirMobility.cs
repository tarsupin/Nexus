using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using Nexus.Objects;

namespace Nexus.ObjectComponents {

	// This power bursts the character in a particular direction, based on their direction pad.
	public class AirMobility : PowerMobility {

		public AirMobility( Character character ) : base( character ) {
			this.IconTexture = "Power/Air";
			this.SetActivationSettings(90, 1, 90);
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

			// If the character is standing on ground, it interferes with actions; fix that.
			if(this.character.physics.touch.toBottom) {
				this.character.physics.touch.ResetTouch();
				this.character.physics.MoveToPosY(this.character.posY - 1);
			}

			// Trigger the Air Burst Action
			ActionMap.AirBurst.StartAction(this.character, directionHor, directionVert);

			// Display the "Air" particle event and play the "Air" sound.
			// TODO UI: Display air particle. PEventAir.activate( char, char.scene.game.particles, dirHor, dirVert );
			// TODO SOUND: Play sound for air. char.scene.soundList.air.play(0.5);

			return true;
		}
	}
}
